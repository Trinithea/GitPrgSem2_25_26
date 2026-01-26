namespace AvaloniaApplication1.Models.Shapes;

public class SingleBlock : Shape
{
    public SingleBlock(int rotace = 0) : base(rotace)
    {
        Rozmery = new int[,]
        {
            { 1 }
        };

        Obsah = 1;
        CenterBod = new int[] { 0, 0 };
    }
}
