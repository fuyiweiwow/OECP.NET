using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HZH_Controls.Controls;

namespace OECP.NET.ControlStation.BaseControl
{
    public partial class BaseTabControl : TabControl
    {
        public BaseTabControl()
        {
            InitializeComponent();
        }

        public BaseTabControl(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle rect = base.DisplayRectangle;
                return new Rectangle(rect.Left - 4, rect.Top - 4, rect.Width + 8, rect.Height + 8);
            }
        }

    }
}
