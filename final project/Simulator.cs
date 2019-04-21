using System;
namespace final_project
{
    public class Simulator

    {
         memory mem = new memory();//creats the memory
        public String Accumulator = "";//accumulator 
        String InstructionRegister;//holds current instruction
        public int opperand;// hold opperand loc
        public int operrationCode;//holds code for operation to be done
        System.IO.StreamWriter file;
        Boolean kill = false;

  Translator translator;

        public Simulator()
        {
            
            Console.WriteLine("Running now");
            translator = new Translator();
            translator.begin();
        }

        public void calculation()
        {
            Console.WriteLine("give Output program file location");
            string location = Console.ReadLine();
            
            file = new System.IO.StreamWriter($@"{location}");

            Console.WriteLine("All input has been recived begining program....");

   


            for (int i = 0; i < 100; i++)
            {
                InstructionRegister = mem.Getmem(i);//take in instruction
                //Console.WriteLine(mem.Getmem(i));
                int holder = Int32.Parse(InstructionRegister);//turns instruction into a normal number
                operrationCode = holder / 100;//gets the code for what to do
                opperand = holder % 100;//gets the location for the program to use in the opperation
                //Console.WriteLine(i);
                //Thread.Sleep(500);

                switch (operrationCode)//determing which opperation to do
                {
                    case 10://reads input from keyboard
                        //Console.WriteLine(mem.Getmem(i));
                        read();
                        break;
                    case 11://write location to screen
                        Console.WriteLine(mem.Getmem(opperand));
                        file.WriteLine(mem.Getmem(opperand));
                        break;
                    case 20://loads loc to accumulator
                        load();
                        break;
                    case 21://stores accumulator to memory
                        store();
                        break;
                    case 30://multiplys mem location to the accumulator
                        multiply();
                        break;
                    case 31://divides mem location to the accumulator
                        divide();
                        break;
                    case 32://adds mem location to the accumulator
                        add();
                        break;
                    case 33://subtracts mem location to the accumulator
                        minus();
                        break;


                    case 40://just branches
                        Console.WriteLine($"controller{i}");
                        i = opperand -1;
                        break;
                    case 41:// branch if accum is neg
                        if (Accumulator.Substring(0, 1) == "-"){
                            i = opperand -1;
            }
                        break;
                    case 42: //branch is accum is zero 
                        if (Accumulator.Equals("+0000")){
                            i = opperand-1;
            }
                        break;
                    case 50://ends program
                        i = 100;
                        break;
                    case 00://ignores memory storage
                        break;
                    default://if incorrect opperation is given
                        error();
                        i = 100;
                        break;
                }
            }

            
            
                        Console.WriteLine("Begining Full memory Dump....");

            int counting = 0;
           for (int J = 0; J < 10; J++)//formats and dumps all of memory
            {
                for (int k = 0; k < 10; k++)
                {
                    mem.Getmem(counting);
                    Console.Write($"{mem.Getmem(counting)} ");
                    file.Write($"{mem.Getmem(counting)} ");
                    counting++;
                }
                Console.WriteLine();
                file.WriteLine();

            }
           file.Close();
           Console.WriteLine("Program completed press enter to quit.");
           Console.ReadLine();
        }


        public void error()//dumps error output
        {
            Console.WriteLine("#############Error Has occured Beginig memory dump...############");
            Console.WriteLine($"Acummulator:             {Accumulator}");
            Console.WriteLine($"Instruction Register:    {InstructionRegister}");
            Console.WriteLine($"Operation Code:          {operrationCode}");
            Console.WriteLine($"Opperand:                {opperand}");


            Console.WriteLine("Begining Full memory Dump....");




                        file.WriteLine("#############Error Has occured Beginig memory dump...############");
            file.WriteLine($"Acummulator:             {Accumulator}");
            file.WriteLine($"Instruction Register:    {InstructionRegister}");
            file.WriteLine($"Operation Code:          {operrationCode}");
            file.WriteLine($"Opperand:                {opperand}");


            file.WriteLine("Begining Full memory Dump....");

            int counting = 0;
           for (int J = 0; J < 10; J++)//formats and dumps all of memory
            {
                for (int k = 0; k < 10; k++)
                {
                    mem.Getmem(counting);
                    Console.Write($"{mem.Getmem(counting)} ");
                    file.Write($"{mem.Getmem(counting)} ");
                    counting++;
                }
                Console.WriteLine();
                file.WriteLine();


            }
        }






