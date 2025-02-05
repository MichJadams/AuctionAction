using AuctionAction;
using AuctionAction.Auctions;

Console.WriteLine("Hello! Please select an auction type you are interested in.");
Console.WriteLine("");


Console.WriteLine("1. Dutch Auction");
Console.WriteLine("2. Vickrey Auction");
Console.WriteLine("3. Penny Auction");

var players = new Players();

players.AddPlayer(new Player("Michaela", 100, 'j'));
players.AddPlayer(new Player("Lindsay", 100, 'k'));
var selection = Console.ReadLine();
if (selection == "1")
{
    DutchAuction.BeginBidding(players);
}

if (selection == "2")
{
    
}
if (selection == "3")
{
    PennyAuction.BeginBidding(players);
}