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
    public partial class OECPVertexControlPanel : UserControl, ILayerControl
    {

        private ICanvasSignal _canvas;

        public OECPVertexControlPanel(ICanvasSignal canvas)
        {
            _canvas = canvas;
            InitializeComponent();
            this.rdBtnSpare.Appearance = Appearance.Button;
            rdBtnSpare.Width = 40;
            this.rdBtnAddVtx.Appearance = Appearance.Button;
            rdBtnAddVtx.Width = 40;
            this.rdBtnDelVtx.Appearance = Appearance.Button;
            rdBtnDelVtx.Width = 40;
        }

        public void ControlLayerVisibility(bool visible)
        {
            _canvas.SetVertexVisible(visible);
        }

        private void OECPVertexControlPanel_Resize(object sender, EventArgs e)
        {
            rdBtnSpare.Left = this.Left + 10;
            rdBtnSpare.Width = (this.Width - 40) / 3;
            rdBtnAddVtx.Left = rdBtnSpare.Right + 10;
            rdBtnAddVtx.Width = rdBtnSpare.Width;
            rdBtnDelVtx.Left = rdBtnAddVtx.Right + 10;
            rdBtnDelVtx.Width = rdBtnAddVtx.Width;
        }
    }
}
