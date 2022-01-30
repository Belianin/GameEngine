using System.Drawing;
using Microsoft.Xna.Framework;

namespace GameEngine.Core.Entities
{
    public static class EntityFactory
    {
        public static Entity SpawnPoint(int x, int y)
        {
            return new Entity(new PointF(x, y));
        }
        
        public static Entity SpawnPoint(int x, int y, Vector2 vector)
        {
            return new MovableEntity(new PointF(x, y))
            {
                Vector = vector
            };
        }
    }
}