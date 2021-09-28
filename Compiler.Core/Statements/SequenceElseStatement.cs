using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
	
        public class SequenceElseStatement : Statement
        {
            public SequenceElseStatement(TypedExpression firstExpression, TypedExpression nextExpression, Statement trueStatement, Statement falseStatement, TokenType type)
            {
                FirstExpression = firstExpression;
                NextExpression = nextExpression;
                TrueStatement = trueStatement;
                FalseStatement = falseStatement;
                _Type = type;
            }

            public TypedExpression FirstExpression { get; private set; }

            public TypedExpression NextExpression { get; private set; }
            public Statement TrueStatement { get; }
            public Statement FalseStatement { get; }
            public TokenType _Type { get; set; }

            public override string Generate(int tabs)
            {
                var key = '{';
                var closeKey = '}';
                var code = GetCodeInit(tabs);

                code += $"if({innerGenerate(tabs,_Type)}){key}{Environment.NewLine}";

                code += $"{TrueStatement.Generate(tabs + 1)};{Environment.NewLine}";

                code += GetCodeInit(tabs);
                code += $"{closeKey}{Environment.NewLine}";
                for (int i = 0; i < tabs; i++)
                {
                    code += "\t";
                }

                code += $"else{key}{Environment.NewLine}";
                code += $"{FalseStatement.Generate(tabs + 1)}; {Environment.NewLine}";
                code += GetCodeInit(tabs);
                code += $"{closeKey}{Environment.NewLine}";
                return code;
               
            }

            public string innerGenerate(int tabs,TokenType type)
            {
                var code = "";
                code += FirstExpression?.Generate();
                code += " "+getTokenType(type)+" " + NextExpression?.Generate();

                return code;

            }

		private string getTokenType(TokenType type)
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

		public override void Interpret()
            {
                if (FirstExpression.Evaluate() && NextExpression.Evaluate())
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
                if (FirstExpression.GetExpressionType() != Type.Bool && NextExpression.GetExpressionType() != Type.Bool)
                {
                    throw new ApplicationException("A boolean is required in ifs");
                }
            }

        }
   
}