        public void read()//puts input int memory
        {
            
            //Console.WriteLine(mem.Getmem(0));
            Console.WriteLine("enter an number");
            int input = Int32.Parse(Console.ReadLine());
            string holding = ($"{input}");
            String convert;
            if (input > 9)//converts input into valid memory input
            {
                
                convert = $"+00{input}";
            }
            else if(input < 9 && input >= 0)
            {
                convert = $"+000{input}";
            }
            else
            {
                if(input > -9 && input < 0)
                {
                    holding = holding.Substring(1, 2);
                    convert = $"-000{holding}";
                }
                else
                {
                    holding = holding.Substring(1, 3);
                    convert = $"-00{holding}";
                }
            }

            mem.Setmem(convert, opperand);//stores converted input to memory

        }
        public void load()//load to accumulator
        {
            Accumulator = mem.Getmem(opperand);
        }
        public void store()//stores accumulator
        {
            mem.Setmem(Accumulator, opperand);
        }

        public void multiply()//multiply
        {
            int holding = Int32.Parse(Accumulator);
            int holding2 = Int32.Parse(mem.Getmem(opperand));
            int give = holding * holding2;
            converter(give);
        }


        public void divide()//division
        {
            int holding = Int32.Parse(Accumulator);
            int holding2 = Int32.Parse(mem.Getmem(opperand));
            if (holding2 != 0)//determisn if it is going to divide by zero
            {
                int give = holding / holding2;
                converter(give);
            }
            else
            {
 file.WriteLine("#######ERROR ATEMPTING TO DIVIDE BY ZERO###########");              
 Console.WriteLine("#######ERROR ATEMPTING TO DIVIDE BY ZERO###########");
            }
            
        }
        public void add()//addision
        {
            int holding = Int32.Parse(Accumulator);
            int holding2 = Int32.Parse(mem.Getmem(opperand));
            int give = holding + holding2;
            converter(give);
        }
        public void minus()//subtraction
        {
            int holding = Int32.Parse(Accumulator);
            int holding2 = Int32.Parse(mem.Getmem(opperand));
            int give = holding - holding2;
            converter(give);
        }















        private void converter(int result)//converts calculations into memory storage 
        {
            string restulhold = ($"{result}");

            if(result >100 || result < -100)//errors out if resulting number for math is too big for memory 
            {
                Console.WriteLine("Error current number is too big for program begining memory dump...");
                error();
            }
            //#############Formating code###############
            else if (result >= 0)
            {
                if (result < 10)
                {
                    Accumulator = ($"+000{result}");
                }
                else if (result < 100)
                {
                    Accumulator = ($"+00{result}");
                }
            }
            else if (result < 0)//converts if result is negative
            {
                
                if (result > -10)
                {
                    restulhold = restulhold.Substring(1, 1);
                    Accumulator = ($"-000{restulhold}");
                    
                }
                else if (result > -100)
                {
                    restulhold = restulhold.Substring(1, 2);
                    Accumulator = ($"-00{restulhold}");

                }
            }
        }









        public void reader()
        {
            //Console.WriteLine("Accepting Input now");
            if (kill == false)
            {
                kill = true;
                for (int l = 0; l < 100; l++){//dispalys input line
                
                    /*if (l < 10)
                    {
                        Console.Write($"0{l} ? ");

                    }
                    else
                    {
                        Console.Write($"{l} ? ");
                    }
                    */


                    string insert = translator.code[l];
                    // Console.WriteLine(insert);
                    
                    if (insert.Equals("-99999"))//begins program execution
                    {
                        l = 100;
                    }
                    else
                    {
                        mem.Setmem(insert, l);

                    }
                    }
                }
           }
            
       
    }
}