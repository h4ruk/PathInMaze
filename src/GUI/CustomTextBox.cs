using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace PathInMaze
{
    public class CustomTextBox : UserControl
    {
        private bool isFocus = false;
        private TextBox textBox;
        public new string Text
        {
            get => textBox.Text;
            set => textBox.Text = value;
        }


        public CustomTextBox()
        {
            textBox = new TextBox();
            textBox.MaxLength = 2;
            textBox.Multiline = false;
            textBox.BorderStyle = BorderStyle.None;
            textBox.TextAlign = HorizontalAlignment.Center;

            textBox.Enter += Entering;
            textBox.Leave += Leaving;

            this.Font = ExtendFont.Satoshi20;
            this.ForeColor = ExtendColor.Black;
            this.BackColor = ExtendColor.White;
            this.SizeChanged += (obj, e) => textBox.Size = Size;

            this.Controls.Add(textBox);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using Pen pen = new Pen((isFocus ? ExtendColor.Pink : ExtendColor.Black), 5) {Alignment = PenAlignment.Center, StartCap = LineCap.Round, EndCap = LineCap.Round};
            e.Graphics.DrawLine(pen, 0, Height, Width, Height);
        }

        private void Entering(object sender, EventArgs e)
        {
            isFocus = true;
            this.Invalidate();
        }

        private void Leaving(object sender, EventArgs e)
        {
            isFocus = false;
            this.Invalidate();
        }

        public bool HasValidNumber(out int number)
        {
            if(!Int32.TryParse(Text, out int value) || value < 5 || value > 30)
            {
                MessageBox.Show("Select number from 5 to 30.");
                number = value;
                return false;
            }
                
            number = value;
            return true;
        }
    }
}