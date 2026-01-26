using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly GameBoard _gameBoard;

    private static readonly IBrush PrazdnaBunka = new SolidColorBrush(Color.Parse("#0f3460"));
    private static readonly IBrush ObsazenaBunka = new SolidColorBrush(Color.Parse("#e94560"));
    private static readonly IBrush TvarBunka = new SolidColorBrush(Color.Parse("#00d9ff"));
    private static readonly IBrush Pruhledna = Brushes.Transparent;
    private static readonly IBrush KonecHryBunka = new SolidColorBrush(Color.Parse("#ff0000"));
    private static readonly IBrush SmazaniBunka = new SolidColorBrush(Color.Parse("#00ff88"));
    private static readonly IBrush CenterBodBunka = new SolidColorBrush(Color.Parse("#00ff88"));
    private static readonly IBrush VybranyBorder = new SolidColorBrush(Color.Parse("#00ff88"));
    private static readonly IBrush NevybranyBorder = Brushes.Transparent;

    private Shape? _vybranyTvar = null;
    private int _vybranyTvarIndex = -1;
    private bool _zpracovavam = false;

    public MainWindowViewModel()
    {
        _gameBoard = new GameBoard();
        _gameBoard.OnKonecHry += OnGameOver;

        HraciPoleCells = new ObservableCollection<CellViewModel>();
        for (int i = 0; i < 64; i++)
        {
            HraciPoleCells.Add(new CellViewModel { Barva = PrazdnaBunka, Index = i });
        }

        Tvar1Cells = new ObservableCollection<IBrush>();
        Tvar2Cells = new ObservableCollection<IBrush>();
        Tvar3Cells = new ObservableCollection<IBrush>();

        Highscore = _gameBoard.NactiHighscore();
        ZobrazUvodniObrazovku();
    }

    [ObservableProperty]
    private int _score;

    [ObservableProperty]
    private int _highscore;

    [ObservableProperty]
    private bool _hraSkoncila = true;

    [ObservableProperty]
    private string _stavHryText = "Klikni na 'Nova hra'";

    [ObservableProperty]
    private IBrush _tvar1Border = Brushes.Transparent;
    [ObservableProperty]
    private IBrush _tvar2Border = Brushes.Transparent;
    [ObservableProperty]
    private IBrush _tvar3Border = Brushes.Transparent;

    public ObservableCollection<CellViewModel> HraciPoleCells { get; }

    public ObservableCollection<IBrush> Tvar1Cells { get; }
    public ObservableCollection<IBrush> Tvar2Cells { get; }
    public ObservableCollection<IBrush> Tvar3Cells { get; }

    [ObservableProperty]
    private int _tvar1Rows = 1;
    [ObservableProperty]
    private int _tvar1Cols = 1;
    [ObservableProperty]
    private int _tvar2Rows = 1;
    [ObservableProperty]
    private int _tvar2Cols = 1;
    [ObservableProperty]
    private int _tvar3Rows = 1;
    [ObservableProperty]
    private int _tvar3Cols = 1;

    [RelayCommand]
    private void NovaHra()
    {
        _gameBoard.ResetHry();
        HraSkoncila = false;
        StavHryText = "Vyber tvar a klikni na pole";
        Score = 0;
        _vybranyTvar = null;
        _vybranyTvarIndex = -1;
        AktualizujHraciPole();
        AktualizujNabidku();
    }

    [RelayCommand]
    private void UlozitHru()
    {
        _gameBoard.UlozHru();
        _gameBoard.UlozHighscore();
    }

    [RelayCommand]
    private void NacistHru()
    {
        if (_gameBoard.NactiHru())
        {
            HraSkoncila = false;
            StavHryText = "Vyber tvar a klikni na pole";
            _vybranyTvar = null;
            _vybranyTvarIndex = -1;
            AktualizujHraciPole();
            AktualizujNabidku();
            Score = _gameBoard.score;
            Highscore = _gameBoard.NactiHighscore();
        }
    }

    [RelayCommand]
    private void VyberTvar1()
    {
        if (HraSkoncila) return;
        VyberTvar(0);
    }

    [RelayCommand]
    private void VyberTvar2()
    {
        if (HraSkoncila) return;
        VyberTvar(1);
    }

    [RelayCommand]
    private void VyberTvar3()
    {
        if (HraSkoncila) return;
        VyberTvar(2);
    }

    private void VyberTvar(int index)
    {
        var tvar = _gameBoard.AktualniNabidka[index];
        if (tvar == null) return;

        _vybranyTvar = tvar;
        _vybranyTvarIndex = index;

        Tvar1Border = (index == 0) ? VybranyBorder : NevybranyBorder;
        Tvar2Border = (index == 1) ? VybranyBorder : NevybranyBorder;
        Tvar3Border = (index == 2) ? VybranyBorder : NevybranyBorder;

        StavHryText = "Klikni na pole pro umisteni";
    }

    [RelayCommand]
    private async Task KlikNaBunku(int index)
    {
        if (HraSkoncila || _zpracovavam) return;
        if (_vybranyTvar == null)
        {
            StavHryText = "Nejdriv vyber tvar!";
            return;
        }

        int x = index % 8;
        int y = index / 8;

        var (vejdeSe, _, _, _, _) = _gameBoard.FitneSeTvar(_vybranyTvar, x, y);
        if (!vejdeSe)
        {
            StavHryText = "Sem se tvar nevejde!";
            return;
        }

        _zpracovavam = true;

        var (radky, sloupce) = _gameBoard.GetUpraveneRadkySloupce(_vybranyTvar, x, y);

        _gameBoard.UmistiTvar(_vybranyTvar, x, y);
        _gameBoard.AktualniNabidka[_vybranyTvarIndex] = null!;

        AktualizujHraciPole();
        AktualizujNabidku();
        Score = _gameBoard.score;

        _vybranyTvar = null;
        _vybranyTvarIndex = -1;
        Tvar1Border = Tvar2Border = Tvar3Border = NevybranyBorder;

        var bunkyKeSmazani = _gameBoard.NajdiBunkyKeSmazani(radky, sloupce);

        if (bunkyKeSmazani.Count > 0)
        {
            foreach (int i in bunkyKeSmazani)
            {
                HraciPoleCells[i].Barva = SmazaniBunka;
            }

            await Task.Delay(250);

            _gameBoard.SmazBunky(bunkyKeSmazani);
            Score = _gameBoard.score;
            AktualizujHraciPole();
        }

        if (_gameBoard.JeNabidkaPrazdna())
        {
            _gameBoard.GenerujNovouNabidku();
            AktualizujNabidku();
        }

        if (!MuzemePokracovat())
        {
            _gameBoard.UlozHighscore();
            OnGameOver();
        }
        else
        {
            StavHryText = "Vyber tvar a klikni na pole";
        }

        _zpracovavam = false;
    }

    private bool MuzemePokracovat()
    {
        foreach (var tvar in _gameBoard.AktualniNabidka)
        {
            if (tvar == null) continue;

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var (vejdeSe, _, _, _, _) = _gameBoard.FitneSeTvar(tvar, x, y);
                    if (vejdeSe) return true;
                }
            }
        }
        return false;
    }

    private void OnGameOver()
    {
        HraSkoncila = true;
        StavHryText = $"KONEC HRY! Score: {_gameBoard.score}";
        Highscore = _gameBoard.NactiHighscore();

        for (int i = 0; i < 64; i++)
        {
            HraciPoleCells[i].Barva = KonecHryBunka;
        }
    }

    private void ZobrazUvodniObrazovku()
    {
        for (int i = 0; i < 64; i++)
        {
            HraciPoleCells[i].Barva = KonecHryBunka;
        }

        Tvar1Cells.Clear();
        Tvar2Cells.Clear();
        Tvar3Cells.Clear();
        Tvar1Cells.Add(Pruhledna);
        Tvar2Cells.Add(Pruhledna);
        Tvar3Cells.Add(Pruhledna);
        Tvar1Rows = Tvar1Cols = 1;
        Tvar2Rows = Tvar2Cols = 1;
        Tvar3Rows = Tvar3Cols = 1;
    }

    private void AktualizujHraciPole()
    {
        var pole = _gameBoard.HraciPole;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                int index = y * 8 + x;
                HraciPoleCells[index].Barva = pole[y, x] == 1 ? ObsazenaBunka : PrazdnaBunka;
            }
        }
    }

    private void AktualizujNabidku()
    {
        var nabidka = _gameBoard.AktualniNabidka;

        AktualizujTvar(nabidka[0], Tvar1Cells, v => { Tvar1Rows = v.rows; Tvar1Cols = v.cols; });
        AktualizujTvar(nabidka[1], Tvar2Cells, v => { Tvar2Rows = v.rows; Tvar2Cols = v.cols; });
        AktualizujTvar(nabidka[2], Tvar3Cells, v => { Tvar3Rows = v.rows; Tvar3Cols = v.cols; });
    }

    private void AktualizujTvar(Shape? tvar, ObservableCollection<IBrush> cells, Action<(int rows, int cols)> setSize)
    {
        cells.Clear();

        if (tvar == null)
        {
            setSize((1, 1));
            cells.Add(Pruhledna);
            return;
        }

        int rows = tvar.Vyska;
        int cols = tvar.Sirka;
        setSize((rows, cols));

        int centerY = tvar.CenterBod[0];
        int centerX = tvar.CenterBod[1];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (tvar.Rozmery[y, x] == 1)
                {
                    bool jeCenter = (y == centerY && x == centerX);
                    cells.Add(jeCenter ? CenterBodBunka : TvarBunka);
                }
                else
                {
                    cells.Add(Pruhledna);
                }
            }
        }
    }
}

public partial class CellViewModel : ObservableObject
{
    [ObservableProperty]
    private IBrush _barva = Brushes.Gray;

    public int Index { get; set; }
}
