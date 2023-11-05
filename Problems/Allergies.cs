namespace Practice.Problems
{
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

    An enumeration type enum called Allergen is already declared in the Code tab and should not be altered.

    The class should have the following members:

    Constructors
    One or more constructors allowing the following instantiations:

    var mary = new Allergies("Mary") ➞ Mary initially has no allergies
    var joe = new Allergies("Joe", 65) ➞ Joe is allergic to Eggs (1) and Pollen (64)
    var rob = new Allergies("Rob", "Peanuts Chocolate Cats Strawberries") ➞ Rob is allergic to Peanuts, Strawberries, Chocolate, and Cats.

    Properties (readonly)
    Name ➞ the name of the person
    Score ➞ returning an `int` value equal to the allergy score
    Methods
    ToString() ⁠— (override) returns a string in one of the following forms

        "Mary has no allergies!"
        "Fred is allergic to Peanuts."
        "Joe is allergic to Eggs and Pollen."
        "Rob is allergic to Peanuts, Strawberries, Chocolate, and Cats."

    IsAllergicTo() ⁠— taking either a string parameter (e.g. "Pollen") or an Allergen enum value and returning true or false depending on whether the person is allergic to the given allergen.
    
    AddAllergy() ⁠— taking string or Allergen parameter as above and updating the Score property by adding the numeric value of the given allergen.

    DeleteAllergy() ⁠— taking string or Allergen parameter as above and updating the Score property by subtracting the numeric value of the given allergen.

    Notes
    The ToString() method should return the allergies in order of their score value
    Check the Resources tab for links to helpful information.
    The allergies string input to the constructor, as above will be space-separated and each word will match precisely to the name of one of the Allergen enum values.
    This is the first challenge I've created from scratch, please leave comments for improvements or errors.
    */
    internal class Problem4
    {
        // do not alter this enum
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

        // constructors 

        // properties

        // methods

        public override string ToString()
        {
            // add code here to return string representation of this instance
            return base.ToString();
        }
    }
}
