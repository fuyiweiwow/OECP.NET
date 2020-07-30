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
            FormBorderStyle = FormBorderStyle.None;

            var root = new  TreeNode("画布图层");
            root.Nodes.Add(new TreeNode("test"));
            layerTree.Nodes.Add(root);
        }
    }
}
