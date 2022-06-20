using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace equationSolve
{
    public class solver
    {
        //will get an expression and reformat it to get like terms merged together
        public static expression putInTerms(string equation)
        {
            string spitEquation = "";
            string[] terms = equation.Split(" ");
            string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string[] doNotCheck = new string[0];
            string[] termTypes = new string[0];
            double constants = 0;
            string[] values = new string[0];
            for (int i = 0; i < terms.Length; i++)
            {
                char[] getTerm = terms[i].ToCharArray();
                if (!isIn(getTerm[getTerm.Length - 1].ToString(), numbers))
                {
                    termTypes = push(termTypes, getTerm[getTerm.Length - 1].ToString());
                }
                else
                {
                    constants += double.Parse(terms[i]);
                }
            }
            //printArr(termTypes);
            for (int j = 0; j < termTypes.Length; j++)
            {
                if (!isIn(termTypes[j], doNotCheck))
                {
                    string[] termWhere = new string[0];
                    for (int i = 0; i < terms.Length; i++)
                    {
                        char[] getTerm = terms[i].ToCharArray();
                        if (!isIn(getTerm[getTerm.Length - 1].ToString(), numbers))
                        {
                            if (getTerm[getTerm.Length - 1].ToString().Equals(termTypes[j]))
                            {
                                termWhere = push(termWhere, i.ToString());
                            }
                        }
                    }
                    /*Console.WriteLine("term " + termTypes[j]);
                    printArr(termWhere);
                    Console.WriteLine("end");*/
                    double addTerm = 0;
                    for (int i = 0; i < termWhere.Length; i++)
                    {
                        //Console.WriteLine(termWhere[i]);
                        //Console.WriteLine(double.Parse(eraseVar("500x")));
                        //Console.WriteLine(terms[int.Parse(termWhere[i])]);
                        addTerm += double.Parse(eraseVar(terms[int.Parse(termWhere[i])]));
                    }
                    values = push(values, addTerm.ToString());
                    //Console.WriteLine(addTerm+termTypes[j]);
                    spitEquation += addTerm.ToString() + termTypes[j]+" ";
                    doNotCheck = push(doNotCheck, termTypes[j]);
                }
            }

            spitEquation += constants.ToString();

            return new expression(spitEquation, doNotCheck, values,constants.ToString());
        }

        //checks if an element exists in an array
        public static bool isIn(string el, string[] arr)
        {
            bool elFound = false;
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(el))
                {
                    elFound = true;
                    break;
                }
            }
            return elFound;
        }

        //gets an array then returns an array with how many of an element there is and where;
        public static string[] findAll(string el, string[] arr)
        {
            int howMany = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(el)) 
                { 
                    howMany++;
                }
            }
            string[] where = new string[howMany];
            int index = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(el))
                {
                    where[index] = i.ToString();
                    index++;
                }
            }
            return where;
        }

        //turns a char array into a string array
        public static string[] turnTostring(char[] arr)
        {
            string[] toString = new string[arr.Length];

            for(int i = 0; i < arr.Length; i++)
            {
                toString[i] = arr[i].ToString();
            }
            return toString;
        }

        //prints out all the element in a string array
        public static void printArr(string[] arr)
        {
            foreach(string str in arr)
            {
                Console.WriteLine(str);
            }
        }

        //returns a string array with element wanted added into it
        public static string[] push(string[] arr,string el)
        {
            string[] holderArr = new string[arr.Length+1];
            for(int i = 0; i < arr.Length; i++)
            {
                holderArr[i] = arr[i];
            }
            holderArr[holderArr.Length-1] = el;
            return holderArr;
        }
        public static string[] remove(string[] arr,string el)
        {
            string[] holderArr = new string[0];
            for(int i = 0; i < arr.Length; i++)
            {
                if(!arr[i].Equals(el))
                {
                    holderArr = push(holderArr,arr[i]);
                }
            }
            return holderArr;
        }
        //removes an element from an array given array and the place to remove it
        public static string[] removeByIndex(string[] arr,int place)
        {
            string[] holderArr = new string[0];
            for(int i = 0; i < arr.Length; i++)
            {
                if (i != place)
                {
                    holderArr = push(holderArr, arr[i]);
                }
            }
            return holderArr;
        }
        public static string eraseVar(string numVar)
        {
            string[] symbol = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9","-","." };
            string num = "";
            string[] parts = turnTostring(numVar.ToCharArray());
            for(int i = 0; i < parts.Length; i++)
            {
                if (isIn(parts[i], symbol))
                {
                    num += parts[i];
                }
                else
                {
                    break;
                }
            }
            return num;

        }
        /*public static void solve(equation equ1, equation equ2, string solveFor, string Cancel)
        {
            equation NewEqu1 = equation.replicate(equ1);
            equation NewEqu2 = equation.replicate(equ2);

            NewEqu2.mult((double.Parse(NewEqu1.exp2.getValueFor(Cancel)) / double.Parse(NewEqu2.exp2.getValueFor(Cancel)))*-1);
            equation Retequ = equation.addEqu(NewEqu1, NewEqu2);

        }*/

        //graps n-1 ammount of equation from an array.
        public static equation[] grap(equation[] equs)
        {
            equation[] newequs = new equation[equs.Length - 1];
            for(int i = 0; i < newequs.Length; i++)
            {
                newequs[i] = equs[i];
            }
            return newequs;
        }

        //work on this for later!!!!!!
        public static expression solvefor(equation[] equs,string solvefor)
        {

            
            string[] terms = equs[0].exp2.getTerms();
            terms = remove(terms,solvefor);
            equation equ1 = equation.replicate(equs[0]);
            
            for (int i = 1; i < equs.Length; i++)
            {
                equation equ2 = equation.replicate(equs[i]);
                //Console.WriteLine(terms[i - 1]+" " + equ2.exp2.getValueFor(terms[i-1]));
                equ1 = partlysolve(equ1, equ2, terms[i-1]);
            }
            //check if this part works by mathing it out when I get home.
            equ1.isolate(solvefor);
            //Console.WriteLine(equ1.getEquation());
            equ1.div(double.Parse(equ1.exp1.getValueFor(solvefor)));
            //Console.WriteLine(equ1.getEquation());
            return putInTerms(equ1.exp2.getExpression());
        }

        public static expression[] solveAll(equation[] equs)
        {
            equation[] allEqus = equation.replicateEqus(equs);

            string[] allTerms = allEqus[0].exp2.getTerms();
            expression[] allValues = new expression[allTerms.Length];
            for(int i = 0; i < allTerms.Length; i++)
            {
                allValues[i] = putInTerms(("1" + allTerms[i]));
            }


            for(int i = 0; i < allTerms.Length; i++)
            {
                
                /*for(int j = 0; j < allValues.Length; j++)
                {
                    Console.WriteLine(j+": "+allValues[j].getExpression()+"~");
                }*/

                expression newExp = solvefor(allEqus, allTerms[i]);
                //Console.WriteLine(allEqus.Length);
                //Console.WriteLine(newExp.getExpression());
                for(int j = 0; j < allEqus.Length; j++)
                {

                    //Console.WriteLine(allEqus[j].getEquation());
                    allEqus[j].exp2.replace(allTerms[i], newExp);
                    //Console.WriteLine(allEqus[j].getEquation());
                    //Console.WriteLine(newExp.getExpression());
                }
                //Console.WriteLine("hello");
                allValues[i].replace(allTerms[i], newExp);
                allEqus = grap(allEqus);
                for(int j = 0; j < allEqus.Length; j++)
                {
                    allEqus[j].exp2.cleanUp();
                    //Console.WriteLine(allEqus[j].getEquation());
                }
            }

            /*for(int i = 0; i < allValues.Length; i++)
            {
                Console.WriteLine(allTerms[i]+" : " + allValues[i].getExpression());
            }*/
            for(int i = allValues.Length-3; i > -1; i--)
            {
                string[] whichVar = allValues[i].findWhichNonZero();

                for(int j = 0; j < whichVar.Length; j++)
                {
                    int num = find(allTerms, whichVar[j]);

                    allValues[i].replace(whichVar[j],putInTerms(allValues[num].getConstant()));
                }
            }
            //Console.WriteLine("\n");

            /*for (int i = 0; i < allValues.Length; i++)
            {
                Console.WriteLine(allTerms[i] + " : " + allValues[i].getExpression());
            }*/
            return allValues;
        }

        public static int find(string[] arr,string el)
        {
            int index = -1;
            for(int i = 0; i < arr.Length; i++)
            {
                if (el.Equals(arr[i]))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }
        public static equation partlysolve(equation equ1, equation equ2,string Cancel)
        {
            equation NewEqu1 = equation.replicate(equ1);
            equation NewEqu2 = equation.replicate(equ2);

            NewEqu2.mult((double.Parse(NewEqu1.exp2.getValueFor(Cancel)) / double.Parse(NewEqu2.exp2.getValueFor(Cancel))) * -1);
            equation Retequ = equation.addEqu(NewEqu1, NewEqu2);
            
            return Retequ;
        }
        public static double random(double range1, double range2)
        {
            Random random = new Random();
            return range1 + (random.NextDouble() * (range2 - range1));
        }
        public static int randint(int smallest, int biggest)
        {
            Random random = new Random();
            return random.Next(biggest-smallest)+smallest;
        }
        public static points readPoints(string point)
        {
            string[] myPoint = point.Split(",");
            string myX = myPoint[0].Split("(")[1];
            //Console.WriteLine(myX);
            string myY = myPoint[1].Split(")")[0];
            //Console.WriteLine(myY);
            return new points(double.Parse(myX), double.Parse(myY));
        }
        public static points[] readMultiplePoints(string allPoints)
        {
            string[] splitPoints = allPoints.Split(" ");
            points[] myPoints = new points[splitPoints.Length];
            for (int i = 0; i < splitPoints.Length; i++)
            {
                myPoints[i] = readPoints(splitPoints[i]);
                //Console.WriteLine(myPoints[i].getX()+", " + myPoints[i].getY());
            }
            return myPoints;
        }
    } 
}
