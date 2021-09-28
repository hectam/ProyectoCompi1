using System;
using System.Collections.Generic;

namespace Compiler.Core.Expressions
{
    public class ArithmeticOperator : TypedBinaryOperator
    {
        private readonly Dictionary<(Type, Type), Type> _typeRules;
        private readonly Dictionary<Type, Func<DateTime, dynamic, dynamic>> _evaluationDictionary;
        public ArithmeticOperator(Token token, TypedExpression leftExpression, TypedExpression rightExpression)
            : base(token, leftExpression, rightExpression, null)
        {
            _typeRules = new Dictionary<(Type, Type), Type>
            {
                { (Type.Float, Type.Float), Type.Float },
                { (Type.Int, Type.Int), Type.Int },
                { (Type.String, Type.String), Type.String },
                { (Type.Float, Type.Int), Type.Float },
                { (Type.Int, Type.Float), Type.Float },
                { (Type.String, Type.Int), Type.String  },
                { (Type.String, Type.Float), Type.String  },
                 {(Type.Date, Type.Year), Type.Date},
                {(Type.Date, Type.Month), Type.Date},
                {(Type.Date, Type.Day), Type.Date},
                //-----------------------------
                {(Type.TimeStamp, Type.TimeStamp), Type.TimeStamp},
                {(Type.TimeStamp, Type.Hour), Type.TimeStamp},
                {(Type.TimeStamp, Type.Minute), Type.TimeStamp},
                {(Type.TimeStamp, Type.Second), Type.TimeStamp},

            };

            _evaluationDictionary = new Dictionary<Type, Func<DateTime, dynamic, dynamic>>
            {
                [Type.Hour] = (leftExpression, rightExpression) => leftExpression.AddHours(rightExpression),
                [Type.Minute] = (leftExpression, rightExpression) => leftExpression.AddMinutes(rightExpression),
                [Type.Second] = (leftExpression, rightExpression) => leftExpression.AddSeconds(rightExpression),
                [Type.Year] = (leftExpression, rightExpression) => leftExpression.AddYears(rightExpression),
                [Type.Month] = (leftExpression, rightExpression) => leftExpression.AddMonths(rightExpression),
                [Type.Day] = (leftExpression, rightExpression) => leftExpression.AddDays(rightExpression),
            };
        }

        public override dynamic Evaluate()
        {
            var rightExpressionType = RightExpression.GetExpressionType();
            if (Token.TokenType == TokenType.Plus)
            {
                if (_evaluationDictionary.ContainsKey(rightExpressionType))
                {
                    return GetExpressionResult(1);
                }
                return LeftExpression.Evaluate() + RightExpression.Evaluate();
            }
            else if (Token.TokenType == TokenType.Minus)
            {
                if (_evaluationDictionary.ContainsKey(rightExpressionType))
                {
                    return GetExpressionResult(-1);
                }

                return LeftExpression.Evaluate() - RightExpression.Evaluate();
            }
            return Token.TokenType switch
            {
                TokenType.Plus => LeftExpression.Evaluate() + RightExpression.Evaluate(),
                TokenType.Minus => LeftExpression.Evaluate() - RightExpression.Evaluate(),
                TokenType.Asterisk => LeftExpression.Evaluate() * RightExpression.Evaluate(),
                TokenType.Division => LeftExpression.Evaluate() / RightExpression.Evaluate(),
                TokenType.PlusPlus => LeftExpression.Evaluate() + 1,
                TokenType.MinMin => LeftExpression.Evaluate() - 1,
                TokenType.Mod => LeftExpression.Evaluate() % RightExpression.Evaluate(),
                _ => throw new NotImplementedException()
            };
            
            throw new ApplicationException($"Cannot perform Binary operation on {LeftExpression.GetExpressionType()}, {RightExpression.GetExpressionType()}");
        }

        public override string Generate()
        {
            if (LeftExpression.GetExpressionType() == Type.String &&
                RightExpression.GetExpressionType() != Type.String)
            {
                return $"{LeftExpression.Generate()} {Token.Lexeme} {RightExpression.Generate()}";
            }

            return $"{LeftExpression.Generate()} {Token.Lexeme} {RightExpression.Generate()}";
        }
        private dynamic GetExpressionResult(int multiplier)
        {
            var rightExpressionType = RightExpression.GetExpressionType();
            var leftExpressionDate = LeftExpression.Evaluate() is DateTime ? (DateTime)LeftExpression.Evaluate() : default;
            var rightExpression = RightExpression.Evaluate();
            return _evaluationDictionary[rightExpressionType](leftExpressionDate, rightExpression * multiplier);
        }

        public override Type GetExpressionType()
        {
            if (_typeRules.TryGetValue((LeftExpression.GetExpressionType(), RightExpression.GetExpressionType()), out var resultType))
            {
                return resultType;
            }

            throw new ApplicationException($"Cannot perform arithmetic operation on {LeftExpression.GetExpressionType()}, {RightExpression.GetExpressionType()}");
        }
    }
}
