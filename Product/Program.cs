using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace produs
{
    class Program
    {

        static WinApiClass.WinApiClass.CRITICAL_SECTION criticala = new WinApiClass.WinApiClass.CRITICAL_SECTION();
        static uint[] a = new uint[33];
        static uint[] b = new uint[33];
        static uint index = 0, suma = 0;

        static uint Thread1Func(IntPtr q)
        {
            while (index < a.Length)
            {
                WinApiClass.WinApiClass.EnterCriticalSection(ref criticala);
                if (index < a.Length)
                {
                    suma += a[index] * b[index];
                    index++;
                }
                WinApiClass.WinApiClass.LeaveCriticalSection(ref criticala);
            }
            return 0;
        }

        static uint Thread2Func(IntPtr q)
        {
            while (index < a.Length)
            {
                WinApiClass.WinApiClass.EnterCriticalSection(ref criticala);
                if (index < a.Length)
                {
                    suma += a[index] * b[index];
                    index++;
                }
                WinApiClass.WinApiClass.LeaveCriticalSection(ref criticala);
            }
            return 0;
        }

        static uint Thread3Func(IntPtr q)
        {
            while (index < a.Length)
            {
                WinApiClass.WinApiClass.EnterCriticalSection(ref criticala);
                if (index < a.Length)
                {
                    suma += a[index] * b[index];
                    index++;
                }
                WinApiClass.WinApiClass.LeaveCriticalSection(ref criticala);
            }
            return 0;
        }

        static uint Thread4Func(IntPtr q)
        {
            WinApiClass.WinApiClass.EnterCriticalSection(ref criticala);
            if (index == a.Length)
            {
                Console.WriteLine(suma);
            }
            WinApiClass.WinApiClass.LeaveCriticalSection(ref criticala);
            return 0;
        }

        static void Main(string[] args)
        {
            uint t1, t2, t3, t4, ot1, ot2, ot3, ot4;

            var rand = new Random();
            for (uint i = 0; i < a.Length; i++)
            {
                a[i] = Convert.ToUInt32(rand.Next(100));
                b[i] = Convert.ToUInt32(rand.Next(100));
            }

            Console.WriteLine("a: b: ");
            for (uint i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i] + " " + b[i]);
            }

            Console.WriteLine("Produsul scalar este: ");
            // Console.WriteLine(suma);

            WinApiClass.WinApiClass.InitializeCriticalSection(out criticala);
            t1 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, Thread1Func, IntPtr.Zero, 0, out ot1);
            t2 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, Thread2Func, IntPtr.Zero, 0, out ot2);
            t3 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, Thread3Func, IntPtr.Zero, 0, out ot3);
            t4 = WinApiClass.WinApiClass.CreateThread(IntPtr.Zero, 0, Thread4Func, IntPtr.Zero, 0, out ot4);

            Console.ReadLine();
        }
    }
}