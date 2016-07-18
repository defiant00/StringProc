using System.Text.RegularExpressions;

namespace StringProc
{
	public class Processor
	{
		public Regex Expression { get; set; }
		public string Replacement { get; set; }

		public Processor(string expr, string repl)
		{
			Expression = new Regex(expr, RegexOptions.Compiled);
			Replacement = repl;
		}

		public string Process(string inp)
		{
			return Expression.Replace(inp, Replacement);
		}
	}
}
