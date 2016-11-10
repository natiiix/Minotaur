using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Minotaur
{
    public partial class FormMain : Form
    {
        private const int MOVE_UP = 0;
        private const int MOVE_RIGHT = 1;
        private const int MOVE_DOWN = 2;
        private const int MOVE_LEFT = 3;

        private int[,] MOVE_OFFSET = new int[4, 2]
        {
            {  0, -1 },
            {  1,  0 },
            {  0,  1 },
            { -1,  0 }
        };

        private string strDirectory;
        private BackgroundWorker bwSolver;
        private Bitmap bmpDisplay;
        private Labyrinth lab;

        #region Events
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            bwSolver = new BackgroundWorker();
            bwSolver.WorkerReportsProgress = true;
            bwSolver.WorkerSupportsCancellation = true;
            bwSolver.DoWork += BwSolver_DoWork;
            bwSolver.ProgressChanged += BwSolver_ProgressChanged;
            bwSolver.RunWorkerCompleted += BwSolver_RunWorkerCompleted;
        }

        private void BwSolver_DoWork(object sender, DoWorkEventArgs e)
        {
            SolveLabyrinth();
        }

        private void BwSolver_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pictureBoxLabyrinth.Image = bmpDisplay;
        }

        private void BwSolver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonLoad.Enabled = true;
            buttonSolve.Enabled = true;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialogLabyrinth.ShowDialog();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            if (bmpDisplay.PixelFormat == System.Drawing.Imaging.PixelFormat.Format4bppIndexed)
            {
                buttonLoad.Enabled = false;
                buttonSolve.Enabled = false;
                bwSolver.RunWorkerAsync();
            }
            else
            {
                buttonSolve.Enabled = false;
                MessageBox.Show("Input bitmap has incorrect pixel format! 4bpp (16 colors) pixel format is required!");
            }
        }

        private void openFileDialogLabyrinth_FileOk(object sender, CancelEventArgs e)
        {
            bmpDisplay = new Bitmap(openFileDialogLabyrinth.FileName);
            buttonSolve.Enabled = true;
            pictureBoxLabyrinth.Image = bmpDisplay;

            strDirectory = openFileDialogLabyrinth.FileName.Replace(openFileDialogLabyrinth.SafeFileName, string.Empty);
        }
        #endregion

        private void SolveLabyrinth()
        {

            lab = new Labyrinth();

            if (!lab.Extract(bmpDisplay))
                return;

            for (int targetIndex = 0; targetIndex < lab.pTarget.Length; targetIndex++)
            {
                SolveTarget(targetIndex);
                bwSolver.ReportProgress(0);
            }
        }

        private void SolveTarget(int targetIndex)
        {
            Stopwatch swTarget = new Stopwatch();
            swTarget.Restart();

            byte[,] pixels = (byte[,])lab.pixels.Clone();
            
            for (int otherTarget = 0; otherTarget < lab.pTarget.Length; otherTarget++)
            {
                if (otherTarget == targetIndex)
                    continue;

                pixels[lab.pTarget[otherTarget].X,
                       lab.pTarget[otherTarget].Y] = Labyrinth.COLOR_BLACK;
            }

            LabyrinthSolution bestSolution = new LabyrinthSolution(new int[0]);
            GetBestSolution(ref bestSolution, pixels, new int[0], lab.pStart, lab.pTarget[targetIndex]);

            Bitmap bmpSolution = GenerateBitmap(pixels, bestSolution);
            swTarget.Stop();
            bmpSolution.Save(strDirectory + "Target[" + targetIndex.ToString() + "]_Moves[" + bestSolution.Moves.Length.ToString() + "]_Time[" + swTarget.ElapsedMilliseconds.ToString() + "].bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            bmpDisplay = bmpSolution;
        }

        private void GetBestSolution(ref LabyrinthSolution solution, byte[,] pixels, int[] moves, Point currentPosition, Point pTarget)
        {
            for (int move = 0; move < MOVE_OFFSET.GetLength(0); move++)
            {
                Point futurePosition = GetPositionAfterMove(currentPosition, move);

                if (futurePosition == pTarget)
                    solution = new LabyrinthSolution(moves);
                else if (CanMoveHere(pixels, futurePosition) &&
                         (solution.Moves.Length == 0 || (moves.Length + 1) < solution.Moves.Length))
                {
                    Misc.ArrayAppend(ref moves, move);
                    pixels[futurePosition.X, futurePosition.Y] = Labyrinth.COLOR_BLUE;
                    
                    GetBestSolution(ref solution, pixels, moves, futurePosition, pTarget);

                    Misc.ArrayRemoveLast(ref moves);
                    pixels[futurePosition.X, futurePosition.Y] = Labyrinth.COLOR_WHITE;
                }
            }
        }

        private Point GetPositionAfterMove(Point position, int move)
        {
            return new Point(position.X + MOVE_OFFSET[move, 0],
                             position.Y + MOVE_OFFSET[move, 1]);
        }

        private bool CanMoveHere(byte[,] pixels, Point p)
        {
            return (p.X >= 0 && p.X < pixels.GetLength(0) &&
                    p.Y >= 0 && p.Y < pixels.GetLength(1) &&
                    pixels[p.X, p.Y] == Labyrinth.COLOR_WHITE);
        }

        private Bitmap GenerateBitmap(byte[,] pixels, LabyrinthSolution solution)
        {
            byte[,] outputPixels = pixels;

            if (solution.Moves.Length > 0)
            {
                Point position = lab.pStart;

                for(int iMove = 0; iMove < solution.Moves.Length; iMove++)
                {
                    position = GetPositionAfterMove(position, solution.Moves[iMove]);
                    outputPixels[position.X, position.Y] = Labyrinth.COLOR_BLUE;
                }
            }

            Bitmap bmpOutput = new Bitmap(outputPixels.GetLength(0), outputPixels.GetLength(1), System.Drawing.Imaging.PixelFormat.Format4bppIndexed);

            Rectangle rect = new Rectangle(0, 0, bmpOutput.Width, bmpOutput.Height);
            System.Drawing.Imaging.BitmapData bmpData = bmpOutput.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpOutput.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * bmpOutput.Height;

            System.Runtime.InteropServices.Marshal.Copy(GetBitmapByteArray(outputPixels, bmpData.Stride), 0, ptr, bytes);
            bmpOutput.UnlockBits(bmpData);

            return bmpOutput;
        }

        private byte[] GetBitmapByteArray(byte[,] pixels, int stride)
        {
            int width = pixels.GetLength(0);
            int height = pixels.GetLength(1);
            byte[] outputBytes = new byte[stride * height];

            for (int y = 0; y < height; y++)
            {
                int yOffset = y * stride;
                
                for(int x = 0; x < stride; x++)
                {
                    int first = x * 2;
                    int second = first + 1;

                    outputBytes[yOffset + x] = JoinPixels((first < width ? pixels[first, y] : 0),
                                                          (second < width ? pixels[second, y] : 0));
                }
            }

            return outputBytes;
        }

        private byte JoinPixels(int pixel1, int pixel2)
        {
            return (byte)((pixel1 * 16) + pixel2);
        }
    }
}
