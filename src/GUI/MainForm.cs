using System;
using System.Drawing;
using System.Windows.Forms;

namespace PathInMaze
{
    public class MainForm : Form
    {
        private Label title;
        private Label rowsLabel;
        private Label columnsLabel;
        private CustomTextBox rowsTextBox;
        private CustomTextBox columnsTextBox;
        private CustomButton generateButton;

        public MainForm()
        {
            //Title Label
            title = new Label();
            title.Text = "PATH IN MAZE";
            title.Font = ExtendFont.Satoshi64;
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.Location = new Point(120, 68);
            title.Size = new Size(480, 79);

            //Rows Label 
            rowsLabel = new Label();
            rowsLabel.Text = "Rows";
            rowsLabel.Font = ExtendFont.Satoshi24;
            rowsLabel.TextAlign = ContentAlignment.MiddleCenter;
            rowsLabel.Location = new Point(185, 154);
            rowsLabel.Size = new Size(72, 25);

            //Coulumns Label 
            columnsLabel = new Label();
            columnsLabel.Text = "Columns";
            columnsLabel.Font = ExtendFont.Satoshi24;
            columnsLabel.TextAlign = ContentAlignment.MiddleCenter;
            columnsLabel.Location = new Point(356, 154);
            columnsLabel.Size = new Size(110, 25);

            //Rows CustomTextBox 
            rowsTextBox = new CustomTextBox();
            rowsTextBox.Text = "5";
            rowsTextBox.Location = new Point(252, 154);
            rowsTextBox.Size = new Size(40, 32);

            //Columns CustomTextBox 
            columnsTextBox = new CustomTextBox();
            columnsTextBox.Text = "7";
            columnsTextBox.Location = new Point(466, 154);
            columnsTextBox.Size = new Size(40, 32);

            //Generate Button
            generateButton = new CustomButton();
            generateButton.Text = "Generate";
            generateButton.Location = new Point(194, 204);
            generateButton.Size = new Size(314, 40);
            generateButton.Click += GenerateMazeForm;

            //Form
            this.CenterToScreen();
            this.Text = "Menu";
            this.Size = new Size(700, 300);
            this.BackColor = ExtendColor.White;
            this.ForeColor = ExtendColor.BlueBerry;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;

            //Add controls
            this.Controls.AddRange(new Control[]{
                title,
                rowsLabel,
                columnsLabel,
                rowsTextBox,
                columnsTextBox,
                generateButton
            });
        }

        private void GenerateMazeForm(object sender, EventArgs e)
        {
            if(!rowsTextBox.HasValidNumber(out int rows) || !columnsTextBox.HasValidNumber(out int columns)) return;
            MazeForm mazeForm = new MazeForm(rows, columns);
            mazeForm.ShowDialog();
        }
    }
}