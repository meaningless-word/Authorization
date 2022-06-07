using Microsoft.AspNetCore.Identity;
using System;

namespace Authorization.Entities
{
	public class ApplicationUser : IdentityUser<Guid>
	{
		//public Guid Id { get; set; }
		//public string Password { get; set; }
		//public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}
