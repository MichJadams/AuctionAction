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

    public static Players BeginBidding(Players players)
    {
        var selectedAuctionItem = new AuctionItem("Book", 45);

        var playersDebts = new Dictionary<string, double>();
        var biddingTimeLeft = 10;
        var currentWinnerName = "No bids";
        Explain();
        while (biddingTimeLeft > 0)
        {
            Console.WriteLine($"Time left: {biddingTimeLeft}");
            Console.WriteLine($"Current winner: {currentWinnerName}");

            var result = Utilities.Offer(1, players);

            while (result == null && biddingTimeLeft > 0)
            {
                biddingTimeLeft--;
                currentWinnerName = result == null ? "No winner" : result.Name;
                Console.WriteLine($"\t\t {biddingTimeLeft}: {currentWinnerName}, {selectedAuctionItem.Name} for {selectedAuctionItem.CurrentBid}");
                result = Utilities.Offer(1, players);
            }

            if (result != null)
            {
                if (!playersDebts.TryAdd(selectedAuctionItem.Name, 1))
                {
                    playersDebts[selectedAuctionItem.Name] += 1;
                }
                currentWinnerName = result.Name;
                selectedAuctionItem.CurrentBid += 10;
                biddingTimeLeft += 5;
            }
            else
            {
                break;
            }
        }
        
        Console.WriteLine($"The winner is {currentWinnerName}, who won a {selectedAuctionItem.Name} for {selectedAuctionItem.CurrentBid}.");
        Console.WriteLine("Total debts:");

        foreach (var playerName in playersDebts.Keys)
        {
            var debts = playersDebts[playerName];
            players.UpdatePlayerDebts(playerName, debts);
        }

        players.PrintPlayers();

        return players;
    }
}