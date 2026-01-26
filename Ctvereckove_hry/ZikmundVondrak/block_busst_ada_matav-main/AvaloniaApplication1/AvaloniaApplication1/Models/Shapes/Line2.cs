namespace AvaloniaApplication1.Models.Shapes;

public class Line2 : Shape
{
    public Line2(int rotace = 0) : base(rotace)
    {
        bool jeHorizontalni = Rotace == 0 || Rotace == 2;

        Rozmery = jeHorizontalni
            ? new int[,] { { 1, 1 } }
            : new int[,] { { 1 }, { 1 } };

        Obsah = 2;
        CenterBod = new int[] { 0, 0 };
    }
}
