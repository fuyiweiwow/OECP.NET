using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.CanvasTools
{
    public class PanTool:CanvasTool
    {
        public PointF StartPoint { get; set; } = PointF.Empty;
        public PointF EndPoint { get; set; } = PointF.Empty;

        public PanTool() : base(CanvasToolType.PanTool)
        {


        }

    }
}
