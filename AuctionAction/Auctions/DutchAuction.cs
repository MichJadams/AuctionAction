using AuctionAction.Models;

namespace AuctionAction.Auctions;

public static class DutchAuction
{
    static void Explain()
    {
        Console.WriteLine("""
                          In a dutch auction the auctioneer starts off with a
                          high price and then slowly lowers it until someone accepts the
                          price. So for this auction we start very high and if you want to bid, please press your 
                          bidding hotkey. Once a bid is placed, the auction is over.
                          """);
    }

    public static void BeginBidding(Players players, ItemBank itemBank)
    {
        Explain();
        var selectedAuctionItem = itemBank.GetRandomItem();
        selectedAuctionItem.CurrentBid = 300;
        Player? winner = null;
        while (selectedAuctionItem.CurrentBid > 0)
        {
            Console.WriteLine("----------------Starting Round--------------------");
            Console.WriteLine($"A beautiful {selectedAuctionItem.Name} going for {selectedAuctionItem.CurrentBid} Bid");
            foreach (var time in new List<string>{"once", "twice", "three times"})
            {
                Console.WriteLine($"Going {time}");
                winner = Utilities.Offer(5, players);
                if (winner == null) continue;
                
                winner.CurrentMoney -= selectedAuctionItem.CurrentBid;
                winner.AuctionItems.Add(selectedAuctionItem);
                Console.WriteLine($"Winner: {winner}");
                return;
            }
            selectedAuctionItem.CurrentBid -= 10;
        }

        if (winner != null)
        {
            itemBank.RemoveItemByName(selectedAuctionItem.Name);
        }
    }
}