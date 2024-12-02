using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar3
{
    class BranchNode
    {
        public List<int> partialRoute;
        public int lowerBound;
        public int upperBound;

        public BranchNode(List<int> partialRoute, int lowerBound, int upperBound)
        {
            this.partialRoute = new List<int>(partialRoute);
            this.lowerBound = lowerBound;
            this.upperBound = upperBound;
        }
    }
}
