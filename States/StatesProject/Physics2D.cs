using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace States.StatesProject
{
    public static class Physics2D
    {
        public static PointF NormalizePoint(PointF vector)
        {
            float distance = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            return new PointF(vector.X / distance, vector.Y / distance);
        }

        public static bool PointTargeting(Point point, Point target, int accuracy)
        {
            Rectangle area = new Rectangle(target.X - accuracy, target.Y - accuracy, accuracy * 2, accuracy * 2);
            return area.Contains(point);
        }

        public static PointF RemoveUnderOnes(PointF point)
        {
            if (point.X < 1 && point.X > 0)
                point.X = point.X > 0.3 ? 1 : 0;
            if (point.X > -1 && point.X < 0) 
                //point.X = -1;
                point.X = point.X < -0.3 ? -1 : 0;
            if (point.Y < 1 && point.Y > 0) 
                //point.Y = 1;
                point.Y = point.Y > 0.3 ? 1 : 0;
            if (point.Y > -1 && point.Y < 0) 
                //point.Y = -1;
                point.Y = point.Y < -0.3 ? -1 : 0;
            return point;
        }

        public static Point SetVector(Point point, Point setPoint)
        {
            point.X = setPoint.X;
            point.Y = setPoint.Y;
            return point;
        }

        public static (int, Point) FindTheNearestPoint(Point startPoint, Point[] targets)
        {
            var vectors = targets.Select(x => new Point() { X = Math.Abs(x.X - startPoint.X), Y = Math.Abs(x.Y - startPoint.Y) }).ToArray();
            var sums = vectors.Select(x => x.X + x.Y).ToArray();
            var minSum = sums.Min(x => x);
            var ind = vectors.Select((item, index) => new { index, item }).FirstOrDefault(x => x.item.Y + x.item.X == minSum).index;
            return (ind, targets[ind]);
        }
    }
}
