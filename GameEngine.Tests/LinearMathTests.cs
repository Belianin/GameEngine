using System.Collections.Generic;
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

        [TestCaseSource(nameof(GetPointsInPolygon))]
        public void DetectPoint_InPolygon(PointF point, PointF[] polygon)
        {
            LinearMath.IsPointInsidePolygon(point, polygon).Should().BeTrue();
        }

        public static IEnumerable<TestCaseData> GetPointsInPolygon()
        {
            yield return new TestCaseData(new PointF(50, 50), new[]
            {
                new PointF(0, 0),
                new PointF(100, 0),
                new PointF(0, 100),
                new PointF(100, 100)
            }) { TestName = "Point in center of aligned rectangle" };
            
            yield return new TestCaseData(new PointF(0, 0), new[]
            {
                new PointF(20, 40),
                new PointF(40, -20),
                new PointF(-20, -40),
                new PointF(-40, 20)
            }) { TestName = "Point in center of not aligned rectangle" };
            
            yield return new TestCaseData(new PointF(0, 0), new[]
            {
                new PointF(0, 100),
                new PointF(100, 40),
                new PointF(50, -40),
                new PointF(0, -50),
                new PointF(-50, -40),
                new PointF(-100, 40)
            }) { TestName = "Point in polygon with six vexes" };
            
            yield return new TestCaseData(new PointF(100, 100), new[]
            {
                new PointF(100, 200),
                new PointF(200, 140),
                new PointF(150, 60),
                new PointF(100, 50),
                new PointF(50, 60),
                new PointF(0, 140)
            }) { TestName = "Point in polygon with six vexes and only positive coordinates" };
            
            yield return new TestCaseData(new PointF(0, 0), new[]
            {
                new PointF(100, 40),
                new PointF(130, 10),
                new PointF(100, -20),
                new PointF(-100, -20),
                new PointF(-130, 10),
                new PointF(-100, 40)
            }) { TestName = "Point in polygon with six vexes and point is \"not on axis\"" };
        }
    }
}