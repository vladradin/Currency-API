using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement
{
	public class JwtConfig
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public ushort ExpiresInMinutes { get; set; }
	}
}
