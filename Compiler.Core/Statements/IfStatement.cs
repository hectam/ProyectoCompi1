using Compiler.Core.Expressions;
using Compiler.Core.Interfaces;
using System;

namespace Compiler.Core.Statements
{
    public class IfStatement : Statement
    {
        public IfStatement(TypedExpression expression, Statement statement)
        {
            Expression = expression;
            Statement = statement;
        }

        public TypedExpression Expression { get; }
        public Statement Statement { get; }

        public override string Generate(int tabs)
        {
            var key = '{';
            var code = GetCodeInit(tabs);
            code += $"if({Expression.Generate()}){key} {Environment.NewLine}";
            key = '}';
            code += $"{Statement.Generate(tabs + 1)}{Environment.NewLine}";
            code += GetCodeInit(tabs);
            code += $"{key}{Environment.NewLine}";
            return code;
        }

        public override void Interpret()
        {
            if (Expression.Evaluate())
            {
                Statement.Interpret();
            }
        }

        public override void ValidateSemantic()
        {
            if (Expression.GetExpressionType() != Type.Bool)
            {
                throw new ApplicationException("A boolean is required in ifs");
            }
        }
    }
}
