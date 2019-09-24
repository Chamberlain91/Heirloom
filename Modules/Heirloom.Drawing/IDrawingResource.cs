namespace Heirloom.Drawing
{
    public interface IDrawingResource
    {
        object NativeObject { get; set; }

        uint Version { get; }

        void UpdateVersionNumber();
    }
}
