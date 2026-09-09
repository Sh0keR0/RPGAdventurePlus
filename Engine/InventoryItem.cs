namespace Engine;

public class InventoryItem : INotifyPropertyChanged
{
    private Item _details = null!;
    private int _quantity;

    public Item Details
    {
        get => _details;
        set { _details = value; OnPropertyChanged(nameof(Details)); }
    }

    public int Quantity
    {
        get => _quantity;
        set { _quantity = value; OnPropertyChanged(nameof(Quantity)); }
    }

    public string Description => Details.Name;

    public InventoryItem(Item details, int quantity)
    {
        Details = details;
        Quantity = quantity;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
