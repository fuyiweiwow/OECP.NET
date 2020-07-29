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

            FormTest form2 = new FormTest();
            form2.Show(dockPanelMain);
            form2.Dock = DockStyle.Left;
            form2.DockState = DockState.DockLeft;
            var canvas = new OECPCanvas();
            canvas.Dock = DockStyle.Fill;
            dockPanelMain.Controls.Add(canvas);
            form2.SendToBack();
            canvas.BringToFront();
            canvas.Init();

        }
    }
}
