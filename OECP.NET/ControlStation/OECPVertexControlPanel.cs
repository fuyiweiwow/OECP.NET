using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OECP.NET.Model;

namespace OECP.NET.ControlStation
{
    public partial class OECPVertexControlPanel : UserControl, ILayerControl
    {

        private ICanvasSignal _canvas;

        public OECPVertexControlPanel(ICanvasSignal canvas)
        {
            _canvas = canvas;
            InitializeComponent();
        }

        private void OECPVertexControlPanel_Resize(object sender, EventArgs e)
        {
            tsbAddVtx.Size = new Size((this.Width - 25) / 2, tsbAddVtx.Height);
            tsbDelVtx.Size = tsbAddVtx.Size;
            toolStrip1.Visible = false;
            toolStrip1.Visible = true;
        }

        private void tsbAddVtx_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            ToolStripButton tsb = (ToolStripButton)sender;
            if (tsb.Checked)
            {
                _canvas.StopDrawing();
            }
            else
            {
                _canvas.StartDrawing();
            }
        }

        private void tsbAddVtx_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;
            if (tsb.Checked)
            {
                tsbDelVtx.Checked = false;
                _canvas.DeleteMode(false);
            }
               
        }

        private void tsbDelVtx_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;
            if (tsb.Checked)
            {
                tsbAddVtx.Checked = false;
                _canvas.StopDrawing();
            }
        }

        private void tsbDelVtx_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            ToolStripButton tsb = (ToolStripButton)sender;
            _canvas.DeleteMode(!tsb.Checked);
        }
    }
}
