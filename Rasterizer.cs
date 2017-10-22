using System;
using System.Drawing;

namespace ConsoleRasterizer
{
    public static class Rasterizer
    {
        public static bool IsSet(Triangle[] scene, int x, int y)
        {
            for (int i = 0; i < scene.Length; i++)
            {
                if (IsIntersecting(x, y, scene[i].P1, scene[i].P2))
                    return true;

                if (IsIntersecting(x, y, scene[i].P2, scene[i].P3))
                    return true;

                if (IsIntersecting(x, y, scene[i].P3, scene[i].P1))
                    return true;
            }

            return false;
        }

        private static bool IsIntersecting(int x, int y, Point p1, Point p2)
        {
            int intersectionType = GetIntersectionType(p1.Y, p2.Y, y);

            if (intersectionType == 1)
            {
                int intersectionX = GetIntersectionWithInts(y, p1, p2);

                if (intersectionX == x)
                    return true;
            }
            else if (intersectionType == 2)
            {
                int minX;
                int maxX;

                if (p1.X <= p2.X)
                {
                    minX = p1.X;
                    maxX = p2.X;
                }
                else
                {
                    minX = p2.X;
                    maxX = p1.X;
                }

                if ((x >= minX) && (x <= maxX))
                    return true;
            }

            return false;
        }

        private static int GetIntersectionType(int y1, int y2, int y)
        {
            // same line
            if ((y1 == y2) && (y1 == y))
                return 2;

            if (y1 > y2)
            {
                int tmp = y2;
                y2 = y1;
                y1 = tmp;
            }

            // one point (in theory, for a continuous line. Here it's discrete though, but we don't really care)
            if ((y >= y1) && (y <= y2))
                return 1;

            // none
            return 0;
        }

        private static int GetIntersectionWithFloats(int y, Point p1, Point p2)
        {
            int dX = p2.X - p1.X;

            if (dX != 0)
            {
                float a = (float)(p2.Y - p1.Y) / ((float)dX);
                float b = p1.Y - (a * p1.X);
                float x = (float)(y - b) / (float)a;

                return (int)Math.Round(x);
            }
            else
            {
                return p1.X;
            }
        }

        private static int GetIntersectionWithInts(int y, Point p1, Point p2)
        {
            int dX = p2.X - p1.X;

            if (dX != 0)
            {
                int dY = (p2.Y - p1.Y);

                int x = DivideAndRound(((y - (p1.Y - DivideAndRound(dY * p1.X, dX))) * dX), dY);

                return x;
            }
            else
            {
                return p1.X;
            }
        }

        private static int DivideAndRound(int a, int b)
        {
            int quotient = a / b;
            int remainder = a % b;

            if (Math.Abs(remainder) * 2 > Math.Abs(b))
                if (quotient >= 0)
                    quotient++;
                else
                    quotient--;

            return quotient;
        }

        public static void Translate(Triangle[] scene, int xOffset, int yOffset)
        {
            for (int i = 0; i < scene.Length; i++)
            {
                Point tmp;

                tmp = scene[i].P1;
                tmp.X += xOffset;
                tmp.Y += yOffset;
                scene[i].P1 = tmp;

                tmp = scene[i].P2;
                tmp.X += xOffset;
                tmp.Y += yOffset;
                scene[i].P2 = tmp;

                tmp = scene[i].P3;
                tmp.X += xOffset;
                tmp.Y += yOffset;
                scene[i].P3 = tmp;
            }
        }

        public static void Scale(Triangle[] scene, int xFactor, int yFactor)
        {
            for (int i = 0; i < scene.Length; i++)
            {
                Point tmp;

                tmp = scene[i].P1;
                tmp.X /= xFactor;
                tmp.Y /= yFactor;
                scene[i].P1 = tmp;

                tmp = scene[i].P2;
                tmp.X /= xFactor;
                tmp.Y /= yFactor;
                scene[i].P2 = tmp;

                tmp = scene[i].P3;
                tmp.X /= xFactor;
                tmp.Y /= yFactor;
                scene[i].P3 = tmp;
            }
        }

