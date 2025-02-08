namespace AuctionAction.Models;

public class ItemBank
{
    private List<AuctionItem> _items =
    [
        new AuctionItem("Chair", 100),
        new AuctionItem("Book", 300),
        new AuctionItem("Bench", 450),
        new AuctionItem("Better Book", 210),
        new AuctionItem("Better Book", 233)
    ];
    
    public AuctionItem GetRandomItem()
    {
        var index = Random.Shared.Next(0, _items.Count); 
        return _items[index];
    }

    public bool RemoveItemByName(string name)
    {
        var item = _items.Find(x => x.Name == name);
        return item != null && _items.Remove(item);
    }
}