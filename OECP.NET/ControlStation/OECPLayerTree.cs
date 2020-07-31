using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        public enum LayerBiz
        {
            Undefined = -1,
            Grid = 0,
            Vertex = 1,
            MLine = 2,
            VLine = 3,
            ALine = 4
        }

        public OECPLayerTree()
        {
            InitializeComponent();
            ResizeImageList();
            FormBorderStyle = FormBorderStyle.None;
            InitTree();
            InitLayers();
        }

        private void InitLayers()
        {
            _gridLayer = new OECPLayer(OECPLayer.Type.Grid);
            _mLineLayer = new OECPLayer(OECPLayer.Type.Line, OECPLayer.LineFcType.M);
            _vLineLayer = new OECPLayer(OECPLayer.Type.Line, OECPLayer.LineFcType.V);
            _aLineLayer = new OECPLayer(OECPLayer.Type.Line, OECPLayer.LineFcType.Aux);
            _vertexLayer = new OECPLayer(OECPLayer.Type.Vertex);
        }


        private void InitTree()
        {
            layerTree.FullRowSelect = true;

            InitNodes();

            layerTree.ExpandAll();
        }


        private void InitNodes()
        {
            var root = new TreeNode("画布图层", 5, 5) { Checked = true };
            root.Nodes.AddRange(InitChildNodes());
            layerTree.Nodes.Add(root);
            ChildNodeActAsParent(root);
        }

        private TreeNode[] InitChildNodes()
        {
            var nodeGrid = new TreeNode("网格", 2, 2) { Tag = (int)LayerBiz.Grid };
            var nodeVertex = new TreeNode("节点", 0, 0) { Tag = (int)LayerBiz.Vertex };
            var nodeMountainLine = new TreeNode("山线", 4, 4) { Tag = (int)LayerBiz.MLine };
            var nodeAuxLine = new TreeNode("辅助线", 1, 1) { Tag = (int)LayerBiz.VLine };
            var nodeValleyLine = new TreeNode("谷线", 3, 3) { Tag = (int)LayerBiz.ALine };
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
            }

        }

        private void LayerVisControl(bool visible, OECPLayer layer)
        {
            layer.IsVisible = visible;
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
            if (node.Nodes.Count == 0)
                return;
            bool state = node.Checked;
            foreach (TreeNode child in node.Nodes)
            {
                child.ForeColor = !state ? Color.Gray : DefaultForeColor;
                child.Checked = state;
            }
        }

        private void layerTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //todo:选择后加载相应类型的工作台
        }


        private bool IsLine(int tagInt)
        {
            return tagInt > 1;
        }

    }
}
