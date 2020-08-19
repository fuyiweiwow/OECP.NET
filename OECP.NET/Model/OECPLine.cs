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


        public bool VertexOnLine(OECPVertex vtx)
        {
            var lxVector = EndVertex.X - StartVertex.X;
            var lyVector = EndVertex.Y - StartVertex.Y;
            var v2StxVector = vtx.X - StartVertex.X;
            var v2StyVector = vtx.Y - StartVertex.Y;

            //向量乘积为0则该点在线上
            double vectorValue = lxVector * v2StyVector - v2StxVector * lyVector;
            if (vectorValue != 0.0)
                return false;
            return true;
        }


    }
}
