using System.Windows.Forms;
using CCWin;
using OECP.Canvas;
using OECP.NET.ControlStation;
using WeifenLuo.WinFormsUI.Docking;

namespace OECP.NET
{
    public partial class MainForm : CCSkinMain
    {
        public MainForm()
        {
            InitializeComponent();


            Panel canvasPanel = new Panel { Dock = DockStyle.Fill };
            var canvas = new OECPCanvas { Dock = DockStyle.Fill };
            dockPanelMain.Controls.Add(canvasPanel);

            OECPLayerTree layerTree = new OECPLayerTree(canvas);
            layerTree.Show(dockPanelMain);
            layerTree.Dock = DockStyle.Left;
            layerTree.DockState = DockState.DockLeft;
            layerTree.SendToBack();

            canvasPanel.Controls.Add(canvas);
            canvasPanel.BringToFront();
            canvas.Init();

        }

    }
}
