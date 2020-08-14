using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Messaging;
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
        /// 漫游开始位置
        /// </summary>
        private PointF _panStartLocation = Point.Empty;

        /// <summary>
        /// 漫游结束位置
        /// </summary>
        private PointF _panEndLocation = Point.Empty;


        /// <summary>
        /// 上次鼠标点击位置
        /// </summary>
        private PointF _lastMouseDownLocation = Point.Empty;

        /// <summary>
        /// 是否开始漫游
        /// </summary>
        private bool _panStart = false;

        /// <summary>
        /// 是否在手绘状态
        /// </summary>
        private bool _allowPaint = false;

        /// <summary>
        /// 是否允许删除
        /// </summary>
        private bool _allowDelete = false;

        /// <summary>
        /// 是否正在做删除操作
        /// </summary>
        private bool _onDelete = false;

        /// <summary>
        /// 当前操作图层
        /// </summary>
        private OECPLayer _curLayer;

        /// <summary>
        /// 上一个高亮图形
        /// </summary>
        private OECPElement _lastHighLight;

        /// <summary>
        /// 图层指针
        /// </summary>
        private OECPLayer _gridLayer;
        private OECPLayer _mLineLayer;
        private OECPLayer _vLineLayer;
        private OECPLayer _aLineLayer;
        private OECPLayer _vtxLayer;

        private PointF _initCanvasLocation;

        private Size _initCanvasSize;


        private enum DrawState
        {
            Drawing = 0,
            EndDraw = 1,
        }

        private DrawState _drawState = DrawState.EndDraw;

        public List<OECPLayer> Layers { get; set; }

        private bool _cornerInit = false;


        public OECPCanvas()
        {
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
            _initCanvasLocation = this.Parent.Location;
            _initCanvasSize = this.Parent.Size;
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
            ToolStripItem restoreSuqareItem = new ToolStripButton("复位");
            restoreSuqareItem.Click += RestoreSquareItem_Click;
            menustrip.Items.Add(restoreSuqareItem);
            this.ContextMenuStrip = menustrip;
        }

        private void RestoreSquareItem_Click(object sender, EventArgs e)
        {
            ReInitSquarePosition();
        }

        private void OECPCanvas_Layout(object sender, LayoutEventArgs e)
        {
           
        }

        public void ReInitSquarePosition()
        {
            _square = InitSquare();
            Invalidate();
        }

        private void ResizeCurrentSquare()
        {
            var wFactor = _initCanvasSize.Width / this.Parent.Width;
            var hFactor = _initCanvasSize.Height / this.Parent.Height;
            var sFactor = wFactor > hFactor ? hFactor : wFactor;
            var length = _square.Width / sFactor;
            var x0 = (this.Parent.Location.X - _initCanvasLocation.X) / wFactor * _square.Location.X;
            var y0 = (this.Parent.Location.Y - _initCanvasLocation.Y) / wFactor * _square.Location.Y;

            _square = new RectangleF(x0, y0, length, length);
            Invalidate();
        }



        private void OECPCanvas_Resize(object sender, EventArgs e)
        {
          
        }

        private void OECPCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_panStart)
            {
                _panStart = false;
            }

            if (_allowDelete)
            {
                _onDelete = false;
            }
        }

        private void OECPCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_allowDelete)
                _onDelete = false;
            if (_panStart)
            {
                _panEndLocation = new PointF(this.Left + e.Location.X - _panStartLocation.X,
                    this.Top + e.Location.Y - _panStartLocation.Y);
                _square.Location = _panEndLocation;
                this.Invalidate();
            }
            else
            {
                if (_curLayer == null)
                    return;
                OECPVertex ivtx  = new OECPVertex(e.Location.X,e.Location.Y);
                var t = C2InitVertex(ivtx);
                _lastHighLight = _curLayer.SearchForHighLight(t.X, t.Y,_dSquare.Width/_square.Width);
                this.Invalidate();
            }
        }

        private void OECPCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            _lastMouseDownLocation = e.Location;
            _panStartLocation = new PointF(e.Location.X - _panEndLocation.X, e.Location.Y - _panEndLocation.Y);
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
                    default:
                        return;
                }

            }

            if (_allowDelete && e.Button == MouseButtons.Left)
            {
                _onDelete = true;
                Invalidate();
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
                        OECPVertex vtx = new OECPVertex(_lastMouseDownLocation.X, _lastMouseDownLocation.Y);
                        DrawVertex(vtx.X, vtx.Y, p, b, e.Graphics);
                        var t =  C2InitVertex(vtx);
                        _curLayer.Elements.Add(t);
                        _drawState = DrawState.EndDraw;
                        break;
                    default:
                        return;
                }
            }

            if (_allowDelete && _onDelete) 
            {
                switch (_curLayer.LayerType)
                {
                    case OECPLayer.Type.Line:
                        break;
                    case OECPLayer.Type.Vertex:
                        if (!_lastHighLight.IsEmpty)
                        {
                            var selVtx = (OECPVertex)_lastHighLight;
                            if (selVtx.IsCornerVertex)
                                return;
                            _curLayer.DeleteVertex(selVtx);
                        }
                       
                        break;
                    default:
                        return;
                }
            }

        }

        //获得初始正方形上对应的点
        private OECPVertex C2InitVertex(OECPVertex vtx)
        {
            var tVtx = (OECPVertex)vtx.Clone();
            var w2dw1 = _square.Width / _dSquare.Width;
            var cUnitVector = new PointF(_square.Location.X - vtx.X, _square.Location.Y - vtx.Y);
            var x0 = _dSquare.Location.X - cUnitVector.X / w2dw1;
            var y0 = _dSquare.Location.Y - cUnitVector.Y / w2dw1;
            tVtx.X = x0;
            tVtx.Y = y0;
            return tVtx;
        }


        //将初始矩形的坐标投影到当前画布状态
        private OECPVertex I2CurVertex(OECPVertex vtx)
        {
            var t = (OECPVertex)vtx.Clone(); ;
            var w2dw1 = _square.Width / _dSquare.Width;
            var cUnitVector = new PointF(_dSquare.Location.X - vtx.X, _dSquare.Location.Y - vtx.Y);
            var x0 = _square.Location.X - cUnitVector.X * w2dw1;
            var y0 = _square.Location.Y - cUnitVector.Y * w2dw1;
            t.X = x0;
            t.Y = y0;
            return t;
        }



        private void ResetSquare(RectangleF rec, Graphics g)
        {
            g.Clear(Color.White);
            Pen p = new Pen(Brushes.Black, 1);
            g.DrawRectangle(p, rec.X, rec.Y, rec.Width, rec.Height);
            DrawGridLine(g);
            if (!_cornerInit)
                InitCornerVertex();
            if (_vtxLayer.IsVisible)
            {
                foreach (var ele in _vtxLayer.Elements)
                {
                    var vtx = (OECPVertex)ele;
                    var t = I2CurVertex(vtx);
                    if (t.IsHighLight && _vtxLayer == _curLayer) 
                    {
                        p.Width = 5;
                        p.Color = t.HighLightColor;
                    }
                    else
                    {
                        p.Color = t.ElementColor;
                        p.Width = 1;
                    }

                    DrawVertex(t.X, t.Y, p, new SolidBrush(p.Color), g);
                }
            }

        }

        private void InitCornerVertex()
        {
            _cornerInit = true;
            var tl = C2InitVertex(new OECPVertex(_square.Left, _square.Top,true));
            var tr = C2InitVertex(new OECPVertex(_square.Right, _square.Top, true));
            var rb = C2InitVertex(new OECPVertex(_square.Right, _square.Bottom, true));
            var lb = C2InitVertex(new OECPVertex(_square.Left, _square.Bottom, true));
            _vtxLayer.Elements.AddRange(new List<OECPElement>(){tl,tr,rb,lb});
        }

        private void DrawVertex(float x, float y, Pen p, Brush b, Graphics g, float width = 5)
        {
            RectangleF lt = new RectangleF(x - width / 2, y - width / 2, width, width);
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
            if (!_gridLayer.IsVisible)
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
            if (e.Delta < 0 && rf.Width <= 10)
                return;
            _square.Inflate(_scale, _scale);
            this.Invalidate();
        }


        public void UpdateGrid(int gridNum)
        {
            _gridNum = gridNum;
            Invalidate();
        }

        public void SetLayerVisible(bool visible, OECPLayer layer)
        {
            layer.IsVisible = visible;
            Invalidate();
        }


        public void StartDrawing()
        {
            _allowPaint = true;
        }

        public void StopDrawing()
        {
            _allowPaint = false;
            _drawState = DrawState.EndDraw;
        }

        public void DeleteMode(bool onDelete)
        {
            _allowDelete = onDelete;
        }

        public void ChangeCurrentLayer(OECPLayer layer)
        {
            _curLayer = layer;
        }
    }
}
