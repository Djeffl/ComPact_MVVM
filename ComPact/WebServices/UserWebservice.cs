using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ComPact
{
	public class UserWebservice : BaseWebservice, IUserWebservice
	{
		//private static User user;
		public UserWebservice()
		{
			//Task.Run(() => this.CreateUser("Jeff", "Liekens","jeffliekens7@hotmail.com","test")).Wait();
		}
		public async Task<Tuple<int, User>> CreateUserAsync(User newUser)
		{
			/**
			 * @Params = specifieke url, object
			 */
			Tuple<int, User> response = await PostRequestAsync<User, User>("users/new", newUser);

			return response;
		}
		/**
		 * return ResponseCode, IdUser
		 */
		public async Task<HttpResponseMessage> LoginUserAsync(string email, string password)
		{
			User loginObj = new User(null, null, null, email, password, null);
			HttpResponseMessage response = await PostRequestAsync<User>("users/login", loginObj);
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
