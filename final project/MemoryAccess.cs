using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace final_project
{
    class memory
    {
        private String getLoc;
           // public String setLoc;
        private String[] inputmem;
        public memory()
        {
            inputmem = new string[100];//sets array size
            for(int i = 0; i < inputmem.Length; i++)//zeros out array
            {
                inputmem[i] = "+0000";
            }
        }
        public String Getmem(int gLoc)//get location string
        {
            getLoc = inputmem[gLoc];
            return getLoc;
        }
        public void Setmem(String input, int sLoc)//set location string
        {
            inputmem[sLoc] = input;
        }
    }
}

