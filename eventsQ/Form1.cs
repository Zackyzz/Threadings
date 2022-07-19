using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace eventsQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        uint t1 = 2000, t2 = 3000;
        IntPtr P1, P2, P3, P4, P5;
        bool qt6 = false;

        private uint ThreadP1(IntPtr q)
        {
            WinApiClass.WinApiClass.WaitForSingleObject(P1, WinApiClass.WinApiClass.INFINITE);
            while (progressBar1.Value <= 2000)
            {
                progressBar1.Value++;
                if (progressBar1.Value == 2000) {
                    WinApiClass.WinApiClass.SetEvent(P3);
                }
            }
            return 0;
        }

        private uint ThreadP2(IntPtr q)
        {
            WinApiClass.WinApiClass.WaitForSingleObject(P1, WinApiClass.WinApiClass.INFINITE);
            while (progressBar2.Value <= 3000)
            {
                progressBar2.Value++;
                if (progressBar2.Value == 3000)
                {
                    WinApiClass.WinApiClass.SetEvent(P4);
                }
            }
            return 0;
        }

        private uint ThreadP3(IntPtr q)
        {
            if (WinApiClass.WinApiClass.WaitForSingleObject(P3, t1) == WinApiClass.WinApiClass.WAIT_TIMEOUT)
            {
                while (progressBar3.Value <= 7000)
                    progressBar3.Value++;
                if (progressBar3.Value == 7000) {
                    qt6 = true;
                }
            }

            else {
                while (progressBar3.Value <= 7000)
                {
                    progressBar3.Value++;
                    if (progressBar3.Value == 7000)
                    {
                        qt6 = true;
                    }
                    while (progressBar1.Value < 10000)
                        progressBar1.Value++;
                }
            }
            return 0;
        }

        private uint ThreadP4(IntPtr q)
        {
            if (WinApiClass.WinApiClass.WaitForSingleObject(P4, t2) == WinApiClass.WinApiClass.WAIT_TIMEOUT)
            {
                while (progressBar4.Value <= 7000)
                    progressBar4.Value++;
                if (progressBar4.Value == 7000 && qt6==true)
                {
                    WinApiClass.WinApiClass.SetEvent(P5);
                }
            }

            else
            {
                while (progressBar4.Value <= 7000)
                {
                    progressBar4.Value++;

                    if (progressBar4.Value == 7000 && qt6 == true)
                    {
                        WinApiClass.WinApiClass.SetEvent(P5);
                    }
                    while (progressBar2.Value < 10000)
                        progressBar2.Value++;
                }
            }
            return 0;
        }

        private uint ThreadP5(IntPtr q)
        {
            WinApiClass.WinApiClass.WaitForSingleObject(P5, WinApiClass.WinApiClass.INFINITE);
            while (progressBar5.Value < 10000) {
                progressBar5.Value++;
            }
            while (progressBar1.Value < 10000)
                progressBar1.Value++;

            while (progressBar2.Value < 10000)
                progressBar2.Value++;

            while (progressBar3.Value < 10000)
                progressBar3.Value++;

            while (progressBar4.Value < 10000)
                progressBar4.Value++;

            return 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uint t1, t2, t3, t4, t5, ot1, ot2, ot3, ot4, ot5;

            P1 = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "P1");
            P2 = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "P2");
            P3 = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "P3");
            P4 = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "P4");
            P5 = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "P5");

            t1 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, ThreadP1, IntPtr.Zero, 0, out ot1);
            t2 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, ThreadP2, IntPtr.Zero, 0, out ot2);
            t3 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, ThreadP3, IntPtr.Zero, 0, out ot3);
            t4 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, ThreadP4, IntPtr.Zero, 0, out ot4);
            t5 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, ThreadP5, IntPtr.Zero, 0, out ot5);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WinApiClass.WinApiClass.SetEvent(P1);
            WinApiClass.WinApiClass.SetEvent(P2);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
