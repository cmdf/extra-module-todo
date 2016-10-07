using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Orez.CmdModule.IO;

namespace Orez.CmdModule.Part {
	class Link {

		#region static method
		/// <summary>
		/// Get link path.
		/// </summary>
		/// <param name="lr">Link root path.</param>
		/// <param name="ln">Link name.</param>
		/// <returns>Link path.</returns>
		public static string Get(string lr, string ln) {
			return Path.Combine(lr, ln + ".cmd");
		}

		/// <summary>
		/// Create link.
		/// </summary>
		/// <param name="l">Link path.</param>
		/// <param name="d">Link description.</param>
		/// <param name="t">Target path.</param>
		public static void Create(string l, string d, string t) {
			var f = new StreamWriter(l);
			f.WriteLine(":: {0}", d);
			f.WriteLine("@\"{0}\" %*", t);
			f.Close();
		}


		/// <summary>
		/// Destroy link.
		/// </summary>
		/// <param name="l">Link path.</param>
		public static void Destroy(string l) {
			File0.Delete(l);
		}


		/// <summary>
		/// List links.
		/// </summary>
		/// <param name="lr">Link root path.</param>
		/// <param name="lnp">Link name pattern.</param>
		/// <param name="dp">Link description pattern.</param>
		/// <param name="tp">Target path pattern.</param>
		/// <returns>Link names.</returns>
		public static IList<string> List(string lr, string lnp, string dp, string tp) {
			var o = new List<string>();
			foreach (var l in Directory.EnumerateFiles(lr, "*.cmd")) {
				var ln = Path.GetFileNameWithoutExtension(l);
				if (!Regex.IsMatch(ln, lnp)) continue;
				if (!Regex.IsMatch(GetDescription(l), dp)) continue;
				if (!Regex.IsMatch(GetTarget(l), tp)) continue;
				o.Add(ln);
			}
			return o;
		}


		/// <summary>
		/// Get link description.
		/// </summary>
		/// <param name="l">Link path.</param>
		/// <returns>Link description.</returns>
		public static string GetDescription(string l) {
			var v = File0.ReadAllLines(l);
			return v.Length > 0 && v[0].Length > 3 ? v[0].Substring(3) : "";
		}

		/// <summary>
		/// Set link description.
		/// </summary>
		/// <param name="l">Link path.</param>
		/// <param name="d">Link description.</param>
		public static void SetDescription(string l, string d) {
			var v = File0.ReadAllLines(l);
			if (v.Length > 0) v[0] = string.Format(":: {0}", d);
			File.WriteAllLines(l, v);
		}


		/// <summary>
		/// Get target path.
		/// </summary>
		/// <param name="l">Link path.</param>
		/// <returns>Target path.</returns>
		public static string GetTarget(string l) {
			var v = File0.ReadAllLines(l);
			return v.Length > 1 && v[1].Length > 6 ? v[1].Substring(2, v[1].Length - 6) : "";
		}


		/// <summary>
		/// Set target path.
		/// </summary>
		/// <param name="l">Link path.</param>
		/// <param name="t">Target path.</param>
		public static void SetTarget(string l, string t) {
			var v = File0.ReadAllLines(l);
			if (v.Length > 1) v[1] = string.Format("@\"{0}\" %*", t);
			File.WriteAllLines(l, v);
		}
		#endregion
	}
}
