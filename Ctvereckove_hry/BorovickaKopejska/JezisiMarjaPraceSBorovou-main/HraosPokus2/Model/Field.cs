using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HraosPokus2.Model
{
    internal class Field
    {
        public static readonly (int dx, int dy)[] NeighborOffsets =
        {
            (-1, -1), (0, -1), (1, -1),
            (-1,  0),          (1,  0),
            (-1,  1), (0,  1), (1,  1),
        };

        public bool IsMine { get; set; }
        public int NeighboringMines { get; set; }
    }
}
