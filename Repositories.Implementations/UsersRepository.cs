using DtoModels;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
	public class UsersRepository : RepositoryBase<UserDTO>, IUserRepository
	{
		public UsersRepository(ConnectionSettings connectionSettings, IDatabaseFactory databaseFactory) 
			: base(connectionSettings, databaseFactory,"users")
		{
		}

		public async Task<UserDTO> GetByName(string name)
		{
			var queryStringBuilder = _dbFactory.GetQueryStringBuilder<UserDTO>();

			queryStringBuilder.Is(user => user.Username, name);

			var users = await QueryDbWith(queryStringBuilder);

			return users.FirstOrDefault();
		}
	}
}
