using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ComPact;
using Newtonsoft.Json;

namespace ComPact
{
	public class UserWebservice : BaseWebservice<User>, IUserWebservice
	{
		public async Task<User> Forgot(string urlExtend, User obj)
		{
			var client = GetHttpClient();

			var data = JsonConvert.SerializeObject(obj);
			var postContent = new StringContent(data, Encoding.UTF8, "application/json");
			HttpResponseMessage response = await client.PostAsync(urlExtend, postContent);
			response.EnsureSuccessStatusCode();
			var te = await response.Content.ReadAsStringAsync();
			//System.Diagnostics.Debug.WriteLine(te);
			var result = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
			System.Diagnostics.Debug.WriteLine(result);
			return result;
		}
	}
}
