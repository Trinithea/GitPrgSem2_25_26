using Minesweeper.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper.ViewModel
{
    internal class CellViewModel : ViewModelBase
    {
        private bool _isRevealed;
        private bool _isFlagged;
        private bool _isMine;
        private int _neighborMines;

        // Pozice v mřížce
        public int Row { get; set; }
        public int Column { get; set; }

        // Je v buňce mina?
        public bool IsMine
        {
            get => _isMine;
            set { _isMine = value; OnPropertyChanged(); }
        }

        // Počet min v okolí (0-8)
        public int NeighborMines
        {
            get => _neighborMines;
            set { _neighborMines = value; OnPropertyChanged(); }
        }

        // Stav: Odkryto
        public bool IsRevealed
        {
            get => _isRevealed;
            set
            {
                _isRevealed = value;
                OnPropertyChanged();
                // Informujeme View, že se mohl změnit i text (číslo vs prázdno)
                OnPropertyChanged(nameof(DisplayText));
            }
        }

        // Stav: Vlaječka
        public bool IsFlagged
        {
            get => _isFlagged;
            set
            {
                _isFlagged = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayText)); // Změní se "" na "🚩"
            }
        }

        // Pomocná property pro XAML, která určí, co se má na tlačítku vypsat
        public string DisplayText
        {
            get
            {
                if (!IsRevealed) return IsFlagged ? "🚩" : "";
                if (IsMine) return "💣";
                return NeighborMines > 0 ? NeighborMines.ToString() : "";
            }
        }
    }
}
