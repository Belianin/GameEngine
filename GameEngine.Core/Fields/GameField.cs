using GameEngine.Core.Entities;

namespace GameEngine.Core.Fields
{
    public class GameField
    {
        public Chunk[,] Chunks { get; }
        public int ChunkSize { get; }

        public GameField(Chunk[,] chunks)
        {
            Chunks = chunks;
            ChunkSize = chunks[0, 0].Width;
        }

        public void SpawnEntity(Entity entity)
        {
            Chunks[(int)entity.Position.X / ChunkSize, (int)entity.Position.Y / ChunkSize]
                .Entities
                .Add(entity);

        }
    }
}