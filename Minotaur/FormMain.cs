using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Minotaur
{
    public partial class FormMain : Form
    {
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

        }
    }
}
