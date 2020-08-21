using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using OECP.NET;
using OECP.NET.Canvas;
using OECP.NET.CanvasTools;
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
        /// 上次鼠标点击位置
        /// </summary>
        private PointF _lastMouseDownLocation = Point.Empty;

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

        private List<OECPVertex> _lineOnDraw = new List<OECPVertex>(2);

        private DrawTool _drawTool;

        private PanTool _panTool;

        private DeleteTool _deleteTool;

        public List<OECPLayer> Layers { get; set; }

        private bool _cornerInit = false;

        private PointF _movingPoint= PointF.Empty;


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
            _square = CanvasUtil.GetPrimeSquare(this.Parent.Width, this.Parent.Height);
            _dSquare = _square;
            this.BackColor = Color.White;
            InitRightClickMenu();
            _drawTool = new DrawTool();
            _drawTool.StopWorking();
            _panTool = new PanTool();
            _panTool.StopWorking();
            _deleteTool = new DeleteTool();
            _deleteTool.StopWorking();
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
            _square = CanvasUtil.GetPrimeSquare(this.Parent.Width, this.Parent.Height);
            Invalidate();
        }


        private void OECPCanvas_Resize(object sender, EventArgs e)
        {
            
        }

        private void OECPCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_panTool.IsWorking())
                _panTool.StopWorking();
            if (_deleteTool.IsWorking())
                _deleteTool.SetWaiting();
        }

        private void OECPCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_drawTool.IsWorking())
            {
                if (_lineOnDraw.Count == 1)
                {
                    _movingPoint = e.Location;
                    Invalidate();
                }
              
            }
            if (_panTool.IsWorking())
            {
                _panTool.EndPoint = new PointF(this.Left + e.Location.X - _panTool.StartPoint.X,
                    this.Top + e.Location.Y - _panTool.StartPoint.Y);
                _square.Location = _panTool.EndPoint;
                this.Invalidate();
            }
            if(_deleteTool.IsWaiting())
            {
                if (_curLayer == null)
                    return;
                OECPVertex ivtx = new OECPVertex(e.Location.X, e.Location.Y);
                var t = C2I(ivtx);
                _lastHighLight = _curLayer.SearchForHighLight(t.X, t.Y, _dSquare.Width / _square.Width);
                if(!_lastHighLight.IsEmpty)
                    _deleteTool.SetBusy();
                this.Invalidate();
            }
        }

        private void OECPCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            _lastMouseDownLocation = e.Location;

            MouseDownSubProcess(e);
        }

        private void MouseDownSubProcess(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Middle:
                    _panTool.SetBusy();
                    _panTool.StartPoint = new PointF(e.Location.X - _panTool.EndPoint.X, e.Location.Y - _panTool.EndPoint.Y);
                    break;
                case MouseButtons.Left:
                {
                    if (_drawTool.IsWaiting())
                    {
                        _drawTool.SetBusy();
                        this.ContextMenuStrip.Visible = false;
                        if (_curLayer.IsLine)
                        {
                            OECPVertex lvtx = new OECPVertex(_lastMouseDownLocation.X, _lastMouseDownLocation.Y);
                            _lineOnDraw.Add(C2I(lvtx));
                        }
                        Invalidate();
                    }
                    if (_deleteTool.IsWaiting())
                    {
                        _deleteTool.SetBusy();
                        Invalidate();
                    }
                    if (e.Button == MouseButtons.Right && _lineOnDraw.Count < 2 && _drawTool.IsWorking())
                    {
                        var tsVtx = C2I(_lineOnDraw[0]);
                        _vtxLayer.DeleteVertex(tsVtx);
                        _lineOnDraw.Clear();
                    }

                    break;
                }
            }
        }

        private void OECPCanvas_Paint(object sender, PaintEventArgs e)
        {
            ResetSquare(_square, e.Graphics);
            //TODO：判断点是否在载体上(边界,网格,线上)
            PaintWork(e.Graphics);
            DeleteWork();
        }

        private void PaintWork(Graphics g)
        {
            if (!_drawTool.IsWorking()) return;
            Pen p = new Pen(_curLayer.LayerColor);
            Brush b = new SolidBrush(Color.Black);
            _drawTool.Pen = p;
            _drawTool.Brush = b;
            _drawTool.Graphics = g;
            ImmediatePaint(g);
        }


        private void ImmediatePaint(Graphics g)
        {
            if (_curLayer.IsLine)
                ImmediatePaintLine(g);
            else
                ImmediatePaintVertex();
        }

        private void ImmediatePaintLine(Graphics g)
        {
            if (_lineOnDraw.Count == 1)
            {
                var tp = I2C(_lineOnDraw[0]);
                _drawTool.DrawShape(tp.X, tp.Y);
                _drawTool.Pen = new Pen(Color.Red);
                _drawTool.DrawShape(tp.X, tp.Y, _movingPoint.X, _movingPoint.Y);
            }
            if (_lineOnDraw.Count == 2)
            {
                var st = C2I(_lineOnDraw[0]);
                _vtxLayer.Elements.Add(st);
                var et = C2I(_lineOnDraw[1]);
                _drawTool.DrawShape(et.X, et.Y);
                _vtxLayer.Elements.Add(et);
                var line = new OECPLine(st, et);
                _curLayer.Elements.Add(line);
                _drawTool.DrawShape(_lineOnDraw[0].X, _lineOnDraw[0].Y, _lineOnDraw[1].X, _lineOnDraw[1].Y);
                _lineOnDraw.Clear();
                this.ContextMenuStrip.Visible = true;
                _drawTool.SetWaiting();
                ResetSquare(_square, g);
            }

        }

        private void ImmediatePaintVertex()
        {
            OECPVertex vtx = new OECPVertex(_lastMouseDownLocation.X, _lastMouseDownLocation.Y);
            _drawTool.DrawShape(vtx.X, vtx.Y);
            var t = C2I(vtx);
            _curLayer.Elements.Add(t);
            _drawTool.SetWaiting();
        }


        private void DeleteWork()
        {
            if (!_deleteTool.IsWorking())
                return;
            if (_curLayer.IsLine)
            {

            }
            else
            {
                if(_lastHighLight == null) return;
                if (_lastHighLight.IsEmpty) return;
                var selVtx = (OECPVertex)_lastHighLight;
                if (selVtx.IsCornerVertex)
                    return;
                _curLayer.DeleteVertex(selVtx);
            }

        }

        //获得初始正方形上对应的点
        private OECPVertex C2I(OECPVertex vtx)
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
        private OECPVertex I2C(OECPVertex vtx)
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
            CanvasUtil.DrawGridLine(g, _gridLayer.IsVisible, _square, _gridNum);
            if (!_cornerInit)
                InitCornerVertex();

            _drawTool.Graphics = g;
            
            foreach (OECPLayer layer in Layers)
                RePaintLayerElements(layer);
        }


        public  void RePaintLayerElements(OECPLayer layer)
        {
            if (!layer.IsVisible)
                return;

            Pen p = new Pen(Color.Black);
            if (layer.IsLine)
            {
                foreach (var ele in layer.Elements)
                    DrawLineElements(ele, p, layer);
            }
            else
            {
                foreach (var ele in layer.Elements)
                    DrawVertexElement(ele, p, layer);
            }
        }


        private void DrawLineElements(OECPElement ele,Pen p,OECPLayer layer)
        {
            var line = (OECPLine)ele;
            var st = I2C(line.StartVertex);
            var ed = I2C(line.EndVertex);

            p.Color = line.IsHighLight ? line.HighLightColor : layer.LayerColor;
            _drawTool.Pen = p;
            _drawTool.Brush?.Dispose();
            _drawTool.Brush = new SolidBrush(p.Color);
            _drawTool.DrawShape(st.X, st.Y, ed.X, ed.Y);
        }

        private void DrawVertexElement(OECPElement ele, Pen p, OECPLayer layer)
        {
            var vtx = (OECPVertex)ele;
            var t = I2C(vtx);

            if (t.IsHighLight)
            {
                p.Width = 5;
                p.Color = t.HighLightColor;
            }
            else
            {
                p.Color = t.ElementColor;
                p.Width = 1;
            }
            _drawTool.Pen = p;
            _drawTool.Brush?.Dispose();
            _drawTool.Brush = new SolidBrush(p.Color);
            _drawTool.DrawShape(t.X, t.Y);
        }

        private void InitCornerVertex()
        {
            _cornerInit = true;
            var tl = C2I(new OECPVertex(_square.Left, _square.Top,true));
            var tr = C2I(new OECPVertex(_square.Right, _square.Top, true));
            var rb = C2I(new OECPVertex(_square.Right, _square.Bottom, true));
            var lb = C2I(new OECPVertex(_square.Left, _square.Bottom, true));
            _vtxLayer.Elements.AddRange(new List<OECPElement>(){tl,tr,rb,lb});
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
            _drawTool.SetWaiting(); 
        }

        public void StopDrawing()
        {
            _drawTool.StopWorking();
        }

        public void DeleteMode(bool onDelete)
        {
            if(onDelete)
                _deleteTool.SetWaiting();
            else
                _deleteTool.StopWorking();
        }

        public void ChangeCurrentLayer(OECPLayer layer)
        {
            _curLayer = layer;
        }
    }
}
