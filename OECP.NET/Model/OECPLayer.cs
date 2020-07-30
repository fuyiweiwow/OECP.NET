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

        public bool IsLocked { get; set; }

        public bool IsVisible { get; set; }

        public Type LayerType { get; set; } = Type.Undefined;

        public List<OECPElement> Elements { get; set; } = new List<OECPElement>();


    }
}
