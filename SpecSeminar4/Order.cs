using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar4
{
    class Order
    {
        public List<int> vertexes;
        public Dictionary<int, List<int>> edges;
        public Dictionary<int, int> resources;
        public Dictionary<int, int> duration;
        public int startTime;
        public int directiveTime;
        public int completionTime;

        public Order(List<int> vertexes, Dictionary<int, List<int>> edges, Dictionary<int, int> resources, Dictionary<int, int> duration, int startTime, int directiveTime)
        {
            this.vertexes = vertexes;
            this.edges = edges;
            this.resources = resources;
            this.duration = duration;
            this.startTime = startTime;
            this.directiveTime = directiveTime;
        }
    }
}
