using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OECP.NET.CanvasTools;

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

        public Color LayerColor { get; set; }

        public bool IsLocked { get; set; }

        public bool IsVisible { get; set; }

        public bool IsLine { get; set; } 

        public bool IsGrid { get; set; }

        public Type LayerType { get; set; }

        public LineFcType LineType { get; set; }

        private Dictionary<Guid,OECPElement> ElementMap { get; set; } = new Dictionary<Guid, OECPElement>();

        private List<OECPElement> Elements { get; set; } = new List<OECPElement>();

        public OECPLayer(Color layerColor,Type layerType = Type.Undefined, LineFcType lineType = LineFcType.Undefined)
        {
            LayerType = layerType;
            LineType = lineType;
            IsLine = LayerType == Type.Line;
            IsGrid = LayerType == Type.Grid;
            LayerColor = layerColor;
        }


        public OECPElement SearchForHighLight(float x,float y,bool setHighLight = true)
        {
            if (Elements.Count == 0)
                return OECPElement.Empty();
            bool isLine = Elements[0].Type == OECPElement.ElementType.Line;
            OECPElement ret = OECPElement.Empty();
            foreach (OECPElement ele in Elements)
            {
                if (isLine)
                {
                    return new OECPElement();
                }
                else
                {
                    var vtx = (OECPVertex) ele;
                    var tole = vtx.BufferTolerance;
                    if (Math.Abs(vtx.X - x) <= tole
                        && Math.Abs(vtx.Y - y) <= tole)
                    {
                        if(setHighLight)
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

        public bool DeleteVertex(OECPVertex vtx)
        {
            //todo:只有孤立的节点才可以删除
            if (vtx.IsEmpty)
                return false;
            foreach (OECPElement ele in Elements)
            {
                var eleVtx = (OECPVertex)ele;
                if (eleVtx == vtx)
                {
                    Elements.Remove(eleVtx);
                    return true;
                }
            }
            return false;
        }

        public void AddElement(OECPElement ele)
        {
            //todo:加入栈中，实现撤销重做
            ele.Layer = this;
            this.Elements.Add(ele);
            this.ElementMap.Add(ele.Eid, ele);
        }

        public void AddElements(IEnumerable<OECPElement> eles)
        {
            foreach (OECPElement ele in eles)
                AddElement(ele);
        }

        public List<OECPElement> LayerElements()
        {
            return Elements;
        }
        


    }
}
