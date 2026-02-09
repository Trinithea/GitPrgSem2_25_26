using WpfApp1.MVVM;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public class TileViewModel : ViewModelBase
    {
        public Tile Model { get; }

        public TileViewModel(Tile model)
        {
            Model = model;
        }

        public bool IsMine => Model.IsMine;
        public int AdjacentMines => Model.AdjacentMines;

        public bool IsRevealed
        {
            get => Model.IsRevealed;
            set
            {
                if (Model.IsRevealed == value) return;
                Model.IsRevealed = value;
                OnPropertyChanged();
            }
        }

        public bool IsFlagged
        {
            get => Model.IsFlagged;
            set
            {
                if (Model.IsFlagged == value) return;
                Model.IsFlagged = value;
                OnPropertyChanged();
            }
        }

        public void Refresh()
        {
            OnPropertyChanged(nameof(IsMine));
            OnPropertyChanged(nameof(AdjacentMines));
            OnPropertyChanged(nameof(IsRevealed));
            OnPropertyChanged(nameof(IsFlagged));
        }
    }
}
