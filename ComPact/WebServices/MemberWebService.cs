using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComPact;
using ComPact.Models;
using Newtonsoft.Json;

namespace ComPact
{
	public class MemberWebService : BaseWebService<WebMember>, IMemberWebService
	{
		public async Task<WebMember> Forgot(string urlExtend, WebMember obj)
		{
			var client = GetHttpClient();

			var data = JsonConvert.SerializeObject(obj);
			var postContent = new StringContent(data, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(urlExtend, postContent);
			response.EnsureSuccessStatusCode();
			var te = await response.Content.ReadAsStringAsync();
			//System.Diagnostics.Debug.WriteLine(te);
			var result = JsonConvert.DeserializeObject<WebMember>(await response.Content.ReadAsStringAsync());
			System.Diagnostics.Debug.WriteLine(result);
			return result;
		}
	}
}
