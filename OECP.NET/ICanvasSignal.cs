using OECP.NET.Model;

namespace OECP.NET
{
    public interface ICanvasSignal
    {
        void UpdateGrid(int gridNum);
        void SetGridVisible(bool visible);

        void SetVertexVisible(bool visible);

        void StartDrawing(OECPLayer layer);

        void StopDrawing();

        void DeleteMode(OECPLayer layer,bool allowDelete);


    }
}