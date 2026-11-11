using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AvaloniaBlackjack.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ObservableCollection<string> Games {get; set;} = ["BlackJack", "Memory Game"];
}
