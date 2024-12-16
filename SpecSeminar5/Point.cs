using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpecSeminar5
{
    class Point
    {
        public float x;
        public float y;
        public int index;

        public Point(int index, float x, float y)
        {
            this.x = x;
            this.y = y;
            this.index = index;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Point point) return index == point.index;
            return false;
        }
        public override int GetHashCode() => index.GetHashCode();
    }
}
