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
    }
}