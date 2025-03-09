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
            Console.WriteLine($"Key available: {!Console.KeyAvailable}");
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
        Console.Clear();

        return players;
    }

    public static void ApraiseItem(Player player, AuctionItem item)
    {
        var price = Random.Shared.Next(5, 10);
        Console.WriteLine($"Player {player}");
        Console.WriteLine($"You now have the option to appraise {item.Name}. By spending {price} coin you will be given a price range the reflects the items 'true value'");
        Console.WriteLine("Do you want to appraise this item? (y/n)");
        var numberOfAppraisals = 0;
        var appraising = Console.ReadLine().ToUpper().Equals("Y");;
        
        while (appraising)
        {
            numberOfAppraisals += 1;
            price = Random.Shared.Next(5, 10);
            
            
            const double baseError = 0.50;
            const double decayFactor = 0.3;
            var errorMargin = (baseError * item.Price) * Math.Exp(-decayFactor * numberOfAppraisals);
            
            var randomFactorHigh = Random.Shared.Next(0, 100) / 100.0;
            var randomFactorLow = Random.Shared.Next(-100, 0) / 100.0;

            var upperEstimate = Math.Round(item.Price + (errorMargin *randomFactorHigh));
            var lowerEstimate = Math.Round(item.Price + (errorMargin *randomFactorLow)); 
            Console.WriteLine($"The item is worth between {lowerEstimate} and {upperEstimate}");

            player.CurrentMoney -= price;
            Console.WriteLine($"Your current money is now {player.CurrentMoney}");
            Console.WriteLine("Do you want to appraise this item? (y/n)");

            appraising = Console.ReadLine().ToUpper().Equals("Y");
        }


        Console.Clear();
    }
}