using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar5
{
    internal class Cluster
    {
        public Point point;
        public List<Point> list;

        public Cluster(List<Point> list, Point point)
        {
            this.list = list;
            this.point = point;
        }

        public void Add(Point p)
        {
            //if (point.index == p.index) throw new Exception();
            list.Add(p);
        }

        public void Print() 
        {
            Console.Write("[ ");
            foreach (Point p in list)
                Console.Write(p.index + " ");
            Console.Write("]");
        }
    }
}
