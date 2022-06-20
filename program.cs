using System;


namespace equationSolve
{
    class Program
    {
        public static void Main(string[] args)
        {
            int newtab = 0;
            string path = @" C:\Users\coupe\Desktop\electron-app";
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
                    Console.WriteLine(returnPoints);

                    /*string text = System.IO.File.ReadAllText(@"C:\Users\coupe\Desktop\electron-app\test.txt");
                    Console.WriteLine(text);*/
                    StreamWriter sw = new StreamWriter(@"C:\Users\coupe\Desktop\electron-app\test.txt");
                    //Write a line of text
                    sw.WriteLine(function+"#"+returnPoints);
                    //Write a second line of text
                    //sw.WriteLine(returnPoints);
                    //Close the file
                    sw.Close();
                    //System.Diagnostics.Process.Start("CMD.exe", "/C Start chrome file:///C:/Users/coupe/Downloads/stuff/thing.html");
                    //System.Diagnostics.Process.Start("CMD.exe", "/C ^C");
                    if (newtab < 1)
                    {
                        System.Diagnostics.Process.Start("CMD.exe", "/C  npm start --prefix " + path);
                        newtab++;
                    }
                }
                catch
                {
                    Console.WriteLine("write a point");
                }
            }
            
        }

    }
}
