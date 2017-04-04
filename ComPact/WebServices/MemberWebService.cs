using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComPact;
using Newtonsoft.Json;

namespace ComPact
{
	public class MemberWebService : BaseWebservice<Member>, IMemberWebService
	{
		public async Task<Member> Forgot(string urlExtend, Member obj)
		{
			var client = GetHttpClient();

			var data = JsonConvert.SerializeObject(obj);
			var postContent = new StringContent(data, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(urlExtend, postContent);
			response.EnsureSuccessStatusCode();
			var te = await response.Content.ReadAsStringAsync();
			//System.Diagnostics.Debug.WriteLine(te);
			var result = JsonConvert.DeserializeObject<Member>(await response.Content.ReadAsStringAsync());
			System.Diagnostics.Debug.WriteLine(result);
			return result;
		}
	}
}
