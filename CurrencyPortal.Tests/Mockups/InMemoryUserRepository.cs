using DtoModels;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
	public class InMemoryUserRepository : IUserRepository
	{
		public IList<UserDTO> Users = new List<UserDTO>();

		public Task<UserDTO> Add(UserDTO newItem)
		{
			newItem.Id = Guid.NewGuid().ToString();
			Users.Add(newItem);
			return Task.FromResult(newItem);
		}

		public Task Add(IEnumerable<UserDTO> items)
			=> Task.FromResult<IEnumerable<UserDTO>>(Users);

		public Task<IEnumerable<UserDTO>> GetAll()
		{
			IEnumerable<UserDTO> noUsers = new UserDTO[0];
			return Task.FromResult(noUsers);
		}

		public Task<UserDTO> GetById(string itemId)
			=> Task.FromResult(
				Users.FirstOrDefault(user => user.Id == itemId)
			);

		public Task<UserDTO> GetByName(string name)
			=> Task.FromResult(
				 Users.FirstOrDefault(user => user.Username == name)
			);

		public async Task<bool> Remove(string itemId)
		{
			var item = await GetById(itemId);

			if (item != null)
			{
				Users.Remove(item);
				return true;
			}

			return false;
		}

		public async Task<bool> Update(string itemId, UserDTO updateItem)
		{
			var removedSuccesfull = await Remove(itemId);

			if (removedSuccesfull)
			{
				await Add(updateItem);
				return true;
			}

			return false;
		}
	}
}
