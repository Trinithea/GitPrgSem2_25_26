namespace AvaloniaApplication1.Models.Shapes;

public class Line4 : Shape
{
    public Line4(int rotace = 0) : base(rotace)
    {
        bool jeHorizontalni = Rotace == 0 || Rotace == 2;

        Rozmery = jeHorizontalni
            ? new int[,] { { 1, 1, 1, 1 } }
            : new int[,] { { 1 }, { 1 }, { 1 }, { 1 } };

        Obsah = 4;
        CenterBod = jeHorizontalni ? new int[] { 0, 1 } : new int[] { 1, 0 };
    }
}
