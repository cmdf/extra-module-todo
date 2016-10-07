using System;
using System.Collections.Generic;
using Orez.CmdModule.Part;

namespace Orez.CmdModule.Zero {
	class Command0 {

		#region static method

		/// <summary>
		/// Output modules.
		/// </summary>
		/// <param name="o">Input options.</param>
		/// <param name="mns">Module names.</param>
		private static void ModulesOutput(Option o, IList<string> mns) {
			Console0.Verbose(o, "Module\tTarget\tSource");
			Console0.Verbose(o, "------\t------\t------");
			foreach (var mn in mns) {
				var m = Module.Get(Module0.Root, mn);
				Console.WriteLine("{0}\t{1}\t{2}", mn, Module.GetTarget(m), Module.GetSource(m));
			}
		}
		#endregion
	}
}
