using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OECP.NET.CanvasTools
{
    public  class CanvasTool :Control
    {
        protected ICanvasSignal _canvas;

        public enum CanvasToolType
        {
            PanTool = 0,
            DrawTool = 1,
            DeleteTool = 2,
        }

        public enum WorkStatus
        {
            IsWorking = 0,
            WorkStopped = 1,
            Waiting = 2,
        }

        public WorkStatus Status { get; set; }

        public CanvasToolType ToolType { get; set; }


        public CanvasTool(CanvasToolType type = CanvasToolType.PanTool)
        {
            ToolType = type;
            this.MouseDown += CanvasTool_MouseDown;
            this.MouseUp += CanvasTool_MouseUp;
            this.MouseMove += CanvasTool_MouseMove;
            this.Paint += CanvasTool_Paint;
        }
       

        public void SetCanvas(ICanvasSignal canvas)
        {
            _canvas = canvas;
        }


        public virtual void CanvasTool_Paint(object sender, PaintEventArgs e)
        {

        }


        public virtual void CanvasTool_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        public virtual void CanvasTool_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        public virtual void CanvasTool_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        public void SetWaiting()
        {
            Status = WorkStatus.Waiting;
        }

        public void StopWorking()
        {
            Status = WorkStatus.WorkStopped;
        }

        public void SetBusy()
        {
            Status = WorkStatus.IsWorking;
        }


        public bool IsWorking()
        {
            return Status == WorkStatus.IsWorking;
        }

        public bool IsWaiting()
        {
            return Status == WorkStatus.Waiting;
        }

        public bool WorkStopped()
        {
            return Status == WorkStatus.WorkStopped;
        }

    }
}
