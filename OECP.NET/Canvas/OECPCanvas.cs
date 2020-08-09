using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using OECP.NET;
using OECP.NET.Model;

namespace OECP.Canvas
{
    class OECPCanvas : Panel, ICanvasSignal
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
        /// 最初大小的正方形
        /// </summary>
        private RectangleF _dSquare;

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
        private PointF _movingPoint = Point.Empty;

        /// <summary>
        /// 是否开始漫游
        /// </summary>
        private bool _panStart = false;

        /// <summary>
        /// 网格是否可见
        /// </summary>
        private bool _gridVisible = true;

        /// <summary>
        /// 节点是否可见
        /// </summary>
        private bool _vertexVisible = true;

        /// <summary>
        /// 是否在手绘状态
        /// </summary>
        private bool _allowPaint = false;

        /// <summary>
        /// 当前操作图层
        /// </summary>
        private OECPLayer _curLayer;

        /// <summary>
        /// 图层指针
        /// </summary>
        private OECPLayer _gridLayer;
        private OECPLayer _mLineLayer;
        private OECPLayer _vLineLayer;
        private OECPLayer _aLineLayer;
        private OECPLayer _vtxLayer;

        private enum DrawState
        {
            Drawing = 0,
            EndDraw = 1,
        }

        private DrawState _drawState = DrawState.EndDraw;

