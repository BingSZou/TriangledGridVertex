using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TriangledGrid.Models;

namespace TriangledGrid.Controllers
{
    public class ValuesController : ApiController
    {
        public static SquareGridModel StaticGrid = new SquareGridModel(6, 6, 10);

        // GET api/values
        public IEnumerable<IEnumerable<Vertex>> Get()
        {
            return StaticGrid.GetAllVertices();
        }

        public IEnumerable<Vertex> GetVerticesForTriangle(string id)
        {
            Console.WriteLine("calling post vertics " + id);
            return StaticGrid.GetVerticesForTriangle (id);
        }
        [HttpPost]
        public string PostTriangleIdFromRowColumn(SquarePos pos)
        {
            Console.WriteLine("calling  PostTriangleIdFromRowColumn " + pos.Row + " " + pos.Column);
            return StaticGrid.GetTriangleIdFromRowColumn(pos.Row, pos.Column);
        }
    }
}
