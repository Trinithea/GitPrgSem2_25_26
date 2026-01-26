namespace AvaloniaApplication1.Models.Shapes;

public class ZShape : Shape
{
    public ZShape(int rotace = 0) : base(rotace)
    {
        bool jeHorizontalni = Rotace == 0 || Rotace == 2;

        Rozmery = jeHorizontalni
            ? new int[,]
            {
                { 1, 1, 0 },
                { 0, 1, 1 }
            }
            : new int[,]
            {
                { 0, 1 },
                { 1, 1 },
                { 1, 0 }
            };

        Obsah = 4;
        CenterBod = new int[] { 1, 1 };
    }
}
