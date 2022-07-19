using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Semafor
{
    public partial class Form1 : Form
    {
        static int nmax;
        static int opene = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

            WinApiClass.WinApiClass.SECURITY_ATTRIBUTES security = new WinApiClass.WinApiClass.SECURITY_ATTRIBUTES();
            IntPtr semaphore = WinApiClass.WinApiClass.CreateSemaphore(ref security, opene, nmax, "semaphore");

            nmax = Int16.Parse(textBox1.Text);
            uint dwWaitResult;
            dwWaitResult = WinApiClass.WinApiClass.WaitForSingleObject(semaphore, (uint)0L);
            switch (dwWaitResult)
            {
                case WinApiClass.WinApiClass.WAIT_OBJECT_0:
                    Form1 myForm = new Form1();
                    opene++;
                    myForm.ShowDialog();
                    //IntPtr opensem = WinApiClass.WinApiClass.OpenSemaphore(1, true, "opensem");
                    break;
                case WinApiClass.WinApiClass.WAIT_TIMEOUT:
                    if (opene==nmax)
                        MessageBox.Show("Prea multe ferestre");
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            WinApiClass.WinApiClass.ReleaseSemaphore(semaphore, 1, IntPtr.Zero);
            opene--;
        }
    }
}
