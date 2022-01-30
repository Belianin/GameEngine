using System.Drawing;

namespace GameEngine.Core
{
    public static class LinearMath
    {
        public static PointF? FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            var a2 = p2.X - p1.X;
            var a1 = p2.Y - p1.Y;
            var b2 = p4.X - p3.X;
            var b1 = p4.Y - p3.Y;

            var denominator = a1 * b2 - a2 * b1;

            var t1 = ((p1.X - p3.X) * b1 + (p3.Y - p1.Y) * b2) / denominator;
            if (float.IsInfinity(t1))
                return null;

            return new PointF(p1.X + a2 * t1, p1.Y + a1 * t1);
        }
    }
}