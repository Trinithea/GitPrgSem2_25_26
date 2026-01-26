using Minesweeper.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Minesweeper.ViewModel
{
    internal class MainViewModel : ViewModelBase
    {
        private DispatcherTimer _timer;
        private DateTime _startTime;
        private int _rows = 10;
        private int _cols = 10;

        public ObservableCollection<CellViewModel> Cells { get; } = new ObservableCollection<CellViewModel>();

        // Property pro Binding v XAML
        private string _elapsedTime = "00:00";
        public string ElapsedTime { get => _elapsedTime; set { _elapsedTime = value; OnPropertyChanged(); } }

        private int _minesLeft;
        public int MinesLeft { get => _minesLeft; set { _minesLeft = value; OnPropertyChanged(); } }

        private bool _isGameActive = false;
        public bool IsGameActive{ get => _isGameActive; set { _isGameActive = value; OnPropertyChanged(); }
        }

        public ICommand FlagCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand RevealCommand { get; }
        public string BestTime { get; private set; }

        public MainViewModel()
        {
            StartCommand = new RelayCommand(_ => SetupGame());
            RevealCommand = new RelayCommand(param => RevealCell((CellViewModel)param));
            FlagCommand = new RelayCommand(param => ToggleFlag((CellViewModel)param));

            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _timer.Tick += (s, e) => ElapsedTime = (DateTime.Now - _startTime).ToString(@"mm\:ss");

            LoadBestTime();
        }

        private void SetupGame()
        {
            _timer.Stop();
            ElapsedTime = "00:00";
            Cells.Clear();
            MinesLeft = 10;
            IsGameActive = true;

            // 1. Vytvořit prázdné buňky
            for (int r = 0; r < _rows; r++)
                for (int c = 0; c < _cols; c++)
                    Cells.Add(new CellViewModel { Row = r, Column = c });

            // 2. Rozmístíme miny náhodně
            var random = new Random();
            int placedMines = 0;
            while (placedMines < MinesLeft)
            {
                var cell = Cells[random.Next(Cells.Count)];
                if (!cell.IsMine) { cell.IsMine = true; placedMines++; }
            }

            // 3. Spočítáme sousedy pro každou buňku
            foreach (var cell in Cells)
            {
                cell.NeighborMines = GetNeighbors(cell).Count(c => c.IsMine);
            }
        }

        /// <summary>
        /// Funkce sloužící k najití sousedů buňky
        /// </summary>
        /// <param name="cell">buňka, jejíž sousedy hledáme</param>
        /// <returns>kolekce sousedů</returns>
        private IEnumerable<CellViewModel> GetNeighbors(CellViewModel cell)
        {
            return Cells.Where(c =>
                Math.Abs(c.Row - cell.Row) <= 1 &&
                Math.Abs(c.Column - cell.Column) <= 1 &&
                c != cell);
        }

        /// <summary>
        /// Funkce na odkrytí buňka
        /// </summary>
        /// <param name="cell">buňka, jež odkrýváme</param>
        private void RevealCell(CellViewModel cell)
        {
            // Základní kontroly (pokud hra neběží, je už odkryto nebo je tam vlajka, nic nedělej)
            if (!IsGameActive || cell.IsRevealed || cell.IsFlagged) return;

            // Spuštění času při prvním kliku
            if (!_timer.IsEnabled)
            {
                _startTime = DateTime.Now;
                _timer.Start();
            }

            cell.IsRevealed = true;

            // Kliknutí na minu
            if (cell.IsMine)
            {
                GameOver(false);
                return;
            }

            // Odkrytí sousedů, pokud je políčko prázdné
            if (cell.NeighborMines == 0)
            {
                foreach (var neighbor in GetNeighbors(cell))
                {
                    RevealCell(neighbor);
                }
            }

            // Po každém odkrytí zkontrolujeme stav
            CheckForWin();
        }
        private void ToggleFlag(CellViewModel cell)
        {
            if (!IsGameActive || cell.IsRevealed) return;

            cell.IsFlagged = !cell.IsFlagged;
            OnPropertyChanged(nameof(MinesLeft)); // Aby se aktualizovalo počítadlo nahoře
        }
        /// <summary>
        /// Metoda kontorlující výhru
        /// </summary>
        private void CheckForWin()
        {
            // Spočítáme všechna políčka, která ještě nejsou odkrytá
            int hiddenCells = Cells.Count(c => !c.IsRevealed);

            // Celkový počet min (v tvém případě 10, nebo použij proměnnou)
            int totalMines = Cells.Count(c => c.IsMine);

            if (hiddenCells == totalMines)
            {
                // Pokud zbývají jen miny, hráč vyhrál!
                GameOver(true);
            }
        }

        /// <summary>
        /// Metoda vypínající hru
        /// </summary>
        /// <param name="win"></param>
        private void GameOver(bool win)
        {
            _timer.Stop();
            IsGameActive = false; // Zamkne plochu

            if (win)
            {
                // Uložíme skóre
                SaveScore(ElapsedTime);
                // Při výhře je hezké označit zbývající miny vlaječkami
                foreach (var mine in Cells.Where(c => c.IsMine))
                {
                    mine.IsFlagged = true;
                }
                MessageBox.Show("Gratuluji! Vyčistil jsi pole bez jediného výbuchu.");
            }
            else
            {
                // Při prohře odhalíme všechny zbývající miny
                foreach (var mine in Cells.Where(c => c.IsMine))
                {
                    mine.IsRevealed = true;
                }
                MessageBox.Show("BUM! Našel jsi minu. Hra končí.");
            }
        }

        private void SaveScore(string time)
        {
            try
            {
                string fileName = "best_times.txt";

                // Formát řádku: Datum a čas dokončení + herní čas
                string record = $"{DateTime.Now:dd.MM.yyyy HH:mm} - Čas: {time}{Environment.NewLine}";

                // AppendAllText soubor vytvoří, pokud neexistuje, 
                // a přidá nový záznam na konec, pokud už existuje.
                File.AppendAllText(fileName, record);
            }
            catch (Exception ex)
            {
                // Pokud by se zápis nepovedl (např. chybějící práva), program nespadne
                Debug.WriteLine($"Chyba při ukládání času: {ex.Message}");
            }
        }

        private void LoadBestTime()
        {
            string fileName = "best_times.txt";
            if (!File.Exists(fileName)) return;

            try
            {
                var lines = File.ReadAllLines(fileName);
                // Předpokládáme formát: "DD.MM.YYYY HH:mm - Čas: mm:ss"
                // Zkusíme vytáhnout všechny časy a najít ten minimální
                var times = lines
                    .Select(line => line.Split(new[] { "Čas: " }, StringSplitOptions.None).LastOrDefault())
                    .Where(t => !string.IsNullOrEmpty(t))
                    .Select(t => TimeSpan.ParseExact(t.Trim(), @"mm\:ss", null))
                    .ToList();

                if (times.Any())
                {
                    var fastest = times.Min();
                    BestTime = fastest.ToString(@"mm\:ss");
                }
            }
            catch { /* Pokud je soubor poškozený, tiše ignorujeme */ }
        }
    }
}
