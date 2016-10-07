using System;
using System.Text.RegularExpressions;
using System.Speech.Synthesis;

namespace Orez.CmdModule.Zero {
	class Console0 {

		#region static data
		/// <summary>
		/// Text dictator.
		/// </summary>
		public static SpeechSynthesizer Dictator;
		#endregion



		#region static method
		/// <summary>
		/// Setup console usage.
		/// </summary>
		public static void Setup() {
			// Dictator = new SpeechSynthesizer();
			// Dictator.SelectVoiceByHints(VoiceGender.Female);
		}


		/// <summary>
		/// Write to console, if verbose enabled.
		/// </summary>
		/// <param name="o">Input options.</param>
		/// <param name="f">Format string.</param>
		/// <param name="a">Arguments.</param>
		public static void Verbose(Option o, string f, params object[] a) {
			if (!o.Verbose) return;
			var m = Regex.Match(f, @"\((.*)\)");
			if (Dictator != null && m.Groups.Count > 1) {
				var mv = m.Groups[1].Value;
				f = f.Replace("(" + mv + ")", "");
				Dictator.SpeakAsync(mv);
			}
			if (o.Verbose) Console.WriteLine(f, a);
		}
		#endregion
	}
}
