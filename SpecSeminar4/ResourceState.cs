using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecSeminar4
{
    internal class ResourceState
    {
        public int freeTime;
        public Operation operation;

        public ResourceState(int freeTime, Operation operation)
        {
            this.freeTime = freeTime;
            this.operation = operation;
        }
    }
}
