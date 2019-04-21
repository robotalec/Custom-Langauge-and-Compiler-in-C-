using System;
using System.Collections.Generic;
using System.Text;
namespace final_project
{
    public class Translator
    {
         static StackInheritance stack = new StackInheritance();
        static StringBuilder inFix;//inpuut
        static StringBuilder postFix = new StringBuilder("", 20);//output
        int toequal;
        public string[] code = new string[100];
        string[] theInput;
        public string[] symbolTable;
        public int[] noLoc;
        int symbolloc = 0;

       public int loc = 0;
        public int memLoc = 99;
        //bool ran = false;
        public Translator()
        {


                
                

        }
        public void begin(){
                        symbolTable = new string[100];
 
            int counting = 0;
            foreach (string line in code)
            {

                code[counting] = "+0000";
                counting++;
            }
            noLoc = new int[code.Length];
            
            input();
            translate();
}
        //initalize constants
        public void input()
        {
            Console.WriteLine("input program file location");
            string location = Console.ReadLine();
            theInput = System.IO.File.ReadAllLines($@"{location}");
        }

        public void translate()
        {
            int counter = 0;

            foreach (string line in theInput)
            {

                string[] holding = line.Split(' ');
                for (int k = 0; k<holding.Length;k++)
                {
                    //counter = Convert.ToInt32(holding[0]);
                    switch (holding[k])
                    {
                        case "rem":
                            rem(holding);
                            break;
                        case "input":
                            input(holding); 
                            //Console.WriteLine(loc);
                            break;

                        case "print":
                                print(holding[2]);
                            break;

                        case "goto":
                            loc = Convert.ToInt32(holding[2]);
                            code[counter] = ($"+40{loc}");
                            break;

                        case "end":   
                            code[loc] = ("+5000");
                            loc++;
                            break;

                        case "let":
                            constAdding(holding);//takes the let and adds any cosntant to symbol table
                            let(holding);

                            break;
                        case "if":

                            break;

                    }

                    counter++;
                }

            }
        }


        public void rem(string[] holding)//adds label of rem to table
        {
            labelAdding(holding);
        }



        public void input(string[] holding)//creates input code and sores it all to code array
        {
            labelAdding(holding);
            symboltable(holding[2]);
            string holder = holding[2];
            int location = variableFinder(holder);
            code[loc] = ($"+10{location}");
            loc++;
        }


        public void print(string holding){//writes code to print
                        string holder = holding;
            int location = variableFinder(holder);
            code[loc] = ($"+11{location}");
            loc++;
}


        public void go_to(string[] holding, int loc){//WIP
            //LabelTable(holding, loc);
            }



        public void symboltable(string holding)//checks if variable exists or not
        {
            bool test = false;

            int testingmem = memLoc;
            for (int i = 0; i < symbolTable.Length; i++)
            {
                for (int o = 0; o < symbolTable.Length; o++)
                {
                    if (symbolTable[o] == ($"{holding},v,{testingmem}"))
                    {
                        test = true;
                    }
                }
                testingmem--;
            }
           if (test == false)
            {
                variableAdding(holding);
            }
        }
        /*public void LabelTable(string holding)//tests if label in goto is already existing
        {
            bool test = false;

            int testingmem = memLoc;
            for (int i = 0; i < symbolTable.Length; i++)
            {
                for (int o = 0; o < symbolTable.Length; o++)
                {
                    if (symbolTable[o] == ($"{holding},l,{testingmem}"))
                    {
                        test = true;
                    }
                }
                testingmem--;
            }
           if (test == false)
            {
                misclabel(holding);
            }
        }


        public void misLabel(string label){

            noLoc[loc] = Convert.ToInt32(label);//if location for goto non existing then store the location beeing looked for into array at location of the goto


}
*/

        public void variableAdding(string holding)//add variable to table
        {

            if(memLoc != 95){//saves loc 95 for temp mem

                    symbolTable[symbolloc] = ($"{holding},v,{memLoc}");
                    memLoc--;
            symbolloc++;
            }else{
                memLoc--;
        }

        }



        public void constAdding(string[] holding){//adds constants to table

            for(int i = 1; i < holding.Length; i++){

                var Numeric = int.TryParse(holding[i], out int n);
                if(Numeric){
                    if(memLoc != 95){
                symbolTable[symbolloc] = ($"{n},c,{memLoc}");
                        if(n < 10){//stores number into the memory
                            code[memLoc] = ($"+000{n}");
}else if(n >=10 && n < 100)
{
                            code[memLoc] = ($"+00{n}");
}

                    memLoc--;
                    symbolloc++;
                        }else{
                        memLoc--;
}

}



}

}



        public void labelAdding(string[] holding)//add labels to table
        {
            if(holding[1] != "rem")
            {
                symbolTable[symbolloc] = ($"{holding[0]},L,{loc}");
                loc++;
                symbolloc++;
            }
            else
            {
                symbolTable[symbolloc] = ($"{holding[0]},L,{loc}");
                symbolloc++;
            }
        }



        public int variableFinder(string variable){//looks for variable in symbol table and return the location of variable
            int loc = 0;
            for(int b = 0; b < symbolTable.Length; b++){
                if(symbolTable[b] != null){
                string[] tableFinder = symbolTable[b].Split(',');
                if(tableFinder[0].Equals(variable)){
                    loc = Convert.ToInt32(tableFinder[2]);
}
}
}
            return loc;
}                                  










