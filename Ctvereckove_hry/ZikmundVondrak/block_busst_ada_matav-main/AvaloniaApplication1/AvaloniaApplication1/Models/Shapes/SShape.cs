namespace AvaloniaApplication1.Models.Shapes;

public class SShape : Shape
{
    public SShape(int rotace = 0) : base(rotace)
    {
        bool jeHorizontalni = Rotace == 0 || Rotace == 2;

        Rozmery = jeHorizontalni
            ? new int[,]
            {
                { 0, 1, 1 },
                { 1, 1, 0 }
            }
            : new int[,]
            {
                { 1, 0 },
                { 1, 1 },
                { 0, 1 }
            };

        Obsah = 4;
        CenterBod = new int[] { 1, 1 };
    }
}
