using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Minotaur
{
    class Labyrinth
    {
        private const byte COLOR_BLACK = 0;
        private const byte COLOR_RED = 9;
        private const byte COLOR_GREEN = 10;
        private const byte COLOR_BLUE = 12;
        private const byte COLOR_WHITE = 15;

        private byte[,] pixels;

        public Labyrinth()
        {

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
            }            

            return false;
        }

        private void separatePixels(byte bIn, out byte bOut1, out byte bOut2)
        {
            bOut1 = (byte)(bIn / 16);
            bOut2 = (byte)(bIn % 16);
        }
    }
}
