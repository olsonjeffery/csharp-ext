using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpExt
{
    public class CSharpSyntaxEtx : CSharpSyntaxRewriter
    {
        public SemanticModel Model {get; private set;}
        public CSharpSyntaxEtx(SemanticModel model)
        {
            Model = model;
        }

        public override SyntaxNode VisitIfStatement(Microsoft.CodeAnalysis.CSharp.Syntax.IfStatementSyntax node)
        {
            return base.VisitIfStatement(node);
        }
    }
}
