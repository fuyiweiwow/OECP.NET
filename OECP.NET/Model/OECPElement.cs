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
        public Color ElementColor { get; set; }

        public OECPElement() { }

        public OECPElement(Color color)
        {
            ElementColor = color;
        }
    }
}
