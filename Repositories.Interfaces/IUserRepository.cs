using DtoModels;
using System;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface IUserRepository : IRepository<UserDTO>
	{
		Task<UserDTO> GetByName(string name);
	}
}
