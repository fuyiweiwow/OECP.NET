using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.Model
{
    class OECPLayer
    {
        public enum Type
        {
            Undefined = -1,
            Line = 0,
            Vertex = 1,
            Grid = 2,
        }

        public enum LineFcType
        {
            Undefined = -1,
            M = 0,
            V = 1,
            Aux = 2,
        }


        public bool IsLocked { get; set; }

        public bool IsVisible { get; set; }

        public bool IsLine { get; set; } 

        public Type LayerType { get; set; }

        public LineFcType LineType { get; set; }

        public List<OECPElement> Elements { get; set; } = new List<OECPElement>();


        public OECPLayer(Type layerType = Type.Undefined, LineFcType lineType = LineFcType.Undefined)
        {
            LayerType = layerType;
            LineType = lineType;
            IsLine = LayerType == Type.Line;
        }

    }
}
