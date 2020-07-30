using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OECP.Canvas
{
    class OECPCanvas : Panel
    {
        /// <summary>
        /// 缩放比例
        /// </summary>
        private int _scale;

        /// <summary>
        /// 绘画正方形
        /// </summary>
        private RectangleF _square;

        /// <summary>
        /// 网格线数目
        /// </summary>
        private int _gridNum = 0;

        /// <summary>
        /// 鼠标按下位置
        /// </summary>
        private Point _mouseDownLocation = Point.Empty;

        /// <summary>
        /// 正在移动的鼠标位置
        /// </summary>
        private Point _movingPoint = Point.Empty;


        /// <summary>
        /// 是否开始漫游
        /// </summary>
        private bool _panStart = false;


        public OECPCanvas()
        {
        }
        public void SetGridNum(int num)
        {
            this._gridNum = num;
        }

        public void Init()
        {
            this.DoubleBuffered = true;
            this.MouseWheel += OECPCanvas_MouseWheel;
            this.Paint += OECPCanvas_Paint;
            this.MouseDown += OECPCanvas_MouseDown;
            this.MouseMove += OECPCanvas_MouseMove;
            this.MouseUp += OECPCanvas_MouseUp;
            this.Resize += OECPCanvas_Resize;
            _square = InitSquare();
            this.BackColor = Color.White;
        }


        public void ReInitSquarePosition()
        {
            _square = InitSquare();
            Invalidate();
        }


        private void OECPCanvas_Resize(object sender, EventArgs e)
        {
            ReInitSquarePosition();
        }

        private void OECPCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_panStart)
            {
                _panStart = false;
            }
        }

        private void OECPCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_panStart)
            {
                _movingPoint = new Point(e.Location.X - _mouseDownLocation.X,
                    e.Location.Y - _mouseDownLocation.Y);
                _square.Location = _movingPoint;
                this.Invalidate();
            }
        }

        private void OECPCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                _mouseDownLocation = e.Location;
                _panStart = true;
            }
        }

        private void OECPCanvas_Paint(object sender, PaintEventArgs e)
        {
            ResetSquare(_square, e.Graphics);
        }

        private void ResetSquare(RectangleF rec, Graphics g)
        {
            g.Clear(Color.White);
            Pen p = new Pen(Brushes.Black, 1);
            g.DrawRectangle(p, rec.X, rec.Y, rec.Width, rec.Height); 
            DrawGridLine(g);
        }


        private RectangleF InitSquare()
        {
            var wx = this.Parent.Width;
            var ht = this.Parent.Height;
            var baseLength = wx > ht ? ht : wx;
            var initLength = (float)baseLength * 4 / 5;
            
            var centerPoint = new PointF((float)wx / 2, (float)ht / 2);
            var slide = initLength / 2;
            var basePoint = new PointF(centerPoint.X - slide,
                centerPoint.Y - slide);

            var ret = new RectangleF(basePoint, new SizeF(initLength, initLength)); 
            return ret;
        }


        private void DrawGridLine(Graphics g)
        {
            var sideLen = _square.Width;

            //横线
            float step = sideLen / _gridNum;

            Pen p = new Pen(Brushes.Gray, 1);

            var sqX = _square.Location.X;
            var sqY = _square.Location.Y;

            for (int i = 0; i < _gridNum; i++)
            {
                var dt = (i + 1) * step;

                g.DrawLine(p, sqX, sqY + dt, sqX + sideLen, sqY + dt);

                g.DrawLine(p, sqX + dt, sqY, sqX + dt, sqY + sideLen);
            }
        }


        private void OECPCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                _scale = 5;
            else
                _scale = -5;
            _square.Inflate(_scale, _scale);
            this.Invalidate();
        }


        


    }
}
