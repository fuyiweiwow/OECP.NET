using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OECP.NET.Model
{
    public class OECPLayer
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

        public Color LayerColor { get; set; } = Color.Black;

        public bool IsLocked { get; set; }

        public bool IsVisible { get; set; }

        public bool IsLine { get; set; } 

        public bool IsGrid { get; set; }

        public Type LayerType { get; set; }

        public LineFcType LineType { get; set; }

        public List<OECPElement> Elements { get; set; } = new List<OECPElement>();

        public UserControl LayerControl { get; set; }

        public OECPLayer(Type layerType = Type.Undefined, LineFcType lineType = LineFcType.Undefined)
        {
            LayerType = layerType;
            LineType = lineType;
            IsLine = LayerType == Type.Line;
            IsGrid = LayerType == Type.Grid;
        }

        public void SetLayerColor(Color color)
        {
            LayerColor = color;
        }


        public bool SearchForHighLight(float x,float y)
        {
            bool flag = false;
            bool isLine = Elements[0].Type == OECPElement.ElementType.Line;
            foreach (OECPElement ele in Elements)
            {
                if (isLine)
                {
                    return flag;
                }
                else
                {
                    var vtx = (OECPVertex) ele;
                    if (Math.Abs(vtx.X - x) <= vtx.BufferTolerance
                        && Math.Abs(vtx.Y - y) <= vtx.BufferTolerance)
                    {
                        flag = true;
                        vtx.IsHighLight = true;
                        break;
                    }
                    else
                        vtx.IsHighLight = false;
                }
            }

            return flag;
        }

        

    }
}
