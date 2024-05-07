namespace Practice.Problems
{
    internal class Problem10
    {
        public static void CoinChangeTest()
        {
            int amount = 20;
            int coinnum = CoinChange(new int[] { 1, 2, 5 }, amount);

            Console.WriteLine($"Miniumum number of coins to make up ${amount}: {coinnum}");
        }

        public static int CoinChange(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
            Array.Fill(dp, amount + 1);
            dp[0] = 0;

            for (int i = 1; i <= amount; i++)
            {
                foreach (int coin in coins)
                {
                    if (coin <= i)
                    {
                        dp[i] = Math.Min(dp[i], dp[i - coin] + 1);
                    }
                }
            }

            return dp[amount] > amount ? -1 : dp[amount];
        }
    }
}
