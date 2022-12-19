using System;
using System.Drawing;
using System.Windows.Forms;

namespace PathInMaze
{
    public class MazeForm : Form
    {
        private enum State
        {
            SELECT,
            EXECUTE,
            FINISH
        }

        private State state;
        private Label notifyLabel;
        private Maze maze;

        public MazeForm(int rows, int columns)
        {
            //State settings
            this.state = State.SELECT;

            //Notify Label settings
            notifyLabel = new Label();
            notifyLabel.Text = "Select start - end";
            notifyLabel.Font = ExtendFont.Satoshi20;
            notifyLabel.TextAlign = ContentAlignment.MiddleCenter;
            notifyLabel.Location = new Point(0, 0);
            notifyLabel.Size = new Size(columns * Node.Area, 21);
            notifyLabel.BackColor = ExtendColor.Red;

            //Maze settings
            maze = new Maze(this, rows, columns);
            maze.Location = new Point(0, 21);
            maze.Size = new Size(columns * Node.Area, rows * Node.Area);

            //Form settings
            this.CenterToScreen();
            this.Text = "Perform";
            this.ClientSize = new Size(columns * Node.Area, rows * Node.Area + 21);
            this.BackColor = ExtendColor.White;
            this.ForeColor = ExtendColor.BlueBerry;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            
            //Add controls to form
            this.Controls.AddRange(new Control[] {
                notifyLabel,
                maze
            });
        }

        public void NextState()
        {
            if(state == State.SELECT) state = State.EXECUTE;
            else state = State.FINISH;
            StateChanged();
        }
        private void StateChanged()
        {
            switch (state)
            {
                case State.EXECUTE:
                    notifyLabel.Text = "Finding Path";
                    notifyLabel.BackColor = ExtendColor.Yellow;
                    Dijkstra dijkstra = new Dijkstra(maze);
                    dijkstra.Run();
                    break;

                case State.FINISH:
                    notifyLabel.Text = "Done!";
                    notifyLabel.BackColor = ExtendColor.Green;
                    break;
                    
            }
        }
    }
}