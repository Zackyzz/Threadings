using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delta
{
    class Program
    {
        static int a, b, c;
        static double delta, x1, x2;
        static IntPtr abcReady, deltaReady, x1Ready, x2Ready;
        static uint deltaThread(IntPtr q) {
            WinApiClass.WinApiClass.WaitForSingleObject(abcReady, WinApiClass.WinApiClass.INFINITE);

                delta = b * b - 4 * a * c;
                WinApiClass.WinApiClass.SetEvent(deltaReady);
            
            return 0;
        }

        static uint x1Thread(IntPtr q)
        {

            WinApiClass.WinApiClass.WaitForSingleObject(deltaReady, WinApiClass.WinApiClass.INFINITE);

                x1 = (-b + Math.Sqrt(delta)) / (2 * a);
                WinApiClass.WinApiClass.SetEvent(x1Ready);
            
            return 0;
        }
        static uint x2Thread(IntPtr q)
        {

            WinApiClass.WinApiClass.WaitForSingleObject(deltaReady, WinApiClass.WinApiClass.INFINITE); 
                x2 = (-b - Math.Sqrt(delta)) / (2 * a);
                WinApiClass.WinApiClass.SetEvent(x2Ready);
            
            return 0;
        }

        static void Main(string[] args)
        {
            uint t1, t2, t3, ot1, ot2, ot3;

            IntPtr[] qq = new IntPtr[] { x1Ready, x2Ready};

            
            t1 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, deltaThread, IntPtr.Zero, 0, out ot1);
            t2 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, x1Thread, IntPtr.Zero, 0, out ot2);
            t3 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, x2Thread, IntPtr.Zero, 0, out ot3);

            abcReady = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "abcReady");
            deltaReady = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "deltaReady");
            x1Ready = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "x1Ready");
            x2Ready = WinApiClass.WinApiClass.CreateEvent(IntPtr.Zero, true, false, "x2Ready");

            Console.WriteLine("Introdu a (numar intreg): ");
            a = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Introdu b (numar intreg): ");
            b = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Introdu c (numar intreg): ");
            c = Convert.ToInt32(Console.ReadLine());

            WinApiClass.WinApiClass.SetEvent(abcReady);

            WinApiClass.WinApiClass.WaitForMultipleObjects(2, qq, true, WinApiClass.WinApiClass.INFINITE);
            //WinApiClass.WinApiClass.WaitForSingleObject(x1Ready, WinApiClass.WinApiClass.INFINITE);
            //WinApiClass.WinApiClass.WaitForSingleObject(x1Ready, WinApiClass.WinApiClass.INFINITE);
            Console.WriteLine("x1= " + x1);
            Console.WriteLine("x2= " + x2);

            Console.ReadLine();
        }
    }
}
