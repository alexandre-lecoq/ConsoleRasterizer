using System.Drawing;

namespace ConsoleRasterizer
{
    public class Triangle
    {
        private Point _p1;
        private Point _p2;
        private Point _p3;
 
        public Point P1
        {
            get { return _p1; }
            set { _p1 = value; }
        }
 
        public Point P2
        {
            get { return _p2; }
            set { _p2 = value; }
        }
 
        public Point P3
        {
            get { return _p3; }
            set { _p3 = value; }
        }
 
        public Triangle(Point p1, Point p2, Point p3)
        {
            _p1 = p1;
            _p2 = p2;
            _p3 = p3;
        }
    }
}

