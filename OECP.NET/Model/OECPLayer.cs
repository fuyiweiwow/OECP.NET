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


        public OECPElement SearchForHighLight(float x,float y,float scale)
        {
            bool isLine = Elements[0].Type == OECPElement.ElementType.Line;
            OECPElement ret = null;
            foreach (OECPElement ele in Elements)
            {
                if (isLine)
                {
                    return new OECPElement();
                }
                else
                {
                    var vtx = (OECPVertex) ele;
                    var tole = scale/ vtx.BufferTolerance;
                    if (Math.Abs(vtx.X - x) <= tole
                        && Math.Abs(vtx.Y - y) <= tole)
                    {
                        vtx.IsHighLight = true;
                        ret = vtx;
                        break;
                    }
                    else
                        vtx.IsHighLight = false;
                }
            }

            return ret;
        }

        

    }
}
