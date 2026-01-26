using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Puzzle15
{
    public class MainViewModel : ObservableObject
    {
        private const string SaveFile = "puzzle_save.json";
        private int _rows = 4;
        private int _cols = 4;
        private int _currentMoves;
        private int _bestScore = 0;
        private bool _isGameWon;

        public ObservableCollection<Tile> Tiles { get; private set; } = new ObservableCollection<Tile>();

        public int Rows { get => _rows; set { _rows = value; OnPropertyChanged(); } }
        public int Cols { get => _cols; set { _cols = value; OnPropertyChanged(); } }

        public int CurrentMoves
        {
            get => _currentMoves;
            set { _currentMoves = value; OnPropertyChanged(); }
        }

        public string BestScoreDisplay => _bestScore == int.MaxValue ? "--" : _bestScore.ToString();

        public bool IsGameWon
        {
            get => _isGameWon;
            set { _isGameWon = value; OnPropertyChanged(); }
        }

        public ICommand MoveCommand { get; }
        public ICommand NewGameCommand { get; }
        public ICommand SaveCommand { get; }

        public MainViewModel()
        {
            MoveCommand = new RelayCommand(MoveTile);
            NewGameCommand = new RelayCommand(_ => StartNewGame());
            SaveCommand = new RelayCommand(_ => SaveGame());

            if (!LoadGame())
            {
                StartNewGame();
            }
        }

        private void StartNewGame()
        {
            IsGameWon = false;
            CurrentMoves = 0;
            InitializeTiles();
            ShuffleTiles();
        }

        private void InitializeTiles()
        {
            Tiles.Clear();
            for (int i = 1; i < Rows * Cols; i++)
            {
                Tiles.Add(new Tile { Number = i, IsEmpty = false });
            }
            Tiles.Add(new Tile { Number = 0, IsEmpty = true });
        }

        private void ShuffleTiles()
        {
            Random rnd = new Random();
            int shuffleMoves = 100; 
            int emptyIndex = Tiles.Count - 1;

            for (int i = 0; i < shuffleMoves; i++)
            {
                var neighbors = GetNeighbors(emptyIndex);
                int randomNeighbor = neighbors[rnd.Next(neighbors.Count)];
                Swap(emptyIndex, randomNeighbor);
                emptyIndex = randomNeighbor;
            }
        }

        private void MoveTile(object parameter)
        {
            if (IsGameWon || parameter is not Tile clickedTile) return;

            int clickedIndex = Tiles.IndexOf(clickedTile);
            int emptyIndex = Tiles.IndexOf(Tiles.First(t => t.IsEmpty));

            if (IsAdjacent(clickedIndex, emptyIndex))
            {
                Swap(clickedIndex, emptyIndex);
                CurrentMoves++;
                CheckWin();
            }
        }

        private bool IsAdjacent(int index1, int index2)
        {
            int row1 = index1 / Cols, col1 = index1 % Cols;
            int row2 = index2 / Cols, col2 = index2 % Cols;

            return Math.Abs(row1 - row2) + Math.Abs(col1 - col2) == 1;
        }

        private List<int> GetNeighbors(int index)
        {
            List<int> neighbors = new List<int>();
            int row = index / Cols;
            int col = index % Cols;

            if (row > 0) neighbors.Add(index - Cols); // Up
            if (row < Rows - 1) neighbors.Add(index + Cols); // Down
            if (col > 0) neighbors.Add(index - 1); // Left
            if (col < Cols - 1) neighbors.Add(index + 1); // Right

            return neighbors;
        }

        private void Swap(int indexA, int indexB)
        {
            var tempNumber = Tiles[indexA].Number;
            var tempEmpty = Tiles[indexA].IsEmpty;

            Tiles[indexA].Number = Tiles[indexB].Number;
            Tiles[indexA].IsEmpty = Tiles[indexB].IsEmpty;

            Tiles[indexB].Number = tempNumber;
            Tiles[indexB].IsEmpty = tempEmpty;
        }

        private void CheckWin()
        {
            for (int i = 0; i < Tiles.Count - 1; i++)
            {
                if (Tiles[i].Number != i + 1) return;
            }

            IsGameWon = true;
            if (_bestScore == int.MaxValue || CurrentMoves < _bestScore)
            {
                _bestScore = CurrentMoves;
                OnPropertyChanged(nameof(BestScoreDisplay));
                SaveGame();
            }
            MessageBox.Show($"Puzzle Solved in {CurrentMoves} moves!");
        }

        public void SaveGame()
        {
            var state = new GameState
            {
                Rows = Rows,
                Cols = Cols,
                CurrentMoves = CurrentMoves,
                BestScore = _bestScore,
                TileValues = Tiles.Select(t => t.Number).ToList()
            };

            string json = JsonSerializer.Serialize(state);
            File.WriteAllText(SaveFile, json);
        }

        private bool LoadGame()
        {
            if (!File.Exists(SaveFile)) return false;

            try
            {
                string json = File.ReadAllText(SaveFile);
                var state = JsonSerializer.Deserialize<GameState>(json);

                Rows = state.Rows;
                Cols = state.Cols;
                CurrentMoves = state.CurrentMoves;
                _bestScore = state.BestScore;
                OnPropertyChanged(nameof(BestScoreDisplay));

                Tiles.Clear();
                foreach (int val in state.TileValues)
                {
                    Tiles.Add(new Tile { Number = val, IsEmpty = (val == 0) });
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}