﻿using System.Windows.Forms;
using CCWin;
using OECP.Canvas;
using OECP.NET.ControlStation;
using WeifenLuo.WinFormsUI.Docking;

namespace OECP.NET
{
    public partial class MainForm : CCSkinMain
    {
        public MainForm()
        {
            InitializeComponent();


            Panel canvasPanel = new Panel { Dock = DockStyle.Fill };
            var canvas = new OECPCanvas { Dock = DockStyle.Fill };
            dockPanelMain.Controls.Add(canvasPanel);

            OECPLayerTree form2 = new OECPLayerTree(canvas);
            form2.Show(dockPanelMain);
            form2.Dock = DockStyle.Left;
            form2.DockState = DockState.DockLeft;
            form2.SendToBack();

            canvasPanel.Controls.Add(canvas);
            canvasPanel.BringToFront();
            canvas.Init();


           

         

        }

    }
}
