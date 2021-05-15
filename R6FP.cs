using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R6FP
{
    public partial class R6FP : Form
    {
        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(int vKey);
        private static int VK_LSHIFT = 0xA0;

        private bool running = false;
        private BackgroundWorker worker = new BackgroundWorker();
        public R6FP()
        {
            InitializeComponent();
            worker.DoWork += new DoWorkEventHandler(FastPeek);
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            running = !running;

            startButton.Text = $"{(running ? "STOP" : "START")}";
            Text = $"{(running ? "R6FastPeek :: Working" : "R6FastPeek")}";

            if (running)
                worker.RunWorkerAsync();
        }

        private void FastPeek(object sender, DoWorkEventArgs e) 
        { 
            while (running)
            {
                short keyState = GetAsyncKeyState(VK_LSHIFT);
                

                bool prntScrnIsPressed = ((keyState >> 15) & 0x0001) == 0x0001;

                if (prntScrnIsPressed)
                {
                    Console.Beep();
                    // To-Do
                }
            }
        }
    }
}
