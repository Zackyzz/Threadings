using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threadssync
{
    class Program
    {
        static uint globalCounter=0;
        static uint max = 100;
        static uint t1, t2, ot1, ot2;
        static WinApiClass.WinApiClass.CRITICAL_SECTION criticala = new WinApiClass.WinApiClass.CRITICAL_SECTION();
        static uint Thread1Func(IntPtr q1)
        {
            while (globalCounter < max)
            {
                WinApiClass.WinApiClass.EnterCriticalSection(ref criticala);
                globalCounter++;
                Console.WriteLine("Thread 1: " + globalCounter);
                WinApiClass.WinApiClass.LeaveCriticalSection(ref criticala);
            }
            return 0;
        }
        static uint Thread2Func(IntPtr q1)
        {
            while (globalCounter < max)
            {
                WinApiClass.WinApiClass.EnterCriticalSection(ref criticala);
                globalCounter++;
                Console.WriteLine("Thread 2: " + globalCounter);
                WinApiClass.WinApiClass.LeaveCriticalSection(ref criticala);
            }
            return 0;
        }
        static void Main(string[] args)
        {
            WinApiClass.WinApiClass.InitializeCriticalSection(out criticala);
            t1= WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, Thread1Func, IntPtr.Zero, 0, out ot1);
            t2 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, Thread2Func, IntPtr.Zero, 0, out ot2);
            Console.ReadLine();
        }
    }
}
