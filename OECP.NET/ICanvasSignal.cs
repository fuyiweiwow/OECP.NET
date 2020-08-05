namespace OECP.NET
{
    public interface ICanvasSignal
    {
        void UpdateGrid(int gridNum);
        void SetGridVisible(bool visible);

        void SetVertexVisible(bool visible);
    }
}