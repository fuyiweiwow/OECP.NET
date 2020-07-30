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
        public OECPLayerTree()
        {
            InitializeComponent();
            resizeImageList();
            FormBorderStyle = FormBorderStyle.None;
            layerTree.FullRowSelect = true;

            var root = new  TreeNode("画布图层",5,5);

            var nodeGrid = new TreeNode("网格",2,2);
            var nodeMountainLine = new TreeNode("山线",4,4);
            var nodeValleyLine = new TreeNode("谷线",3,3);
            var nodeAuxLine = new TreeNode("辅助线",1,1);
            var nodeVertex = new TreeNode("节点",0,0);

            root.Nodes.AddRange(new[]
            {
                nodeGrid, nodeMountainLine, nodeValleyLine, nodeAuxLine, nodeVertex
            });


            layerTree.Nodes.Add(root);

            layerTree.ExpandAll();
        }

        private void resizeImageList()
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


    }
}
