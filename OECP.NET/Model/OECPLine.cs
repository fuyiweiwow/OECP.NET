using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.Model
{
    public class OECPLine:OECPElement
    {
        public OECPVertex StartVertex { get; set; }
        public OECPVertex EndVertex { get; set; }


        public OECPLine()
        {
            StartVertex = (OECPVertex)Empty();
            EndVertex = (OECPVertex)Empty();
            IsEmpty = true;
            Type = ElementType.Line;
        }

        public OECPLine(OECPVertex stVtx, OECPVertex edVtx)
        {
            StartVertex = stVtx;
            EndVertex = edVtx;
            Type = ElementType.Line;
        }

        public double Distance(OECPVertex vtx)
        {
            double cross = (EndVertex.X -StartVertex.X) * (vtx.X - StartVertex.X) + (EndVertex.Y - StartVertex.Y) * (vtx.Y - StartVertex.Y);
            if (cross <= 0) return Math.Sqrt((vtx.X - StartVertex.X) * (vtx.X - StartVertex.X) + (vtx.Y - StartVertex.Y) * (vtx.Y - StartVertex.Y));

            double d2 = (EndVertex.X - StartVertex.X) * (EndVertex.X - StartVertex.X) + (EndVertex.Y - StartVertex.Y) * (EndVertex.Y - StartVertex.Y);
            if (cross >= d2) return Math.Sqrt((vtx.X - EndVertex.X) * (vtx.X - EndVertex.X) + (vtx.Y - EndVertex.Y) * (vtx.Y - EndVertex.Y));

            double r = cross / d2;
            double px = StartVertex.X + (EndVertex.X - StartVertex.X) * r;
            double py = StartVertex.Y + (EndVertex.Y - StartVertex.Y) * r;
            return Math.Sqrt((vtx.X - px) * (vtx.X - px) + (py - vtx.Y) * (py - vtx.Y));
        }

        public bool VertexOnLine(OECPVertex vtx)
        {
            return Distance(vtx) <= 1;
        }

        public OECPVertex GetProjectVertex(OECPVertex vtx)
        {
            double a1 = EndVertex.X - StartVertex.X;
            double b1 = EndVertex.Y - StartVertex.Y;
            double y1 = StartVertex.Y;
            double x1 = StartVertex.X;
            double y2 = EndVertex.Y;
            double x2 = EndVertex.X;
            double y3 = vtx.Y;
            double x3 = vtx.X;
            double a1a1 = Math.Pow(a1, 2.0);
            double b1b1 = Math.Pow(b1, 2.0);
            double denominator = a1a1 + b1b1;
            if (denominator == 0) return vtx;

            double x1y2 = x1 * y2;
            double x2y1 = x2 * y1;
            double a1b1 = a1 * b1;
            double moleculey = b1b1 * y3 + a1b1 * x3 - a1 * x1y2 + a1 * x2y1;
            double moleculex = a1a1 * x3 + a1b1 * y3 - b1 * x2y1 + b1 * x1y2;

            return new OECPVertex((float)(moleculex / denominator), (float)(moleculey / denominator));


        }



    }
}
