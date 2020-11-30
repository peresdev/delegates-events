using System;
using System.Threading;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            NumberGenerator g = new NumberGenerator();
            g.OnGenerated += G_OnGenerated;
            g.Start();
        }

        private static void G_OnGenerated(object sender, NumberEventArgs args)
        {
            Console.WriteLine("Número gerado: " + args.Number);
        }
    }

    public delegate void NumberHandler(object sender, NumberEventArgs args);

    class NumberGenerator
    {
        public event NumberHandler OnGenerated;

        Random r = new Random();

        public void Start()
        {
            while (true)
            {
                int n = r.Next(100);

                if (OnGenerated != null)
                {
                    NumberEventArgs args = new NumberEventArgs() { Number = n };
                    OnGenerated(this, args);
                }

                Thread.Sleep(1000);
            }
        }
    }

    public class NumberEventArgs : EventArgs
    {
        public int Number { get; set; }
    }
}
