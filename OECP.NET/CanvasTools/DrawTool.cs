using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OECP.NET.Model;

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

       


        public DrawTool(ShapeType type = ShapeType.Undefined) :base(CanvasToolType.DrawTool)
        {
            DrawShapeType = type;
        }

        public OECPLayer CurrentLayer()
        {
            if (_canvas == null)
                return null;

            var curLayer = _canvas.CurrentLayer();

            Pen = new Pen(curLayer.LayerColor);

            Brush = new SolidBrush(Pen.Color);

            return curLayer;
        }


        /// <summary>
        /// 绘制圆形
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        public void DrawShape(float x, float y, Graphics g, float width = 5)
        {
            RectangleF lt = new RectangleF(x - width / 2, y - width / 2, width, width);
            g.DrawEllipse(Pen, lt);
            g.FillEllipse(Brush, lt);
        }

        public static void DrawShape(float x, float y, Pen p, Brush b, Graphics g, float width = 5)
        {
            RectangleF lt = new RectangleF(x - width / 2, y - width / 2, width, width);
            g.DrawEllipse(p, lt);
            g.FillEllipse(b, lt);
        }


        /// <summary>
        /// 画线
        /// </summary>
        /// <param name="x1">点1x</param>
        /// <param name="y1">点1y</param>
        /// <param name="x2">点2x</param>
        /// <param name="y2">点2y</param>
        public void DrawShape(float x1, float y1, float x2, float y2, Graphics g)
        {
            g.DrawLine(Pen, x1, y1, x2, y2);
        }


        public static void DrawShape(float x1, float y1, float x2, float y2, Pen p, Graphics g)
        {
            g.DrawLine(p, x1, y1, x2, y2);
        }

        protected void DrawLocatePoint(float x, float y, Graphics g, float width = 10)
        {
            RectangleF lt = new RectangleF(x - width / 2, y - width / 2, width, width);
            g.DrawRectangle(new Pen(Color.LawnGreen), lt.X, lt.Y, lt.Width, lt.Height);
            g.FillRectangle(new SolidBrush(Color.LawnGreen), lt);
        }


    }
}
