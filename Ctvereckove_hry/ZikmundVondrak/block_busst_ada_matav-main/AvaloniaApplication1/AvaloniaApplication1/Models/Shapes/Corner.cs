namespace AvaloniaApplication1.Models.Shapes;

public class Corner : Shape
{
    public Corner(int rotace = 0) : base(rotace)
    {
        Rozmery = Rotace switch
        {
            0 => new int[,]
            {
                { 1, 0 },
                { 1, 1 }
            },
            1 => new int[,]
            {
                { 1, 1 },
                { 1, 0 }
            },
            2 => new int[,]
            {
                { 0, 1 },
                { 1, 1 }
            },
            3 => new int[,]
            {
                { 1, 1 },
                { 0, 1 }
            },
            _ => new int[,]
            {
                { 1, 0 },
                { 1, 1 }
            }
        };

        Obsah = 3;
        CenterBod = Rotace switch
        {
            0 => new int[] { 1, 0 },
            1 => new int[] { 0, 0 },
            2 => new int[] { 1, 1 },
            3 => new int[] { 0, 1 },
            _ => new int[] { 1, 0 }
        };
    }
}
