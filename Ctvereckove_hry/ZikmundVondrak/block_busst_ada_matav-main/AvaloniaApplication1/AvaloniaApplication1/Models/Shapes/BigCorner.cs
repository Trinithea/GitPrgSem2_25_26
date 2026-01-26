namespace AvaloniaApplication1.Models.Shapes;

public class BigCorner : Shape
{
    public BigCorner(int rotace = 0) : base(rotace)
    {
        Rozmery = Rotace switch
        {
            0 => new int[,]
            {
                { 1, 0, 0 },
                { 1, 0, 0 },
                { 1, 1, 1 }
            },
            1 => new int[,]
            {
                { 1, 1, 1 },
                { 0, 0, 1 },
                { 0, 0, 1 }
            },
            2 => new int[,]
            {
                { 0, 0, 1 },
                { 0, 0, 1 },
                { 1, 1, 1 }
            },
            3 => new int[,]
            {
                { 1, 1, 1 },
                { 1, 0, 0 },
                { 1, 0, 0 }
            },
            _ => new int[,]
            {
                { 1, 0, 0 },
                { 1, 0, 0 },
                { 1, 1, 1 }
            }
        };

        Obsah = 5;
        CenterBod = Rotace switch
        {
            0 => new int[] { 2, 0 },
            1 => new int[] { 0, 2 },
            2 => new int[] { 2, 2 },
            3 => new int[] { 0, 0 },
            _ => new int[] { 2, 0 }
        };
    }
}
