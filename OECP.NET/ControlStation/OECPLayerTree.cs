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
using WeifenLuo.WinFormsUI.Docking;

namespace OECP.NET.ControlStation
{
    public partial class OECPLayerTree : DockContent
    {
        public enum LayerBiz
        {
            Grid = 0,
            Vertex = 1,
            Line = 2,
        }

        public OECPLayerTree()
        {
            InitializeComponent();
            ResizeImageList();
            FormBorderStyle = FormBorderStyle.None;
            InitTree();
        }

        private void InitTree()
        {
            layerTree.FullRowSelect = true;

            InitNodes();

            layerTree.ExpandAll();
        }


        private void InitNodes()
        {
            var root = new TreeNode("画布图层", 5, 5);

            root.Nodes.AddRange(InitChildNodes());

            layerTree.Nodes.Add(root);
        }

        private TreeNode[] InitChildNodes()
        {
            var nodeGrid = new TreeNode("网格", 2, 2) { Tag = (int)LayerBiz.Grid };
            var nodeVertex = new TreeNode("节点", 0, 0) { Tag = (int)LayerBiz.Vertex };
            var nodeMountainLine = new TreeNode("山线", 4, 4) { Tag = (int)LayerBiz.Line };
            var nodeAuxLine = new TreeNode("辅助线", 1, 1) { Tag = (int)LayerBiz.Line };
            var nodeValleyLine = new TreeNode("谷线", 3, 3) { Tag = (int)LayerBiz.Line };
            return new[] {nodeGrid, nodeMountainLine, nodeValleyLine, nodeAuxLine, nodeVertex};
        }


        private void ResizeImageList()
        {
            Point destPt = new Point(6, 0);
            Size size = new Size(22, 16);
            layerTree.ImageList = new ImageList();
            layerTree.ImageList.ImageSize = size;
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
            var node = e.Node;
            bool state = node.Checked;
            foreach (TreeNode child in node.Nodes)
            {
                child.ForeColor = !state ? Color.Gray : DefaultForeColor;
                child.Checked = state;
            }

        }

        private void layerTree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == Color.Gray)
                e.Cancel = true;  //不让选中禁用节点
        }

        private void layerTree_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == Color.Gray)
                e.Cancel = true;  //不让选中禁用节点
        }


    }
}
