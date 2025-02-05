using System.Security.AccessControl;

namespace AuctionAction;

public class AuctionItem(string name, double price)
{
    public string Name { get; init; } = name;
    public double Price { get; init; } = price;
    public double CurrentBid { get; set; }
}