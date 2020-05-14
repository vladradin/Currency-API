using Repositories.Interfaces.SQLCrudHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Repositories.Implementations.MySql
{
	public class MySqlQueryStringBuilder<T> : ISqlQueryStringBuilder<T>
	{
		IList<string> filterStrings = new List<string>();

		public string Create(string tableName)
		{
			string whereClause = BuildWhereClause();
			return $"select * from {tableName} {whereClause};";
		}

		private string BuildWhereClause()
		{
			if (filterStrings.Any())
				return $"WHERE {string.Join(" AND ", filterStrings)}";
			else
				return string.Empty;
		}

		public ISqlQueryStringBuilder<T> Is(Expression<Func<T, int>> selector, int value)
			=> AddFilterWhenValid(selector, propertyName => $"{propertyName}={value}");

		public ISqlQueryStringBuilder<T> Is(Expression<Func<T, string>> selector, string value)
			=> AddFilterWhenValid(selector, propertyName => $"{propertyName}='{value}'");

		public ISqlQueryStringBuilder<T> Contains(Expression<Func<T, string>> selector, string value)
			=> AddFilterWhenValid(selector, propertyName => $"{propertyName} LIKE '%{value}%'");

		public ISqlQueryStringBuilder<T> HasInRange(Expression<Func<T, int>> selector, int minValue, int maxValue)
			=> AddFilterWhenValid(selector, propertyName => $"{propertyName} BETWEEN {minValue} and {maxValue}");


		MySqlQueryStringBuilder<T> AddFilterWhenValid<U>(Expression<Func<T, U>> selector, Func<string, string> filterSpecificString)
		{
			var propertyName = ReadPropertyName(selector);
			if (propertyName != null)
			{
				var filter = filterSpecificString(propertyName);
				filterStrings.Add(filter);
			}

			return this;
		}

		private string ReadPropertyName<U>(Expression<Func<T, U>> selector)
		{
			if (selector.Body.NodeType == ExpressionType.MemberAccess)
			{
				var memberExpression = selector.Body as MemberExpression;
				return memberExpression.Member.Name;
			}
			return null;
		}

		private static readonly IEnumerable<PropertyInfo> TProperties = typeof(T).GetProperties();
	}
}
