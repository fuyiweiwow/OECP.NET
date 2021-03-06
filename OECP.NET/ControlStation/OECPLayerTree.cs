﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OECP.Canvas;
using OECP.NET.CanvasTools;
using OECP.NET.ControlStation.BaseControl;
using OECP.NET.Model;
using WeifenLuo.WinFormsUI.Docking;

namespace OECP.NET.ControlStation
{
    public partial class OECPLayerTree : DockContent
    {
        private OECPLayer _gridLayer;
        private OECPLayer _mLineLayer;
        private OECPLayer _vLineLayer;
        private OECPLayer _aLineLayer;
        private OECPLayer _vertexLayer;
        private OECPGridControlPanel _gridControl;
        private OECPVertexControlPanel _vertexControl;
        private OECPLineControlPanel _lControl;

        private List<UserControl> _controlList = new List<UserControl>();
        private ICanvasSignal _canvas;

        private DrawTool _vtxTool;
        private DrawTool _lineTool;

        public enum LayerBiz
        {
            Undefined = -1,
            Grid = 0,
            Vertex = 1,
            MLine = 2,
            VLine = 3,
            ALine = 4
        }

        public OECPLayerTree(ICanvasSignal canvas)
        {
            _canvas = canvas;
            InitLayers();
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ResizeImageList();
            InitTree();
            InitLayerControlPanel();
            AllLayerControlInvisible();
        }


        private void InitLayers()
        {
            _gridControl = new OECPGridControlPanel(_canvas) { Dock = DockStyle.Fill };
            _vertexControl = new OECPVertexControlPanel(_canvas) { Dock = DockStyle.Fill };
            _lControl = new OECPLineControlPanel(_canvas) { Dock = DockStyle.Fill };
            _controlList.AddRange(new List<UserControl>() { _gridControl, _vertexControl, _lControl });

            _gridLayer = new OECPLayer(Color.Gray, OECPLayer.Type.Grid);
           
            _mLineLayer = new OECPLayer(Color.Red,OECPLayer.Type.Line, OECPLayer.LineFcType.M);

            _vLineLayer = new OECPLayer(Color.Blue, OECPLayer.Type.Line, OECPLayer.LineFcType.V);

            _aLineLayer = new OECPLayer(Color.Gray,OECPLayer.Type.Line, OECPLayer.LineFcType.Aux);

            _vertexLayer = new OECPLayer(Color.Black, OECPLayer.Type.Vertex);
           
            var trueCanvas = (OECPCanvas) _canvas;
            trueCanvas.Layers = new List<OECPLayer>(){ _mLineLayer , _vLineLayer , _aLineLayer , _vertexLayer};
            trueCanvas.RegisterLayerPtr(_gridLayer, _mLineLayer, _vLineLayer, _vertexLayer, _aLineLayer);
        }

        private void InitLayerControlPanel()
        {
            foreach (UserControl control in _controlList)
                tabPageLayerControl.Controls.Add(control);
        }


        private void InitTree()
        {
            layerTree.FullRowSelect = true;

            InitNodes();

            layerTree.ExpandAll();
        }


        private void InitNodes()
        {
            var root = new TreeNode("画布图层", 5, 5) {Checked = true, Tag = (int) LayerBiz.Undefined};
            root.Nodes.AddRange(InitChildNodes());
            layerTree.Nodes.Add(root);
            ChildNodeActAsParent(root);
        }

        private TreeNode[] InitChildNodes()
        {
            var nodeGrid = new TreeNode("网格", 2, 2) { Tag = (int)LayerBiz.Grid };
            var nodeVertex = new TreeNode("节点", 0, 0) { Tag = (int)LayerBiz.Vertex };
            var nodeMountainLine = new TreeNode("山线", 4, 4) { Tag = (int)LayerBiz.MLine };
            var nodeAuxLine = new TreeNode("辅助线", 1, 1) { Tag = (int)LayerBiz.ALine };
            var nodeValleyLine = new TreeNode("谷线", 3, 3) { Tag = (int)LayerBiz.VLine };
            return new[] { nodeGrid, nodeMountainLine, nodeValleyLine, nodeAuxLine, nodeVertex };
        }


        private void ResizeImageList()
        {
            Point destPt = new Point(6, 0);
            Size size = new Size(22, 16);
            layerTree.ImageList = new ImageList {ImageSize = size};
            foreach (var imgKey in treeLayerImages.Images.Keys)
            {
                Bitmap bmp = new Bitmap(size.Width, size.Height);
                Graphics g = Graphics.FromImage(bmp);
                var img = treeLayerImages.Images[imgKey];
                g.DrawImage(img, destPt);
                g.Dispose();
                layerTree.ImageList.Images.Add(imgKey, (Image)bmp);
            }
        }

        private void layerTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            ChildNodeActAsParent(e.Node);
        }

        private void LayerProcess(TreeNode node, bool checkState,Action<bool,OECPLayer> action)
        {
            switch ((LayerBiz)node.Tag)
            {
                case LayerBiz.Grid:
                    action(checkState, _gridLayer);
                    break;
                case LayerBiz.ALine:
                    action(checkState, _aLineLayer);
                    break;
                case LayerBiz.MLine:
                    action(checkState, _mLineLayer);
                    break;
                case LayerBiz.VLine:
                    action(checkState, _vLineLayer);
                    break;
                case LayerBiz.Vertex:
                    action(checkState, _vertexLayer);
                    break;
                default:
                    return;
            }

        }

        private void LayerChangeEvent(bool checkState,OECPLayer layer)
        {
            AllLayerControlInvisible();
            if (layer.IsVisible)
            {
                _canvas.ChangeCurrentLayer(layer);
                if (layer.IsGrid)
                {
                    _gridControl.Visible = true;
                }
                else if (layer.IsLine)
                {
                    _lControl.Visible = true;
                    if (_lineTool == null)
                    {
                        _lineTool = new LineTool();
                    }
                    _canvas.SetCurrentDrawTool(_lineTool);
                }
                else
                {
                    _vertexControl.Visible = true;
                    if ((_vtxTool) == null)
                    {
                        _vtxTool = new VertexTool();
                    }
                    _canvas.SetCurrentDrawTool(_vtxTool);
                }
            }
                  
        }


        private void AllLayerControlInvisible()
        {
            foreach (UserControl control in _controlList)
                control.Visible = false;
        }


        private void LayerVisControl(bool visible, OECPLayer layer)
        {
            layer.IsVisible = visible;
           _canvas.SetLayerVisible(visible, layer);
        }


        private void layerTree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == Color.Gray)
                e.Cancel = true;
        }

        private void layerTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == Color.Gray)
                e.Cancel = true;
        }

        private void ChildNodeActAsParent(TreeNode node)
        {
            bool state = node.Checked;
            LayerProcess(node, state, LayerVisControl);
            if (!state)
            {
                AllLayerControlInvisible();
                layerTree.SelectedNode = null;
            }
            foreach (TreeNode child in node.Nodes)
            {
                LayerProcess(child, state, LayerVisControl);
                child.ForeColor = !state ? Color.Gray : DefaultForeColor;
                child.Checked = state;
            }
        }

        private void layerTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LayerProcess(e.Node,e.Node.Checked, LayerChangeEvent);
        }


        private bool IsLine(int tagInt)
        {
            return tagInt > 1;
        }

    }
}
