using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
	public class SimpleAddStatement: Statement
	{
		public SimpleAddStatement(Id id)
		{
			Id = id;
			
		
		}

		public Id Id { get; }
        public TypedExpression Expression { get; }

        public bool declared { get; set; }

        public override string Generate(int tabs)
        {
            var code = GetCodeInit(tabs);
            

            code += $"{Id.Generate()}++;{Environment.NewLine}";
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
