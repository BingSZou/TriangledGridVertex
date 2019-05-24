using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TriangledGrid.Models
{
    public class SquareGridModel
    {
        // not sure using singleton is a good idea here, 
        // as we want the grid to be any size, not limit to 6x6 grid. 
        public static SquareGridModel SquareGridInstance
        {
            get
            {
                return _staticGrid ?? (_staticGrid = new SquareGridModel(6, 6, 10));
            }
        }

        private static SquareGridModel _staticGrid = null;

        private List<List<Square>> _allSquares = null;
        public SquareGridModel(int numColumns, int numRows, int width)
        {
            _allSquares = new List<List<Square>>();

            for (var i = 0; i < numColumns; i++)
            {
                var squareRow = new List<Square>();
                _allSquares.Add(squareRow);

                for (int j = 0; j < numRows; j++)
                {
                    squareRow.Add(new Square(i, j, width));
                }
            }
            _RowLetters = new List<char>();
            int seed = 'A';
            for(var indexer = 0; indexer < 26; indexer ++)
            {
                _RowLetters.Add((char) (seed + indexer));
            }
        }
        public IEnumerable<IEnumerable<Vertex>> GetAllVertices()
        {
            var allV = new List<List<Vertex>>();
            foreach (var rowVetex in _allSquares)
            {
                foreach (var s in rowVetex)
                {
                    allV.Add(new List<Vertex>(s.AllVertices));
                }
            }
            return allV;
        }
        private (int, int) GetRowAndColumnFromId(string id)
        {
            // error handling 
            var rowLetter = id.ToUpper().First<char>();
            var columnLetter = id.Substring(1);
            if (!int.TryParse(columnLetter, out int columnIdx) || columnIdx < 1)
            {
                // error handling
                return (-1, -1);
            }
            columnIdx -= 1;  // we want the 0 index based since array is 0 index based. 

            int rowIdx = _RowLetters.IndexOf(rowLetter);
            if (rowIdx < 0)
            {
                // error handling
                return (-1, -1);
            }
            return (rowIdx, columnIdx);
        }
        readonly  List<char> _RowLetters = null;
        
       public IEnumerable<Vertex> GetVerticesForTriangle(string id)
        {
            (var rowIdx, var columnIdx) = GetRowAndColumnFromId(id);
            if (rowIdx < 0 || columnIdx < 0)
            {
                // error handling input parameter error
                return null;
            }
          
            var square = GetSquare(rowIdx, columnIdx);

            if(square == null)
            {
                // error handling square Not found
                return null;
            }

            bool isLeftTriangle = (columnIdx % 2) == 0;
            return isLeftTriangle ? square.LeftTriangleVertices : square.RightTriangleVertices;
        }
        private Square GetSquare(int row, int column)
        {
            if (row < 0 && column < 0)
                return null;

            var squareRow = _allSquares.ElementAtOrDefault(row);
            if (squareRow == null)
            {
                return null;
            }
            // process the column,  the column come in here is the triangle column, not the square column
            column = column / 2;
            var square = squareRow.ElementAtOrDefault(column);
            return square;
        }
        public string GetTriangleIdFromRowColumn(int row, int column)
        {
            if ( row < 0 || column< 0)
            {
                // error handling
                return string.Empty;
            }
            char rowLetter = _RowLetters.ElementAtOrDefault(row -1); // the letterMapping is 0 based
            if( !Regex.IsMatch(rowLetter.ToString(), "[A-Z]") )
            {
                // erorr handling not recognized row
                return string.Empty;
            }

            return $"{rowLetter}{column}";
        }
    }
}