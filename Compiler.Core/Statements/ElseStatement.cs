using Compiler.Core.Expressions;
using Compiler.Core.Interfaces;
using System;

namespace Compiler.Core.Statements
{
    public class ElseStatement : Statement
    {
        public ElseStatement(TypedExpression expression, Statement trueStatement, Statement falseStatement)
        {
            Expression = expression;
            TrueStatement = trueStatement;
            FalseStatement = falseStatement;
        }

        public TypedExpression Expression { get; }
        public Statement TrueStatement { get; }
        public Statement FalseStatement { get; }

        public override string Generate(int tabs)
        {
            var key = '{';
            var closeKey = '}';
            var code = GetCodeInit(tabs);
            code += $"if({Expression.Generate()}){key}{Environment.NewLine}";

            code += $"{TrueStatement.Generate(tabs + 1)}{Environment.NewLine}";
            
            code += GetCodeInit(tabs);
            code += $"{closeKey}{Environment.NewLine}";
            for (int i = 0; i < tabs; i++)
            {
                code += "\t";
            }
           
            code += $"else{key}{Environment.NewLine}";
            code += $"{FalseStatement.Generate(tabs + 1)}{Environment.NewLine}";
            code += GetCodeInit(tabs);
            code += $"{closeKey}{Environment.NewLine}";
            return code;
        }

        public override void Interpret()
        {
            if (Expression.Evaluate())
            {
                TrueStatement.Interpret();
            }
            else
            {
                FalseStatement.Interpret();
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
