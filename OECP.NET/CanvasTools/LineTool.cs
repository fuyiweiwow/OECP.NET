using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OECP.NET.Model;

namespace OECP.NET.CanvasTools
{
    class LineTool:DrawTool
    {

        private  List<OECPVertex> _ptCollection = new List<OECPVertex>(2);

        private  Point _tempPoint = Point.Empty;

        public LineTool():base(ShapeType.Line)
        {
            
        }

        public override void CanvasTool_MouseDown(object sender, MouseEventArgs e)
        {
            if (_canvas == null||CurrentLayer() == null|| !CurrentLayer().IsLine)
                return;
            if (WorkStopped())
                return;
            OECPVertex vtx = new OECPVertex(e.Location.X, e.Location.Y);
            if (IsWaiting())
            {
                if (e.Button != MouseButtons.Left)
                    return;

                SetBusy();
                var st = _canvas.C2I(vtx);
                _ptCollection.Add(st);
                _canvas.VertexLayer().Elements.Add(st);
                _canvas.RepaintCanvas();
                return;
            }
            if (IsWorking())
            {
                SetWaiting();

                _canvas.FreezeRightClickMenu(true);
                if (e.Button == MouseButtons.Left)
                {
                    
                    var edPt = _canvas.C2I(vtx);
                    _ptCollection.Add(edPt);
                    _canvas.VertexLayer().Elements.AddRange(_ptCollection);

                    var line = new OECPLine(_ptCollection[0], _ptCollection[1]);
                    CurrentLayer().Elements.Add(line);

                    _ptCollection.Clear();
                    _canvas.RepaintCanvas();
                }

                if (e.Button == MouseButtons.Right) 
                {
                   
                    var vtxLayer = _canvas.VertexLayer();
                    if (vtxLayer == null)
                        return;
                    vtx = _ptCollection[0];
                    vtxLayer.DeleteVertex(vtx);
                    _ptCollection.Clear();
                    _canvas.RepaintCanvas();
                }

            }
        }

        public override void CanvasTool_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canvas == null || CurrentLayer() == null || !CurrentLayer().IsLine)
                return;
            if (!IsWorking())
                return;

            _tempPoint = e.Location;
            OECPVertex vtx = new OECPVertex(_tempPoint.X, _tempPoint.Y);
            var tpEd = _canvas.C2I(vtx);
            if(_ptCollection.Count == 2)
                _ptCollection[1] = tpEd;
            else
                _ptCollection.Add(tpEd);
            _canvas.RepaintCanvas();

        }

        public override void CanvasTool_Paint(object sender, PaintEventArgs e)
        {
            if (_canvas == null || CurrentLayer() == null || !CurrentLayer().IsLine)
                return; 
            if (!IsWorking())
                return;

            OECPVertex vtx = new OECPVertex(_tempPoint.X, _tempPoint.Y);
            var stPt = _canvas.I2C(_ptCollection[0]);
            if (_ptCollection.Count == 1)
                DrawShape(stPt.X, stPt.Y, e.Graphics);

            if (_ptCollection.Count == 2)
                DrawShape(stPt.X, stPt.Y, vtx.X, vtx.Y, e.Graphics);

        }


        public override void CanvasTool_MouseUp(object sender, MouseEventArgs e)
        {
          
        }
    }
}
