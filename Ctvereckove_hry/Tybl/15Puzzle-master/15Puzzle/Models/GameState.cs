using System.Collections.Generic;

namespace Puzzle15
{
    public class GameState
    {
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int CurrentMoves { get; set; }
        public int BestScore { get; set; } = int.MaxValue;
        public List<int> TileValues { get; set; }
    }
}