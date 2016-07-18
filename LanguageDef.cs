using System.Collections.Generic;

namespace StringProc
{
	public class LanguageDef
	{
		public string Name { get; set; }
		public List<Construct> Constructs { get; set; }

		public List<Processor> GetProcessors()
		{
			var procs = new List<Processor>();
			foreach (var c in Constructs)
			{
				procs.Add(new Processor(c.Pattern, c.Replacement));
			}
			return procs;
		}

		public class Construct
		{
			public string Pattern { get; set; }
			public string Replacement { get; set; }
		}
	}
}
