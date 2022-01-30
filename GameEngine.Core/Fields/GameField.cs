using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            GetChunkByPosition((int)entity.Position.X, (int)entity.Position.Y)
                .Entities
                .Add(entity);
        }

        public void Tick()
        {
            var chunkTransfer = new List<(Entity entity, Chunk from, Chunk to)>();
                
            for (int x = 0; x < Chunks.GetLength(0); x++)
            {
                for (int y = 0; y < Chunks.GetLength(1); y++)
                {
                    var chunk = Chunks[x, y];
                    foreach (var entity in chunk.Entities.OfType<MovableEntity>().ToArray())
                    {
                        entity.Position = new PointF(entity.Position.X + entity.Vector.X, entity.Position.Y + entity.Vector.Y);
                        var newChunk = GetChunkByPosition((int)entity.Position.X, (int)entity.Position.Y);
                        if (chunk != newChunk) 
                            chunkTransfer.Add((entity, chunk, newChunk));
                    }
                }
            }

            foreach (var (entity, from, to) in chunkTransfer)
            {
                from.Entities.Remove(entity);
                to.Entities.Add(entity);
            }
        }

        private Chunk GetChunkByPosition(int x, int y)
        {
            return Chunks[x / ChunkSize, y / ChunkSize];
        }
    }
}