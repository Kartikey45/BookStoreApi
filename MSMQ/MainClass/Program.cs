using MailKit;
using MSMQ.MsmqListener;
using System;
using System.Collections.Generic;
using System.Text;
using Experimental.System.Messaging;

namespace MSMQ.MainClass
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var listener = new Listener(@".\Private$\MyQueue");
            listener.MessageReceived += new MessageReceivedEventHandler(listener_MessageReceived);
            listener.Start();
            Console.WriteLine("Read Message");
            Console.ReadLine();
            listener.Stop();
        }

        public static void listener_MessageReceived(object sender, MsmqListener.MessageEventArgs args)
        {
            Console.WriteLine(args.MessageBody);
        }
    }
}
