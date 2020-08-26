using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OECP.NET.CanvasTools
{
    public class PanTool:CanvasTool
    {
        public PointF StartPoint { get; set; } = PointF.Empty;
        public PointF EndPoint { get; set; } = PointF.Empty;

        private bool _registerLeft = false;
        public bool RegisterLeft => _registerLeft;

        public PanTool() : base(CanvasToolType.PanTool)
        {


        }

        public override void CanvasTool_MouseDown(object sender, MouseEventArgs e)
        {
            bool rd = RegisterLeft && e.Button == MouseButtons.Left;

            if (e.Button != MouseButtons.Middle && !rd)
                return;

            if (IsWaiting())
            {
                SetBusy();
                StartPoint = new PointF(e.Location.X - EndPoint.X, e.Location.Y - EndPoint.Y);
            }
           

        }

        public override void CanvasTool_MouseMove(object sender, MouseEventArgs e)
        {
            if(!IsWorking())
                return;

            EndPoint = new PointF(_canvas.CanvasLeft() + e.Location.X - StartPoint.X,
                _canvas.CanvasTop() + e.Location.Y - StartPoint.Y);
            _canvas.SetCurrentSquareLocation(EndPoint);
            _canvas.RepaintCanvas();
        }


        public override void CanvasTool_MouseUp(object sender, MouseEventArgs e)
        {
           if(IsWorking())
               SetWaiting();
        }
    }
}
