using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PushButtonCells;
using TenTec.Windows.iGridLib;

namespace OECP.NET.ControlStation
{
    public partial class OECPTabledGridControlPanel : UserControl, ILayerControl
    {

        private ICanvasSignal _canvas;
        private int _lastProperValue;
        private readonly int _maxGridNum = 256;
        private iGButtonColumnManager _bcm = new iGButtonColumnManager();

        public int MaxGridNum => _maxGridNum;

        public OECPTabledGridControlPanel(ICanvasSignal canvas)
        {
            InitializeComponent();
            _canvas = canvas;
            _lastProperValue = 0;
            this.BorderStyle = BorderStyle.None;
            InitTabledControls();
        }


        private void InitTabledControls()
        {
            this.Controls.Add(iGrid1);
            iGrid1.Header.Visible = false;
            iGrid1.AutoResizeCols = true;

            iGrid1.Dock = DockStyle.Fill;
            iGrid1.Cols.Count = 3;
            iGrid1.Rows.Count = 2;
            iGrid1.Rows[0].Height = 30;
            iGrid1.Rows[1].Height = 30;
            var cell = iGrid1.Rows[0].Cells[1];
            cell.SpanCols = 2;

            iGrid1.Rows[1].Tag = "Hide cell button";

            iGrid1.Cols[0].Tag = iGButtonColumnManager.BUTTON_COLUMN_TAG;

            _bcm.Attach(iGrid1);
            iGrid1.DefaultRow.Height = iGrid1.GetPreferredRowHeight(true, false);
            // Demonstrate disabled buttons.
            iGrid1.Cells[1, 1].Enabled = iGBool.False;
            iGrid1.EndUpdate();
            _bcm.CellButtonClick += _bcm_CellButtonClick; 
        }

        private void _bcm_CellButtonClick(object sender, iGButtonColumnManager.iGCellButtonClickEventArgs e)
        {
            MessageBox.Show(String.Format("Button cell ({0}, {1}) clicked!", e.RowIndex, e.ColIndex));
        }

        public void ControlLayerVisibility(bool visible)
        {
            _canvas.SetGridVisible(visible);
        }

        private void OECPTabledGridControlPanel_Load(object sender, EventArgs e)
        {
          
        }

  
    }
}
