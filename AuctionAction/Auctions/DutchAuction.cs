using AuctionAction.Models;

namespace AuctionAction.Auctions;

public static class DutchAuction
{
    static void Explain()
    {
        Console.WriteLine("""
                          In a dutch auction the auctioneer starts off with a
                          high price and then slowly lowers it until someone accepts the
                          price. So for this auction we
                          """);
    }

    static AuctionItem PickItem()
    {
        var item = new AuctionItem("Chair", 145.99)
        {
            CurrentBid = 200 // set the starting bid very high
        };
        return item;
    }

    public static Players BeginBidding(Players players)
    {
        var selectedAuctionItem = PickItem();
        while (selectedAuctionItem.CurrentBid > 0)
        {
            Console.WriteLine("----------------Starting Round--------------------");
            Console.WriteLine($"A beautiful {selectedAuctionItem.Name} going for {selectedAuctionItem.CurrentBid} Bid");
            foreach (var time in new List<string>{"once", "twice", "three times"})
            {
                Console.WriteLine($"Going {time}");
                var winner = Utilities.Offer(5, players);
                if (winner == null) continue;
                
                winner.CurrentMoney -= selectedAuctionItem.CurrentBid;
                winner.AuctionItems.Add(selectedAuctionItem);
                Console.WriteLine($"Winner: {winner}");
                return players;
            }
            selectedAuctionItem.CurrentBid -= 10;
        }

        return players;
    }
}