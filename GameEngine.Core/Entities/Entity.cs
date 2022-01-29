using System.Drawing;

namespace GameEngine.Core.Entities
{
    public class Entity
    {
        public PointF Position { get; }

        public Entity(PointF position)
        {
            Position = position;
        }
    }
}