using AuctionAction;
using AuctionAction.Auctions;
using AuctionAction.Models;

Console.WriteLine("Hello! We will begin with character creation.");

var itemBank = new ItemBank();

var players = Utilities.GeneratePlayers();

Console.WriteLine("Hello! Please select an auction type you are interested in.");

var stillPlaying = true;
while (stillPlaying)
{
    Console.WriteLine("1. Dutch Auction");
    Console.WriteLine("2. Vickrey Auction");
    Console.WriteLine("3. Penny Auction");
    var selection = Console.ReadLine();
    Console.Clear();

    switch (selection)
    {
        case "1":
            DutchAuction.BeginBidding(players,itemBank);
            break;
        case "2":
            VickreyAuction.BeginBidding(players,itemBank);
            break;
        case "3":
            PennyAuction.BeginBidding(players,itemBank);
            break;
        default:
            Console.WriteLine("Please select a valid selection.");
            break;
    }
    Console.WriteLine("Play again? Y/N");
    stillPlaying = Console.ReadLine().ToUpper().Equals("Y");
    Console.Clear();

}

Console.WriteLine("Final Talley of players and ultimate winner, as ordered by ending net worth.");
var place = 1;
foreach (var player in players.GetPlayersOrderByScore())
{
    Console.WriteLine($"{place} - {player.ToString()}");
}