using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
// using System.Speech.Synthesis;
using Orez.CmdModule.Part;
using Orez.CmdModule.Zero;

namespace Orez.CmdModule {
	class Program {

		// type
		/// <summary>
		/// Delegate to a command function.
		/// </summary>
		/// <param name="o">Input options.</param>
		private delegate void CmdFn(Option o);


		// constant data
		/// <summary>
		/// Application name.
		/// </summary>
		public const string APP = "omodule";


		// static data
		/// <summary>
		/// Application data path.
		/// </summary>
		public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		/// <summary>
		/// Command to command function map.
		/// </summary>
		private static IDictionary<string, CmdFn> Fn = new Dictionary<string, CmdFn>() {
			["get"] = Module0.CmdGet, ["set"] = Module0.CmdSet, ["add"] = Module0.CmdAdd, ["remove"] = Module0.CmdRemove, ["update"] = Module0.CmdUpdate,
			["link get"] = Link0.CmdGet, ["link set"] = Link0.CmdSet, ["link add"] = Link0.CmdAdd, ["link remove"] = Link0.CmdRemove
		};



		// static method
		/// <summary>
		/// It can be very hard to detect just how much our judgement is constantly affected by our feelings.
		/// We should - at points - take care to be very sceptical of our first impulses.
		/// : Efil fo Loohcs Eht
		/// </summary>
		/// <param name="args">Input Arguments.</param>
		static void Main(string[] args) {
			try {
				Setup();
				Option o = new Option(args);
				if (Fn.ContainsKey(o.Command)) Fn[o.Command](o);
				else throw new Exception("Bad command: " + o.Command);
			}
			catch (Exception e) { Console.Error.WriteLine("{0}: {1}", APP, e.Message); }
		}

		/// <summary>
		/// Setup application.
		/// </summary>
		private static void Setup() {
			Console0.Setup();
			Module0.Setup();
			Link0.Setup();
		}
	}
}
