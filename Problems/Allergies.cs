namespace Practice.Problems
{
    // Allergens and their point values
    [Flags]
    public enum Allergen
    {
        Eggs = 1,
        Peanuts = 2,
        Shellfish = 4,
        Strawberries = 8,
        Tomatoes = 16,
        Chocolate = 32,
        Pollen = 64,
        Cats = 128
    }

    internal class Problem4
    {
        public static void AllergiesTest()
        {
            Allergen allergen = Allergen.Pollen;

            // Mary initially has no allergies
            var mary = new Allergies("Mary");
            Console.WriteLine(mary.ToString());
            Console.WriteLine($"{mary.Name} is allergic to {allergen} (string): {mary.IsAllergicTo(allergen.ToString())}");
            Console.WriteLine($"{mary.Name} is allergic to {allergen} (Allergen): {mary.IsAllergicTo(allergen)}");

            // Joe is allergic to Eggs(1) and Pollen(64)
            var joe = new Allergies("Joe", 65);
            Console.WriteLine(joe.ToString());
            Console.WriteLine($"{joe.Name} is allergic to {allergen} (string): {joe.IsAllergicTo(allergen.ToString())}");
            Console.WriteLine($"{joe.Name} is allergic to {allergen} (Allergen): {joe.IsAllergicTo(allergen)}");

            // Rob is allergic to Peanuts(2), Strawberries(8), Chocolate(32), and Cats(128)
            var rob = new Allergies("Rob", "Peanuts Chocolate Cats Strawberries");
            Console.WriteLine(rob.ToString());
            Console.WriteLine($"{rob.Name} is allergic to {allergen} (string): {rob.IsAllergicTo(allergen.ToString())}");
            Console.WriteLine($"{rob.Name} is allergic to {allergen} (Allergen): {rob.IsAllergicTo(allergen)}");
            allergen = Allergen.Peanuts;
            Console.WriteLine($"{rob.Name} is allergic to {allergen} (string): {rob.IsAllergicTo(allergen.ToString())}");
            Console.WriteLine($"{rob.Name} is allergic to {allergen} (Allergen): {rob.IsAllergicTo(allergen)}");
        }
    }

    /*
    Create an Allergies class that holds a person's name and the things s/he is allergic to.
    Each allergy has a unique score value as follows:

        Allergy	Score
        Eggs	1
        Peanuts	2
        Shellfish	4
        Strawberries	8
        Tomatoes	16
        Chocolate	32
        Pollen	64
        Cats	128

    By combining the scores for each allergy suffered by a person their overall allergy score is obtained.
    For example, if someone was allergic to Peanuts, Tomatoes, and Pollen, their allergy score would be
    2 (Peanuts) + 16 (Tomatoes) + 64 (Pollen) their allergy score would be 82.

    Constructors:
    Constructors allowing the following instantiations:

        var mary = new Allergies("Mary") ➞ Mary initially has no allergies
        var joe = new Allergies("Joe", 65) ➞ Joe is allergic to Eggs (1) and Pollen (64)
        var rob = new Allergies("Rob", "Peanuts Chocolate Cats Strawberries") ➞ Rob is allergic to Peanuts, Strawberries, Chocolate, and Cats.
    */
    internal class Allergies
    {
        // Name ➞ the name of the person
        public string? Name { get; } = string.Empty;

        // Score ➞ returns an `int` value equal to the sum value of allergies
        public int Score { get; } = 0;

        private List<Allergen> _allergens = new List<Allergen>();

        // Constructors
        public Allergies(string name)
        {
            this.Name = name;
        }

        public Allergies(string name, int score)
        {
            this.Name = name;
            this.Score = score;
            this._allergens = AllergensFromInt(score).OrderBy(x => (int)x).ToList();
        }

        public Allergies(string name, string allergies)
        {
            this.Name = name;

            this._allergens = AllergensFromString(allergies).OrderBy(x => (int)x).ToList();

            this.Score = CalculateScore();
        }

        // Methods
        // Public methods

        /*
        Return the allergies in order of their score value
        Examples:
            "Mary has no allergies!"
            "Fred is allergic to Peanuts."
            "Joe is allergic to Eggs and Pollen."
            "Rob is allergic to Peanuts, Strawberries, Chocolate, and Cats."
        */
        public override string? ToString()
        {
            string noAllergies = "has no allergies";
            string hasAllergies = "is allergic to ";
            string punctuation = _allergens.Count == 0 ? "!" : ".";

            string message = this.Name + " " + (_allergens.Count == 0 ? noAllergies : hasAllergies);

            // Join the list of Allergens and separate with "and"
            string allergies = string.Join(" and ", _allergens);

            message += allergies;
            message += punctuation;

            return message;
        }

        // IsAllergicTo() ⁠— taking either a string parameter(e.g. "Pollen") or an Allergen enum value
        // and returning true or false depending on whether the person is allergic to the given allergen.
        public bool IsAllergicTo(string allergen)
        {
            return _allergens.Contains((Allergen)Enum.Parse(typeof(Allergen), allergen));
        }

        public bool IsAllergicTo(Allergen allergen)
        {
            return _allergens.Contains(allergen);
        }

        // AddAllergy() ⁠— taking string or Allergen parameter as above and updating the Score property
        // by adding the numeric value of the given allergen.
        public void AddAllergy(string allergen)
        {
            Allergen a = (Allergen)Enum.Parse(typeof(Allergen), allergen);

            if (!_allergens.Contains(a))
            {
                _allergens.Add(a);
            }
        }

        public void AddAllergy(Allergen allergen)
        {
            if (!_allergens.Contains(allergen))
            {
                _allergens.Add(allergen);
            }
        }


        // DeleteAllergy() ⁠— taking string or Allergen parameter as above and updating the Score property
        // by subtracting the numeric value of the given allergen.
        public void DeleteAllergy(string allergen)
        {
            Allergen a = (Allergen)Enum.Parse(typeof(Allergen), allergen);

            if (_allergens.Contains(a))
            {
                _allergens.Remove(a);
            }
        }

        public void DeleteAllergy(Allergen allergen)
        {
            if (_allergens.Contains(allergen))
            {
                _allergens.Add(allergen);
            }
        }

        // Private methods
        private int CalculateScore()
        {
            int score = 0;

            return _allergens.Sum(a => score += (int)a);
        }

        private List<Allergen> AllergensFromString(string allergies)
        {
            string[] allergiesArray = allergies.Split(new char[] { ' ', ',' });

            return allergiesArray.Select(a => (Allergen)Enum.Parse(typeof(Allergen), a)).ToList<Allergen>();
        }

        private List<Allergen> AllergensFromInt(int score)
        {
            List<Allergen> allergens = new List<Allergen>();
            int remainingScore = score;
            int total = 0;

            while (total < score)
            {
                Allergen? allergen = FindMostSevereAllergenFromScore(remainingScore);

                if (allergen != null)
                {
                    allergens.Add(allergen.Value);
                    total += (int)allergen.Value;
                    remainingScore = score - total;
                }
            }

            return allergens;
        }

        private Allergen? FindMostSevereAllergenFromScore(int score)
        {
            Allergen? mostSevereAllergen = null;

            // Find the highest-valued allergen possible given the score
            foreach (Allergen allergen in Enum.GetValues(typeof(Allergen)))
            {
                if ((int)allergen <= score)
                {
                    mostSevereAllergen = allergen;
                }
                else
                {
                    break;
                }
            }

            return mostSevereAllergen;
        }
    }
}
