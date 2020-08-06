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
        private OECPLayer _layer;

        public OECPVertexControlPanel(ICanvasSignal canvas,ref OECPLayer layer)
        {
            _canvas = canvas;
            _layer = layer;
            InitializeComponent();
        }

        public void ControlLayerVisibility(bool visible)
        {
            _canvas.SetVertexVisible(visible);
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
                _canvas.DrawVertex(_layer);
            }
        }
    }
}
