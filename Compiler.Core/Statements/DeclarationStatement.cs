using Compiler.Core.Expressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Core.Statements
{
	public class DeclarationStatement : Statement
	{
		public DeclarationStatement(Id iD)
		{
			this.iD = iD;
		}

		public Id iD { get; }
		public override string Generate(int tabs)
		{
			throw new NotImplementedException();
		}

		public override void Interpret()
		{
			throw new NotImplementedException();
		}

		public override void ValidateSemantic()
		{
			throw new NotImplementedException();
		}
	}
}
