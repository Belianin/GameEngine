using System.Drawing;
using FluentAssertions;
using GameEngine.Core;
using GameEngine.Core.Entities;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace GameEngine.Tests
{
    public class LinearMathTests
    {
        [Test]
        public void Intersect_TwoLines()
        {
            var e1 = new MovableEntity(new PointF(0, 0))
            {
                Vector = new Vector2(100, 100)
            };
            var e2 = new MovableEntity(new PointF(0, 100))
            {
                Vector = new Vector2(100, -100)
            };


           var result = LinearMath.FindIntersection(e1.Position, e1.Position.Add(e1.Vector), e2.Position, e2.Position.Add(e2.Vector));

           result.Should().BeEquivalentTo(new PointF(50, 50));
        }
    }
}