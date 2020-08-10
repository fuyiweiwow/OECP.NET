using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.Model
{
    public class OECPVertex:OECPElement
    {
        private static float _tolerance = 0.01F;
        public float X { get; set; }
        public float Y { get; set; }

        public bool IsCornerVertex { get; set; }

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

     

    }
}
