using System;
namespace ComPact
{
	public class Registration
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public bool Admin { get; set; }
		/**
		 * Params id, firstname, lastname, email, password
		 */
		public Registration(string firstName, string lastName, string email, string password, string confirmPassword, bool admin)
		{
		FirstName = firstName;
		LastName = lastName;
		Email = email;
		Password = password;
		Admin = admin;
		}
		public override string ToString()
		{
			return string.Format("[Registration: FirstName={0}, LastName={1}, Email={2}, Password={3}, ConfirmPassword={4}, Admin={5}]", FirstName, LastName, Email, Password, ConfirmPassword, Admin);
		}
	}
}
