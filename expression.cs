using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace equationSolve
{
    public class expression
    {
        string exp;
        string[] terms;
        string[] values;
        string constant;

        public expression(string exp, string[] terms,string[] values,string constant)
        {
            this.exp = exp;
            this.terms = terms;
            this.values = values;
            this.constant = constant;
        }

        public void updateVarAndValue(expression exp)
        {
            string[] variablesToCheck = exp.getTerms();
            for(int i = 0; i < variablesToCheck.Length; i++)
            {
                if (solver.isIn(variablesToCheck[i], this.terms) == false)
                {
                    this.terms = solver.push(this.terms,variablesToCheck[i]);
                    this.values = solver.push(this.values, "0");
                }
            }
            this.updateExp();
        }
        public string getExpression()
        {
            return exp;
        }
        public string[] getTerms()
        {
            return this.terms;
        }
        public string[] getValues()
        {
            return this.values;
        }
        public string getValueFor(string find)
        {
            if (find.Equals("co"))
            {
                return this.constant;
            }
            else
            {
                int index = -1;
                for (int i = 0; i < this.terms.Length; i++)
                {
                    if (find.Equals(this.terms[i]))
                    {
                        index = i;
                        break;
                    }
                }
                if (index != -1)
                {
                    return this.values[index];
                }
                else
                {
                    return "0";
                }
            }
        }
        public void mult(double num)
        {
            for(int i = 0; i < this.values.Length; i++)
            {
                double setValue = num * double.Parse(this.values[i]);
                this.values[i] = setValue.ToString();
            }

            this.constant = (num * double.Parse(this.constant)).ToString();
            this.updateExp();
        }

        public void div(double num)
        {
            for(int i = 0; i < values.Length; i++)
            {
                double setValue = double.Parse(this.values[i]) / num;
                this.values[i] = setValue.ToString();
            }
            this.constant = (double.Parse(this.constant)/num).ToString();
            this.updateExp();
        }

        public void updateExp()
        {
            this.exp = "";
            for(int i = 0; i < this.values.Length; i++)
            {
                this.exp+= this.values[i]+this.terms[i]+" ";
            }
            this.exp += this.constant +"";
        }


        public string getConstant()
        {
            return this.constant;
        }

        //subracts a sepecific term or constant from another expression
        public void sub(expression exp, string var)
        {
            updateVarAndValue(exp);
            string myExp = (double.Parse(this.getValueFor(var)) - double.Parse(exp.getValueFor(var))).ToString();
            this.updateTerm(var,myExp);
        }

        //subtracts an entire expression;
        public void subExp(expression exp)
        {
            updateVarAndValue(exp);
            for (int i = 0; i < this.terms.Length; i++)
            {
                string myExp = (double.Parse(this.getValueFor(this.terms[i])) - double.Parse(exp.getValueFor(this.terms[i]))).ToString();
                this.values[i] = myExp;
            }
            this.constant = (double.Parse(this.getConstant()) - double.Parse(exp.getConstant())).ToString();
            this.updateExp();
        }

        //subtracts a sepcific term or constant by an arbitrary
        public void subVar(double num, string var)
        {
            //updateVarAndValue(exp);
            if (var.Equals("co"))
            {
                this.constant = (double.Parse(this.constant) - num).ToString();
            }
            else
            {
                if (solver.isIn(var, this.terms))
                {
                    string myExp = (double.Parse(this.getValueFor(var)) - num).ToString();
                    this.updateTerm(var, myExp);
                }
                else
                {
                    Console.WriteLine("could not substract " + num + " from " + var);
                }
            }
            this.updateExp();
        }

        public void addExp(expression exp)
        {
            updateVarAndValue(exp);
            for (int i = 0; i < this.terms.Length; i++)
            {
                string myExp = (double.Parse(this.getValueFor(this.terms[i])) + double.Parse(exp.getValueFor(this.terms[i]))).ToString();
                this.values[i] = myExp;
            }
            this.constant = (double.Parse(this.getConstant()) + double.Parse(exp.getConstant())).ToString();
            this.updateExp();
        }

        //replaces a term with a new value
        public void updateTerm(string var, string newValue)
        {
            for(int i = 0; i < this.terms.Length; i++)
            {
                if (this.terms[i].Equals(var))
                {
                    this.values[i] = newValue;
                    break;
                }
            }
            this.updateExp();
        }

        /*plugs in a new value for a term ex
            4 = 4x + 2y + 1z
            x = 1y + 4
            replaces x with new values
            4 = 4(1y+4) + 2y + 1z
            4 = 4y + 16 + 2y + 1z
            4 = 6y + 16 + 1z
        */
        public void replace(string term, expression expGet)
        {
            expression exp = solver.putInTerms(expGet.getExpression());
            exp.mult(double.Parse(this.getValueFor(term)));
            this.updateTerm(term, "0");
            this.exp += " ";
            //Console.WriteLine(this.exp + "~");
            this.exp += exp.getExpression();
            //Console.WriteLine(this.exp+"~");
            solver.putInTerms(this.exp);
            this.copy(solver.putInTerms(this.exp));
            //Console.WriteLine(this.exp);
        }
        public void copy(expression exp)
        {
            this.updateVarAndValue(exp);
            for(int i = 0; i < exp.terms.Length; i++)
            {
                this.values[i] = "0";
            }
            for(int i = 0; i < exp.terms.Length; i++)
            {
                this.updateTerm(exp.terms[i], exp.values[i]);
            }
            this.constant = exp.getConstant();
            this.updateExp();
        }
        public void cleanUp()
        {
            for(int i = 0; i < this.terms.Length; i++)
            {
                if (this.values[i].Equals("0"))
                {
                    this.terms = solver.removeByIndex(this.terms,i);
                    this.values = solver.removeByIndex(this.values, i);
                    i = -1;
                }
            }
            this.updateExp();
        }

        public static expression makeRandom(int length)
        {
            
            char[] letters = variables.alphabet.ToCharArray();
            string exp = "";
            for(int i = 0; i < length; i++)
            {
                int term = solver.randint(0, letters.Length);
                if (letters[term].ToString().Equals("["))
                {
                    exp += solver.randint(1, 80).ToString();
                }
                else
                {
                    exp += solver.randint(1, 80).ToString() + letters[term];
                }
                if (i < (length-1))
                {
                    exp += " ";
                }
            }
            Console.WriteLine(exp);
            return solver.putInTerms(exp);
        }
        public static expression makeFormated(int length)
        {
            string exp = "";
            for (int i = 0; i < length; i++)
            {
                if (variables.letters[i].ToString().Equals("["))
                {
                    exp += solver.randint(1, 80).ToString();
                }
                else
                {
                    exp += solver.randint(1, 80).ToString() + variables.letters[i];
                }
                if (i < (length - 1))
                {
                    exp += " ";
                }
            }
            //Console.WriteLine(exp);
            return solver.putInTerms(exp);
        }
        public string[] findWhichNonZero()
        {
            string[] retString = new string[0];
            for(int i = 0; i < this.terms.Length; i++)
            {
                if (!this.getValueFor(this.terms[i]).Equals("0"))
                {
                    retString = solver.push(retString, this.terms[i]);
                }
            }
            return retString;
        }

    }
}
