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
        private Rectangle _square;

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
            Init();
        }
        public void SetGridNum(int num)
        {
            this._gridNum = num;
        }

        private void Init()
        {
            this.DoubleBuffered = true;
            this.MouseWheel += OECPCanvas_MouseWheel;
            this.Paint += OECPCanvas_Paint;
            this.MouseDown += OECPCanvas_MouseDown;
            this.MouseMove += OECPCanvas_MouseMove;
            this.MouseUp += OECPCanvas_MouseUp;
            _square = InitSquare();
            this.BackColor = Color.White;
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

        private void ResetSquare(Rectangle rec, Graphics g)
        {
            g.Clear(Color.White);
            Pen p = new Pen(Brushes.Black, 1);
            g.DrawRectangle(p, rec);
            DrawGridLine(g);
        }


        private Rectangle InitSquare()
        {
            var baseLength = this.Width > this.Height ? Height : Width;
            var initLength = baseLength - 5;
            var lx = Width / 2 - initLength / 2;
            var basePoint = new Point(lx,
                lx);
            var ret = new Rectangle(basePoint, new Size(initLength, initLength));
            ret.Inflate(200,200);
            return ret;
        }


        private void DrawGridLine(Graphics g)
        {
            var sideLen = _square.Width;

            //横线
            float step = (float)sideLen / _gridNum;

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
