using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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

        private BackgroundWorker bwSolver;
        private Labyrinth lab;

        private Bitmap bmpLabyrinth;
        private Bitmap bmpDisplay;
        private Bitmap[] bmpSolutions;

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
            lab = new Labyrinth();

            if (lab.Extract(bmpLabyrinth))
                SolveLabyrinth();
        }

        private void BwSolver_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pictureBoxLabyrinth.Image = bmpDisplay;
        }

        private void BwSolver_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonLoad.Enabled = true;
            pictureBoxLabyrinth.Image = bmpLabyrinth;
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialogLabyrinth.ShowDialog();
        }

        private void buttonSolve_Click(object sender, EventArgs e)
        {
            if (bmpLabyrinth.PixelFormat == System.Drawing.Imaging.PixelFormat.Format4bppIndexed)
            {
                buttonLoad.Enabled = false;
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
            bmpLabyrinth = new Bitmap(openFileDialogLabyrinth.FileName);
            buttonSolve.Enabled = true;
            pictureBoxLabyrinth.Image = bmpLabyrinth;
        }
        #endregion

        private void SolveLabyrinth()
        {
            for(int target = 0; target < lab.pTarget.Length; target++)
            {
                byte[,] pixels = lab.pixels;

                for(int otherTarget = 0; otherTarget < lab.pTarget.Length; otherTarget++)
                {
                    if (otherTarget == target)
                        continue;

                    pixels[lab.pTarget[otherTarget].X,
                           lab.pTarget[otherTarget].Y] = Labyrinth.COLOR_BLACK;
                }

                Point currentPosition = lab.pStart;
                int[] moves = new int[0];


            }
        }

        private bool CanPerformMove(byte[,] pixels, Point position, int move)
        {
            return (pixels[position.X + MOVE_OFFSET[move, 0],
                           position.X + MOVE_OFFSET[move, 1]] == Labyrinth.COLOR_WHITE);
        }

        private void PerformMove(ref Point position, int move)
        {
            position = new Point(
                            position.X + MOVE_OFFSET[move, 0],
                            position.X + MOVE_OFFSET[move, 1]);
        }
    }
}
