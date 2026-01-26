namespace AvaloniaApplication1.Models.Shapes;

public class SmallSquare : Shape
{
    public SmallSquare(int rotace = 0) : base(rotace)
    {
        Rozmery = new int[,]
        {
            { 1, 1 },
            { 1, 1 }
        };

        Obsah = 4;
        CenterBod = new int[] { 0, 0 };
    }
}
