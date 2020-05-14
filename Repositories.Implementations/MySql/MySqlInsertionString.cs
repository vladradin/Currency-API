using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Repositories.Implementations.MySql
{
	public class MySqlInsertionString<T> : ISqlInsertionString<T>
	{
		public string Create(T itemToAdd,string tableName)
		{
			var sqlFieldValueDictionary = new Dictionary<string, string>();
			foreach (var property in TProperties)
			{
				var propertyValue = property.GetValue(itemToAdd);

				if (property.PropertyType == typeof(DateTime))
				{
					var dateTimeValue = (DateTime)propertyValue;
					sqlFieldValueDictionary.Add(property.Name.ToLower(), dateTimeValue.ToString("yyyy-MM-dd"));
				}

				else
					sqlFieldValueDictionary.Add(property.Name.ToLower(), propertyValue.ToString());
			}
			return CreateInsertionString(tableName, sqlFieldValueDictionary);
		}

		private string CreateInsertionString(string tableName,IDictionary<string,string> slqDictionaryValues)
		{
			var ( sqlFieldList,sqlValueList) = createFieldAndValuesList(slqDictionaryValues);
			return $"INSERT INTO `{tableName}` ({sqlFieldList}) VALUES ({sqlValueList})";
		}

		private (string fieldList,string valuesList) createFieldAndValuesList(IDictionary<string, string> slqDictionaryValues)
		{
			var sqlFieldNames = slqDictionaryValues.Keys.Select(fieldName => $"`{fieldName}`");
			var sqlFieldValues = slqDictionaryValues.Values.Select(fieldValue => $"'{fieldValue}'");
			return (
				string.Join(',', sqlFieldNames),
				string.Join(',', sqlFieldValues)
				);
		}

		private static IEnumerable<PropertyInfo> TProperties = typeof(T).GetProperties();
	}
}
