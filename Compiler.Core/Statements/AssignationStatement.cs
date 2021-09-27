using Compiler.Core.Expressions;
using Compiler.Core.Interfaces;
using System;

namespace Compiler.Core.Statements
{
    public class AssignationStatement : Statement
    {
        public AssignationStatement(Id id, TypedExpression expression)
        {
            Id = id;
            Expression = expression;
            declared = false;
        }

        public Id Id { get; }
        public TypedExpression Expression { get; }

        public bool declared { get; set; }

        public override string Generate(int tabs)
        {
            var code = GetCodeInit(tabs);
            var declaration = "";
           
                declaration += $"var {Id.Generate()};{Environment.NewLine}";
                declaration += GetCodeInit(tabs);
              

            
            code += declaration;
            code += $"{Id.Generate()} = {Expression.Generate()};{Environment.NewLine}";
            return code;
        }

       

        public override void Interpret()
        {
            EnvironmentManager.UpdateVariable(Id.Token.Lexeme, Expression.Evaluate());
        }

        public override void ValidateSemantic()
        {
            if (Id.GetExpressionType() != Expression.GetExpressionType())
            {
                throw new ApplicationException($"Type {Id.GetExpressionType()} is not assignable to {Expression.GetExpressionType()}");
            }
        }
    }
}
