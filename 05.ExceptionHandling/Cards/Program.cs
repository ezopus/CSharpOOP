
string[] tokens = Console.ReadLine().Split(new [] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
List<Card> cards = new List<Card>();

for (int i = 0; i < tokens.Length - 1; i += 2)
{
    string currentFace = tokens[i];
    string currentSuit = tokens[i + 1];
    try
    {
        Card card = new Card(currentFace, currentSuit);
        cards.Add(card);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

Console.WriteLine(string.Join(" ", cards));

public class Card
{
    private string face;
    private string suit;
    private List<string> faces = new() { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

    private Dictionary<string, string> suits = new()
    {
        {"S", "\u2660"},
        {"H", "\u2665"},
        {"D", "\u2666"},
        {"C", "\u2663"}
    };
    private List<Card> cards;
    public Card(string face, string suit)
    {
        Face = face;
        Suit = suit;
    }

    public string Face
    {
        get => face;
        set
        {
            if (faces.Contains(value))
            {
                face = value;
            }
            else
            {
                throw new ArgumentException("Invalid card!");
            }
        }
    }
    public string Suit
    {
        get => suit;
        set
        {
            if (suits.ContainsKey(value))
            {
                suit = suits[value];
            }
            else
            {
                throw new ArgumentException("Invalid card!");
            }
        }
    }
    public override string ToString()
    {
        return $"[{Face}{Suit}]";
    }
}