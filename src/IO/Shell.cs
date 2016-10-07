using System.Diagnostics;

namespace Orez.CmdModule.IO {
	class Shell {

		// static method
		/// <summary>
		/// Run shell command synchronously.
		/// </summary>
		/// <param name="c">Shell command.</param>
		/// <returns>Process.</returns>
		public static Process Run(string c) {
			var p = RunAsync(c);
			p.WaitForExit();
			return p;
		}

		/// <summary>
		/// Run shell command asynchronously.
		/// </summary>
		/// <param name="c">Shell command.</param>
		/// <returns>Process.</returns>
		public static Process RunAsync(string c) {
			var p = new Process();
			var i = new ProcessStartInfo("cmd.exe", "/c " + c);
			i.UseShellExecute = false;
			p.StartInfo = i;
			p.Start();
			return p;
		}
	}
}
