namespace AvaloniaApplication1.Models;

public abstract class Shape
{
    public int Rotace { get; protected set; }
    public int[,] Rozmery { get; protected set; } = new int[0, 0];
    public int Obsah { get; protected set; }
    public int[] CenterBod { get; protected set; }

    protected Shape(int rotace)
    {
        Rotace = rotace % 4;
        if (Rotace < 0) Rotace += 4;
    }

    public int Vyska => Rozmery.GetLength(0);
    public int Sirka => Rozmery.GetLength(1);
}
