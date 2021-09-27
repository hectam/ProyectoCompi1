using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
	public class MethodStatement : Statement
	{
		public MethodStatement(string lexeme, Id id, ArgumentExpression arg,Statement stm)
		{
			Lexeme = lexeme;
			this.id = id;
			this.arg = arg;
			Stm = stm;
		}

		public string Lexeme { get; }

		public Id id { get; }

		public ArgumentExpression arg { get; }

		public Statement Stm { get; }

		public override string Generate(int tabs)
		{
			var key = "{";
			var closekey = "}";
			var code = "";
			code += GetCodeInit(tabs);
			code += $" function {id.Generate()}({arg.Token.Lexeme}){key}{Environment.NewLine}";
			code += GetCodeInit(tabs);
			code += $" {Stm.Generate(tabs+1)} {closekey}{Environment.NewLine}";
			return code;
		}

		public override void Interpret()
		{
			arg.LeftExpression?.Evaluate();
			arg.RightExpression?.Evaluate();
		}

		public override void ValidateSemantic()
		{
			Console.WriteLine("asda");
		}
	}
}
