using AuctionAction;
using AuctionAction.Auctions;
using AuctionAction.Models;


Console.WriteLine("Hello! We will begin with character creation.");

var players = Utilities.GeneratePlayers();

Console.WriteLine("Hello! Please select an auction type you are interested in.");
Console.WriteLine("1. Dutch Auction");
Console.WriteLine("2. Vickrey Auction");
Console.WriteLine("3. Penny Auction");

var selection = Console.ReadLine();

switch (selection)
{
    case "1":
        DutchAuction.BeginBidding(players);
        break;
    case "2":
        VickreyAuction.BeginBidding(players);
        break;
    case "3":
        PennyAuction.BeginBidding(players);
        break;
    default:
        Console.WriteLine("Please select a valid selection.");
        break;
}