        private static readonly sbyte[] cosTable = new sbyte[]
            {
                127, 127, 127, 127, 127, 127, 126, 126, 126, 125, 125, 125, 124,
                124, 123, 123, 122, 121, 121, 120, 119, 119, 118, 117, 116,
                115, 114, 113, 112, 111, 110, 109, 108, 107, 105, 104, 103,
                101, 100, 99, 97, 96, 94, 93, 91, 90, 88, 87, 85,
                83, 82, 80, 78, 76, 75, 73, 71, 69, 67, 65, 64,
                62, 60, 58, 56, 54, 52, 50, 48, 46, 43, 41, 39,
                37, 35, 33, 31, 29, 26, 24, 22, 20, 18, 15, 13,
                11, 9, 7, 4, 2, 0, -2, -4, -7, -9, -11, -13,
                -15, -18, -20, -22, -24, -26, -29, -31, -33, -35, -37, -39,
                -41, -43, -46, -48, -50, -52, -54, -56, -58, -60, -62, -63,
                -65, -67, -69, -71, -73, -75, -76, -78, -80, -82, -83, -85,
                -87, -88, -90, -91, -93, -94, -96, -97, -99, -100, -101, -103,
                -104, -105, -107, -108, -109, -110, -111, -112, -113, -114, -115, -116,
                -117, -118, -119, -119, -120, -121, -121, -122, -123, -123, -124, -124,
                -125, -125, -125, -126, -126, -126, -127, -127, -127, -127, -127
            };

        private static readonly sbyte[] sinTable = new sbyte[]
            {
                0, 2, 4, 7, 9, 11, 13, 15, 18, 20, 22, 24, 26,
                29, 31, 33, 35, 37, 39, 41, 43, 46, 48, 50, 52,
                54, 56, 58, 60, 62, 63, 65, 67, 69, 71, 73, 75,
                76, 78, 80, 82, 83, 85, 87, 88, 90, 91, 93, 94,
                96, 97, 99, 100, 101, 103, 104, 105, 107, 108, 109, 110,
                111, 112, 113, 114, 115, 116, 117, 118, 119, 119, 120, 121,
                121, 122, 123, 123, 124, 124, 125, 125, 125, 126, 126, 126,
                127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 127, 126,
                126, 126, 125, 125, 125, 124, 124, 123, 123, 122, 121, 121,
                120, 119, 119, 118, 117, 116, 115, 114, 113, 112, 111, 110,
                109, 108, 107, 105, 104, 103, 101, 100, 99, 97, 96, 94,
                93, 91, 90, 88, 87, 85, 83, 82, 80, 78, 76, 75,
                73, 71, 69, 67, 65, 63, 62, 60, 58, 56, 54, 52,
                50, 48, 46, 43, 41, 39, 37, 35, 33, 31, 29, 26,
                24, 22, 20, 18, 15, 13, 11, 9, 7, 4, 2
            };

        private static readonly int tableFactor = 127;

        static int GetCos(int degree)
        {
            degree %= 360;

            if (degree > 179)
                return (-cosTable[degree - 180]);
            else
                return (cosTable[degree]);
        }

        static int GetSin(int degree)
        {
            degree %= 360;

            if (degree > 179)
                return (-sinTable[degree - 180]);
            else
                return (sinTable[degree]);
        }

        public static void Rotate(Triangle[] scene, int degree)
        {
            int sinAngle = GetSin(degree);
            int cosAngle = GetCos(degree);

            for (int i = 0; i < scene.Length; i++)
            {
                Point tmp = new Point();

                tmp.X = (scene[i].P1.X * cosAngle) / tableFactor - (scene[i].P1.Y * sinAngle) / tableFactor;
                tmp.Y = (scene[i].P1.X * sinAngle) / tableFactor + (scene[i].P1.Y * cosAngle) / tableFactor;
                scene[i].P1 = tmp;

                tmp.X = (scene[i].P2.X * cosAngle) / tableFactor - (scene[i].P2.Y * sinAngle) / tableFactor;
                tmp.Y = (scene[i].P2.X * sinAngle) / tableFactor + (scene[i].P2.Y * cosAngle) / tableFactor;
                scene[i].P2 = tmp;

                tmp.X = (scene[i].P3.X * cosAngle) / tableFactor - (scene[i].P3.Y * sinAngle) / tableFactor;
                tmp.Y = (scene[i].P3.X * sinAngle) / tableFactor + (scene[i].P3.Y * cosAngle) / tableFactor;
                scene[i].P3 = tmp;
            }
        }
    }
}