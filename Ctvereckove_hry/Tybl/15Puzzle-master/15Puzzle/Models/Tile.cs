namespace Puzzle15
{
    public class Tile : ObservableObject
    {
        private int _number;
        private bool _isEmpty;

        public int Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(); OnPropertyChanged(nameof(Display)); }
        }

        public bool IsEmpty
        {
            get => _isEmpty;
            set { _isEmpty = value; OnPropertyChanged(); OnPropertyChanged(nameof(Display)); }
        }

        public string Display => IsEmpty ? "" : Number.ToString();
    }
}