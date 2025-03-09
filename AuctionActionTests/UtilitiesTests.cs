using AuctionAction;
using AuctionAction.Auctions;
using AuctionAction.Models;

namespace AuctionActionTests;

public class UtilitiesTests
{
    [Fact]
    public async void Offer_Test()
    {
        var playerOne = new Player("name", 100, 'f'); 
        var playerTwo = new Player("name_two", 100, 'g');
        var players = new Players(); 
        players.AddPlayer(playerOne);
        players.AddPlayer(playerTwo);

        Player? playerOut = null;  
        var offerTask = Task.Run(() =>
        {
            playerOut = Utilities.Offer(5, players);
            ;
        });
        Task.Delay(2000);

        var keyboardInputTask = Task.Run(() =>
        {
            SendKeys.SendWait("{ENTER}");
            Console.SetIn(new StringReader("f"));
            Console.WriteLine("Set In");
        });

        await offerTask;
        await keyboardInputTask;
        ;


    }
}