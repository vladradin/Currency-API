
using System;
using System.Linq.Expressions;

namespace Repositories.Interfaces.SQLCrudHelpers
{
	public interface ISqlQueryStringBuilder<T>
	{
		string Create(string tableName);
		ISqlQueryStringBuilder<T> Is(Expression<Func<T, int>> selector, int value);
		ISqlQueryStringBuilder<T> Is(Expression<Func<T, string>> selector, string value);
		ISqlQueryStringBuilder<T> Contains(Expression<Func<T, string>> selector, string value);
		ISqlQueryStringBuilder<T> HasInRange(Expression<Func<T, int>> selector, int minValue, int maxValue);
	}
}
