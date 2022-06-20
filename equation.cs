namespace equationSolve
{
    public class equation
    {
        public expression exp1;
        public expression exp2;
        public equation(expression exp1, expression exp2)
        {
            this.exp1 = exp1;
            this.exp2 = exp2;

            this.formatForElimination();
        }
        public void formatForElimination()
        {
            this.exp1.updateVarAndValue(this.exp2);
            this.exp2.updateVarAndValue(this.exp1);

            for (int i = 0; i < this.exp2.getTerms().Length; i++)
            {
                double exp1term = double.Parse(this.exp1.getValueFor(this.exp2.getTerms()[i]));

                this.exp1.subVar(exp1term, this.exp2.getTerms()[i]);
                this.exp2.subVar(exp1term, this.exp2.getTerms()[i]);
            }


            double exp2const = double.Parse(this.exp2.getConstant());
            this.exp2.subVar(exp2const, "co");
            this.exp1.subVar(exp2const, "co");
        }
        public void isolate(string var)
        {
            double exp1term = double.Parse(this.exp2.getValueFor(var));
            this.exp1.subVar(exp1term, var);
            this.exp2.subVar(exp1term, var);

            double exp2const = double.Parse(this.exp1.getConstant());
            this.exp1.subVar(exp2const, "co");
            this.exp2.subVar(exp2const,"co");
        }
        public string getEquation()
        {
            return this.exp1.getExpression() + " = " + this.exp2.getExpression();
        }
        public void mult(double num)
        {
            this.exp2.mult(num);
            this.exp1.mult(num);
        }
        public void div(double num)
        {
            this.exp2.div(num);
            this.exp1.div(num);
        }
        public static equation addEqu(equation equ1,equation equ2)
        {

            equ1.exp1.addExp(equ2.exp1);
            expression retExp1 = solver.putInTerms(equ1.exp1.getExpression());

            equ1.exp2.addExp(equ2.exp2);
            expression retExp2 = solver.putInTerms(equ1.exp2.getExpression());
            return new equation(retExp1,retExp2);
        }
        public static equation replicate(equation equ)
        {
            return new equation(solver.putInTerms(equ.exp1.getExpression()), solver.putInTerms(equ.exp2.getExpression()));

        }
        public static equation makeRandom()
        {
            
            return new equation(expression.makeRandom(solver.randint(700, 800)), expression.makeRandom(solver.randint(700, 800)));
        }
        public static string[] findCommon(equation equ1, equation equ2)
        {
            string[] retArr = new string[0];
            for(int i = 0; i < equ1.exp2.getTerms().Length; i++)
            {
                if (!equ1.exp2.getValueFor(equ1.exp2.getTerms()[i]).Equals("0") && !equ2.exp2.getValueFor(equ1.exp2.getTerms()[i]).Equals("0"))
                {
                    retArr = solver.push(retArr, equ1.exp2.getTerms()[i]);
                    //Console.WriteLine(retArr);
                    //Console.WriteLine(equ1.exp2.getValueFor(equ1.exp2.getTerms()[i]));
                    //Console.WriteLine(equ2.exp2.getValueFor(equ1.exp2.getTerms()[i]));
                }
            }
            return retArr;
        }
        public static equation makeFormated(int length)
        {
            return new equation(solver.putInTerms(solver.randint(0,100).ToString()), expression.makeFormated(length));
        }
        public string[] getTerms()
        {
            this.formatForElimination();
            return this.exp2.getTerms();
        }
        public static equation[] replicateEqus(equation[] equs)
        {
            equation[] retEqus = new equation[equs.Length];

            for(int i = 0; i < equs.Length; i++)
            {
                retEqus[i] = replicate(equs[i]);
            }
            return retEqus;
        }
    }
}
