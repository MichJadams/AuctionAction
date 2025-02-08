using AuctionAction.Auctions;

namespace AuctionAction.Models;

public class Players
{
    private List<Player> _playerCollection = [];

    public void AddPlayer(Player player)
    {
        if (HotkeyInUse(player.HotKey))
        {
            throw new ArgumentException($"A player with that hotkey is already added. {player.HotKey} is already in use. Select another.");
        }
        _playerCollection.Add(player);
    }

    public List<Player> GetPlayers()
    {
        return _playerCollection;
    }
    public void PrintPlayers()
    {
        Console.WriteLine("Current Players and their scores");
        foreach (var player in _playerCollection)
        {
            Console.WriteLine(player.ToString());
        }
    }

    public Player GetPlayerByHotkey(ConsoleKeyInfo keyInfo)
    {
        var res =  _playerCollection
            .FirstOrDefault(player => player.HotKey == keyInfo.KeyChar);
        if (res == null)
        {
            throw new ArgumentException($"Player {keyInfo.KeyChar} not found");
        }

        return res;
    }
    
    private bool HotkeyInUse(char keyInfo)
    {
        return _playerCollection
            .Any(p => p.HotKey == keyInfo);
    }

    public Player GetPlayerByName(string name)
    {
        return _playerCollection.FirstOrDefault(p => p.Name == name) 
               ?? throw new ArgumentException($"Player {name} not found in player collection");
    }

    public bool UpdatePlayerDebts(string name, double amountToRemove)
    {
        var player = GetPlayerByName(name);
        if (player == null)
        {
            return false;
        }
        player.CurrentMoney -= amountToRemove;
        return true;
    }

    public List<Player> GetPlayersOrderByScore()
    {
        return _playerCollection.OrderByDescending(player => player.GetTotalPlayerWorth()).ToList();
    }
}