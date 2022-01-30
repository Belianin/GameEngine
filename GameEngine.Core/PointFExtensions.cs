using System.Drawing;
using Microsoft.Xna.Framework;

namespace GameEngine.Core
{
    public static class PointFExtensions
    {
        public static PointF Add(this PointF point, Vector2 vector)
        {
            return new PointF(point.X + vector.X, point.Y + vector.Y);
        }
    }
}