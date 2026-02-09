using stanclova_loyd_15.Model;
using stanclova_loyd_15.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace stanclova_loyd_15.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand StartCommand => new RelayCommand(execute => StartGame(), canExecute => _isGameRunning == false);
        public RelayCommand CardClickCommand => new RelayCommand(execute => CardClicked(execute as CardViewModel), canExecute => _isGameRunning == true);

        //omlouvám se, ale s těmito commandy a obecne s uložením hry mi velmi pomohl chatgpt. nevěděla jsem jak na to i když jsem četla váš chat. myslím si, že jsem toale pochopila, protožče jsem se velmi vyptávala chata a snažila s ejednotlivé nové commandy atd. pochopit co nejlépe
        public RelayCommand SaveGameCommand => new RelayCommand(execute => SaveGame(), canExecute => _isGameRunning == true);
        public RelayCommand LoadGameCommand => new RelayCommand(execute => LoadGame(), canExecute => _isGameRunning == false);


        public MainWindowViewModel() 
        {
            Cards = new ObservableCollection<CardViewModel>();
            LoadBestScore();
        }

        private Color _defaultColor = Colors.AliceBlue;
        private Color _higlightColor  = Colors.Green;

        private bool _isGameRunning = false;

        const int CardCount = 15;

        #region Data Binding
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


        //POMOCNÉ FUNKCE NA UKLÁDÁNÍ BEST SCORE
        private int _bestScore;
        public int BestScore
        {
            get => _bestScore;
            set
            {
                if (_bestScore != value)
                {
                    _bestScore = value;
                    OnPropertyChanged();
                }
            }
        }

        //můžu ji číst - nebude se už vubec menit, nastaví se cesta k souboru a ta zůstane, string, path.combine spojí ty cesty, co do ní dám (3 parametry - sám si vše ohlídá), Environment.GetFolderPath pomůž najít tu složku?? - omlouvám se, tuto část již nechápu, v ní jsem se ztratila, s tímto řádkem mi velmi pomohl chat a internet,
        //Environment.SpecialFolder.LocalApplicationData - výčet toho co teda chci - chci složku local app data; "stanclova_loyd_15" je ta složka aplikace, "bestscore.txt" je přímo hledaný soubor
        private readonly string ScorePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "stanclova_loyd_15", "bestscore.txt");

        private void LoadBestScore()
        {
            try
            {
                if (File.Exists(ScorePath)) //pokud ta slozka bude existovat
                {
                    BestScore = int.Parse(File.ReadAllText(ScorePath)); //vezmu to cislo ze slozky a uložím ho jako best
                }
                else
                {
                    BestScore = int.MaxValue; //pokud tam nic není --- zatím žádný best --- neukládám nic --- jen do best dám max, aby se to hned naplnilo po výhře
                }
            }
            catch
            {
                BestScore = int.MaxValue; //kdyby cokoliv, tak ot nechám catchnout a uložím jak max value, aby se to mohlo přepsat
            }
        }

        private void SaveBestScore()
        {
            string dir = Path.GetDirectoryName(ScorePath)!; //najde mi to automaticky cestu k souboru
            Directory.CreateDirectory(dir); //tohle zajistí, že se to celý totálně nezkazí ... když složka neexistuje,t ak ji vytvořím, když existuje tak ok, 

            File.WriteAllText(ScorePath, BestScore.ToString()); //do cesty k souboru který mám v scorepath vložím  best score ve stringu
        }
        //--------------------

        //POMOCNÉ FUNKCE K ULOŽENÍ HRY
        //toto je ta stejná funkce jako používám u ukládání skore... vysvetlená tam
        private readonly string GameSavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "stanclova_loyd_15", "savegame.json");

        private void SaveGame()
        {
            var state = new GameState
            {
                CardsID = Cards.Select(c => c.Id).ToList(), //karty uložím do listu 
                Score = Score // a zároven si uložím i skore ... mám to jako GameStat ... ta má parametry kartyID a skore
            };

            string dir = Path.GetDirectoryName(GameSavePath)!; //získám cestu k uložení hry
            Directory.CreateDirectory(dir);

            string json = JsonSerializer.Serialize(state); // prevede gamestate na json string
            File.WriteAllText(GameSavePath, json); //zapíše to do gamesavepath.json
        }

        private void LoadGame()
        {
            if (!File.Exists(GameSavePath)) //pokud by soubor neexistoval, return
                return;

            string json = File.ReadAllText(GameSavePath); //prectu obsah souboru
            var state = JsonSerializer.Deserialize<GameState>(json); // a prevedu to zpatky ten json na objekt typu gamestate, abych s nim zde mohla pracovat

            if (state == null) //pokud by se to nepovedlo nacíst, tak to vrátí
                return;

            Cards.Clear(); //vymaži aktuální karrty

            foreach (int id in state.CardsID) //vytvořím novou kartu, zabalím je do viewmodelu karet a vytvořím objekty karett
            {
                Cards.Add(new CardViewModel(new Card(id)));
            }

            Score = state.Score; //ulozím skore
            _isGameRunning = true; //zapnu hry

            OnPropertyChanged(nameof(GridSize)); //v UI se přepočítá velikost gridu pokud je potřeba
        }
        //----------------



        #endregion

        #region Herní logika
        public void StartGame()
        {
            Cards.Clear();
            Score = 0;

            CreateGameCards();
            ShuffleCards();
            OnPropertyChanged(nameof(GridSize));
            _isGameRunning = true;
        }

        private void CreateGameCards()
        {
            for (int i = 1; i <= CardCount; i++)
            {
                Cards.Add(new CardViewModel(new Card(i)));
            }
            Cards.Add(new CardViewModel(new Card(-1))); //"prázdné místo" = "prázdná karta"
        }

        private void ShuffleCards()
        {
            Random rng = new Random();
            int n = Cards.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (Cards[i], Cards[j]) = (Cards[j], Cards[i]); 
            }
        }

        private bool CanSwap(int cardIndex, int spaceIndex)
        {
            //nahoru dolu
            if (cardIndex + 4 == spaceIndex || cardIndex - 4 == spaceIndex)
            {
                return true;
            }

            //vlevo vpravo
            //nejdřív musím zjistit řádek ... pomocí / ... to provede dělení a "zahodí" zbytek -> 1 / 4 = 0 ... dělím 4, protože mám 4 řádky a chci zjistit, na jakém z nich to je
            if (cardIndex / 4 == spaceIndex / 4)
            {
                if (cardIndex - 1 == spaceIndex)
                {
                    return true;
                }

                else if (cardIndex + 1 == spaceIndex)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckWin()
        {
            for (int i = 0; i < Cards.Count - 1; i++) //poslední karta je ID = -1 ... neřeším
            {
                if (Cards[i].Id != i + 1)
                    return false;
            }
            return true;
        }


        private async void CardClicked(CardViewModel clicked)
        {
            //najdu volné políčko
            var space = Cards.FirstOrDefault(c => c.Model.Id == -1);
            int spaceIndex = Cards.IndexOf(space);
            int clickedIndex = Cards.IndexOf(clicked);

            if (CanSwap(clickedIndex, spaceIndex) == true) //karta je nad nebo pod
            {
                (Cards[clickedIndex], Cards[spaceIndex]) = (Cards[spaceIndex], Cards[clickedIndex]);
                Score++;
            }

            bool win = CheckWin();
            if (win == true)
            {
                _isGameRunning = false;

                if (Score < BestScore)
                {
                    BestScore = Score;
                    SaveBestScore();
                }

                MessageBox.Show("🎉 Jupí! Vyhrál/a jsi! 🎉");
            }

            else
            {
                return;
            }       
        }
        #endregion
    }
}

    

