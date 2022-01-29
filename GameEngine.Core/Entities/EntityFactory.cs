using System.Drawing;

namespace GameEngine.Core.Entities
{
    public static class EntityFactory
    {
        public static Entity SpawnPoint(int x, int y)
        {
            return new Entity(new PointF(x, y));
        }
    }
}