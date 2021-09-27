using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
	public class SequenceIfStatement : Statement
	{
		public SequenceIfStatement(TypedExpression firstStatement, TypedExpression nextStatement, Statement statement, TokenType type)
		{
            FirstExpression = firstStatement;
            NextExpression = nextStatement;
			Statement = statement;
            Type = type;
		}

		public TypedExpression FirstExpression { get; private set; }

        public TypedExpression NextExpression { get; private set; }
        public Statement Statement { get; }
        public TokenType Type { get; }

        public override string Generate(int tabs)
        {
            var code = "";


            var key = '{';
            code += GetCodeInit(tabs);
            code += $"if({innerGenerate(tabs,Type)}){key} {Environment.NewLine}";
            key = '}';
            code += $"{Statement.Generate(tabs + 1)}{Environment.NewLine}";
            code += GetCodeInit(tabs);
            code += $"{key}{Environment.NewLine}";


            return code;
        }

        public string innerGenerate(int tabs,TokenType type)
		{
            var code ="";
            code += FirstExpression?.Generate();
            code += " "+ getTokenType(type) + " "+NextExpression?.Generate();

            return code;

        }

        public string getTokenType(TokenType type)
		{
			if (type == TokenType.AND)
			{
                return "&&";
			}else if (type == TokenType.OR)
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
            Statement.ValidateSemantic();
        }

        public override void Interpret()
        {
            Statement.ValidateSemantic();
        }

    }
}