        public List<OECPLayer> Layers { get; set; }


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
            this.Layout += OECPCanvas_Layout;
            _square = InitSquare();
            _dSquare = _square;
            this.BackColor = Color.White;
            InitRightClickMenu();
        }

        public void RegisterLayerPtr(OECPLayer gird, OECPLayer ml, OECPLayer vl, OECPLayer vtx, OECPLayer aux)
        {
            _gridLayer = gird;
            _mLineLayer = ml;
            _vLineLayer = vl;
            _vtxLayer = vtx;
            _aLineLayer = aux;
        }



        private void InitRightClickMenu()
        {
            var menustrip = new ContextMenuStrip();
            ToolStripItem restoreSuqarsItem = new ToolStripButton("复位");
            restoreSuqarsItem.Click += RestoreSuqarsItem_Click;
            menustrip.Items.Add(restoreSuqarsItem);
            this.ContextMenuStrip = menustrip;
        }

        private void RestoreSuqarsItem_Click(object sender, EventArgs e)
        {
            ReInitSquarePosition();
        }

        private void OECPCanvas_Layout(object sender, LayoutEventArgs e)
        {
            ReInitSquarePosition();
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
                _movingPoint = new PointF(this.Left + e.Location.X - _mouseDownLocation.X,
                    this.Top + e.Location.Y - _mouseDownLocation.Y);
                _square.Location = _movingPoint;
                this.Invalidate();
            }
        }

        private void OECPCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDownLocation = e.Location;
            if (e.Button == MouseButtons.Middle)
            {
                _panStart = true;
            }

            if (_allowPaint && e.Button == MouseButtons.Left && _drawState == DrawState.EndDraw)
            {
                _drawState = DrawState.Drawing;
                //开始绘画
                switch (_curLayer.LayerType)
                {
                    case OECPLayer.Type.Line:
                        break;
                    case OECPLayer.Type.Vertex:
                        Invalidate();
                        break;
                    case OECPLayer.Type.Grid:
                        break;
                    default:
                        return;
                }
                
            }
        }

        private void OECPCanvas_Paint(object sender, PaintEventArgs e)
        {
            ResetSquare(_square, e.Graphics);
            //TODO：判断点是否在载体上(边界,网格,线上)
            if (_allowPaint && _drawState == DrawState.Drawing)
            {
                Pen p = new Pen(_curLayer.LayerColor);
                switch (_curLayer.LayerType)
                {
                    case OECPLayer.Type.Line:
                        break;
                    case OECPLayer.Type.Vertex:
                        Brush b = new SolidBrush(Color.Black);
                        DrawVertex(_mouseDownLocation.X,_mouseDownLocation.Y,p,b,e.Graphics);
                        _curLayer.Elements.Add(C2iVertex(_mouseDownLocation.X, _mouseDownLocation.Y));
                        _drawState = DrawState.EndDraw;
                        break;
                    case OECPLayer.Type.Grid:
                        break;
                    default:
                        return;
                }

            }
        }

        //获得初始正方形上对应的点
        private OECPVertex C2iVertex(float x,float y)
        {
            var w2dw1 = _square.Width / _dSquare.Width;
            var cUnitVector = new PointF(_square.Location.X - x, _square.Location.Y - y);
            float x0, y0;
            x0 = _dSquare.Location.X - cUnitVector.X / w2dw1;
            y0 = _dSquare.Location.Y - cUnitVector.Y / w2dw1;
            return new OECPVertex(x0, y0);
        }


        //将初始矩形的坐标投影到当前画布状态
        private OECPVertex I2CurVertex(float x, float y)
        {
            var w2dw1 = _square.Width / _dSquare.Width;
            var cUnitVector = new PointF(_dSquare.Location.X - x, _dSquare.Location.Y - y);
            float x0, y0;
            x0 = _square.Location.X - cUnitVector.X * w2dw1;
            y0 = _square.Location.Y - cUnitVector.Y * w2dw1;
            return new OECPVertex(x0, y0);
        }



        private void ResetSquare(RectangleF rec, Graphics g)
        {
            g.Clear(Color.White);
            Pen p = new Pen(Brushes.Black, 1);
            g.DrawRectangle(p, rec.X, rec.Y, rec.Width, rec.Height); 
            DrawGridLine(g);
            DrawVertex(g);
            //todo:绘制历史信息
            foreach (var ele in _vtxLayer.Elements)
            {
                var vtx = (OECPVertex) ele;
                if (vtx.IsCornerVertex)
                    continue;
                vtx = I2CurVertex(vtx.X, vtx.Y);
                DrawVertex(vtx.X,vtx.Y,p,new SolidBrush(Color.Black),g);
            }

        }

        private void DrawVertex(Graphics g)
        {
            if (!_vertexVisible)
                return;
            //画正方形四周的角点
            Pen p = new Pen(Brushes.Black, 1);
            Brush b = new SolidBrush(Color.Black);
            DrawCornerVertex(g);
        }

        private void DrawCornerVertex(Graphics g)
        {
            Pen p = new Pen(Brushes.Black, 1);
            Brush b = new SolidBrush(Color.Black);
            var textH = 5;
            RectangleF lt = new RectangleF((float)(_square.Left - textH/2), (float)(_square.Top - textH/2), textH, textH);

            DrawVertex(_square.Left, _square.Top, p, b, g);
            DrawVertex(_square.Right, _square.Top, p, b, g);
            DrawVertex(_square.Right, _square.Bottom, p, b, g);
            DrawVertex(_square.Left, _square.Bottom, p, b, g);
        }

        private void DrawVertex(float x,float y, Pen p ,Brush b, Graphics g , float width = 5)
        {
            RectangleF lt = new RectangleF(x - width/2, y - width/2, width, width);
            g.DrawEllipse(p, lt);
            g.FillEllipse(b, lt);
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
            if (!_gridVisible)
                return;

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
                _scale = 10;
            else
                _scale = -10;
            RectangleF rf = new RectangleF(_square.Location, _square.Size);
            if (e.Delta  < 0 && rf.Width <= 10)
                return;
            _square.Inflate(_scale, _scale);
            this.Invalidate();
        }


        public void UpdateGrid(int gridNum)
        {
            _gridNum = gridNum;
            Invalidate();
        }

        public void SetGridVisible(bool visible)
        {
            _gridVisible = visible;
            Invalidate();
        }

        public void SetVertexVisible(bool visible)
        {
            _vertexVisible = visible;
            Invalidate();
        }

        public void DrawVertex(OECPLayer layer)
        {
            _allowPaint = true;
            _curLayer = layer;
            if (_curLayer.Elements.Count == 0)
            {
                //无元素时添加 默认四个角,角点不参与手绘
                var v1 = new OECPVertex(_square.Left, _square.Top, true);
                var v2 = new OECPVertex(_square.Right, _square.Top, true);
                var v3 = new OECPVertex(_square.Right, _square.Bottom, true);
                var v4 = new OECPVertex(_square.Left, _square.Bottom, true);
                _curLayer.Elements.AddRange(new List<OECPElement>() {v1, v2, v3, v4});
            }

        }

        public void StopDrawing()
        {
            _allowPaint = false;
            _drawState = DrawState.EndDraw;
        }
    }
}
