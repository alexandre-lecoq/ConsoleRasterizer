using System;
using System.Drawing;

namespace ConsoleRasterizer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Triangle[] starOne = new Triangle[3];
            starOne[0] = new Triangle(new Point(0, 10), new Point(52, 10), new Point(26, 19));
            starOne[1] = new Triangle(new Point(26, 0), new Point(41, 25), new Point(15, 16));
            starOne[2] = new Triangle(new Point(9, 25), new Point(26, 0), new Point(35, 16));

            Triangle[] starTwo = new Triangle[3];
            starTwo[0] = new Triangle(new Point(0, 20), new Point(52, 20), new Point(26, 38));
            starTwo[1] = new Triangle(new Point(26, 0), new Point(41, 50), new Point(16, 31));
            starTwo[2] = new Triangle(new Point(9, 50), new Point(26, 0), new Point(35, 32));

            Rasterizer.Scale(starTwo, 1, 2);
            Rasterizer.Rotate(starTwo, 180);
            Rasterizer.Translate(starTwo, 20, 0);

            Triangle[] starThree = new Triangle[2];
            starThree[0] = new Triangle(new Point(0, 8), new Point(52, 8), new Point(26, 28));
            starThree[1] = new Triangle(new Point(0, 22), new Point(52, 22), new Point(26, 2));

            Rasterizer.Rotate(starThree, 90);
            Rasterizer.Scale(starThree, 1, 2);
            Rasterizer.Translate(starThree, 30, 0);

            Triangle[] timeWord = new Triangle[22];
            timeWord[0] = new Triangle(new Point(7, 5), new Point(17, 8), new Point(7, 8));
            timeWord[1] = new Triangle(new Point(7, 5), new Point(17, 5), new Point(17, 8));
            timeWord[2] = new Triangle(new Point(11, 8), new Point(14, 16), new Point(11, 16));
            timeWord[3] = new Triangle(new Point(14, 8), new Point(14, 16), new Point(11, 8));
            timeWord[4] = new Triangle(new Point(20, 5), new Point(23, 16), new Point(20, 16));
            timeWord[5] = new Triangle(new Point(20, 5), new Point(23, 16), new Point(23, 5));
            timeWord[6] = new Triangle(new Point(26, 5), new Point(29, 16), new Point(26, 16));
            timeWord[7] = new Triangle(new Point(26, 5), new Point(29, 16), new Point(29, 5));
            timeWord[8] = new Triangle(new Point(29, 5), new Point(33, 9), new Point(29, 9));
            timeWord[9] = new Triangle(new Point(29, 9), new Point(33, 9), new Point(33, 12));
            timeWord[10] = new Triangle(new Point(33, 9), new Point(37, 5), new Point(37, 9));
            timeWord[11] = new Triangle(new Point(37, 9), new Point(33, 12), new Point(33, 9));
            timeWord[12] = new Triangle(new Point(37, 5), new Point(40, 16), new Point(40, 5));
            timeWord[13] = new Triangle(new Point(37, 5), new Point(40, 16), new Point(37, 16));
            timeWord[14] = new Triangle(new Point(43, 5), new Point(46, 16), new Point(43, 16));
            timeWord[15] = new Triangle(new Point(43, 5), new Point(46, 16), new Point(46, 5));
            timeWord[16] = new Triangle(new Point(46, 5), new Point(51, 8), new Point(46, 8));
            timeWord[17] = new Triangle(new Point(46, 5), new Point(51, 8), new Point(51, 5));
            timeWord[18] = new Triangle(new Point(46, 13), new Point(51, 16), new Point(46, 16));
            timeWord[19] = new Triangle(new Point(46, 13), new Point(51, 16), new Point(51, 13));
            timeWord[20] = new Triangle(new Point(46, 10), new Point(50, 11), new Point(46, 11));
            timeWord[21] = new Triangle(new Point(46, 10), new Point(50, 11), new Point(50, 10));

            Rasterizer.Rotate(timeWord, 90);
            Rasterizer.Translate(timeWord, 30, -2);

            RenderTriangles(starOne);
            RenderTriangles(starTwo);
            RenderTriangles(starThree);
            RenderTriangles(timeWord);
        }

        private static void RenderTriangles(Triangle[] triangles)
        {
            for (int y = 0; y < 70; y++)
            {
                for (int x = 0; x < 70; x++)
                {
                    bool isSet = Rasterizer.IsSet(triangles, x, y);

                    Console.Write(isSet ? '*' : ' ');
                }

                Console.Write('\n');
            }
        }
    }
}
