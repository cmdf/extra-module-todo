using System.Runtime.Serialization;
using Orez.CmdModule.Net.Ajax;

namespace Orez.CmdModule.Net.GitHub {
	[DataContract]
	class Release {

		// data
		[DataMember(Name = "url")]
		public string Url { get; set; }
		[DataMember(Name = "html_url")]
		public string HtmlUrl { get; set; }
		[DataMember(Name = "assets_url")]
		public string AssetsUrl { get; set; }
		[DataMember(Name = "upload_url")]
		public string UploadUrl { get; set; }
		[DataMember(Name = "tarball_url")]
		public string TarballUrl { get; set; }
		[DataMember(Name = "zipball_url")]
		public string ZipballUrl { get; set; }
		[DataMember(Name = "id")]
		public int Id { get; set; }
		[DataMember(Name = "tag_name")]
		public string TagName { get; set; }
		[DataMember(Name = "target_commitish")]
		public string TargetCommitish { get; set; }
		[DataMember(Name = "name")]
		public string Name { get; set; }
		[DataMember(Name = "body")]
		public string Body { get; set; }
		[DataMember(Name = "draft")]
		public bool Draft { get; set; }
		[DataMember(Name = "prerelease")]
		public bool Prerelease { get; set; }
		[DataMember(Name = "created_at")]
		public string CreatedAt { get; set; }
		[DataMember(Name = "published_at")]
		public string PublishedAt { get; set; }
		[DataMember(Name = "author")]
		public User Author { get; set; }
		[DataMember(Name = "assets")]
		public Asset[] Assets { get; set; }


		// static method
		/// <summary>
		/// Get latest release.
		/// </summary>
		/// <param name="p">Repo path.</param>
		/// <returns>Release.</returns>
		public static Release GetLatest(string p) {
			var url = "https://api.github.com/repos/" + p + "/releases/latest";
			return Json.Get(url, typeof(Release)) as Release;
		}

		/// <summary>
		/// Get release by tag name.
		/// </summary>
		/// <param name="p">Repo path.</param>
		/// <param name="t">Tag name.</param>
		/// <returns>Release.</returns>
		public static Release GetByTagName(string p, string t) {
			var url = "https://api.github.com/repos/" + p + "/releases/tags/" + t;
			return Json.Get(url, typeof(Release)) as Release;
		}
	}
}
