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
        private int _scale;

        private RectangleF _square;

        private RectangleF _dSquare;

        private int _gridNum = 0;

        private OECPLayer _curLayer;

        private OECPElement _lastHighLight;

        private OECPLayer _gridLayer;
        private OECPLayer _mLineLayer;
        private OECPLayer _vLineLayer;
        private OECPLayer _aLineLayer;
        private OECPLayer _vtxLayer;


        private PanTool _panTool;

        private DeleteTool _deleteTool;

        public List<OECPLayer> Layers { get; set; }

        private bool _cornerInit = false;

        private ContextMenuStrip _rclMenuStrip;
        private DrawTool _drawTool;


        public OECPCanvas()
        {
        }

        public void Init()
        {
            this.DoubleBuffered = true;
            CommonEventHandler();
            _square = CanvasUtil.GetPrimeSquare(this.Parent.Width, this.Parent.Height);
            _dSquare = _square;
            this.BackColor = Color.White;
            InitRightClickMenu();
            InitPanTool();

            _deleteTool = new DeleteTool();
            _deleteTool.StopWorking();
        }

        private void CommonEventHandler()
        {
            this.MouseWheel += OECPCanvas_MouseWheel;
            this.Paint += OECPCanvas_Paint;
            this.MouseDown += OECPCanvas_MouseDown;
            this.MouseMove += OECPCanvas_MouseMove;
            this.MouseUp += OECPCanvas_MouseUp;
        }

        private void InitPanTool()
        {
            _panTool = new PanTool();
            _panTool.SetCanvas(this);
            _panTool.SetWaiting();
            this.MouseMove += _panTool.CanvasTool_MouseMove;
            this.MouseDown += _panTool.CanvasTool_MouseDown;
            this.MouseUp += _panTool.CanvasTool_MouseUp;
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
            _rclMenuStrip = new ContextMenuStrip();
            ToolStripItem restoreSuqareItem = new ToolStripButton("复位");
            restoreSuqareItem.Click += RestoreSquareItem_Click;
            _rclMenuStrip.Items.Add(restoreSuqareItem);
            this.ContextMenuStrip = _rclMenuStrip;
        }

        private void RestoreSquareItem_Click(object sender, EventArgs e)
        {
            ReInitSquarePosition();
        }


        public void ReInitSquarePosition()
        {
            _square = CanvasUtil.GetPrimeSquare(this.Parent.Width, this.Parent.Height);
            Invalidate();
        }


        private void OECPCanvas_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void OECPCanvas_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OECPCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (this.ContextMenuStrip == null)
                    this.ContextMenuStrip = _rclMenuStrip;
            }
        }

        private void OECPCanvas_Paint(object sender, PaintEventArgs e)
        {
            ResetSquare(_square, e.Graphics);
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

        public OECPLayer VertexLayer()
        {
            return _vtxLayer;
        }

        public OECPLayer CurrentLayer()
        {
            return _curLayer;
        }

        public void RepaintCanvas()
        {
            this.Invalidate();
        }

        public void FreezeRightClickMenu(bool frozen)
        {
            if (frozen)
                this.ContextMenuStrip = null;
        }


        public RectangleF GetPrimeSquare()
        {
            return _dSquare;
        }

        public void SetCurrentSquareLocation(PointF ptf)
        {
            _square.Location = ptf;
        }

        public bool VertexOnLine(OECPVertex vtx, ref OECPVertex projVtx)
        {
            var lst = new List<OECPLayer>();
            foreach (OECPLayer layer in Layers)
            {
                if(!layer.IsLine)
                    continue;
                if(!layer.IsVisible)
                    continue;
                lst.Add(layer);
            }
            return CanvasUtil.VertexOnLine(vtx, lst, ref projVtx);
        }

        public void SetCurrentDrawTool(DrawTool dtool)
        {
            if (_drawTool != null)
            {
                MouseMove -= _drawTool.CanvasTool_MouseMove;
                MouseDown -= _drawTool.CanvasTool_MouseDown;
                Paint -= _drawTool.CanvasTool_Paint;
            }
            _drawTool = dtool;
            _drawTool.SetCanvas(this);
            MouseMove += _drawTool.CanvasTool_MouseMove;
            MouseDown += _drawTool.CanvasTool_MouseDown;
            Paint += _drawTool.CanvasTool_Paint;
        }

        public int CanvasLeft()
        {
            return Left;
        }

        public int CanvasTop()
        {
            return Top;
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
            g.SmoothingMode = SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            Pen p = new Pen(Brushes.Black, 1);
            g.DrawRectangle(p, rec.X, rec.Y, rec.Width, rec.Height);
            CanvasUtil.DrawGridLine(g, _gridLayer.IsVisible, _square, _gridNum);
            if (!_cornerInit)
                InitCornerVertex();

            foreach (OECPLayer layer in Layers)
                RePaintLayerElements(layer,g);
        }


        public  void RePaintLayerElements(OECPLayer layer, Graphics g)
        {
            if (!layer.IsVisible)
                return;

            Pen p = new Pen(Color.Black);
            if (layer.IsLine)
            {
                foreach (var ele in layer.LayerElements())
                    DrawLineElements(ele, p, layer, g);
            }
            else
            {
                foreach (var ele in layer.LayerElements())
                    DrawVertexElement(ele, p, layer, g);
            }
        }


        private void DrawLineElements(OECPElement ele,Pen p,OECPLayer layer,Graphics g)
        {
            var line = (OECPLine)ele;
            var st = I2C(line.StartVertex);
            var ed = I2C(line.EndVertex);

            p.Color = line.IsHighLight ? line.HighLightColor : layer.LayerColor;
            DrawTool.DrawShape(st.X, st.Y, ed.X, ed.Y, p, g);
        }

        private void DrawVertexElement(OECPElement ele, Pen p, OECPLayer layer, Graphics g)
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
            var b = new SolidBrush(p.Color);
            DrawTool.DrawShape(t.X, t.Y, p, b, g);
        }

        private void InitCornerVertex()
        {
            _cornerInit = true;
            var tl = C2I(new OECPVertex(_square.Left, _square.Top,true));
            var tr = C2I(new OECPVertex(_square.Right, _square.Top, true));
            var rb = C2I(new OECPVertex(_square.Right, _square.Bottom, true));
            var lb = C2I(new OECPVertex(_square.Left, _square.Bottom, true));
            _vtxLayer.AddElements(new List<OECPElement>(){tl,tr,rb,lb});
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

        OECPVertex ICanvasSignal.I2C(OECPVertex iVtx)
        {
            return I2C(iVtx);
        }

        OECPVertex ICanvasSignal.C2I(OECPVertex cVtx)
        {
            return C2I(cVtx);
        }
    }
}
