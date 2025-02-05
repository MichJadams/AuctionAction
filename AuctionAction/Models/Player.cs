namespace AuctionAction.Auctions;

public class Player(string name, double startingMoney, char hotKey)
{
    public string Name = name;
    public double StartingMoney = startingMoney;
    public double CurrentMoney = startingMoney;
    public char HotKey = hotKey;
    public List<AuctionItem> AuctionItems = [];

    public override string ToString()
    {
        return $"Name {Name}, starting money {StartingMoney}, current money {CurrentMoney} and hotkey: {HotKey}";
    }
}