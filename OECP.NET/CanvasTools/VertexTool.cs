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
    public class VertexTool : DrawTool
    {
        private bool _allowPointSpawn = false;
        private Point _tempPoint = Point.Empty;
        private OECPVertex _projVtx;

        public VertexTool() : base(ShapeType.Point)
        {

        }

        public override void CanvasTool_MouseDown(object sender, MouseEventArgs e)
        {
            if (_canvas == null || CurrentLayer() == null || CurrentLayer().LayerType != OECPLayer.Type.Vertex)
                return;
            if (WorkStopped())
                return;
            SetBusy();

            if (!_allowPointSpawn)
                return;

            var ele = CurrentLayer().SearchForHighLight(e.Location.X, e.Location.Y, false);
            if (!ele.IsEmpty)
                return;
            _canvas.VertexLayer().AddElement(_projVtx);
            _canvas.RepaintCanvas();
        }

        public override void CanvasTool_MouseMove(object sender, MouseEventArgs e)
        {
            if (_canvas == null || CurrentLayer() == null || CurrentLayer().LayerType != OECPLayer.Type.Vertex)
                return;
            if (WorkStopped())
                return;
            _tempPoint = e.Location;

            var sq = _canvas.GetPrimeSquare();

            var invtx = _canvas.C2I(new OECPVertex(e.Location.X, e.Location.Y));

            bool atBd = VertexAtBoundary(invtx, sq);
            bool online = _canvas.VertexOnLine(invtx,ref _projVtx);
            _allowPointSpawn = atBd || online;
            _canvas.RepaintCanvas();
        }

        public override void CanvasTool_Paint(object sender, PaintEventArgs e)
        {
            if(_allowPointSpawn)
                DrawLocatePoint(_tempPoint.X, _tempPoint.Y,e.Graphics);

        }



        bool VertexAtBoundary(OECPVertex vtx , RectangleF square)
        {
            var leftLine = new OECPLine(new OECPVertex(square.Left, square.Top), new OECPVertex(square.Left, square.Bottom), true);
            var rightLine = new OECPLine(new OECPVertex(square.Right, square.Top), new OECPVertex(square.Right, square.Bottom), true);
            var topLine = new OECPLine(new OECPVertex(square.Left, square.Top), new OECPVertex(square.Right, square.Top), true);
            var botLine = new OECPLine(new OECPVertex(square.Left, square.Bottom), new OECPVertex(square.Right, square.Bottom), true);

            bool onLeft = leftLine.VertexOnLine(vtx);
            if (onLeft)
            {
                _projVtx = leftLine.GetProjectVertex(vtx);
                return true;
            }
            bool onRight = rightLine.VertexOnLine(vtx);
            if (onRight)
            {
                _projVtx = rightLine.GetProjectVertex(vtx);
                return true;
            }
            bool onTop = topLine.VertexOnLine(vtx);
            if (onTop)
            {
                _projVtx = topLine.GetProjectVertex(vtx);
                return true;
            }
            bool onBot = botLine.VertexOnLine(vtx);
            if (onBot)
            {
                _projVtx = botLine.GetProjectVertex(vtx);
                return true;
            }
            return false;
        }



    }
}
