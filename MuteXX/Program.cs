using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MuteXX
{
    class Program
    {

        static void Main(string[] args)
        {

            Thread.Sleep(5000);
            IntPtr text = WinApiClass.WinApiClass.CreateFile(@"C:\Users\desktop\source\repos\MuteXX\fasaki.txt",
            WinApiClass.WinApiClass.FileAccess.FILE_GENERIC_READ, 0, IntPtr.Zero, 
            WinApiClass.WinApiClass.FileMode.OPEN_EXISTING, WinApiClass.WinApiClass.FileAttributes.Normal, IntPtr.Zero);
            Console.WriteLine(WinApiClass.WinApiClass.GetLastError());


            IntPtr q1 = WinApiClass.WinApiClass.CreateMutex(IntPtr.Zero, true, "q1");

            FileInfo qt = new FileInfo(@"C:\Users\desktop\source\repos\MuteXX\fasaki.txt");
            long q2 = qt.Length;

            byte[] buffer = new byte[q2];
            uint bitiCititi=0;
            WinApiClass.WinApiClass.ReadFile(text, buffer, (uint)q2, out bitiCititi, IntPtr.Zero);
            

            /*for (int i = 0; i < q2; i++) {
                Console.WriteLine((char)buffer[i]);
            }*/



            Console.WriteLine(bitiCititi); 
            WinApiClass.WinApiClass.CloseHandle((uint)text);
            WinApiClass.WinApiClass.ReleaseMutex(q1);
            Console.ReadLine();



        }
    }
}
