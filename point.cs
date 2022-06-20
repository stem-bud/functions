namespace equationSolve
{
    public class points
    {
        double x;
        double y;
        public points(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public static string makeEquation(points[] arr)
        {
            equation[] retEqu = new equation[arr.Length];
            for (int i = 0; i < arr.Length; i++) {
                string exp2 = "";
                string exp1 = (arr[i].y).ToString();
                for (int j = (arr.Length - 1); j > -1; j--)
                {
                    //Console.WriteLine(variables.letters[j]);
                    exp2 += Math.Pow(arr[i].x, j).ToString() + variables.letters[(arr.Length -1)-j].ToString();
                    if(j > 0)
                    {
                        exp2 += " ";
                    }
                }
                //Console.WriteLine(exp2+"~");
                retEqu[i] = new equation(solver.putInTerms(exp1),solver.putInTerms(exp2));
                //Console.WriteLine(retEqu[i].getEquation());
            }

            expression[] thing = solver.solveAll(retEqu);
            for(int i = 0; i < thing.Length; i++)
            {
                Console.WriteLine(thing[i].getExpression());
            }
            string func = "f(x) = ";
            for(int i = 0; i < thing.Length; i++)
            {
                func += thing[i].getConstant()+"x^"+((thing.Length-1)-i).ToString();
                if(i < thing.Length - 1)
                {
                    func += " + ";
                }
            }
            return func;
        }
        public double getX()
        {
            return this.x;
        }
        public double getY()
        {
            return this.y;
        }
    }
}