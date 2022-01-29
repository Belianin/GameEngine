using System.Collections.Generic;
using System.Drawing;

namespace GameEngine.Core.Fields
{
    public class Chunk
    {
        public Point Start { get; }
        public int Width { get; }
        public int Height { get; }
        public HashSet<Entity> Entities { get; } = new HashSet<Entity>();

        public Chunk(Point start, int width, int height)
        {
            Start = start;
            Width = width;
            Height = height;
        }
    }
}