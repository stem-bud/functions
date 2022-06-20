using System;


namespace equationSolve
{
    class Program
    {
        public static void Main(string[] args)
        {
            //something's wrong here
            while (true)
            {
                try
                {
                    string userPoints = Console.ReadLine();
                    string function = points.makeEquation(solver.readMultiplePoints(userPoints));
                    Console.WriteLine(function+"\n");
                    //System.Diagnostics.Process.Start("CMD.exe", "/C Start chrome file:///C:/Users/coupe/Downloads/stuff/thing.html");
                    string[] allPoints = userPoints.Split(" ");
                    string returnPoints = "";
                    for(int i = 0; i < allPoints.Length; i++)
                    {
                        returnPoints += allPoints[i];
                        if(i < allPoints.Length - 1)
                        {
                            returnPoints += ",";
                        }
                    }
                    Console.WriteLine(function);
                    Console.WriteLine(returnPoints);
                }
                catch
                {
                    Console.WriteLine("write a point");
                }
            }
            
        }

    }
}
