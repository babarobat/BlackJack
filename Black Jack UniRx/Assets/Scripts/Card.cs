using System.Collections.Generic;

public readonly struct Card
{
    public readonly int Value;
    public string StringValue => Strings[Value];
    public readonly Suit Suit;
    public static readonly Dictionary<int, string> Strings;

    static Card()
    {
        Strings = new Dictionary<int, string>
        {
            [6] = "6",
            [7] = "7",
            [8] = "8",
            [9] = "9",
            [10] = "10",
            [2] = "J",
            [3] = "Q",
            [4] = "K",
            [11] = "A",
        };
    }

    public Card(int value, Suit suit)
    {
        Value = value;
        Suit = suit;
    }

    public override string ToString()
    {
        return $"{StringValue} {Suit}";
    }
}