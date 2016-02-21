using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpreter_pt3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool appRepeat = true;

            while (appRepeat)
            {
                try {
                    Console.Write("=>: ");
                    var currentLine = Console.ReadLine();
                    if (currentLine == "qt")
                    {
                        appRepeat = false;
                    }
                    else
                    {
                        var Interp = new Interpreter(currentLine);
                        Console.WriteLine(Interp.Interpret());
                    }
                }
                catch(ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
