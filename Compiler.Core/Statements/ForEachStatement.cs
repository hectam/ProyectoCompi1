using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
    public class ForeachStatement : Statement
    {
        public ForeachStatement(Token token1, Token token2, Statement statement)
        {
            Token1 = token1;
            Token2 = token2;
            Statement = statement;
        }
        public Statement Statement { get; }
        public Token Token1 { get; }
        public Token Token2 { get; }
        public override void Interpret()
        {
            Statement?.Interpret();
        }

        public override void ValidateSemantic()
        {
            Statement?.ValidateSemantic();
        }

        public override string Generate(int tabs)
        {
            var key = "{";
            var closeKey = "}";
            var code =GetCodeInit(tabs);
            code += $"{Token2.Lexeme}.forEach(myfunction);{Environment.NewLine}";
            code += GetCodeInit(tabs);
            code += $"function myfunction({Token1.Lexeme}){key}{Environment.NewLine}";
            code += GetCodeInit(tabs);
            code += $" {Statement.Generate(tabs)}; {closeKey}{Environment.NewLine}";

            return code;
        }
    }
}
