using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project
{
    class Program
    {
        
        static void Main(string[] args)
        {
          
           
            //reading.input();
            Simulator sim = new Simulator();//creats a sim to begin
            sim.reader();//begins readin in the commands
            sim.calculation();//starts the prgram that put in
        }
    }
}
