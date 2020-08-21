using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OECP.NET.Model;

namespace OECP.NET.CanvasTools
{
    public class DeleteTool:CanvasTool
    {
        public OECPLayer LayerOnDelete { get; set; }

        public DeleteTool():base(CanvasToolType.DeleteTool)
        {


        }

        public void DeleteElement(OECPElement ele)
        {
            if (LayerOnDelete.IsLine)
            {

            }
            else
            {
                LayerOnDelete.DeleteVertex((OECPVertex) ele);
            }

        }


    }
}
