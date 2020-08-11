using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.Model
{
    public class OECPElement
    {
        public enum ElementType
        {
            Undefined = -1,
            Vertex = 0,
            Line = 1
        }


        public ElementType Type { get; set; }
             
        public Color ElementColor { get; set; }

        public Color HighLightColor { get; set; } = Color.GreenYellow; 

        public bool IsHighLight { get; set; }

        public bool IsEmpty { get; set; } = false;

        public OECPElement()
        {
            IsEmpty = true;
        }

        public OECPElement(Color color)
        {
            Type = ElementType.Undefined;
            ElementColor = color;
            IsHighLight = false;
        }

        public static OECPElement Empty()
        {
            return new OECPElement();
        }
    }
}
