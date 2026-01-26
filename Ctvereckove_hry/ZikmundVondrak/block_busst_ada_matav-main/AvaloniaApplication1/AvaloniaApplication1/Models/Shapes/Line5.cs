namespace AvaloniaApplication1.Models.Shapes;

public class Line5 : Shape
{
    public Line5(int rotace = 0) : base(rotace)
    {
        bool jeHorizontalni = Rotace == 0 || Rotace == 2;

        Rozmery = jeHorizontalni
            ? new int[,] { { 1, 1, 1, 1, 1 } }
            : new int[,] { { 1 }, { 1 }, { 1 }, { 1 }, { 1 } };

        Obsah = 5;
        CenterBod = jeHorizontalni ? new int[] { 0, 2 } : new int[] { 2, 0 };
    }
}
