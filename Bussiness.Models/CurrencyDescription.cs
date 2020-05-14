
namespace Bussiness.Models
{
	public class CurrencySymbol
	{
		private string _symbol;

		public CurrencySymbol(string symbol) { _symbol = symbol; }

		public override string ToString()
		{
			return _symbol;
		}
	}
}
