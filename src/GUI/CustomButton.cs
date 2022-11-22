using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PathInMaze
{
    public class CustomButton : Button
    {
        private bool isMouseEnter = false;

        public CustomButton()
        {
            this.Paint += Painting;
            this.MouseEnter += Entering;
            this.MouseLeave += Leaving;

            this.Font = ExtendFont.Satoshi32;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.FlatStyle = FlatStyle.Popup;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = ExtendColor.White;
            this.ForeColor = ExtendColor.BlueBerry;
        }

        private void Painting(object sender, PaintEventArgs e)
        {
            using Pen pen = new Pen((isMouseEnter ? ExtendColor.Pink : ExtendColor.BlueBerry), 4) {Alignment = PenAlignment.Center};
            e.Graphics.DrawRectangle(pen, 0, 0, Width, Height);
        }

        private void Entering(object sender, EventArgs e)
        {
            isMouseEnter = true;
            ForeColor = ExtendColor.Pink;
            this.Invalidate();
        }

        private void Leaving(object sender, EventArgs e)
        {
            isMouseEnter = false;
            ForeColor = ExtendColor.BlueBerry;
            this.Invalidate();
        }
    }
}