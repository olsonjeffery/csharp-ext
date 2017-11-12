using System;
using CSharpExt;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;

namespace CSharpExtTestApp
{
    public class CompileInfrastructure
    {
        public void AssertCompilesAndMatches(string input, string expected)
        {

            var test = BuildFromInputTest(input);
        
            foreach (SyntaxTree sourceTree in test.SyntaxTrees)
            {
                var model = test.GetSemanticModel(sourceTree);

                var rewriter = new CSharpSyntaxEtx(model);

                var newSource = rewriter.Visit(sourceTree.GetRoot());

                var output = newSource.ToFullString();
                if(output != expected)
                {
                    throw new Exception(string.Format("Output code didn't match expected output.\n\ninput: {0}\n\nexpected: {1}\n\nactual: {2}", input, output, expected));
                }
            }
        }

        public CSharpCompilation BuildFromInputTest(string inputProgram)
        {
            var programTree = CSharpSyntaxTree.ParseText(inputProgram)
                                           .WithFilePath("Program.cs");


            var sourceTrees = new SyntaxTree[] { programTree };

            var mscorlib =
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            var codeAnalysis =
                    MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location);
            var csharpCodeAnalysis =
                    MetadataReference.CreateFromFile(typeof(CSharpSyntaxTree).Assembly.Location);

            var references = new MetadataReference[] { mscorlib, codeAnalysis, csharpCodeAnalysis };

            return CSharpCompilation.Create("CSharpExt",
                                            sourceTrees,
                                            references,
                                            new CSharpCompilationOptions(
                                                    OutputKind.ConsoleApplication));
        }
    }
}