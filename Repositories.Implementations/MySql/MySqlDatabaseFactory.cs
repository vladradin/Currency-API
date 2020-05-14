using MySql.Data.MySqlClient;
using Repositories.Interfaces;
using Repositories.Interfaces.SQLCrudHelpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Repositories.Implementations.MySql
{

	public class MySqlDatabaseFactory : IDatabaseFactory
	{
		private MySqlConnection _connection;
		private MySqlCommand _command;

		public DbConnection CreateConnection(string connectionString)
		{
			_connection = new MySqlConnection(connectionString);
			return _connection;
		}

		public DbCommand CreateCommand(string command)
		{
			_command = new MySqlCommand(command, _connection);
			return _command;
		}		

		public DbDataReader CreateReader()
			=> _command.ExecuteReader();

		public ISqlInsertionString<T> GetInsertionString<T>()
			=> new MySqlInsertionString<T>();

		public ISqlQueryStringBuilder<T> GetQueryStringBuilder<T>()
			=> new MySqlQueryStringBuilder<T>();
	}
}
