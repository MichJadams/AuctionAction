using AuctionAction.Auctions;
using AuctionAction.Models;

namespace AuctionAction;

public static class Utilities
{
    public static Player? Offer(decimal duration, Players players)
    {
        var startTime = DateTime.UtcNow;
        while (DateTime.UtcNow.Subtract(startTime).Seconds < duration)
        {
            if (!Console.KeyAvailable) continue;
            var key = Console.ReadKey(true);
            Console.WriteLine($"KEY PRESSED: {key}");
            return players.GetPlayerByHotkey(key);
        }
        return null;
    }

    public static Players GeneratePlayers()
    {
        var players = new Players();

        Console.WriteLine("Creating starting players...");
        Console.WriteLine("How many players do you want to play? (2 to 9)");
        var numberOfPlayers = Convert.ToInt32(Console.ReadLine());
        const int startingMoney = 250;

        foreach (var playerIndex in Enumerable.Range(1, numberOfPlayers))
        {
            Console.WriteLine("Please enter the number of the player you want to play: ");
            var playerName = Console.ReadLine();
    
            Console.WriteLine("Please enter the hotkey you would like to use to play bids (used only in a few auction types)");
            var hotkey = Convert.ToChar(Console.ReadLine());

            var player = new Player(playerName, startingMoney, hotkey);
            try
            {
                players.AddPlayer(player);
                Console.WriteLine("Player added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // TODO: handle gracefully 
            }
        }
        players.PrintPlayers();
        
        return players;
    }
}