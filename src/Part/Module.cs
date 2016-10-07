using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Orez.CmdModule.IO;

namespace Orez.CmdModule.Part {
	class Module {

		// static method
		/// <summary>
		/// Get module path.
		/// </summary>
		/// <param name="mr">Module root path.</param>
		/// <param name="mn">Module name.</param>
		/// <returns>Module path.</returns>
		public static string Get(string mr, string mn) {
			return Path.Combine(mr, mn);
		}

		/// <summary>
		/// Create module.
		/// </summary>
		/// <param name="m">Module path.</param>
		/// <param name="s">Source path.</param>
		public static void Create(string m, string s) {
			Directory0.Delete(m);
			Source.Load(m, s);
			SetSource(m, new string[] {s});
			SetTarget(m, FindTarget(m));
		}

		/// <summary>
		/// Destroy module.
		/// </summary>
		/// <param name="m">Module path.</param>
		public static void Destroy(string m) {
			Directory0.Delete(m);
			File0.Delete(m + ".source");
			File0.Delete(m + ".target");
		}


		/// <summary>
		/// List modules.
		/// </summary>
		/// <param name="mr">Modules root path.</param>
		/// <param name="mnp">Module name pattern.</param>
		/// <param name="sp">Source path pattern.</param>
		/// <param name="tp">Target path pattern.</param>
		/// <returns>Modules list.</returns>
		public static IList<string> List(string mr, string mnp, string sp, string tp) {
			var o = new List<string>();
			foreach (var m in Directory.EnumerateDirectories(mr)) {
				var mn = Path.GetFileName(m);
				var ok = Regex.IsMatch(mn, mnp);
				if (!ok) continue;
				ok = false;
				foreach (var s in GetSource(m))
					if (Regex.IsMatch(s, sp)) { ok = true; break; }
				if (!ok) continue;
				ok = false;
				foreach (var t in GetTarget(m))
					if (Regex.IsMatch(t, tp)) { ok = true; break; }
				if (!ok) continue;
				o.Add(mn);
			}
			return o;
		}


		/// <summary>
		/// Get module source.
		/// </summary>
		/// <param name="m">Module path.</param>
		/// <returns>Source paths.</returns>
		public static IList<string> GetSource(string m) {
			return File0.ReadAllLines(m + ".source");
		}

		/// <summary>
		/// Set module source.
		/// </summary>
		/// <param name="m">Module path.</param>
		/// <param name="s">Source paths.</param>
		public static void SetSource(string m, IList<string> s) {
			File.WriteAllLines(m + ".source", s);
		}


		/// <summary>
		/// Get module target.
		/// </summary>
		/// <param name="m">Module path.</param>
		/// <returns>Target paths.</returns>
		public static IList<string> GetTarget(string m) {
			return File0.ReadAllLines(m + ".target");
		}

		/// <summary>
		/// Set module target.
		/// </summary>
		/// <param name="m">Module path.</param>
		/// <param name="ts">Target paths.</param>
		public static void SetTarget(string m, IList<string> ts) {
			File.WriteAllLines(m, ts);
		}


		/// <summary>
		/// Find module target.
		/// </summary>
		/// <param name="m">Module path.</param>
		/// <returns>Target path.</returns>
		private static IList<string> FindTarget(string m) {
			List<string> mt = new List<string>();
			foreach (var p in new string[] { "*.exe", "*.com", "*.cmd", "*.bat" })
				foreach (var f in Directory0.GetFiles(m, p, true))
					mt.Add(f);
			return mt;
		}
	}
}
