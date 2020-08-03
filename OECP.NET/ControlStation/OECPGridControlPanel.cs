using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OECP.NET.ControlStation
{
    public partial class OECPGridControlPanel : UserControl
    {
        private ICanvasSignal _canvas;
        public OECPGridControlPanel(ICanvasSignal canvas)
        {
            InitializeComponent();
            _canvas = canvas;
        }

        private void btnM2_Click(object sender, EventArgs e)
        {
            nudGridNum.Value = Convert.ToInt32(nudGridNum.Value * 2);
        }

        private void btnD2_Click(object sender, EventArgs e)
        {
            nudGridNum.Value = Convert.ToInt32(nudGridNum.Value / 2);
        }

        private void nudGridNum_ValueChanged(object sender, EventArgs e)
        {
            _canvas.UpdateGrid(Convert.ToInt32(nudGridNum.Value));
        }
    }
}
