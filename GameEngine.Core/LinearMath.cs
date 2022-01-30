using System;
using System.Drawing;

namespace GameEngine.Core
{
    public static class LinearMath
    {
        public static PointF? FindIntersection(PointF p1, PointF p2, PointF p3, PointF p4)
        {
            var a2 = p2.X - p1.X;
            var a1 = p2.Y - p1.Y;
            var b2 = p4.X - p3.X;
            var b1 = p4.Y - p3.Y;

            var denominator = a1 * b2 - a2 * b1;

            var t1 = ((p1.X - p3.X) * b1 + (p3.Y - p1.Y) * b2) / denominator;
            if (float.IsInfinity(t1))
                return null;

            return new PointF(p1.X + a2 * t1, p1.Y + a1 * t1);
        }

        public static bool ArePolygonsIntersect(PointF[] polygon1, PointF[] polygon2)
        {
            foreach (var point in polygon1)
            {
                if (IsPointInsidePolygon(point, polygon2))
                    return true;
            }

            return false;
        }

        public static bool IsPointInsidePolygon(PointF point, PointF[] polygon)
        {
            // Create a point for line segment from p to infinite
            var extreme = new PointF(1000000, point.Y);
 
            // Count intersections of the above line
            // with sides of polygon
            int count = 0, i = 0;
            do
            {
                int next = (i + 1) % polygon.Length;
 
                // Check if the line segment from 'p' to
                // 'extreme' intersects with the line
                // segment from 'polygon[i]' to 'polygon[next]'
                if (DoIntersect(polygon[i], polygon[next], point, extreme))
                {
                    // If the point 'p' is collinear with line
                    // segment 'i-next', then check if it lies
                    // on segment. If it lies, return true, otherwise false
                    if (GetOrientation(polygon[i], point, polygon[next]) == 0)
                    {
                        return IsOnSegment(polygon[i], point,
                            polygon[next]);
                    }
                    count++;
                }
                i = next;
            } while (i != 0);
 
            // Return true if count is odd, false otherwise
            return (count % 2 == 1); // Same as (count%2 == 1)
        }
        
        // The function that returns true if
        // line segment 'p1q1' and 'p2q2' intersect.
        private static bool DoIntersect(PointF p1, PointF q1, PointF p2, PointF q2)
        {
            // Find the four orientations needed for
            // general and special cases
            int o1 = GetOrientation(p1, q1, p2);
            int o2 = GetOrientation(p1, q1, q2);
            int o3 = GetOrientation(p2, q2, p1);
            int o4 = GetOrientation(p2, q2, q1);
 
            // General case
            if (o1 != o2 && o3 != o4)
            {
                return true;
            }
 
            // Special Cases
            // p1, q1 and p2 are collinear and
            // p2 lies on segment p1q1
            if (o1 == 0 && IsOnSegment(p1, p2, q1))
            {
                return true;
            }
 
            // p1, q1 and p2 are collinear and
            // q2 lies on segment p1q1
            if (o2 == 0 && IsOnSegment(p1, q2, q1))
            {
                return true;
            }
 
            // p2, q2 and p1 are collinear and
            // p1 lies on segment p2q2
            if (o3 == 0 && IsOnSegment(p2, p1, q2))
            {
                return true;
            }
 
            // p2, q2 and q1 are collinear and
            // q1 lies on segment p2q2
            if (o4 == 0 && IsOnSegment(p2, q1, q2))
            {
                return true;
            }
 
            // Doesn't fall in any of the above cases
            return false;
        }
        
        // Given three collinear points p, q, r,
        // the function checks if point q lies
        // on line segment 'pr'
        private static bool IsOnSegment(PointF p, PointF q, PointF r)
        {
            return q.X <= Math.Max(p.X, r.X) &&
                   q.X >= Math.Min(p.X, r.X) &&
                   q.Y <= Math.Max(p.Y, r.Y) &&
                   q.Y >= Math.Min(p.Y, r.Y);
        }
        
        // To find orientation of ordered triplet (p, q, r).
        // The function returns following values
        // 0 --> p, q and r are collinear
        // 1 --> Clockwise
        // 2 --> Counterclockwise
        private static int GetOrientation(PointF p, PointF q, PointF r)
        {
            var val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);
 
            if (val == 0)
                return 0; // collinear
           
            return val > 0 ? 1 : 2; // clock or counterclock wise
        }
    }
}