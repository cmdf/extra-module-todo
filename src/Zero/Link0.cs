using System;
using System.IO;
using System.Collections.Generic;
using Orez.CmdModule.Part;
using static Orez.CmdModule.Text.String0;

namespace Orez.CmdModule.Zero {
	class Link0 {

		#region static data
		/// <summary>
		/// Link root path.
		/// </summary>
		public static string Root = Path.Combine(Program.AppData, Program.APP);
		#endregion



		#region static method
		/// <summary>
		/// Setup link usage.
		/// </summary>
		public static void Setup() {
			Directory.CreateDirectory(Root);
			var path = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
			if (!path.Contains(Root)) Environment.SetEnvironmentVariable("Path", path + ";" + Root, EnvironmentVariableTarget.User);
		}


		/// <summary>
		/// Command: Get link details.
		/// </summary>
		/// <param name="o">Input arguments.</param>
		public static void CmdGet(Option o) {
			Output(o, List(o));
		}

		/// <summary>
		/// Command: Set link details.
		/// </summary>
		/// <param name="o">Input arguments.</param>
		public static void CmdSet(Option o) {
			if (o.Link == null) throw new Exception("Bad link: (none)");
			var l = Get(o.Link, true);
			if (o.Module != null) {
				var m = Module0.Get(o.Module, true);
				Link.SetDescription(l, o.Module);
				Link.SetTarget(l, Module.GetTarget(m)[0]);
			}
			if (o.Target != null)
				Link.SetTarget(l, o.Target);
			Output(o, new string[] { o.Link });
		}

		/// <summary>
		/// Command: Add link.
		/// </summary>
		/// <param name="o">Input options.</param>
		public static void CmdAdd(Option o) {
			if (o.Link == null) throw new Exception("Bad link: (none)");
			var l = Get(o.Link, false);
			var mn = Module0.ListOne(o);
			var m = Module.Get(Module0.Root, mn);
			var mt = o.Target == null ? Module.GetTarget(m)[0] : o.Target;
			Link.Create(l, mn, mt);
			Console0.Verbose(o, "Link: {0} -> Module: {1} (linked)", o.Link, Link.GetDescription(l));
			Console0.Verbose(o, "[Target: {0}]", mt);
		}


		/// <summary>
		/// Command: Remove links.
		/// </summary>
		/// <param name="o">Input arguments.</param>
		public static void CmdRemove(Option o) {
			foreach (var ln in List(o)) {
				var l = Link.Get(Root, ln);
				Console0.Verbose(o, "Link: X {0} -> Module: {1}", ln, Link.GetDescription(l));
				Link.Destroy(l);
			}
			Console0.Verbose(o, "(done)");
		}


		/// <summary>
		/// Output link names.
		/// </summary>
		/// <param name="o">Input options.</param>
		/// <param name="lns">Link names.</param>
		public static void Output(Option o, IList<string> lns) {
			Console0.Verbose(o, "Link\tModule\tTarget");
			Console0.Verbose(o, "----\t------\t------");
			foreach (var ln in lns) {
				var l = Get(ln, true);
				Console.WriteLine("{0}\t{1}\t{2}", ln, Link.GetDescription(l), Link.GetTarget(l));
			}
		}


		/// <summary>
		/// Get link path from name.
		/// </summary>
		/// <param name="ln">Link name.</param>
		/// <param name="e">Should exist?</param>
		/// <returns>Link path.</returns>
		public static string Get(string ln, bool e) {
			var l = Link.Get(Root, ln);
			var em = e ? " (not exists)" : " (exists)";
			if (e ^ File.Exists(l)) throw new Exception("Bad link: " + ln + em);
			return l;
		}

		/// <summary>
		/// List links.
		/// </summary>
		/// <param name="o">Input options.</param>
		/// <returns>Link names.</returns>
		public static IList<string> List(Option o) {
			var lnp = WildcardRegex(o.Link);
			var dp = WildcardRegex(o.Module);
			var tp = WildcardRegex(o.Target);
			return Link.List(Root, lnp, dp, tp);
		}
		#endregion
	}
}
