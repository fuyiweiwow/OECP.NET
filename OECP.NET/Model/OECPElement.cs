﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OECP.NET.Model
{
    class OECPElement
    {
        public Color ElementColor { get; set; }

        OECPElement(Color color)
        {
            ElementColor = color;
        }
    }
}