        public void let(string[] holding){//builds an equation using memory locations

        symboltable(holding[2]);
            StringBuilder equation = new StringBuilder("",holding.Length);
            for(int c = 4; c < holding.Length; c++){
                int checker = variableFinder(holding[c]);
                int number = 0;
                var isnumeric = int.TryParse(holding[c], out number);
                if(checker != 0){
                    equation.Append(checker.ToString());
}else{
                    equation.Append(holding[c]);
}
                Console.WriteLine(equation);
}
            mathTranslator(equation.ToString(), holding);
            code[loc] = ($"+21{variableFinder(holding[2])}");
            loc++;
}














    





        


       public  void mathTranslator(string equation, string[] holding){
            inFix= new StringBuilder(equation, 20);
            toequal = variableFinder(holding[2]);
            infixtoPostfix();
            postFixEvaluation();
            Console.ReadLine();
            
}





        public void infixtoPostfix()
        {
            
            inFix.Append(new char[] { ')' });
            stack.Push("(");
            do
            {
                for (int i = 0; i < inFix.Length; i++)
                {
                    char ch = inFix[i];
                    if (Char.IsNumber(ch))
                    {

                        postFix.Append(new char[] { ch });
                        if (Char.IsNumber(inFix[i + 1]))//to determine if next character is a number or not
                        {

                        }
                        else
                        {
                            postFix.Append(" ");//puts a space after each number
                        }
                    }
                    else if (ch == '(')//puch left if left is in infix
                    {
                        stack.Push("(");
                    }
                    else if (ch == '+' || ch == '-' || ch == '/' || ch == '*')//if operator
                    {
                        presidence(ch);
                        //postFix.Append(" ");
                    }
                    else if (ch == ')')//end part
                    {
                        do
                        {
                            string checker = stack.Peek();
                            stack.Pop();
                            postFix.Append(checker);
                        } while (stack.Peek() != "(");
                        stack.Pop();
                    }

                }
            } while (stack.expressions.Count != 0);

            // Console.WriteLine(stack.Peek());
            //Console.WriteLine(inFix[3]);



            Console.WriteLine(postFix);

            

        }





        public void postFixEvaluation()
        {
            
            postFix.Append(")");
            string post = postFix.ToString();
            string[] holder = post.Split(' ');


            int count = 0;
            code[loc] =($"+20{holder[0]}");
            loc++;
            char check;
            string toStack = "";

            do
            {

                check = postFix[count];

                if (Char.IsNumber(check))
                {
                    toStack = ($"{toStack}{check}");


                }else if(check == ' ' && toStack != "")
                    {

                    stack.Push(toStack);
                    toStack = "";


                }else if (check == '+' || check == '-' || check == '/' || check == '*')
                {
                    switch (check)
                    {
                        case '+':
                            int op2 = Convert.ToInt32(stack.Peek());
                            stack.Pop();
                            int op1 = Convert.ToInt32(stack.Peek());
                            stack.Pop();

                            code[loc] = ($"+32{op2}");
                            loc++;
                            code[loc] = ($"+2195");
                            loc++;
                            stack.Push("95");
                            break;
                        case '-':
                            int op4 = Convert.ToInt32(stack.Peek());
                            stack.Pop();
                            int op3 = Convert.ToInt32(stack.Peek());
                            stack.Pop();
                            code[loc] = ($"+33{op4}");
                            loc++;
                            code[loc] = ($"+2195");
                            loc++;
                            stack.Push("95");
                            break;
                        case '*':
                            int op6 = Convert.ToInt32(stack.Peek());
                            stack.Pop();
                            int op5 = Convert.ToInt32(stack.Peek());
                            stack.Pop();
                            code[loc] = ($"+30{op6}");
                            loc++;
                            code[loc] = ($"+2195");
                            loc++;
                            stack.Push("95");
                            break;
                        case '/':
                            int op8 = Convert.ToInt32(stack.Peek());
                            stack.Pop();
                            int op7 = Convert.ToInt32(stack.Peek());
                            stack.Pop();
                            code[loc] = ($"+31{op8}");
                            loc++;
                            code[loc] = ($"+2195");
                            loc++;
                            stack.Push("95");
                            break;
                    }
                    code[loc] = ($"+2095");
                    loc++;
                }

                count++;
            } while (check != ')');
            //code[loc] = ($"+21{variableFinder(holder[2])}");
            //loc++;
            //Console.WriteLine(stack.Peek());

        }






         void presidence(char ch)
        {
            string check = stack.Peek();
            if (check == "+" || check == "-" || check == "/" || check == "*")//cheacks if stack has operator on top
            {
                switch (ch)
                {
                    case '+':
                        string hold = stack.Peek();
                        stack.Pop();
                        postFix.Append(hold);
                        stack.Push(ch.ToString());

                        break;
                    case '-':
                        string hold2 = stack.Peek();
                        stack.Pop();
                        postFix.Append(hold2);
                        stack.Push(ch.ToString());
                        break;
                    case '*':
                        if (check == "/" || check == "*")
                        {
                            string hold3 = stack.Peek();
                            stack.Pop();
                            postFix.Append(hold3);
                            stack.Push(ch.ToString());
                        }
                        else
                        {
                            stack.Push(ch.ToString());
                        }
                        break;
                    case '/':
                        if (check == "/" || check == "*")
                        {
                            string hold4 = stack.Peek();
                            stack.Pop();
                            postFix.Append(hold4);
                            stack.Push(ch.ToString());
                        }
                        else
                        {
                            stack.Push(ch.ToString());
                        }
                        break;
                }
            }
            else
            {
                stack.Push(ch.ToString());
                // Console.WriteLine(ch);
            }
        }

    }




    





    public class StackInheritance
    {
        public Stack<string> expressions;
        public StackInheritance()

        {
            expressions = new Stack<string>();
        }


        public void Push(string expres)
        {
            expressions.Push(expres);
        }

        public void Pop()
        {
            expressions.Pop();
        }
        public string Peek()
        {
            return expressions.Peek();
        }


}



}
