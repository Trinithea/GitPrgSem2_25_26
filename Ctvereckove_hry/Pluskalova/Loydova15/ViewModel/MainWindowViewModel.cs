using Loydova15.Model;
using Loydova15.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace Loydova15.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand StartCommand => new RelayCommand(execute => StartGame(), canExecute => _isGameRunning == false);
        public RelayCommand CardClickCommand => new RelayCommand(execute => CardClicked(execute as CardViewModel), canExecute => _isGameRunning == true);

        public MainWindowViewModel()
        {
            Cards = new ObservableCollection<CardViewModel>();
            CreateGameCards();
        }

        private Color _defaultColor = Colors.AliceBlue;
        private Color _higlightColor = Colors.Green;

        private bool _isGameRunning = false;
        private bool _isBusy = false;

        private CardViewModel _firstSelected;
        private CardViewModel _secondSelected;


        const int CardCount = 16;

        #region Data Binding

        // Vlastnosti, na nichž máme data binding: karty pexesa, velikost gridu (neměnné), skóre
        public ObservableCollection<CardViewModel> Cards { get; set; }

        public int GridSize => (int)Math.Sqrt(Cards.Count);

        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Herní logika
        // Samotná herní logika
        public void StartGame()
        {
            
            ShuffleCards();
            OnPropertyChanged(nameof(GridSize)); // máme nachystané karty, vyvoláme funkci, že se grid změnil
            _isGameRunning = true;
        }
        private void CreateGameCards()
        {
            // přidáme dvojice karet
            for (int i = 0; i < CardCount; i++)
            {
                Cards.Add(new CardViewModel(new Card(i+1)));
            }
            Cards.RemoveAt(Cards.Count - 1);
            Cards.Add(new CardViewModel(new Card(-1)));
        }

        private void ShuffleCards()
        {
            Random rng = new Random();
            int n = Cards.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (Cards[i], Cards[j]) = (Cards[j], Cards[i]); // Swap
            }
        }

        private async void CardClicked(CardViewModel clicked)
        {
            if (_isBusy) return; // probíhá čekání u 2 ukázaných karet

            var Blank = Cards.FirstOrDefault(c => c.Model.Id == -1);

            int ClicledIndex = Cards.IndexOf(clicked);
            int BlankIndex = Cards.IndexOf(Blank);

            int size = GridSize;

            bool canMove =
                // nahoru / dolů
                ClicledIndex + size == BlankIndex ||
                ClicledIndex - size == BlankIndex ||

                // doprava (nesmí přeskočit řádek)
                (ClicledIndex + 1 == BlankIndex && ClicledIndex % size != size - 1) ||

                // doleva (nesmí přeskočit řádek)
                (ClicledIndex - 1 == BlankIndex && ClicledIndex % size != 0);


            if (!canMove)
                return;


            (Cards[ClicledIndex], Cards[BlankIndex]) = (Cards[BlankIndex], Cards[ClicledIndex]);
            Score++;

            bool done = CorrectOrder();
            if (done)
            {
                _isGameRunning = false;
                MessageBox.Show("Gratulace, vyhrál jsi!");
            }

        }

        private bool CorrectOrder()
        {
            for (int i = 0; i < Cards.Count - 1; i++) // poslední karta je prázdná
            {
                if (Cards[i].Model.Id != i + 1)
                    return false;
            }
            return Cards.Last().Model.Id == -1; // poslední musí být prázdná
        }



        #endregion

    }

}

    

