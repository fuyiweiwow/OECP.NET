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
    public partial class OECPLineControlPanel : UserControl, ILayerControl
    {

        private ICanvasSignal _canvas;
        private OECPLayer _layer;
        public OECPLineControlPanel(ICanvasSignal canvas)
        {
            _canvas = canvas;
            InitializeComponent();
        }

        public void SetLayerUnderControl(OECPLayer layer)
        {
            _layer = layer;
        }


        public void ControlLayerVisibility(bool visible)
        {
            _canvas.SetLayerVisible(visible,_layer);
        }


        private void OECPLineControlPanel_Resize(object sender, EventArgs e)
        {
            tsbAddLine.Size = new Size((this.Width - 25) / 2, tsbAddLine.Height);
            tsbDelLine.Size = tsbAddLine.Size;
            toolStrip1.Visible = false;
            toolStrip1.Visible = true;
        }

        private void tsbAddLine_MouseDown(object sender, MouseEventArgs e)
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

        private void tsbDelLine_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            ToolStripButton tsb = (ToolStripButton)sender;
            _canvas.DeleteMode(!tsb.Checked);
        }

        private void tsbAddLine_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;
            if (tsb.Checked)
            {
                tsbDelLine.Checked = false;
                _canvas.DeleteMode(false);
            }
        }

        private void tsbDelLine_CheckStateChanged(object sender, EventArgs e)
        {
            ToolStripButton tsb = (ToolStripButton)sender;
            if (tsb.Checked)
            {
                tsbAddLine.Checked = false;
                _canvas.StopDrawing();
            }
        }
    }
}
