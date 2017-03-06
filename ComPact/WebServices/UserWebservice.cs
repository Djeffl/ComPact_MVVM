using System;
using System.Threading.Tasks;

namespace ComPact
{
	public class UserWebservice: BaseWebservice, IUserWebservice
	{
		//private static User user;
		public UserWebservice()
		{
			//Task.Run(() => this.CreateUser("Jeff", "Liekens","jeffliekens7@hotmail.com","test")).Wait();
		}
		public async Task<string> CreateUserAsync(User newUser)
		{
			/**
			 * @Params = specifieke url, object
			 */
			string response = await base.PostRequestAsync("users/new", newUser);

			return response;
		}

		//var response = await client.GetAsync(uri);
		//	if (response.IsSuccessStatusCode)
		//	{
		//		var content = await response.Content.ReadAsStringAsync();
		//		Items = JsonConvert.DeserializeObject<List<TodoItem>>(content);
		//	}
	}
}
