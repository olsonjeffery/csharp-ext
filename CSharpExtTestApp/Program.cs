using System;

namespace CSharpExtTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var compiler = new CompileInfrastructure();
            compiler.AssertCompilesAndMatches(input: @"
public class A
{
    public A()
    {
        Console.WriteLine(""sdfsdf"");
    }
}
            ", expected: @"
public class A
{
    public A()
    {
        Console.WriteLine(""sdfsdf"");
    }
}
            ");

        }
    }
}
