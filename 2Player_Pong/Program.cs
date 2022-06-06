using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace _2Player_Pong
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.CursorVisible = false;
            pong pong = new pong(75, 25);
            pong.Run();

            Console.ReadKey();
        }
    }
}
