using System;
using System.Collections.Generic;
using AvaloniaApplication1.Models.Shapes;
using System.IO;

namespace AvaloniaApplication1.Models;

public class GameBoard
{
    private int[,] _hraciPole = new int[8, 8];
    private int _volnaMista = 64;
    public int score { get; private set; }

    public event Action? OnKonecHry;

    public bool HraSkoncila { get; private set; } = false;
    public bool HraBezi { get; private set; } = false;

    public GameBoard()
    {
        for (int i = 0; i < _hraciPole.GetLength(0); i++)
        {
            for (int j = 0; j < _hraciPole.GetLength(1); j++)
            {
                _hraciPole[i, j] = 0;
            }
        }
    }

    public (bool vejdeSe, int xLeva, int xPrava, int yHore, int yDolu) FitneSeTvar(Shape blok, int startX, int startY)
    {
        if (_volnaMista < blok.Obsah)
        {
            KonecHry();
            return (false, 0, 0, 0, 0);
        }

        int tvarVyska = blok.Rozmery.GetLength(0);
        int tvarSirka = blok.Rozmery.GetLength(1);

        // CenterBod[0] = Y, CenterBod[1] = X
        int centerRadek = blok.CenterBod[0];
        int centerSloupec = blok.CenterBod[1];

        int x_prava_posun = tvarSirka - 1 - centerSloupec;
        int x_leva_posun = centerSloupec;
        int y_dolu_posun = tvarVyska - 1 - centerRadek;
        int y_hore_posun = centerRadek;

        if (startX + x_prava_posun >= 8 || startX - x_leva_posun < 0 ||
            startY + y_dolu_posun >= 8 || startY - y_hore_posun < 0)
            return (false, x_leva_posun, x_prava_posun, y_hore_posun, y_dolu_posun);

        for (int i = 0; i < tvarVyska; i++)
        {
            for (int j = 0; j < tvarSirka; j++)
            {
                if (blok.Rozmery[i, j] == 1)
                {
                    int poleY = startY - y_hore_posun + i;
                    int poleX = startX - x_leva_posun + j;

                    if (_hraciPole[poleY, poleX] != 0)
                        return (false, x_leva_posun, x_prava_posun, y_hore_posun, y_dolu_posun);
                }
            }
        }

        return (true, x_leva_posun, x_prava_posun, y_hore_posun, y_dolu_posun);
    }

    public bool UmistiTvar(Shape blok, int startX, int startY)
    {
        var (vejdeSe, xLeva, xPrava, yHore, yDolu) = FitneSeTvar(blok, startX, startY);

        if (!vejdeSe)
            return false;

        HashSet<int> upraveneRadky = new HashSet<int>();
        HashSet<int> upraveneSloupce = new HashSet<int>();

        int tvarVyska = blok.Rozmery.GetLength(0);
        int tvarSirka = blok.Rozmery.GetLength(1);

        for (int i = 0; i < tvarVyska; i++)
        {
            for (int j = 0; j < tvarSirka; j++)
            {
                if (blok.Rozmery[i, j] == 1)
                {
                    int poleY = startY - yHore + i;
                    int poleX = startX - xLeva + j;
                    _hraciPole[poleY, poleX] = 1;
                    _volnaMista--;

                    upraveneRadky.Add(poleY);
                    upraveneSloupce.Add(poleX);
                }
            }
        }

        //pridani bodu
        score += blok.Obsah;

        return true;
    }

    public HashSet<int> NajdiBunkyKeSmazani(HashSet<int> radky, HashSet<int> sloupce)
    {
        HashSet<int> bunkyKeSmazani = new HashSet<int>();

        foreach (int radek in radky)
        {
            bool plny = true;
            for (int x = 0; x < 8; x++)
            {
                if (_hraciPole[radek, x] == 0)
                {
                    plny = false;
                    break;
                }
            }
            if (plny)
            {
                for (int x = 0; x < 8; x++)
                {
                    bunkyKeSmazani.Add(radek * 8 + x);
                }
            }
        }

        foreach (int sloupec in sloupce)
        {
            bool plny = true;
            for (int y = 0; y < 8; y++)
            {
                if (_hraciPole[y, sloupec] == 0)
                {
                    plny = false;
                    break;
                }
            }
            if (plny)
            {
                for (int y = 0; y < 8; y++)
                {
                    bunkyKeSmazani.Add(y * 8 + sloupec);
                }
            }
        }

        return bunkyKeSmazani;
    }

    public void SmazBunky(HashSet<int> indexy)
    {
        if (indexy.Count == 0) return;

        HashSet<int> smazaneRadky = new HashSet<int>();
        HashSet<int> smazaneSloupce = new HashSet<int>();

        foreach (int index in indexy)
        {
            int y = index / 8;
            int x = index % 8;

            bool celyRadek = true;
            for (int xx = 0; xx < 8; xx++)
            {
                if (!indexy.Contains(y * 8 + xx))
                {
                    celyRadek = false;
                    break;
                }
            }
            if (celyRadek) smazaneRadky.Add(y);

            bool celySloupec = true;
            for (int yy = 0; yy < 8; yy++)
            {
                if (!indexy.Contains(yy * 8 + x))
                {
                    celySloupec = false;
                    break;
                }
            }
            if (celySloupec) smazaneSloupce.Add(x);
        }

        foreach (int index in indexy)
        {
            int y = index / 8;
            int x = index % 8;
            if (_hraciPole[y, x] == 1)
            {
                _hraciPole[y, x] = 0;
                _volnaMista++;
            }
        }

        score += (smazaneRadky.Count + smazaneSloupce.Count) * 100;
    }

