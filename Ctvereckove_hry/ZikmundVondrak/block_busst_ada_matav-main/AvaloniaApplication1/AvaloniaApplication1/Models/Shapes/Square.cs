namespace AvaloniaApplication1.Models.Shapes;

public class Square : Shape
{
    public Square(int rotace = 0) : base(rotace)
    {
        Rozmery = new int[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
        };
        Obsah = 9;
        CenterBod = new int[] { 1, 1 };
    }
}
