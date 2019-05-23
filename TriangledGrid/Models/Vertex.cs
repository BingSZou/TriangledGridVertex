using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriangledGrid.Models
{
    public class Vertex
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
    }
    public class SquarePos
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}