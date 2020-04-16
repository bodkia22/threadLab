using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLab1
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Thread t=new Thread(WriteY);
        //    t.Start();

        //    for (int i = 0; i < 50; i++)
        //    {
        //        Console.WriteLine("x");
        //    }
        //}

        //static void WriteY()
        //{
        //    for (int i = 0; i < 50; i++)
        //    {
        //        Console.WriteLine("y");
        //    }
        //}

        //task2
        //static void Main()
        //{
        //    Console.WriteLine("Main thread has id:" + Thread.CurrentThread.ManagedThreadId);

        //    new Thread(Go).Start();
        //    Go();
        //}


        //static void Go()
        //{
        //    for (int cycles = 0; cycles < 5; cycles++)
        //        Console.WriteLine($"P {Thread.CurrentThread.ManagedThreadId}");
        //}

        //task3

        //class ThreadTest
        //{
        //    bool done = false;
        //    static void Main()
        //    {
        //        ThreadTest tt = new ThreadTest();
        //        new Thread(tt.Go).Start();
        //        tt.Go();
        //    }
        //    void Go()
        //    {
        //        Console.WriteLine($"Thread witch call the function {Thread.CurrentThread.ManagedThreadId}");
        //        if (!done)
        //        {
        //            done = true; Console.WriteLine($"Done with thread {Thread.CurrentThread.ManagedThreadId}");
        //        }
        //    }
        //}

        class ThreadTest
        {
            static bool done; // Статическое поле, разделяемое потоками

            static void Main()
            {
                new Thread(Go).Start();
                Go();
            }

            //    static void Go()
            //    {
            //        Console.WriteLine($"Thread witch call the function {Thread.CurrentThread.ManagedThreadId}");
            //        if (!done) { done = true; Console.WriteLine("Done"); }
            //    }
            //} 
            static void Go()
            {
                if (!done)
                {
                    Console.WriteLine($"Thread witch call the function {Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine("Done");
                    done = true;
                }
            }
        }
    }
}
