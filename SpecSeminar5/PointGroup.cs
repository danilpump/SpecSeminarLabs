using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar5
{
    internal class PointGroup
    {
        public int clustersCount = 0;
        public Dictionary<int, Cluster> dict;

        public PointGroup()
        {
            dict = new Dictionary<int, Cluster>();
        }

        public PointGroup(PointGroup pg)
        {
            dict = new Dictionary<int, Cluster>();
            foreach(KeyValuePair<int, Cluster> cl in pg.dict)
                this.dict.Add(cl.Key, cl.Value);
        }

        public void AddPoint(int clusterIndex, Point p) 
        {
            if (clusterIndex == p.index) throw new Exception();
            dict[clusterIndex].Add(p);          
        }
        public void CreateCluster(int clusterIndex)
        {
            dict.Add(clusterIndex, new Cluster(new List<Point>(), clusterIndex));
        }

        public void Print()
        {
            Console.Write("{ ");
            foreach (KeyValuePair<int,Cluster> cl in dict)
            { Console.Write(cl.Value.index + " "); cl.Value.Print(); }
            Console.WriteLine(" }");
        }

    }
}
