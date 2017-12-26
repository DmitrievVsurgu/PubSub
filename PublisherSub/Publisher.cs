using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using ZeroMQ;

namespace Examples
{
    class Publisher
    {
        public static void Main(string[] args)
        {
            using (var context = new ZContext())
            using (var publisher = new ZSocket(context, ZSocketType.PUB))
            {
                //publisher.Linger = TimeSpan.Zero;
                publisher.Bind("tcp://*:5555");

                int published = 0;
                while (true)
                {

                    using (var message = new ZMessage())
                    {
                        published++;
                        message.Add(new ZFrame(string.Format("B {0}", published)));
                        message.Add(new ZFrame(string.Format("Still to New Year {0} days", (new DateTime(DateTime.Now.Year + 1, 01, 01) - DateTime.Now).Days.ToString())));
                        Thread.Sleep(10000);

                        Console.WriteLine("Publishing" + " the message is sent");
                        publisher.Send(message);
                    }

                    using (var message = new ZMessage())
                    {
                        published++;
                        message.Add(new ZFrame(string.Format("A {0}", published)));
                        message.Add(new ZFrame(string.Format("Heppy New Year!!!")));
                        Thread.Sleep(10000);

                        Console.WriteLine("Publishing " + "the message is sent");
                        publisher.Send(message);
                    }
                }
            }
        }
    }
}