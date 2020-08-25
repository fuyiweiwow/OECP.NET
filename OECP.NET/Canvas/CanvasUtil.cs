using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OECP.NET.CanvasTools;
using OECP.NET.Model;

namespace OECP.NET.Canvas
{
    public class CanvasUtil
    {

        public static RectangleF GetPrimeSquare(int w,int h)
        {
            var baseLength = w > h ? h : w;
            var initLength = (float)baseLength * 4 / 5;

            var centerPoint = new PointF((float)w / 2, (float)h / 2);
            var slide = initLength / 2;
            var basePoint = new PointF(centerPoint.X - slide,
                centerPoint.Y - slide);

            var ret = new RectangleF(basePoint, new SizeF(initLength, initLength));
            return ret;
        }

        public static void DrawGridLine(Graphics g,bool gridVisible,RectangleF curSquare,int gridNum)
        {
            if (!gridVisible)
                return;

            var sideLen = curSquare.Width;
            //横线
            float step = sideLen / gridNum;

            Pen p = new Pen(Brushes.Gray, 1);

            var sqX = curSquare.Location.X;
            var sqY = curSquare.Location.Y;

            for (int i = 0; i < gridNum; i++)
            {
                var dt = (i + 1) * step;

                g.DrawLine(p, sqX, sqY + dt, sqX + sideLen, sqY + dt);

                g.DrawLine(p, sqX + dt, sqY, sqX + dt, sqY + sideLen);
            }
        }

        public static bool VertexOnLine(OECPVertex vtx,List<OECPLayer> lineLayers, ref OECPVertex projVtx)
        {
            foreach (OECPLayer lineLayer in lineLayers)
            {
                foreach (OECPElement ele in lineLayer.Elements)
                {
                    var line = (OECPLine) ele;

                    if (line.Distance(vtx) <= vtx.BufferTolerance)
                    {
                        projVtx = line.GetProjectVertex(vtx);
                        return true;
                    }
                }
            }
            return false;
        }



    }
}
