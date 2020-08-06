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
    public partial class OECPGridControlPanel : UserControl, ILayerControl
    {
        
        private ICanvasSignal _canvas;
        private int _lastProperValue;
        private readonly int _maxGridNum = 256;

        public int MaxGridNum => _maxGridNum;
        private OECPLayer _layer;

        public OECPGridControlPanel(ICanvasSignal canvas,OECPLayer layer)
        {
            InitializeComponent();
            _canvas = canvas;
            _lastProperValue = (int)nudGridNum.Value;
            this.BorderStyle = BorderStyle.None;
            _layer = layer;
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
            var currentValue = Convert.ToInt32(nudGridNum.Value);
            if (currentValue > _maxGridNum)
                nudGridNum.Value = _lastProperValue;
            else
            {
                _canvas.UpdateGrid(currentValue, _layer);
                _lastProperValue = currentValue;
            }
               
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            nudGridNum.Value = 0;
            _canvas.UpdateGrid(0, _layer);
        }

        public void ControlLayerVisibility(bool visible)
        {
            _canvas.SetGridVisible(visible);
        }


        private void OECPGridControlPanel_Resize(object sender, EventArgs e)
        {
            lblGridNum.Left = this.Left + 5;
            lblGridNum.Width = this.Width * 2 / 5;

            nudGridNum.Left = lblGridNum.Right + 3;
            nudGridNum.Width = this.Width - 13 - lblGridNum.Width;

            btnM2.Left = this.Left + 5;
            btnM2.Width = (this.Width - 16) / 3;
            btnD2.Left = btnM2.Right + 3;
            btnD2.Width = btnM2.Width;
            btnReset.Left = btnD2.Right + 3;
            btnReset.Width = btnD2.Width;
        }
    }
}
