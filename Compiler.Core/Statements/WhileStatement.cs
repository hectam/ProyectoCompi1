using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
	public class WhileStatement : Statement
	{
		public WhileStatement(Statement statement, TypedExpression token1)
		{
			Statement = statement;
			Token1 = token1;
			
		}

		public Statement Statement { get; }
		public TypedExpression Token1 { get; }
		
		public override string Generate(int tabs)
		{
			var key = '{';
			var closekey = '}';
			var code = GetCodeInit(tabs);
			code += $"while({Token1.Generate()}){key} {Environment.NewLine}";
			
			code += $"{Statement.Generate(tabs + 1)}{Environment.NewLine}";
			code += GetCodeInit(tabs);
			code += $"{closekey}{Environment.NewLine}";
			return code;
		}

		public override void Interpret()
		{
			if (Token1.Evaluate())
			{
				Statement.Interpret();
			}
		}

		public override void ValidateSemantic()
		{
			if (Token1.GetExpressionType() != Type.Bool)
			{
				throw new ApplicationException("A boolean is required in ifs");
			}
		}
	}

		
	
}
