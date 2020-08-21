using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.CanvasTools
{
    public class DrawTool: CanvasTool
    {
        
        public enum ShapeType
        {
            Undefined = -1,
            Point = 0,//本质实际是其他东西,圆形或者方形
            Line = 1,
        }

        public ShapeType DrawShapeType { get; set; }

        public Pen Pen { get; set; }

        public Brush Brush { get; set; }

        public Graphics Graphics { get; set; }

        public DrawTool() :base(CanvasToolType.DrawTool)
        {

        }

      


        /// <summary>
        /// 绘制圆形
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        public void DrawShape(float x, float y, float width = 5)
        {
            RectangleF lt = new RectangleF(x - width / 2, y - width / 2, width, width);
            Graphics.DrawEllipse(Pen, lt);
            Graphics.FillEllipse(Brush, lt);
        }

        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="x1">点1x</param>
        /// <param name="y1">点1y</param>
        /// <param name="x2">点2x</param>
        /// <param name="y2">点2y</param>
        public void DrawShape(float x1, float y1, float x2, float y2)
        {
            Graphics.DrawLine(Pen, x1, y1, x2, y2);
        }




    }
}
