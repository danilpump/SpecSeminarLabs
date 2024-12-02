using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar3
{
    class Delivery
    {
        public int n;
        public int[] timeRequirement;
        public int[,] moveTime;

        public Delivery(int n, int[] timeRequirement, int[,] moveTime)
        {
            this.n = n;
            this.timeRequirement = timeRequirement;
            this.moveTime = moveTime;
        }
    }
}
