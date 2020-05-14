using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement
{
	public interface IJwtTokenService
	{
		string GenerateToken(AppUser appUser);
	}
}
