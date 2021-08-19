using System;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**************************");
            Console.WriteLine("*** Welcome to TOY ROBOT ***");
            Console.WriteLine("**************************");
            Console.WriteLine("Type EXIT to close the app");
            Console.WriteLine("");
            Console.WriteLine("Commands:");
            Console.WriteLine("---------");
            Console.WriteLine("PLACE X,Y,DIRECTION");
            Console.WriteLine("MOVE");
            Console.WriteLine("LEFT");
            Console.WriteLine("RIGHT");
            Console.WriteLine("REPORT");
            Console.WriteLine("---------");
            Console.WriteLine("");

            Commands command = new Commands();
            var exit = false;
            do
            {
                string cmd = Console.ReadLine();
                if (!cmd.ToLower().Equals("exit"))
                {
                    string ret = command.RunCommand(cmd);
                    if (!string.IsNullOrEmpty(ret)) Console.WriteLine(ret);
                }
                else
                {
                    exit = !exit;
                }

            } while (!exit);
        }
    }
}
