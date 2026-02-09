using WpfApp1.MVVM;
using WpfApp1.Model;
using System.Collections.ObjectModel;
using System.Windows.Threading;


namespace WpfApp1.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<TileViewModel> Tiles { get; } = new();

        public int Rows { get; } = 16;
        public int Columns { get; } = 16;
        public int MineCount { get; } = 40;

        private int _timeSeconds;
        public int TimeSeconds
        {
            get => _timeSeconds;
            set { _timeSeconds = value; OnPropertyChanged(); }
        }

        private int _minesLeft;
        public int MinesLeft
        {
            get => _minesLeft;
            set { _minesLeft = value; OnPropertyChanged(); }
        }

        private readonly DispatcherTimer _timer;
        private readonly Random _rng = new();

        public RelayCommand ResetCommand { get; }
        public RelayCommand TileLeftClickCommand { get; }
        public RelayCommand TileRightClickCommand { get; }

        public MainWindowViewModel()
        {
            ResetCommand = new RelayCommand(_ => NewGame());
            TileLeftClickCommand = new RelayCommand(t => Reveal(t as TileViewModel));
            TileRightClickCommand = new RelayCommand(t => Flag(t as TileViewModel));

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (_, __) => TimeSeconds++;

            NewGame();
        }

        private void NewGame()
        {
            _timer.Stop();

            Tiles.Clear();
            TimeSeconds = 0;
            MinesLeft = MineCount;

            for (int i = 0; i < Rows * Columns; i++)
                Tiles.Add(new TileViewModel(new Tile()));

            PlaceMines();
            CalculateNumbers();

            foreach (var t in Tiles)
                t.Refresh();

            _timer.Start();
        }

        private void PlaceMines()
        {
            int placed = 0;
            while (placed < MineCount)
            {
                int i = _rng.Next(Tiles.Count);
                if (!Tiles[i].Model.IsMine)
                {
                    Tiles[i].Model.IsMine = true;
                    placed++;
                }
            }
        }

        private void CalculateNumbers()
        {
            for (int i = 0; i < Tiles.Count; i++)
            {
                if (Tiles[i].Model.IsMine) continue;

                int count = 0;
                foreach (var n in GetNeighbors(i))
                    if (Tiles[n].Model.IsMine) count++;

                Tiles[i].Model.AdjacentMines = count;
            }

            foreach (var t in Tiles)
                t.Refresh();
        }

        private IEnumerable<int> GetNeighbors(int index)
        {
            int x = index % Columns;
            int y = index / Columns;

            for (int dx = -1; dx <= 1; dx++)
                for (int dy = -1; dy <= 1; dy++)
                {
                    if (dx == 0 && dy == 0) continue;

                    int nx = x + dx;
                    int ny = y + dy;

                    if (nx >= 0 && ny >= 0 && nx < Columns && ny < Rows)
                        yield return ny * Columns + nx;
                }
        }

        private void Reveal(TileViewModel tile)
        {
            if (tile == null || tile.IsFlagged || tile.IsRevealed)
                return;

            tile.IsRevealed = true;
            tile.Refresh();

            if (tile.IsMine)
            {
                foreach (var t in Tiles)
                {
                    t.IsRevealed = true;
                    t.Refresh();
                }
                _timer.Stop();
                return;
            }

            if (tile.AdjacentMines == 0)
            {
                int index = Tiles.IndexOf(tile);
                foreach (var n in GetNeighbors(index))
                    Reveal(Tiles[n]);
            }
        }

        private void Flag(TileViewModel tile)
        {
            if (tile == null || tile.IsRevealed)
                return;

            tile.IsFlagged = !tile.IsFlagged;
            tile.Refresh();

            MinesLeft += tile.IsFlagged ? -1 : 1;
        }
    }
}
