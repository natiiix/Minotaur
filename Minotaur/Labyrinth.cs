using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Minotaur
{
    class Labyrinth
    {
        public const byte COLOR_BLACK = 0;  // wall
        public const byte COLOR_RED = 9;    // target destination
        public const byte COLOR_GREEN = 10; // start point
        public const byte COLOR_BLUE = 12;  // path traveled
        public const byte COLOR_WHITE = 15; // floor

        public byte[,] pixels { get; private set; }
        public Point pStart { get; private set; }
        public Point[] pTarget;

        public Labyrinth()
        {
            pStart = new Point(-1, -1);
        }

        public bool Extract(Bitmap bmpLabyrinth)
        {
            Rectangle rect = new Rectangle(0, 0, bmpLabyrinth.Width, bmpLabyrinth.Height);
            BitmapData bmpData = bmpLabyrinth.LockBits(rect, ImageLockMode.ReadOnly, bmpLabyrinth.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            int bytes = Math.Abs(bmpData.Stride) * bmpLabyrinth.Height;
            byte[] colorValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, colorValues, 0, bytes);
            bmpLabyrinth.UnlockBits(bmpData);
            
            pixels = new byte[rect.Width, rect.Height];
            pTarget = new Point[0];

            int rowByteLen = rect.Width / 2;
            int rowByteOdd = rect.Width % 2;

            for (int iy = 0; iy < rect.Height; iy++)
            {
                int yOffset = iy * bmpData.Stride;
                int pixelX = 0;

                for (int ix = 0; ix < rowByteLen + rowByteOdd; ix++)
                {
                    if(ix < rowByteLen)
                    {
                        separatePixels(colorValues[yOffset + ix],
                            out pixels[pixelX, iy],
                            out pixels[pixelX + 1, iy]);

                        pixelX += 2;
                    }
                    else
                    {
                        byte bDump = 0;

                        separatePixels(colorValues[yOffset + ix],
                            out pixels[pixelX, iy],
                            out bDump);
                    }
                }

                for(int ix = 0; ix < rect.Width; ix++)
                {
                    if (pixels[ix, iy] == COLOR_GREEN)
                    {
                        if (pStart.X < 0 || pStart.Y < 0)
                            pStart = new Point(ix, iy);
                        else
                            return false;
                    }
                    else if (pixels[ix, iy] == COLOR_RED)
                        Misc.ArrayAppend(ref pTarget, new Point(ix, iy));
                }
            }

            return ((pStart.X >= 0 &&
                     pStart.Y >= 0) &&
                    pTarget.Length > 0);
        }

        private void separatePixels(byte bIn, out byte bOut1, out byte bOut2)
        {
            bOut1 = (byte)(bIn / 16);
            bOut2 = (byte)(bIn % 16);
        }
    }
}
