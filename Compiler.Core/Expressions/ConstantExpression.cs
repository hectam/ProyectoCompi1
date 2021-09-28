using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Core.Expressions;

namespace Compiler.Core.Expressions
{
    public class ConstantExpression : TypedExpression
    {
        public string Lexeme { get; }

        public ConstantExpression(Token token, Type type, string lexeme)
            : base(token, type)
        {
            Lexeme = lexeme;
        }

        public override dynamic Evaluate()
        {
            switch (Token.Lexeme)
            {
                case "Date":
                    return DateTime.Parse(Lexeme);
                case "TimeStamp":
                    return TimeSpan.Parse(Lexeme);
                case "Hour":
                case "Minute":
                case "Second":
                case "Year":
                case "Month":
                case "Day":
                    return int.Parse(Lexeme);
            }

            return null;
        }

		

		public override string Generate()
		{
			throw new NotImplementedException();
		}

		public override Type GetExpressionType()
		{
			throw new NotImplementedException();
		}
	}
}
