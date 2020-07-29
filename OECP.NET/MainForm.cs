using System.Windows.Forms;
using CCWin;
using OECP.Canvas;

namespace OECP.NET
{
    public partial class MainForm : CCSkinMain
    {
        public MainForm()
        {
            InitializeComponent();

            var canvas = new OECPCanvas();
            canvas.Dock = DockStyle.Fill;
            this.mainContainer.Panel2.Controls.Add(canvas);


        }
    }
}
