using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace StringProc
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("String Processor v0.1");
			Console.WriteLine();

			var config = new Config(args);

			if (config.IsSet("?") || !config.IsSet("out") || config.InputFiles.Count == 0 || config.LanguageFiles.Count == 0)
			{
				Console.WriteLine("Usage:    StringProc (language files) (input files) [args]");
				Console.WriteLine("Required: One language and input file, out argument");
				Console.WriteLine();
				Console.WriteLine("Args:");
				Console.WriteLine("    /out  - Specifies the output file.");
				Console.WriteLine("    /wait - Pauses the program on completion.");
			}
			else
			{
				Console.Write("Loading language files...");
				var langs = new List<LanguageDef>();
				foreach (string f in config.LanguageFiles)
				{
					langs.Add(JsonConvert.DeserializeObject<LanguageDef>(File.ReadAllText(f)));
				}
				Console.WriteLine("Done");
				Console.Write("Creating processors...");
				var procs = new List<Processor>();
				foreach(var l in langs)
				{
					procs.AddRange(l.GetProcessors());
				}
				Console.WriteLine("Done");
				Console.Write("Processing input files...");
				using (var writer = new StreamWriter(config["out"]))
				{
					foreach (string f in config.InputFiles)
					{
						string priorInp = "";
						string newInp = File.ReadAllText(f);
						while (priorInp != newInp)
						{
							priorInp = newInp;
							foreach(var p in procs)
							{
								newInp = p.Process(newInp);
							}
						}
						writer.WriteLine(priorInp);
					}
				}
				Console.WriteLine("Done");

				Console.WriteLine("Finished");
			}

			if (config.IsSet("wait"))
			{
				Console.WriteLine();
				Console.WriteLine("Press any key...");
				Console.ReadKey();
			}
		}
	}
}
