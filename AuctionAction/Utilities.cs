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
            Console.WriteLine($"KEY PRESSED: {key.Key}");
            try
            {
                return players.GetPlayerByHotkey(key);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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

        for (var i = 0; i < numberOfPlayers; i++)
        {
            Console.WriteLine($"Please enter the name of player {i}: ");
            var playerName = Console.ReadLine();
    
            Console.WriteLine($"Please enter the hotkey you would like to use to play bids (used only in a few auction types) for player {i}");
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
                i--; // Do not increment I and make player try again from the beginning
                Console.WriteLine("Player was not added, please try again.");
            }
        }
        players.PrintPlayers();
        
        return players;
    }

    public static void ApraiseItem(Player player, AuctionItem item)
    {
        
    }
}