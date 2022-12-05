using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

/*
 * Use case 1:
 * call a python function with a list containing 3 complex objects.
 * Instantiate a new list with all items where x starts with "a",
 * preferably using an Enumerable.
 * 
 * Use case 2:
 * 
 * call a python function with a single object.
 * It must return true if a field has some value, false otherwise.
 * 
 */

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Microsoft.Scripting.Hosting.ScriptEngine python = IronPython.Hosting.Python.CreateEngine();
            
            python.Runtime.LoadAssembly(typeof(Program).Assembly);
            python.Runtime.LoadAssembly(typeof(Enumerable).Assembly);
            /* Use case 1: invoke a function list */
            Microsoft.Scripting.Hosting.ScriptScope scope = python.CreateScope();
            List<SomeObject> theList = new List<SomeObject>();
            IEnumerable<SomeObject> s = theList;
            
            theList.Add(new SomeObject("a"));
            theList.Add(new SomeObject("b"));
            theList.Add(new SomeObject("abacate"));
            Stopwatch watch = new Stopwatch();
            Stopwatch fullWatch = new Stopwatch();
            fullWatch.Start();

            long readTime = -1;
            long executionTime = -1;
            long doApplyExecutionTime = -1;
            long doFilterExecutionTime = -1;
            try
            {
                watch.Start();
                ScriptSource source = python.CreateScriptSourceFromFile("..\\..\\..\\test.py");
                readTime = watch.ElapsedTicks;
                watch.Restart();
                source.Execute(scope);
                dynamic doApply = scope.GetVariable("doApply");
                dynamic doFilter = scope.GetVariable("doFilter");
                executionTime = watch.ElapsedTicks;
                watch.Restart();
                List<object> onlyA = ((IEnumerable<object>)doApply(theList)).ToList();
                doApplyExecutionTime = watch.ElapsedTicks;
                Console.WriteLine("THE LIST WITH 'A' entries = " + string.Join(',', onlyA));
                SomeObject abacaxi = new SomeObject("abacaxi");
                watch.Restart();
                bool doFilterResult = doFilter(abacaxi);
                doFilterExecutionTime = watch.ElapsedTicks;
                Console.WriteLine("Does abacaxi starts with 'a'?" + doFilterResult);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(string.Join(',', scope.GetVariableNames()));
            } finally
            {
                Console.WriteLine("Elapsed times: readTime = " + readTime + " ; execution time = " + executionTime + " ; apply time = " + doApplyExecutionTime + " ; doFilter execution time=" + doFilterExecutionTime);
                fullWatch.Stop();
                Console.WriteLine("The whole process took " + fullWatch.ElapsedMilliseconds + " ms; "+fullWatch.ElapsedTicks + " ticks");
            }
            
            
            
            
            

        }


    }
    public class SomeObject {
        public String x;

        public override string ToString()
        {
            return x;
        }
        public SomeObject(string v)
        {
            x = v;
        }
    }
}
