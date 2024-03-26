using Microsoft.Data.SqlClient;

namespace Practice.Problems
{
    // Connection Class: Represents a database connection and can be used for opening and closing the connection.
    internal class Connection : IDisposable
    {
        private SqlConnectionStringBuilder? _sqlConnectionStringBuilder = null;
        private SqlConnection? _connection = null;
        public string? Server { get; private set; } = string.Empty;
        public string? UserId { get; private set; } = string.Empty;
        public string? Password { get; private set; } = string.Empty;
        public string? Database { get; private set; } = string.Empty;
        public string LastError { get; private set; } = string.Empty;

        private bool _disposed = false;

        ~Connection()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects)
            }

            // Free unmanaged resources (unmanaged objects) set large fields to null
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            _disposed = true;
        }

        public bool OpenConnection(
            string? server,
            string? database,
            string? userId,
            string? password
            )
        {
            Server = server;
            Database = database;
            UserId = userId;
            Password = password;

            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder();

            // These properties will all be used to formulate the connection string
            _sqlConnectionStringBuilder.DataSource = Server;
            _sqlConnectionStringBuilder.InitialCatalog = Database;
            _sqlConnectionStringBuilder.UserID = UserId;
            _sqlConnectionStringBuilder.Password = Password;

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }

            try
            {
                _connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
                _connection.Open();
            }
            catch (SqlException e)
            {
                LastError = e.ToString();
                Console.WriteLine(LastError);

                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }

            return _connection != null &&
                _connection.State == System.Data.ConnectionState.Open;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Connection);
        }

        public bool Equals(Connection? other)
        {
            return other != null &&
                   Server == other.Server &&
                   Database == other.Database;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Server, Database);
        }

        public void CloseConnection()
        {
            if (_connection != null &&
                _connection.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    _connection.Close();
                }
                catch (SqlException e)
                {
                    LastError = e.ToString();
                    Console.WriteLine(LastError);
                }
            }
        }

        public void ExecuteQuery(string sqlquery)
        {
            if (sqlquery == null ||
                _connection == null ||
                _connection.State != System.Data.ConnectionState.Open)
            {
                return;
            }

            using (SqlCommand command = new SqlCommand(sqlquery, _connection))
            {
                try
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Name: {reader.GetString(0)} {reader.GetString(1)}");
                        }
                    }
                }
                catch (SqlException e)
                {
                    LastError = e.ToString();
                    Console.WriteLine(LastError);
                }
            }
        }
    }

    // ConnectionPool manages a pool of database connections.
    // Connections in the pool are created lazily, i.e., only when needed.
    // Only one caller can use a connection at a time.
    internal class ConnectionPool : IDisposable
    {
        // Members for maintaining the singleton instance
        private static readonly ConnectionPool instance = new ConnectionPool();
        public static ConnectionPool Instance => instance;

        // Members for maintaining the pool size
        private int _connectionCapacity = 10;
        public int ConnectionCapacity
        {
            get => _connectionCapacity;
            set
            {
                if (value > 0)
                {
                    _connectionCapacity = value;

                    // Allocate a new list of the requested size and copy all existing connections to it
                    if (_connectionCapacity != _databaseConnections?.Count)
                    {
                        List<DatabaseConnection>? newList = new List<DatabaseConnection>(_connectionCapacity);

                        for (int i = 0; i < newList.Capacity; i++)
                        {
                            if (i < _databaseConnections?.Count &&
                                _databaseConnections[i] != null)
                            {
                                newList.Add(_databaseConnections[i]);
                            }
                            else
                            {
                                break;
                            }
                        }

                        _databaseConnections = newList;
                        newList = null;
                    }
                }
                else
                {
                    throw new ArgumentException("ConnectionCapacity must be greater than zero.");
                }
            }
        }

        private List<DatabaseConnection>? _databaseConnections;
        public int ConnectionCount => _databaseConnections?.Count ?? 0;

        // Lock objects for thread safety
        private readonly object _connectionLock = new object();
        private static readonly object _singletonLock = new object();

        private bool _disposed = false;

        private ConnectionPool()
        {
            _databaseConnections = new List<DatabaseConnection>(_connectionCapacity);
        }

        ~ConnectionPool()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);

            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            // Free unmanaged resources (unmanaged objects) set large fields to null
            foreach (DatabaseConnection connection in _databaseConnections ?? Enumerable.Empty<DatabaseConnection>())
            {
                connection.Connection?.Dispose();
                connection.Connection = null;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects)
                _databaseConnections?.Clear();
                _databaseConnections = null;
            }

            _disposed = true;
        }

        // Represents a Connection instance to a database and its availabilty (already being used or not)
        private class DatabaseConnection
        {
            public Connection? Connection { get; set; } = null;
            public bool IsAvailable { get; set; } = false;
        }

        public Connection? AcquireConnection(
            string? server,
            string? database,
            string? userId,
            string? password
            )
        {
            DatabaseConnection? databaseConnection = null;
            Connection? connection = null;
            bool isOpen = false;

            lock (_connectionLock)
            {
                // Look for a matching connection already that's available (not currently used)
                databaseConnection = _databaseConnections?.Where(dbc =>
                    dbc.Connection?.Server == server &&
                    dbc.Connection?.Database == database &&
                    dbc.IsAvailable == true).FirstOrDefault();

                if (databaseConnection != null)
                {
                    databaseConnection.IsAvailable = false;
                    connection = databaseConnection.Connection;
                    isOpen = true;
                }
                // If the connection doesn't exist, create it, add it, and return it
                else
                {
                    // Only create the new connection if we're not at capacity
                    if (_databaseConnections?.Count < ConnectionCapacity)
                    {
                        connection = new Connection();
                        isOpen = connection.OpenConnection(server, database, userId, password);

                        // Only add the connection if it was created and opened successfully
                        if (isOpen)
                        {
                            databaseConnection = new DatabaseConnection()
                            {
                                Connection = connection,
                                IsAvailable = false
                            };

                            _databaseConnections.Add(databaseConnection);
                        }
                    }
                }
            }

            return isOpen ? connection : null;
        }

        public void ReleaseConnection(Connection? connection)
        {
            DatabaseConnection? databaseConnection;

            lock (_connectionLock)
            {
                // Look for a matching connection already that's available (not currently used)
                databaseConnection = _databaseConnections?.Where(dbc =>
                    dbc.Connection?.Server == connection?.Server &&
                    dbc.Connection?.Database == connection?.Database).FirstOrDefault();

                // If the connection exists, set its availability to true
                if (databaseConnection != null)
                {
                    databaseConnection.IsAvailable = true;
                }
            }
        }
    }

    internal class Problem8
    {
        public static void DatabaseConnectionPoolingTest()
        {
            try
            {
                string? sqlServerUri = string.Empty;
                string? sqlDatabase = string.Empty;
                string? username = string.Empty;
                string? password = string.Empty;

                Console.Write("Enter the SQL Server URI: ");
                sqlServerUri = Console.ReadLine();

                Console.Write("Enter the SQL database name: ");
                sqlDatabase = Console.ReadLine();

                Console.Write("Enter the username: ");
                username = Console.ReadLine();

                Console.Write("Enter the password: ");
                password = Console.ReadLine();

                using (ConnectionPool? connectionPool = ConnectionPool.Instance)
                {
                    UseConnectionPool(connectionPool, sqlServerUri, sqlDatabase, username, password);

                    connectionPool.ConnectionCapacity = 2;

                    UseConnectionPool(connectionPool, sqlServerUri, sqlDatabase, username, password);

                    connectionPool.ConnectionCapacity = 15;

                    UseConnectionPool(connectionPool, sqlServerUri, sqlDatabase, username, password);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void UseConnectionPool(
            ConnectionPool connectionPool,
            string? sqlServerUri,
            string? sqlDatabase,
            string? username,
            string? password)
        {
            Connection? connection = connectionPool.AcquireConnection(
                        server: sqlServerUri,
                        database: sqlDatabase,
                        userId: username,
                        password: password
                    );

            if (connection == null)
            {
                Console.WriteLine("The connection could not be created or retrieved.");
                Console.WriteLine("Please check if the ConnectionPool is already at capacity:");
                Console.WriteLine($"\tConnectionPool count: {connectionPool.ConnectionCount}");
                Console.WriteLine($"\tConnectionPool capacity: {connectionPool.ConnectionCapacity}");
            }
            else
            {
                string sqlquery = "SELECT FirstName, LastName FROM dbo.TestData ORDER BY FirstName ASC";

                Console.WriteLine();
                connection?.ExecuteQuery(sqlquery);

                connectionPool.ReleaseConnection(connection);
            }
        }
    }
}
