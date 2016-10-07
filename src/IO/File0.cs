using System.IO;

namespace Orez.CmdModule.IO {
	class File0 {

		// static method
		/// <summary>
		/// Delete file.
		/// </summary>
		/// <param name="f">File path.</param>
		public static void Delete(string f) {
			Delete(new FileInfo(f));
		}

		/// <summary>
		/// Delete file.
		/// </summary>
		/// <param name="f">File.</param>
		public static void Delete(FileInfo f) {
			if (f.Exists) f.Delete();
		}

		
		/// <summary>
		/// Get path of temporary file.
		/// </summary>
		/// <returns>File path.</returns>
		public static string Temp(string fn = null) {
			fn = fn == null ? Path.GetRandomFileName() : fn;
			return Path.Combine(Path.GetTempPath(), fn);
		}


		/// <summary>
		/// Read all lines from file.
		/// </summary>
		/// <param name="f">File path.</param>
		/// <returns>Lines.</returns>
		public static string[] ReadAllLines(string f) {
			return ReadAllLines(new FileInfo(f));
		}

		/// <summary>
		/// Read all lines from file.
		/// </summary>
		/// <param name="f">File.</param>
		/// <returns>Lines.</returns>
		public static string[] ReadAllLines(FileInfo f) {
			return f.Exists ? File.ReadAllLines(f.FullName) : new string[0];
		}


		/// <summary>
		/// Read all text from file.
		/// </summary>
		/// <param name="f">File path.</param>
		/// <returns>Text.</returns>
		public static string ReadAllText(string f) {
			return ReadAllText(new FileInfo(f));
		}

		/// <summary>
		/// Read all text from file.
		/// </summary>
		/// <param name="f">File.</param>
		/// <returns>Text.</returns>
		public static string ReadAllText(FileInfo f) {
			return f.Exists ? File.ReadAllText(f.FullName) : "";
		}
	}
}