    public (HashSet<int> radky, HashSet<int> sloupce) GetUpraveneRadkySloupce(Shape blok, int startX, int startY)
    {
        var (vejdeSe, xLeva, xPrava, yHore, yDolu) = FitneSeTvar(blok, startX, startY);
        if (!vejdeSe) return (new HashSet<int>(), new HashSet<int>());

        HashSet<int> radky = new HashSet<int>();
        HashSet<int> sloupce = new HashSet<int>();

        int tvarVyska = blok.Rozmery.GetLength(0);
        int tvarSirka = blok.Rozmery.GetLength(1);

        for (int i = 0; i < tvarVyska; i++)
        {
            for (int j = 0; j < tvarSirka; j++)
            {
                if (blok.Rozmery[i, j] == 1)
                {
                    int poleY = startY - yHore + i;
                    int poleX = startX - xLeva + j;
                    radky.Add(poleY);
                    sloupce.Add(poleX);
                }
            }
        }

        return (radky, sloupce);
    }

    private void KonecHry()
    {
        HraSkoncila = true;
        HraBezi = false;
        UlozHighscore();
        OnKonecHry?.Invoke();
    }

    public void ResetHry()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                _hraciPole[y, x] = 0;
            }
        }
        _volnaMista = 64;
        score = 0;
        HraSkoncila = false;
        HraBezi = true;
        GenerujNovouNabidku();
    }

    private static readonly Random _random = new Random();

    private static readonly Func<int, Shape>[] _vsechnyTvary = new Func<int, Shape>[]
    {
        (r) => new SingleBlock(r),
        (r) => new Line2(r),
        (r) => new Line(r),
        (r) => new Line4(r),
        (r) => new Line5(r),
        (r) => new SmallSquare(r),
        (r) => new Square(r),
        (r) => new Corner(r),
        (r) => new BigCorner(r),
        (r) => new SmallL(r),
        (r) => new LShape(r),
        (r) => new JShape(r),
        (r) => new TShape(r),
        (r) => new SShape(r),
        (r) => new ZShape(r),
        (r) => new Plus(r),
        (r) => new DiagonalPair(r)
    };

    public Shape[] AktualniNabidka { get; private set; } = new Shape[3];

    public void GenerujNovouNabidku()
    {
        for (int i = 0; i < 3; i++)
        {
            int index = _random.Next(_vsechnyTvary.Length);
            int rotace = _random.Next(4);
            AktualniNabidka[i] = _vsechnyTvary[index](rotace);
        }
    }

    public Shape? VyberTvarZNabidky(int pozice) //tohle bude na naklliknuti
    {
        if (pozice < 0 || pozice > 2 || AktualniNabidka[pozice] == null)
            return null;

        Shape vybrany = AktualniNabidka[pozice];
        AktualniNabidka[pozice] = null!;
        return vybrany;
    }

    public bool JeNabidkaPrazdna()
    {
        return AktualniNabidka[0] == null &&
               AktualniNabidka[1] == null &&
               AktualniNabidka[2] == null;
    }

    private static readonly string SaveFolder = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        "BlockBlast"
    );
    private static readonly string HighscoreFile = Path.Combine(SaveFolder, "highscore.txt");
    private static readonly string SaveFile = Path.Combine(SaveFolder, "savegame.txt");

    public static string SlozkaSUlozenymiSoubory => SaveFolder;

    public int NactiHighscore()
    {
        if (File.Exists(HighscoreFile))
        {
            string text = File.ReadAllText(HighscoreFile);
            if (int.TryParse(text, out int highscore))
                return highscore;
        }
        return 0;
    }

    public void UlozHighscore()
    {
        Directory.CreateDirectory(SaveFolder);
        int aktualniHighscore = NactiHighscore();
        if (score > aktualniHighscore)
        {
            File.WriteAllText(HighscoreFile, score.ToString());
        }
    }

    public void UlozHru()
    {
        Directory.CreateDirectory(SaveFolder);
        using (StreamWriter sw = new StreamWriter(SaveFile))
        {
            sw.WriteLine(score);

            for (int y = 0; y < 8; y++)
            {
                string radek = "";
                for (int x = 0; x < 8; x++)
                {
                    radek += _hraciPole[y, x];
                    if (x < 7) radek += ",";
                }
                sw.WriteLine(radek);
            }
        }
    }

    public bool NactiHru()
    {
        if (!File.Exists(SaveFile))
            return false;

        try
        {
            string[] radky = File.ReadAllLines(SaveFile);

            score = int.Parse(radky[0]);

            _volnaMista = 64;
            for (int y = 0; y < 8; y++)
            {
                string[] hodnoty = radky[y + 1].Split(',');
                for (int x = 0; x < 8; x++)
                {
                    _hraciPole[y, x] = int.Parse(hodnoty[x]);
                    if (_hraciPole[y, x] == 1)
                        _volnaMista--;
                }
            }

            GenerujNovouNabidku();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public int[,] HraciPole => _hraciPole;
    public int VolnaMista => _volnaMista;
}
