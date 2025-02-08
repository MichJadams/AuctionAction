using AuctionAction.Models;

namespace AuctionAction.Auctions;

public static class PennyAuction
{
    private static void Explain()
    {
        Console.WriteLine("""
               In a penny auction, participants pay a non-refundable fee to place small
               incremental bids (often one cent or a penny) on an item.
               Each bid increases the item's price by a small amount and extends the auction time slightly.
               The winner is the last bidder when the time runs out.
               While the final price can be low, participants may spend
               significant amounts on bidding fees.
               """);
    }

    public static void BeginBidding(Players players, ItemBank itemBank)
    {
        var selectedAuctionItem = itemBank.GetRandomItem();
        selectedAuctionItem.CurrentBid = 5;
        var playersDebts = new Dictionary<string, double>();
        var biddingTimeLeft = 10;
        var currentWinnerName = "No bids";
        Explain();
        
        Console.WriteLine($"Time left: {biddingTimeLeft}");
        Console.WriteLine($"Current winner: {currentWinnerName}");

        while (biddingTimeLeft > 0)
        {
            biddingTimeLeft--;
            var result = Utilities.Offer(1, players);
            if (result != null)
            {
                currentWinnerName = result.Name;
                if (!playersDebts.TryAdd(currentWinnerName, 1))
                {
                    playersDebts[currentWinnerName] += 1;
                }
                selectedAuctionItem.CurrentBid += 10;
                biddingTimeLeft += 5;
            }
            Console.WriteLine($"\t\t {biddingTimeLeft}: {currentWinnerName}, {selectedAuctionItem.Name} for {selectedAuctionItem.CurrentBid}");
        }

        if (currentWinnerName == "No bids")
        {
            Console.WriteLine("There have been no bids in the auction. I'm disappointed.");
        }
        else
        {
            Console.WriteLine($"The winner is {currentWinnerName}, who won a {selectedAuctionItem.Name} for {selectedAuctionItem.CurrentBid}.");
            Console.WriteLine($"However the other players still awe their participation and bidding fees.");
            Console.WriteLine("Total debts:");

            var winner = players.GetPlayerByName(currentWinnerName);
            winner.AuctionItems.Add(selectedAuctionItem);
            winner.CurrentMoney -= selectedAuctionItem.CurrentBid;
            foreach (var playerName in playersDebts.Keys)
            {
                players.UpdatePlayerDebts(playerName, playersDebts[playerName]);
            }
            itemBank.RemoveItemByName(selectedAuctionItem.Name);
        }

        players.PrintPlayers();
    }
}