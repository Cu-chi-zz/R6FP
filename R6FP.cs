using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;
using WindowsInput;

namespace R6FP
{
    public partial class R6FP : Form
    {
        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vKey);

        const int VK_MBUTTON = 0x4;

        private bool running = false;
        private BackgroundWorker worker = new BackgroundWorker();
        private InputSimulator sim = new InputSimulator();

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
                short keyState = GetAsyncKeyState(VK_MBUTTON);
                Random r = new Random();
                bool kIsPressed = ((keyState >> 15) & 0x0001) == 0x0001;

                if (kIsPressed)
                {
                    sim.Mouse.LeftButtonClick();
                    Task.Delay(r.Next(5, 20));
                }
            }
        }
    }
}
