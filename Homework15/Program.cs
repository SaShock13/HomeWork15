using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homework15
{

    //Разработайте приложение, которое будет выводить сообщения в консоль в каждом потоке с разной задержкой.
    //Вывод должен быть следующим: текст вывода и имя потока в конце.
    //Совет: Можно использовать асинхронное программирование(async/await).

    internal class Program
    {
        static Random rnd = new Random();
        static object lockObj = new object();
        static int counter = 0;

        static void Main(string[] args)
        {
            //ParameterizedThreadStart pts = new ParameterizedThreadStart(ShowMessage);
            //Thread thread2 = new Thread(pts);

            //Thread thread3 = new Thread(pts);



            //thread2.Start(500);
            //thread3.Start(1000);
            //ShowMessage(250);

            ShowMessageAsync(100);

            Task<int> task = Task.Run(() => CalcFibAsync(20));
            ShowMessage(200);
            Console.WriteLine($"Main закончила работу в потоке {Thread.CurrentThread.ManagedThreadId}");
            Console.ReadLine();


            int fib = task.Result;
            Console.WriteLine($"Сумма фибоначи от 20 равна {fib}");


        }

        static async Task<bool> ShowMessageAsync(int pause)
        {
            
           
            await Task.Run(() => ShowMessage(pause));

            Console.WriteLine($"ShowMessageAsync закончила работу в потоке {Thread.CurrentThread.ManagedThreadId}");
            return true;
        }


        public static void ShowMessage(int pause)
        {
            
            for (int i = 0; i < 50; i++)
            {
                Thread.Sleep(pause);
                Console.WriteLine($"Работает поток {Thread.CurrentThread.GetHashCode()}");
                
                
            }
            
        }

        static async Task<int> CalcFibAsync(int n)
        {

            
            int sum = await Task.Run(() => Fib(n));

            Console.WriteLine($"Фибоначчи закончила работу в потоке {Thread.CurrentThread.ManagedThreadId}");

            return sum;
        }

        public static int Fib(int n)
        {

            Console.WriteLine($"ВХОД в метод фиб №{++counter}"); ;

            if (n == 0 || n == 1) return n;

            return Fib(n - 1) + Fib(n - 2);
        }
    }
}
