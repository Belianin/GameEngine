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

        // Не работает :/
        public static bool IsPointInsidePolygon2(PointF point, PointF[] polygon)
        {
            var j = polygon.Length - 1;
            var result = false;
            for (int i = 0; i < polygon.Length; i++)
            {
                if ((polygon[i].Y <= point.Y && point.Y < polygon[j].Y || polygon[j].Y <= point.Y && point.Y < polygon[i].Y) &&
                    point.X > (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                    result = !result;
                j = i;
            }

            return result;
        }
        
        // Пока, боюсь представить как это работает
        public static bool IsPointInsidePolygon(PointF point, PointF[] p)
        {
            var i1 = 0;
            var i2 = 0;
            var S = 0f;
            var S1 = 0f;
            var S2 = 0f;
            var S3 = 0f;
            var flag = false;
            for (var n = 0; n < p.Length; n++)
            {
                i1 = n < p.Length-1 ? n + 1 : 0;
                while (!flag)
                {
                    i2 = i1 + 1;
                    if (i2 >= p.Length)
                        i2 = 0;
                    if (i2 == (n < p.Length-1 ? n + 1 : 0))
                        break;
                    S = Math.Abs(p[i1].X * (p[i2].Y - p[n ].Y) +
                             p[i2].X * (p[n ].Y - p[i1].Y) +
                             p[n].X  * (p[i1].Y - p[i2].Y));
                    S1 = Math.Abs(p[i1].X * (p[i2].Y - point.Y) +
                              p[i2].X * (point.Y       - p[i1].Y) +
                              point.X       * (p[i1].Y - p[i2].Y));
                    S2 = Math.Abs(p[n ].X * (p[i2].Y - point.Y) +
                                  p[i2].X * (point.Y       - p[n ].Y) +
                                  point.X       * (p[n ].Y - p[i2].Y));
                    S3 = Math.Abs(p[i1].X * (p[n ].Y - point.Y) +
                                  p[n ].X * (point.Y       - p[i1].Y) +
                                  point.X       * (p[i1].Y - p[n ].Y));
                    if (Math.Abs(S - (S1 + S2 + S3)) < 0.1) 
                    {
                        flag = true;
                        break;
                    }
                    i1 = i1 + 1;
                    if (i1 >= p.Length)
                        i1 = 0;
                    break;
                }
                if (flag)
                    break;
            }
            return flag;
        }
    }
}