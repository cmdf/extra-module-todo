using System;
using System.IO;
using System.Collections.Generic;
using Orez.CmdModule.Part;
using static Orez.CmdModule.Text.String0;

namespace Orez.CmdModule.Zero {
	class Module0 {

		#region static data
		/// <summary>
		/// Module root path.
		/// </summary>
		public static string Root = Path.Combine(Link0.Root, "0");
		#endregion



		#region static method
		public static void Setup() {
			Directory.CreateDirectory(Root);
		}


		/// <summary>
		/// Command: Get module details.
		/// </summary>
		/// <param name="o">Input options.</param>
		public static void CmdGet(Option o) {
			Output(o, List(o));
		}

		/// <summary>
		/// Command: Set module details.
		/// </summary>
		/// <param name="o">Input options.</param>
		public static void CmdSet(Option o) {
			if (o.Module == null) return;
			var m = Get(o.Module, true);
			if (o.Source != null)
				Module.SetSource(m, new string[] { o.Source });
			if (o.Target != null)
				Module.SetTarget(m, new string[] { o.Target });
			Output(o, new string[] { o.Module });
		}

		/// <summary>
		/// Command: Add module.
		/// </summary>
		/// <param name="o">Input options.</param>
		public static void CmdAdd(Option o) {
			if (o.Source == null) throw new Exception("Bad source: (none)");
			var mn = o.Module == null ? Source.GetName(o.Source) : o.Module;
			var m = Get(mn, false);
			Console0.Verbose(o, "Module: {0} (adding)", mn);
			Module.Create(m, o.Source);
			var mt = Module.GetTarget(m)[0];
			var ln = o.Link == null ? Path.GetFileNameWithoutExtension(mt) : o.Link;
			var l = Link0.Get(ln, false);
			Console0.Verbose(o, "Link: {0} (linking)", ln);
			Link.Create(l, mn, mt);
			Console0.Verbose(o, "Link: {0} -> Module: {1} (done)", ln, mn);
			Console0.Verbose(o, "[Target: {0}]", mt);
			Console0.Verbose(o, "[Source: {0}]", o.Source);
		}

		/// <summary>
		/// Command: Remove modules.
		/// </summary>
		/// <param name="o">Input options.</param>
		public static void CmdRemove(Option o) {
			foreach (var mn in List(o)) {
				foreach (var ln in Link.List(Link0.Root, ".*", mn, ".*")) {
					Console0.Verbose(o, "Link: X {0} -> Module: {1}", ln, mn);
					Link.Destroy(Link.Get(Link0.Root, ln));
				}
				Module.Destroy(Module.Get(Root, mn));
				Console0.Verbose(o, "Module: X {0} (removed)", mn);
			}
		}

		/// <summary>
		/// Update modules.
		/// </summary>
		/// <param name="o">Input options.</param>
		public static void CmdUpdate(Option o) {
			o.Link = null;
			foreach (var mn in List(o)) {
				var m = Get(mn, true);
				o.Module = mn;
				o.Source = Module.GetSource(m)[0];
				o.Target = Module.GetTarget(m)[0];
				CmdRemove(o);
				CmdAdd(o);
				Console0.Verbose(o, "");
			}
		}


		/// <summary>
		/// Output module names.
		/// </summary>
		/// <param name="o">Input options.</param>
		/// <param name="mns">Module names.</param>
		public static void Output(Option o, IList<string> mns) {
			Console0.Verbose(o, "Module\tSource\tTarget");
			Console0.Verbose(o, "------\t------\t------");
			foreach (var mn in mns) {
				var m = Get(mn, true);
				Console.WriteLine("{0}\t{1}\t{2}", mn, Module.GetSource(m)[0], Module.GetTarget(m));
			}
		}


		/// <summary>
		/// Get module path from name.
		/// </summary>
		/// <param name="mn">Module name.</param>
		/// <param name="e">Should exist?</param>
		/// <returns>Module path.</returns>
		public static string Get(string mn, bool e) {
			var m = Module.Get(Root, mn);
			var em = e ? " (not exists)" : " (exists)";
			if (e ^ Directory.Exists(m)) throw new Exception("Bad module: " + mn + em);
			return m;
		}

		/// <summary>
		/// List modules.
		/// </summary>
		/// <param name="o">Input options.</param>
		/// <returns>Modules.</returns>
		public static IList<string> List(Option o) {
			var mnp = WildcardRegex(o.Module);
			var sp = WildcardRegex(o.Source);
			var tp = WildcardRegex(o.Target);
			return Module.List(Root, mnp, sp, tp);
		}

		/// <summary>
		/// Get module name from given patterns.
		/// </summary>
		/// <param name="o">Input options.</param>
		/// <returns>Module name.</returns>
		public static string ListOne(Option o) {
			var mns = List(o);
			if (mns.Count == 0) throw new Exception("Bad module: (no match)");
			if (mns.Count > 1) throw new Exception("Bad module: " + mns + " (multi match)");
			return mns[0];
		}
		#endregion
	}
}
