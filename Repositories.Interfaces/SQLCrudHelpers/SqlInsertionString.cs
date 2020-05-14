using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interfaces
{
	public interface ISqlInsertionString<T>
	{
		string Create(T itemToAdd, string tableName);
	}
}
