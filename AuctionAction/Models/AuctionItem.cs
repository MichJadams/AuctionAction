namespace AuctionAction.Models;

public class AuctionItem(string name, double price)
{
    public string Name { get; } = name;
    public double Price { get; } = price;
    public double CurrentBid { get; set; }
}