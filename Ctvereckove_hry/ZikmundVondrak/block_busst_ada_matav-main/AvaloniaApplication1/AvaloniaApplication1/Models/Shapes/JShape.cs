namespace AvaloniaApplication1.Models.Shapes;

public class JShape : Shape
{
    public JShape(int rotace = 0) : base(rotace)
    {
        Rozmery = Rotace switch
        {
            0 => new int[,]
            {
                { 0, 1 },
                { 0, 1 },
                { 1, 1 }
            },
            1 => new int[,]
            {
                { 1, 1, 1 },
                { 0, 0, 1 }
            },
            2 => new int[,]
            {
                { 1, 1 },
                { 1, 0 },
                { 1, 0 }
            },
            3 => new int[,]
            {
                { 1, 0, 0 },
                { 1, 1, 1 }
            },
            _ => new int[,]
            {
                { 0, 1 },
                { 0, 1 },
                { 1, 1 }
            }
        };

        Obsah = 4;
        CenterBod = Rotace switch
        {
            0 => new int[] { 2, 1 },
            1 => new int[] { 0, 2 },
            2 => new int[] { 0, 0 },
            3 => new int[] { 1, 0 },
            _ => new int[] { 2, 1 }
        };
    }
}
