using System;
using System.Drawing;

namespace GameEngine.Core.Fields
{
    public static class GameFieldFactory
    {
        public static GameField GetField(int width, int height, int chunkSize)
        {
            var horizontalChunksCount = (int) Math.Ceiling(width / (double) chunkSize);
            var verticalChunkCount = (int) Math.Ceiling(height / (double) chunkSize);

            var chunks = new Chunk[horizontalChunksCount, verticalChunkCount];
            for (int x = 0; x < horizontalChunksCount; x++)
            {
                for (int y = 0; y < verticalChunkCount; y++)
                {
                    var chunkWidth = x == horizontalChunksCount - 1
                        ? width - chunkSize * (horizontalChunksCount - 1)
                        : chunkSize;
                    var chunkHeight = y == verticalChunkCount - 1
                        ? width - chunkSize * (verticalChunkCount - 1)
                        : chunkSize;
                    
                    chunks[x, y] = new Chunk(new Point(x * chunkSize, y * chunkSize), chunkWidth, chunkHeight);
                }
            }

            return new GameField(chunks);
        }
    }
}