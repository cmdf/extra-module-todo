using System;
using System.Collections.Generic;

namespace Orez.CmdModule {
	class Option {

		#region static data
		/// <summary>
		/// Command type from name ('.' = break, ' ' = continue).
		/// </summary>
		private static IDictionary<string, char> CmdType = new Dictionary<string, char>() {
			["add"] = '.', ["remove"] = '.', ["get"] = '.', ["list"] = '.', ["update"] = '.', ["link"] = ' '
		};
		#endregion



		#region data
		/// <summary>
		/// Command to be executed.
		/// </summary>
		public string Command;
		/// <summary>
		/// Defines module name.
		/// </summary>
		public string Module;
		/// <summary>
		/// Defines module source path.
		/// </summary>
		public string Source;
		/// <summary>
		/// Defines module target path.
		/// </summary>
		public string Target;
		/// <summary>
		/// Defines link name.
		/// </summary>
		public string Link;
		/// <summary>
		/// Output in detail.
		/// </summary>
		public bool Verbose;
		/// <summary>
		/// Command data.
		/// </summary>
		public IList<string> Data = new List<string>();
		#endregion



		#region constructor
		/// <summary>
		/// Get options from input arguments.
		/// </summary>
		/// <param name="a">Input arguments.</param>
		public Option(string[] a) {
			Command = "";
			for (var i = 0; i < a.Length; i++) {
				var b = a[i];
				if (b.StartsWith("--")) SetLong(a, ref i);
				else if (b.StartsWith("-")) SetShort(a, ref i);
				else if (!Command.EndsWith(".")) SetCommand(a, ref i);
				else SetData(a, ref i);
			}
			SetCommandEnd();
		}
		#endregion



		#region method
		/// <summary>
		/// Set command option from input arguments.
		/// </summary>
		/// <param name="a">Input arguments.</param>
		/// <param name="i">Index.</param>
		private void SetCommand(string[] a, ref int i) {
			var b = a[i].ToLower();
			if (CmdType.ContainsKey(b)) Command += b + CmdType[b];
			else throw new Exception("Bad command: " + a[i]);
		}

		/// <summary>
		/// Set command option at the end.
		/// </summary>
		private void SetCommandEnd() {
			if (Command == "") throw new Exception("Bad command: (none)");
			Command = Command.Remove(Command.Length - 1);
			if (Data.Count == 0) return;
			if (Command.StartsWith("link")) Link = Data[0];
			else Module = Data[0];
			Data.RemoveAt(0);
		}

		/// <summary>
		/// Set data option from input arguments.
		/// </summary>
		/// <param name="a">Input arguments.</param>
		/// <param name="i">Index.</param>
		private void SetData(string[] a, ref int i) {
			Data.Add(a[i]);
		}


		/// <summary>
		/// Set long options from input arguments.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="i"></param>
		private void SetLong(string[] a, ref int i) {
			var b = a[i].Substring(2).ToLower();
			if (b == "module") Module = a[++i];
			else if (b == "source") Source = a[++i];
			else if (b == "target") Target = a[++i];
			else if (b == "link") Link = a[++i];
			else if (b == "verbose") Verbose = true;
			else throw new Exception("Bad option: --" + b);
		}

		/// <summary>
		/// Set short options from input arguments.
		/// </summary>
		/// <param name="a">Input arguments.</param>
		/// <param name="i">Index.</param>
		private void SetShort(string[] a, ref int i) {
			foreach(var b in a[i].Substring(1).ToLower()) {
				if (b == 'm') Module = a[++i];
				else if (b == 's') Source = a[++i];
				else if (b == 't') Target = a[++i];
				else if (b == 'l') Link = a[++i];
				else if (b == 'v') Verbose = true;
				else throw new Exception("Bad option: -" + b);
			}
		}
		#endregion
	}
}
