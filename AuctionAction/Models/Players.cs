using AuctionAction.Auctions;

namespace AuctionAction;

public class Players
{
    private List<Player> PlayerCollection = new List<Player>();

    public void AddPlayer(Player player)
    {
        PlayerCollection.Add(player);
    }

    public List<Player> GetPlayers()
    {
        return PlayerCollection;
    }
    public void PrintPlayers()
    {
        Console.WriteLine("Current Players and their scores");
        foreach (var player in PlayerCollection)
        {
            Console.WriteLine(player.ToString());
        }
    }

    public Player? GetPlayerByHotkey(ConsoleKeyInfo keyInfo)
    {
        return PlayerCollection
            .FirstOrDefault(player => player.HotKey == keyInfo.KeyChar);
    }

    public Player? GetPlayerByName(string name)
    {
        return PlayerCollection.FirstOrDefault(player => player.Name == name);
    }

    public bool UpdatePlayerDebts(string name, double amountToRemove)
    {
        var player = GetPlayerByName(name);
        if (player == null)
        {
            return false;
        }
        else
        {
            player.CurrentMoney -= amountToRemove;
            return true;
        }
    }
}