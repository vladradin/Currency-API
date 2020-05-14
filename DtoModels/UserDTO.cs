using System;
using System.Collections.Generic;
using System.Text;

namespace DtoModels
{
	public class UserDTO : BaseItem
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public DateTime CreationDate { get; set; }

		public string Email{get;set;}
	}
}
