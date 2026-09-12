using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ManagerWall
{
    internal class UI
    {
        public const string RESET = "\x1b[0m";
        public const string GREEN = "\x1b[32m";
        public const string RED = "\x1b[31m";
        public const string BLUE = "\x1b[34m";
        public const string YELLOW = "\x1b[33m";
        public const string CYAN = "\x1b[36m";
        public const string WHITE = "\x1b[37m";
        public const string BOLD = "\x1b[1m";

        public static void DrawLogo()
        {
            Console.WriteLine($"{BOLD}{CYAN}");
            Console.WriteLine("███╗░░░███╗░█████╗░███╗░░██╗░█████╗░░██████╗░███████╗██████╗░░██╗░░░░░░░██╗░█████╗░██╗░░░░░██╗░░░░░\r\n████╗░████║██╔══██╗████╗░██║██╔══██╗██╔════╝░██╔════╝██╔══██╗░██║░░██╗░░██║██╔══██╗██║░░░░░██║░░░░░\r\n██╔████╔██║███████║██╔██╗██║███████║██║░░██╗░█████╗░░██████╔╝░╚██╗████╗██╔╝███████║██║░░░░░██║░░░░░\r\n██║╚██╔╝██║██╔══██║██║╚████║██╔══██║██║░░╚██╗██╔══╝░░██╔══██╗░░████╔═████║░██╔══██║██║░░░░░██║░░░░░\r\n██║░╚═╝░██║██║░░██║██║░╚███║██║░░██║╚██████╔╝███████╗██║░░██║░░╚██╔╝░╚██╔╝░██║░░██║███████╗███████╗\r\n╚═╝░░░░░╚═╝╚═╝░░╚═╝╚═╝░░╚══╝╚═╝░░╚═╝░╚═════╝░╚══════╝╚═╝░░╚═╝░░░╚═╝░░░╚═╝░░╚═╝░░╚═╝╚══════╝╚══════╝");
            Console.WriteLine($"{RESET}");
        }



    }
}
