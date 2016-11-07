namespace Minotaur
{
    /// <summary>
    /// Inherits from PictureBox; adds Interpolation Mode Setting
    /// </summary>
    public class PictureBoxWithInterpolationMode : System.Windows.Forms.PictureBox
    {
        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode { get; set; }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs paintEventArgs)
        {
            paintEventArgs.Graphics.InterpolationMode = InterpolationMode;
            base.OnPaint(paintEventArgs);
        }
    }
}
