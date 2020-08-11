using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.Model
{
    public class OECPVertex:OECPElement,ICloneable
    {
        private static float _tolerance = 0.01F;
        public float X { get; set; }
        public float Y { get; set; }

        public bool IsCornerVertex { get; set; }

        public float BufferTolerance { get; set; } = 0.1F;



        public OECPVertex()
        {
            IsEmpty = true;
        }


        public OECPVertex(float x, float y, bool isCorner = false)
        :base(Color.Black)
        {
            X = x;
            Y = y;
            IsCornerVertex = isCorner;
            Type = ElementType.Vertex;
        }

        public static bool operator !=(OECPVertex pt1, OECPVertex pt2)
        {
            return Math.Abs(pt1.X - pt2.X) > _tolerance || Math.Abs(pt1.Y - pt2.Y) > _tolerance;
        }

        public static bool operator ==(OECPVertex pt1, OECPVertex pt2)
        {
            return Math.Abs(pt1.X - pt2.X) < _tolerance && Math.Abs(pt1.Y - pt2.Y) < _tolerance;
        }

        public override bool Equals(object obj)
        {
            var another = (OECPVertex) obj;
            if (another.IsEmpty)
                return false;
            return Math.Abs(X - another.X) < _tolerance && Math.Abs(Y - another.Y) < _tolerance;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }



    }
}
