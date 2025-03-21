﻿using AuctionAction.Models;

namespace AuctionAction.Auctions;

public static class VickreyAuction
{
    private static void Explain()
    {
        Console.WriteLine("""
                          Also known as a second-price sealed-bid auction,
                          participants submit written bids without knowing the bids of others.
                          The highest bidder wins, but the price paid is the second-highest bid.
                          This auction type encourages bidders to bid their true value,
                          as the winner pays less than their bid if they win.
                          """);
    }

    public static void BeginBidding(Players players, ItemBank itemBank)
    {
        Explain();
        var selectedAuctionItem = itemBank.GetRandomItem();
        foreach (var player in players.GetPlayers())
        {
            Utilities.ApraiseItem(player, selectedAuctionItem);
        }
        var bids = new Dictionary<string, double>();
        
        Console.WriteLine("Bidding has begun");

        foreach (var player in players.GetPlayers())
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine($"Player: {player.Name}, Please input your big");
            var bid = Console.ReadLine();
            if (double.TryParse(bid, out var bidValue))
            {
                bids[player.Name] = bidValue;
            }
            else
            {
                Console.WriteLine("invalid input");
            }
        }

        if (bids.Count != players.GetPlayers().Count)
        {
            Console.WriteLine("There are no bids that have been paid.");
        }
        else
        {
            var winner = bids.OrderByDescending(x => x.Value).First();
            var second = bids.OrderByDescending(x => x.Value).Skip(1).First();
            
            players.GetPlayerByName(winner.Key)?.AuctionItems.Add(selectedAuctionItem);
            players.GetPlayerByName(winner.Key).CurrentMoney -= second.Value;
            
            Console.WriteLine($"The winner is {winner.Key}, they bid {winner.Value}, but will only pay the amount the running up bid which is {second.Value}");
            itemBank.RemoveItemByName(selectedAuctionItem.Name);
        }
    }
}