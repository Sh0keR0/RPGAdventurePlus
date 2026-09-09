namespace Engine;

public class PlayerQuest : INotifyPropertyChanged
{
    private bool _isCompleted;

    public Quest Details { get; set; } = null!;

    public bool IsCompleted
    {
        get => _isCompleted;
        set
        {
            _isCompleted = value;
            OnPropertyChanged(nameof(IsCompleted));
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Name => Details.Name;

    public PlayerQuest(Quest details, bool isCompleted = false)
    {
        Details = details;
        IsCompleted = isCompleted;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
