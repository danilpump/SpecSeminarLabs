using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            this.point = new(point);
        }

        public void Add(Point p)
        {
            //if (point.index == p.index) throw new Exception();
            list.Add(p);
            //пересчет центра кластера
            RecalculateCenter();
        }
        private void RecalculateCenter()
        {
            float x = 0;
            float y = 0;
            foreach (Point p in list)
            {
                x += p.x;
                y += p.y;
            }
            x /= list.Count;
            y /= list.Count;
            point.x = x;
            point.y = y;
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
