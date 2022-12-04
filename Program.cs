using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Microsoft.Scripting.Hosting.ScriptEngine python = IronPython.Hosting.Python.CreateEngine();
            
            python.Runtime.LoadAssembly(typeof(Program).Assembly);
            python.Runtime.LoadAssembly(typeof(Enumerable).Assembly);
            Microsoft.Scripting.Hosting.ScriptScope scope = python.CreateScope();
            SomeObject input = new SomeObject();
            input.x = "42 to you!";
            ScriptSource source = python.CreateScriptSourceFromFile("test.py");
            scope.SetVariable("input", input);
            source.Execute(scope);
            Console.WriteLine("input x after changed. Is it preciouss???");
            Console.WriteLine(input.x);
            Console.WriteLine("output variable. Will it only show 2 entries");
            IEnumerable<SomeObject> output = scope.GetVariable("output");
            Console.WriteLine(string.Join(",", output.ToList()));
            
            

        }


    }
    public class SomeObject {
        public String x;
        public List<SomeObject> l = new List<SomeObject>();

        public override string ToString()
        {
            return x + ": " + string.Join(",", l);
        }
    }
}
