using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TriangledGrid.Models
{
    public class Square
    {
        // row and the column where the square is in the grid
        public int Row { get; private set; }
        public int Column { get; private set; }
        public int Width { get; private set; }

        // the 4 vertex of the square clock wise,  from TopLeft, BottomLeft, BottomRight, TopRight
        public IEnumerable<Vertex> AllVertices { get; private set; }

        public IEnumerable<Vertex> LeftTriangleVertices { get; private set; }
        public IEnumerable<Vertex> RightTriangleVertices { get; private set; }
        public Square(int row, int column, int width)
        {
            Row = row;
            Column = column;
            Width = width;
            
            AllVertices = new List<Vertex>(4)
            {
                new Vertex {XPos = Column * Width, YPos = Row * Width},
                new Vertex {XPos = Column * Width, YPos = (Row + 1) * Width },
                new Vertex {XPos = (Column + 1) * Width, YPos = (Row + 1)* Width },
                new Vertex {XPos = (Column + 1) * Width, YPos = Row * Width }
            };
            LeftTriangleVertices = new List<Vertex>(3)
            {
                AllVertices.ElementAt(0),
                AllVertices.ElementAt(1),
                AllVertices.ElementAt(2),
            };
            RightTriangleVertices = new List<Vertex>(3)
            {
                AllVertices.ElementAt(0),
                AllVertices.ElementAt(3),
                AllVertices.ElementAt(2),
            };
        }
    }
    public enum CornersEnum
    {
        TopLeft = 0,
        BottomLeft,
        BottomRight,
        TopRight
    }
}