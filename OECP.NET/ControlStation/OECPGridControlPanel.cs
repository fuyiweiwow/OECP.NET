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
    public partial class OECPGridControlPanel : UserControl, ILayerControl
    {
        
        private ICanvasSignal _canvas;
        private int _lastProperValue;
        private readonly int _maxGridNum = 256;

        public int MaxGridNum => _maxGridNum;

        public OECPGridControlPanel(ICanvasSignal canvas)
        {
            InitializeComponent();
            _canvas = canvas;
            _lastProperValue = (int)nudGridNum.Value;
            this.BorderStyle = BorderStyle.None;
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
                _canvas.UpdateGrid(currentValue);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            nudGridNum.Value = 0;
            _canvas.UpdateGrid(0);
        }

        public void ControlLayerVisibility(bool visible)
        {
            _canvas.SetGridVisible(visible);
        }
    }
}
