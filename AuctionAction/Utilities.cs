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
        Console.WriteLine($"Player {player}");
        Console.WriteLine($"You now have the option to appraise {item.Name}. By spending 5 coin you will be given a price range the reflects the items 'true value'");
        Console.WriteLine("Do you want to appraise this item? (y/n)");
        var numberOfAppraisals = 0;
        var appraising = true;
        while (appraising)
        {
            numberOfAppraisals += 1;
       
            var upperEstimate = item.Price + GenerateDeviation(numberOfAppraisals);
            var lowerEstimate = item.Price - GenerateDeviation(numberOfAppraisals); 
            Console.WriteLine($"The item is worth between {lowerEstimate} and {upperEstimate}");

            player.CurrentMoney -= 5;
            Console.WriteLine($"Your current money is now {player.CurrentMoney}");
            Console.WriteLine("Do you want to appraise this item farther for another 5 coin? (y/n)");
            appraising = Console.ReadLine().ToUpper().Equals("Y");
        }


        Console.Clear();
    }
    public static double GenerateDeviation(int numberOfAppraisals)
    {
        var baseError = 0.50;
        var e = Math.E;
        var decayFactor = 0.3;
        var randomFactor = Random.Shared.Next(-100, 100) / 100.0;
        return baseError * Math.Pow(e,-decayFactor*numberOfAppraisals) * randomFactor;
    }

}