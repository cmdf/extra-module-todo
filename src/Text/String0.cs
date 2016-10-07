using System.Text.RegularExpressions;

namespace Orez.CmdModule.Text {
	class String0 {

		#region static method
		/// <summary>
		/// Gets regular expression string for wildcard string.
		/// </summary>
		/// <param name="a">Wildcard string.</param>
		/// <returns>Regular expression string.</returns>
		public static string WildcardRegex(string a) {
			if (a == null) return ".*";
			return Regex.Escape(a.Replace("?", ".").Replace("*", ".*"));
		}
		#endregion
	}
}
