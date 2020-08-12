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
        public OECPLineControlPanel(ICanvasSignal canvas, OECPLayer layer)
        {
            _canvas = canvas;
            _layer = layer;
            InitializeComponent();
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
    }
}
