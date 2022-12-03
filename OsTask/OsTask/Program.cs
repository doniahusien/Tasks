using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace OsTask
{
    internal class Program
    {
        static Semaphore[] Fork = new Semaphore[5];
        static Semaphore m = new Semaphore(1, 1);
        static void Main(string[] args)
        {
            for (int i = 0; i < Fork.Length; i++)
            {
                Fork[i] = new Semaphore(1, 1);
            }
            Thread ph1 = new Thread(() => { setting(0); });
            Thread ph2 = new Thread(() => { setting(1); });
            Thread ph3 = new Thread(() => { setting(2); });
            Thread ph4 = new Thread(() => { setting(3); });
            Thread ph5 = new Thread(() => { setting(4); });
            ph1.Start();
            ph2.Start();
            ph3.Start();
            ph4.Start();
            ph5.Start();
        }
        static void set(int ph_n)
        {
            do
            {
               m.WaitOne();
                Fork[ph_n].WaitOne();
                Fork[(ph_n + 1) % 5].WaitOne();
                m.Release();
                Console.WriteLine("philsopher {0} is eating", ph_n);
                Fork[ph_n].Release();
                Fork[(ph_n + 1) % 5].Release();
                Console.WriteLine("philsopher {0} is Thinking", ph_n);
            } while (true);
        }
    }
}
