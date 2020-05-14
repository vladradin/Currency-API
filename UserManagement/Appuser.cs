using Microsoft.AspNetCore.Identity;
using System;

namespace UserManagement
{
	public class AppUser 
	{
		public string UserId { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public DateTime CreationDate { get; set; }
		public string Email { get; set; }
	}
}
