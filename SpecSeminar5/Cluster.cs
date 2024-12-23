using System;
using System.Collections;
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

        public Point endPoint = null;
        public Point startPoint = null;
        public Cluster(List<Point> list, Point point)
        {
            this.list = list;
            this.point = new(point);
        }

        public Cluster(Cluster cl)
        {
            this.point = cl.point is null ? null : new(cl.point);
            this.list = cl.list is null ? null : new(cl.list);            
            this.endPoint = cl.endPoint is null ? null : new(cl.endPoint);
            this.startPoint = cl.startPoint is null ? null : new(cl.startPoint);
        }

        public void Add(Point p)
        {
            //if (point.index == p.index) throw new Exception();
            list.Add(p);
        }

        public void AddWR(Point p)
        {
            //if (point.index == p.index) throw new Exception();
            list.Add(p);

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
        public List<Point> calculatePathGreedy()
        {
            List<Point> path = new List<Point>();
            path.Add(startPoint);
            List<Point> temp = new(list);
            temp.Remove(startPoint);
            temp.Remove(endPoint);
            if (temp.Count == 0) 
            {
                path.Add(endPoint);
                return path;
            }

            Point cur = startPoint;
            Point tempP = null;
            float min = float.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                min = float.MaxValue;
                foreach (Point p in temp)
                {
                    if (min > calculateDistance(cur, p))
                    {
                        min = calculateDistance(cur, p);
                        tempP = p;
                    }
                }

                temp.Remove(tempP);
                cur = tempP;
                path.Add(tempP);

                if (temp.Count == 0)
                {
                    path.Add(endPoint);
                    break;
                }
            }
            return path;
        }

        private float calculateDistance(Point p1, Point p2)
        {
            return (float)Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
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
