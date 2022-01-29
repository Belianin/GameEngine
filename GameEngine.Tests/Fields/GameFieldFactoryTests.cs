using FluentAssertions;
using GameEngine.Core.Fields;
using NUnit.Framework;

namespace GameEngine.Tests.Fields
{
    public class Tests
    {
        [Test]
        public void GenerateField_WithRoundParameters()
        {
            var field = GameFieldFactory.GetField(100, 100, 20);

            field.Chunks.GetLength(0).Should().Be(5);
            field.Chunks.GetLength(1).Should().Be(5);

            foreach (var chunk in field.Chunks) 
                chunk.Should().Match<Chunk>(x => x.Height == 20 && x.Width == 20);
        }
        
        [Test]
        public void GenerateField_WithRoundParameters_NotSqaure()
        {
            var field = GameFieldFactory.GetField(100, 60, 20);

            field.Chunks.GetLength(0).Should().Be(5);
            field.Chunks.GetLength(1).Should().Be(3);

            foreach (var chunk in field.Chunks) 
                chunk.Should().Match<Chunk>(x => x.Height == 20 && x.Width == 20);
        }
        
        [Test]
        public void GenerateField_WithNotRoundParameters()
        {
            var field = GameFieldFactory.GetField(100, 100, 30);

            field.Chunks.GetLength(0).Should().Be(4);
            field.Chunks.GetLength(1).Should().Be(4);

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    var expectedWidth = x == 3 ? 10 : 30;
                    var expectedHeight = y == 3 ? 10 : 30;

                    field.Chunks[x, y].Should()
                        .Match<Chunk>(c => c.Width == expectedWidth && c.Height == expectedHeight);
                }
            }
        }
    }
    
}