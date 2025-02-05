using AuctionAction.Auctions;

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
}