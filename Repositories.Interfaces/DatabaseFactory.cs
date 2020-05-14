using Repositories.Interfaces.SQLCrudHelpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Repositories.Interfaces
{
	public interface IDatabaseFactory
	{
		DbConnection CreateConnection(string connectionString);
		DbCommand CreateCommand(string command);
		DbDataReader CreateReader();
		ISqlInsertionString<T> GetInsertionString<T>();
		ISqlQueryStringBuilder<T> GetQueryStringBuilder<T>();
	}
}
