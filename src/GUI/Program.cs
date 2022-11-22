using System;
using System.Windows.Forms;

namespace PathInMaze
{
    public class Program
    {
        [STAThread]
        public static void Main()
        {
            MainForm present = new MainForm();
            Application.Run(present);
        }
    }
}