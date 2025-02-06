using AuctionAction.Auctions;

namespace AuctionAction.Models;

public class Players
{
    private List<Player> _playerCollection = [];

    public void AddPlayer(Player player)
    {
        if (HotkeyInUse(player.HotKey))
        {
            throw new ArgumentException($"Player {player.Name} is already added");
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

    public Player? GetPlayerByName(string name)
    {
        return _playerCollection.FirstOrDefault(player => player.Name == name);
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
}