using DtoModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
	public interface IRepository<T>
		where T:BaseItem
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(string itemId);
		Task<T> Add(T newItem);
		Task Add(IEnumerable<T> items);
		Task<bool> Update(string itemId, T updateItem);		
		Task<bool> Remove(string itemId);
	}
}
