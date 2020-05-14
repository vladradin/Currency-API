using DtoModels;
using MySql.Data.MySqlClient;
using Repositories.Implementations.MySql;
using Repositories.Interfaces;
using Repositories.Interfaces.SQLCrudHelpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
	public class RepositoryBase<T> : IRepository<T>
		where T: BaseItem,new()

	{
		private readonly ConnectionSettings _settings;
		private readonly string _tableName;
		protected readonly IDatabaseFactory _dbFactory;

		public RepositoryBase(ConnectionSettings connectionSettings, IDatabaseFactory databaseFactory,string tableName)
		{
			_settings = connectionSettings;
			_dbFactory = databaseFactory;
			_tableName = tableName;
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			var queryStringBuilder = _dbFactory.GetQueryStringBuilder<T>();

			var entities = await QueryDbWith(queryStringBuilder);

			return entities;
		}

		public async Task<T> Add(T newItem)
		{
			newItem.Id = newItem.Id ?? Guid.NewGuid().ToString();

			var connection = _dbFactory.CreateConnection(_settings.ConnectionString);

			using (connection)
			{
				connection.Open();
				var insertionsString = _dbFactory.GetInsertionString<T>().Create(newItem, _tableName);
				var insertCommand = _dbFactory.CreateCommand(insertionsString);
				var rowsAffected = await insertCommand.ExecuteNonQueryAsync();
			}

			return newItem;
		}

		public async Task Add(IEnumerable<T> items)
		{
			foreach(var item in items)
			{
				await Add(item);
			}
		}

		public async Task<T> GetById(string itemId)
		{
			var queryStringBuilder = _dbFactory.GetQueryStringBuilder<T>()
												.Is(entity => entity.Id,itemId);

			var entities = await QueryDbWith(queryStringBuilder);

			return entities.FirstOrDefault();
		}

		protected async Task<IEnumerable<T>> QueryDbWith(ISqlQueryStringBuilder<T> configuredQueryBuilder)
		{
			var connection = _dbFactory.CreateConnection(_settings.ConnectionString);

			using (connection)
			{
				connection.Open();

				var queryString = configuredQueryBuilder.Create(_tableName);

				var command = _dbFactory.CreateCommand(queryString);

				var entities = await PopulateDataFrom(command);

				return entities;
			}
		}

		private async Task<IEnumerable<T>> PopulateDataFrom(DbCommand command)
		{
			IList<T> entities = new List<T>();
			using (var reader = await command.ExecuteReaderAsync())
			{
				while (reader.Read())
				{
					var entity = new T();

					foreach (var property in TProperties)
					{
						var propertyValue = reader[property.Name];
						property.SetValue(entity, propertyValue);
					}

					entities.Add(entity);
				}
			}
			return entities;
		}

		private static IEnumerable<PropertyInfo> TProperties = typeof(T).GetProperties();

		public Task<bool> Update(string itemId, T updateItem)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Remove(string itemId)
		{
			throw new NotImplementedException();
		}
	}
}
