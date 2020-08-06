using OECP.NET.Model;

namespace OECP.NET
{
    public interface ICanvasSignal
    {
        void UpdateGrid(int gridNum, OECPLayer layer);
        void SetGridVisible(bool visible);

        void SetVertexVisible(bool visible);

        void DrawVertex(OECPLayer layer);

        void StopDrawing();

    }
}