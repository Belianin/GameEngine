namespace GameEngine.Core.Fields
{
    public class GameField
    {
        public Chunk[,] Chunks { get; }

        public GameField(Chunk[,] chunks)
        {
            Chunks = chunks;
        }
    }
}