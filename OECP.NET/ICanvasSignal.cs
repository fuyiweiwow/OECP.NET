﻿using System.Drawing;
using OECP.NET.CanvasTools;
using OECP.NET.Model;

namespace OECP.NET
{
    public interface ICanvasSignal
    {
        void UpdateGrid(int gridNum);

        void SetLayerVisible(bool visible, OECPLayer layer);

        void StartDrawing();

        void StopDrawing();

        void DeleteMode(bool allowDelete);

        void ChangeCurrentLayer(OECPLayer layer);

        OECPVertex I2C(OECPVertex iVtx);

        OECPVertex C2I(OECPVertex cVtx);

        OECPLayer VertexLayer();

        OECPLayer CurrentLayer();

        void RepaintCanvas();

        void FreezeRightClickMenu(bool frozen);

        RectangleF GetPrimeSquare();

        void SetCurrentSquareLocation(PointF ptf);

        bool VertexOnLine(OECPVertex vtx,ref OECPVertex projVtx );

        void SetCurrentDrawTool(DrawTool dtool);

        int CanvasLeft();

        int CanvasTop();

    }
}