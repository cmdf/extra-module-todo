using System.IO;

namespace Orez.CmdModule.IO {
	class Directory0 {

		// static method
		/// <summary>
		/// Create directory.
		/// </summary>
		/// <param name="d">Directory path.</param>
		public static void Create(string d) {
			Create(new DirectoryInfo(d));
		}
		
		/// <summary>
		/// Create directory.
		/// </summary>
		/// <param name="d">Directory.</param>
		public static void Create(DirectoryInfo d) {
			if (!d.Exists) d.Create();
		}


		/// <summary>
		/// Delete directory.
		/// </summary>
		/// <param name="d">Directory path.</param>
		/// <param name="r">Recursively delete?</param>
		public static void Delete(string d, bool r = false) {
			Delete(new DirectoryInfo(d), r);
		}

		/// <summary>
		/// Delete directory.
		/// </summary>
		/// <param name="d">Directory.</param>
		/// <param name="r">Recursively delete?</param>
		public static void Delete(DirectoryInfo d, bool r = false) {
			if (!d.Exists) return;
			SetAttributes(d, FileAttributes.Normal);
			d.Delete(r);
		}


		/// <summary>
		/// Copy directory.
		/// </summary>
		/// <param name="ds">Source directory path.</param>
		/// <param name="dd">Destination directory path.</param>
		/// <param name="r">Recursively copy?</param>
		public static void Copy(string ds, string dd, bool r = false) {
			Copy(new DirectoryInfo(ds), new DirectoryInfo(dd), r);
		}
		
		/// <summary>
		/// Copy directory.
		/// </summary>
		/// <param name="ds">Source directory.</param>
		/// <param name="dd">Destination directory.</param>
		/// <param name="r">Recursively copy?</param>
		public static void Copy(DirectoryInfo ds, DirectoryInfo dd, bool r = false) {
			if (!dd.Exists) dd.Create();
			foreach (var dsf in ds.EnumerateFiles())
				dsf.CopyTo(Path.Combine(dd.FullName, dsf.Name), true);
			if (r) foreach (var dsd in ds.EnumerateDirectories())
				Copy(dsd, new DirectoryInfo(Path.Combine(dd.FullName, dsd.Name)), r);
		}

		
		/// <summary>
		/// Get files in directory.
		/// </summary>
		/// <param name="d">Directory path.</param>
		/// <param name="p">Search pattern.</param>
		/// <param name="r">Recursively get?</param>
		/// <returns>File paths.</returns>
		public static string[] GetFiles(string d, string p = "", bool r = false) {
			SearchOption o = r ? SearchOption.TopDirectoryOnly : SearchOption.TopDirectoryOnly;
			return Directory.Exists(d)? Directory.GetFiles(d, p, o) : new string[0];
		}

		/// <summary>
		/// Get files in directory.
		/// </summary>
		/// <param name="d">Directory.</param>
		/// <param name="p">Search pattern.</param>
		/// <param name="o">Recursively get?</param>
		/// <returns>Files.</returns>
		public static FileInfo[] GetFiles(DirectoryInfo d, string p = "*", bool r = false) {
			SearchOption o = r ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
			return d.Exists ? d.GetFiles(p, o) : new FileInfo[0];
		}
		

		/// <summary>
		/// Get non empty directory.
		/// </summary>
		/// <param name="d">Directory path.</param>
		/// <returns>Non empty directory path.</returns>
		public static string NonEmpty(string d) {
			return NonEmpty(new DirectoryInfo(d)).FullName;
		}

		/// <summary>
		/// Get non empty directory.
		/// </summary>
		/// <param name="d">Directory.</param>
		/// <returns>Non empty directory.</returns>
		public static DirectoryInfo NonEmpty(DirectoryInfo d) {
			if (!d.Exists) return d;
			while (true) {
				var dfs = d.GetFileSystemInfos();
				if (dfs.Length == 1 && Directory.Exists(dfs[0].FullName)) break;
				d = (DirectoryInfo) dfs[0];
			}
			return d;
		}


		/// <summary>
		/// Set directory attributes.
		/// </summary>
		/// <param name="d">Directory path.</param>
		/// <param name="a">File attributes.</param>
		/// <param name="r">Recursively set?</param>
		public static void SetAttributes(string d, FileAttributes a, bool r = false) {
			SetAttributes(new DirectoryInfo(d), a, r);
		}

		/// <summary>
		/// Set directory attributes.
		/// </summary>
		/// <param name="d">Directory.</param>
		/// <param name="a">File attributes.</param>
		/// <param name="r">Recursively set?</param>
		public static void SetAttributes(DirectoryInfo d, FileAttributes a, bool r = false) {
			if (!d.Exists) return;
			foreach (var df in d.EnumerateFiles())
				df.Attributes = a;
			if (r) foreach (var dd in d.EnumerateDirectories())
				SetAttributes(dd, a);
		}
	}
}
