using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar5
{
    class PointGroup
    {
        public int targetClCount = 0;
        public Dictionary<int, Cluster> Clusters;

        public PointGroup(int clC)
        {
            Clusters = new Dictionary<int, Cluster>();
            targetClCount = clC;
        }

        public PointGroup(PointGroup pg)
        {
            Clusters = new Dictionary<int, Cluster>();
            foreach(KeyValuePair<int, Cluster> cl in pg.Clusters)
                this.Clusters.Add(cl.Key, cl.Value);
            targetClCount = pg.targetClCount;
        }

        public void AddPoint(int clusterIndex, Point p) 
        {
            if (clusterIndex == p.index)
                CreateCluster(p);
            Clusters[clusterIndex].Add(p);          
        }
        public void CreateCluster(Point cl)
        {
            Clusters.Add(cl.index, new Cluster(new List<Point>(), cl));
            //clustersCount++;
        }

        public void Print()
        {
            Console.Write("{ ");
            foreach (KeyValuePair<int,Cluster> cl in Clusters)
            { Console.Write(cl.Value.point.index + " "); cl.Value.Print(); }
            Console.WriteLine(" }");
        }

    }
}
