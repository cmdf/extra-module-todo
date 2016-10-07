using System;
using System.Net;
using System.Runtime.Serialization.Json;

namespace Orez.CmdModule.Net.Ajax {
	class Json {

		// static method
		/// <summary>
		/// Get JSON response.
		/// </summary>
		/// <param name="url">Get URL.</param>
		/// <param name="type">JSON type.</param>
		/// <returns>JSON object.</returns>
		public static object Get(string url, Type type) {
			var req = WebRequest.CreateHttp(url);
			req.Method = "GET";
			req.UserAgent = "test";
			var res = req.GetResponse();
			var jo = new DataContractJsonSerializer(type);
			return jo.ReadObject(res.GetResponseStream());
		}
	}
}
