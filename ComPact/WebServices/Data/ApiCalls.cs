using System;
namespace ComPact.WebServices.Data
{
	public static class ApiCalls
	{
		///URLS
		public const string BaseUserPath = "/api/users";
		public const string BaseAssignemntPath = "/api/assignments";
		public const string BasePaymentPath = "/api/payments";
		public const string BaseAuthPath = "/api/authentication";
		public const string CreateUserPath = "/api/auth/register";
		public const string LoginAuthPath = "/api/auth/login";
		public const string LoginTokenPath = "/api/users/login";

		public const string CreateAssignmentPath = "/api/assignments/create";
		public const string CreateMemberPath = "/api/users/addmember";

	}
}
