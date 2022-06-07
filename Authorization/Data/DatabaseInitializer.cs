using Authorization.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace Authorization.Data
{
	public static class DatabaseInitializer
	{
		internal static void Init(IServiceProvider scopeServiceProvider)
		{
			//var context = scopeServiceProvider.GetService<ApplicationDbContext>();
			var userManager = scopeServiceProvider.GetService<UserManager<ApplicationUser>>();

			var user = new ApplicationUser()
			{
				UserName = "User",
				//Password = "123qwe",
				FirstName = "FirstName",
				LastName = "LastName"
			};

			var result = userManager.CreateAsync(user, "123qwe").GetAwaiter().GetResult();
			if (result.Succeeded)
			{
				userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Administrator")).GetAwaiter().GetResult();
			}

			//context.Users.Add(user);
			//context.SaveChanges();
		}
	}
}
