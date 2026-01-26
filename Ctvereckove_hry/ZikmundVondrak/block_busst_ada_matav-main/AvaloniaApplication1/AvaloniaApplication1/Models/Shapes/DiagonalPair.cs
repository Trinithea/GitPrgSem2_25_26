namespace AvaloniaApplication1.Models.Shapes;

public class DiagonalPair : Shape
{
    public DiagonalPair(int rotace = 0) : base(rotace)
    {
        bool varianta1 = Rotace == 0 || Rotace == 2;

        Rozmery = varianta1
            ? new int[,]
            {
                { 1, 0 },
                { 0, 1 }
            }
            : new int[,]
            {
                { 0, 1 },
                { 1, 0 }
            };

        Obsah = 2;
        CenterBod = varianta1 ? new int[] { 0, 0 } : new int[] { 1, 0 };
    }
}
