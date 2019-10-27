namespace Heirloom.Math
{
    public interface IShape
    {
        Rectangle Bounds { get; }
        float Area { get; }

        // 
        Vector ClosestPoint(in Vector point);

        // 
        bool Contains(in Vector point);

        // 
        bool Overlaps(IShape shape);

        // 
        bool Raycast(in Ray ray, out Contact contact);
        bool Raycast(in Ray ray);
    }
}
