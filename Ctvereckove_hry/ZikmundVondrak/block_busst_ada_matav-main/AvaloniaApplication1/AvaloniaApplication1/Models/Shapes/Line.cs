namespace AvaloniaApplication1.Models.Shapes;

public class Line : Shape
{
    public Line(int rotace = 0) : base(rotace)
    {
        bool jeHorizontalni = Rotace == 0 || Rotace == 2;

        Rozmery = jeHorizontalni
            ? new int[,] { { 1, 1, 1 } }           // Horizontální: 1x3
            : new int[,] { { 1 }, { 1 }, { 1 } };  // Vertikální: 3x1

        Obsah = 3;
        CenterBod = jeHorizontalni ? new int[] { 0, 1 } : new int[] { 1, 0 };
    }
}
