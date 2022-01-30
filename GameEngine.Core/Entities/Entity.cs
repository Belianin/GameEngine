using System.Drawing;

namespace GameEngine.Core.Entities
{
    public class Entity
    {
        public PointF Position { get; set; }

        public Entity(PointF position)
        {
            Position = position;
        }
    }
}