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
        public float X { get; set; }
        public float Y { get; set; }

        public bool IsCornerVertex { get; set; }

        public OECPVertex(float x, float y, bool isCorner = false)
        :base(Color.Black)
        {
            X = x;
            Y = y;
            IsCornerVertex = isCorner;
        }



    }
}
