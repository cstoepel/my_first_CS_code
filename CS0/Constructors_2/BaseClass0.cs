using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructors_2
{
    internal class BaseClass0
    {
        // Konstruktor muss Public sein damit Kind Konstruktor sichbar ist?!
        public BaseClass0() { Console.Write("Konstruktor: |BaseClass0|\n"); }

        public int basis {  get; set; }
    }
}
