namespace AvaloniaApplication1.Models.Shapes;

public class Plus : Shape
{
    public Plus(int rotace = 0) : base(rotace)
    {
        Rozmery = new int[,]
        {
            { 0, 1, 0 },
            { 1, 1, 1 },
            { 0, 1, 0 }
        };

        Obsah = 5;
        CenterBod = new int[] { 1, 1 };
    }
}
