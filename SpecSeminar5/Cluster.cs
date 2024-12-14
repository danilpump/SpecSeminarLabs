using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar5
{
    internal class Cluster
    {
        public int index;
        public List<Point> list;

        public Cluster(List<Point> list, int index)
        {
            this.list = list;
            this.index = index;
        }

        public void Add(Point p)
        {
            if (index == p.index) throw new Exception();
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
