using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using Orez.CmdModule.IO;

namespace Orez.CmdModule.Part {
	class Source {

		// static method
		/// <summary>
		/// Get name from source URL.
		/// </summary>
		/// <param name="s">Source URL.</param>
		/// <returns>Name.</returns>
		public static string GetName(string s) {
			s = Regex.Replace(s, @"\S+:\/\/", @"A:\");
			s = s.Replace("@", "").Replace('/', '\\');
			return Path.GetFileName(s);
		}


		/// <summary>
		/// Load from source path/URL.
		/// </summary>
		/// <param name="d">Destination path.</param>
		/// <param name="s">Source path/URL.</param>
		public static void Load(string d, string s) {
			if (s.StartsWith("git:") || s.StartsWith("ssh:") || s.EndsWith(".git")) LoadGit(d, s);
			else if (s.StartsWith("https:") || s.StartsWith("http:")) LoadUrl(d, s);
			else if (s.StartsWith("@")) LoadGitHub(d, s);
			else if (Directory.Exists(s)) LoadDirectory(d, s);
			else if (File.Exists(s)) LoadFile(d, s);
			else throw new IOException("Bad source: " + s);
		}

		/// <summary>
		/// Load from source file.
		/// </summary>
		/// <param name="d">Destination path.</param>
		/// <param name="s">Source path.</param>
		/// <param name="r">Remove source?</param>
		private static void LoadFile(string d, string s, bool r = false) {
			if (Path.GetExtension(s).ToLower() == ".zip") { LoadZip(d, s, r); return; }
			Directory0.Create(d);
			var f = Path.Combine(d, Path.GetFileName(s));
			if (r) File.Move(s, f);
			File.Copy(s, f, true);
		}

		/// <summary>
		/// Load from source Zip.
		/// </summary>
		/// <param name="d">Destination path.</param>
		/// <param name="s">Source path.</param>
		/// <param name="r">Remove source?</param>
		private static void LoadZip(string d, string s, bool r = false) {
			var t = File0.Temp();
			Directory0.Create(t);
			ZipFile.ExtractToDirectory(s, t);
			LoadDirectory(d, t, true);
			if (r) File.Delete(s);
		}

		/// <summary>
		/// Load from source directory.
		/// </summary>
		/// <param name="d">Destination path.</param>
		/// <param name="s">Source path.</param>
		/// <param name="r">Remove source?</param>
		private static void LoadDirectory(string d, string s, bool r = false) {
			var t = File0.Temp();
			if (r) Directory.Move(s, t);
			else Directory0.Copy(s, t, true);
			Directory.Move(Directory0.NonEmpty(t), d);
			Directory0.Delete(Path.Combine(d, ".git"), true);
			Directory0.Delete(t, true);
		}

		/// <summary>
		/// Load from URL.
		/// </summary>
		/// <param name="d">Destination path.</param>
		/// <param name="s">Source URL.</param>
		private static void LoadUrl(string d, string s) {
			var t = File0.Temp(GetName(s));
			new WebClient().DownloadFile(s, t);
			LoadFile(d, t, true);
		}

		/// <summary>
		/// Load from Git URL.
		/// </summary>
		/// <param name="d">Destination path.</param>
		/// <param name="s">Source URL.</param>
		private static void LoadGit(string d, string s) {
			Shell.Run("git clone --depth=1 " + s + " " + d);
			Directory0.Delete(Path.Combine(d, ".git"), true);
		}

		/// <summary>
		/// Load from GitHub URL.
		/// </summary>
		/// <param name="d">Destination path.</param>
		/// <param name="s">Source URL.</param>
		private static void LoadGitHub(string d, string s) {
			var gr = Net.GitHub.Release.GetLatest(s.Substring(1));
			LoadUrl(d, gr.Assets[0].BrowserDownloadUrl);
		}
	}
}
