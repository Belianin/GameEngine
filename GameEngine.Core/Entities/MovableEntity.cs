using System.Drawing;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.Entities
{
    public class MovableEntity : Entity
    {
        
        public Vector2 Vector { get; set; }

        public MovableEntity(PointF position) : base(position)
        {
        }
    }
}