using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
	public class SequenceWhileStatement : Statement
	{
		public SequenceWhileStatement( TypedExpression token1, TypedExpression token2, Statement statement, TokenType type)
		{
			Statement = statement;
			Token1 = token1;
			Token2 = token2;
			Tpy = type;
		}

		public Statement Statement { get; }
		public TypedExpression Token1 { get; }
		public TypedExpression Token2 { get; }

		public TokenType Tpy { get; }

		public override string Generate(int tabs)
		{
			var key = '{';
			var closekey = '}';
			var code = GetCodeInit(tabs);
			code += $"while({innerGenerate(tabs, Tpy)}){key} {Environment.NewLine}";

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
		public string innerGenerate(int tabs, TokenType type)
		{
			var code = "";
			code += Token1?.Generate();
			code += " " + getTokenType(type) + " " + Token2?.Generate();

			return code;

		}

		public string getTokenType(TokenType type)
		{
			if (type == TokenType.AND)
			{
				return "&&";
			}
			else if (type == TokenType.OR)
			{
				return "||";
			}
			else
			{
				return "!";
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
