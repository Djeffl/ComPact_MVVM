using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ComPact
{
	public class BaseWebservice
	{
		private static HttpClient _instance;
		private static string _baseUri = "http://10.99.9.93:8080/"; //"http://192.168.56.1:8080/"

		/**
		 * 
		 */
		void CreateHttpClient()
		{
			_instance = new HttpClient();
			_instance.BaseAddress = new Uri(_baseUri);
			_instance.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		}
		/**
		 * 
		 */
		HttpClient GetHttpClient()
		{
			if (_instance == null)
				CreateHttpClient();
			return _instance;
		}
		/**
		 * 
		 */
		protected async Task<T> GetRequestAsync<T>(string urlExtend)
			where T : new()
		{
			HttpClient client = GetHttpClient();
			T result;
			var response = await client.GetAsync(urlExtend);
			if (response.IsSuccessStatusCode)
			{
				var getResponseString = await response.Content.ReadAsStringAsync();
				result = await Task.Run(() => JsonConvert.DeserializeObject<T>(getResponseString));
				return result;
			}
			else
			{
				result = new T();
			}
			return result;
		}
		/**
		 * Reference for HttpClient.PostAsJsonAsync 'System.Net.Http.Formatting.dll'
		 * return status code and Object
		 */
		protected async Task<Tuple<int, R>> PostRequestAsync<T, R>(string urlExtend, T obj)
		{
			HttpClient client = GetHttpClient();
			//HttpResponseMessage response = await client.PostAsJsonAsync(urlExtend, obj);
			//response.EnsureSuccessStatusCode();

			var data = JsonConvert.SerializeObject(obj);
			var postContent = new StringContent(data, Encoding.UTF8, "application/json");
			var response = await client.PostAsync(urlExtend, postContent);
			//string responseMsg = await response.Content.ReadAsStringAsync();
			int statusCode = (int)response.StatusCode;
			var result = JsonConvert.DeserializeObject<R>(response.Content.ReadAsStringAsync().Result);

			return new Tuple<int, R>(statusCode, result);
		}
		/**
		 * Reference for HttpClient.PostAsJsonAsync 'System.Net.Http.Formatting.dll'
		 * return status code and Object
		 */
		protected async Task<HttpResponseMessage> PostRequestAsync<T>(string urlExtend, T obj)
		{
			HttpClient client = GetHttpClient();
			//HttpResponseMessage response = await client.PostAsJsonAsync(urlExtend, obj);
			//response.EnsureSuccessStatusCode();

			var data = JsonConvert.SerializeObject(obj);
			var postContent = new StringContent(data, Encoding.UTF8, "application/json");
			var response = await client.PostAsync(urlExtend, postContent);
			//string responseMsg = await response.Content.ReadAsStringAsync();

			return response;
		}
		//public async Task<Tuple<int, R>> PostRequestAsync<T, R>(string urlExtend, T obj)
		//{
		//	HttpClient client = this.getHttpClient();
		//	//HttpResponseMessage response = await client.PostAsJsonAsync(urlExtend, obj);
		//	//response.EnsureSuccessStatusCode();

		//	var data = JsonConvert.SerializeObject(obj);
		//	var postContent = new StringContent(data, Encoding.UTF8, "application/json");
		//	var response = await client.PostAsync(urlExtend, postContent);
		//	//string responseMsg = await response.Content.ReadAsStringAsync();
		//	int statusCode = (int)response.StatusCode;
		//	var result = JsonConvert.DeserializeAnonymousType(response.Content.ReadAsStringAsync().Result, typeof(string[]));
		//	return new Tuple<int, R>(statusCode, result);

		//public async Task<T> UpdateProductAsync<T>(string urlExtend, T obj)
		//{
			//HttpClient client = this.getHttpClient();
			//HttpResponseMessage response = await client.PutAsJsonAsync(urlExtend, obj);
			//response.EnsureSuccessStatusCode();
			// Deserialize the updated product from the response body.
			//T updatedObj = await response.Content.ReadAsAsync<T>();

			//return updatedObj;
		//}
		//public async Task DeleteProductAsync(params)
		//{
		//	HttpResponseMessage response = await client.DeleteAsync(urlExtend, params);
		//	return response.StatusCode;
		//}
	}
}
