using Bussiness.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Implementations
{
	class CurrencyJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return false;
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var currencies = new List<Currency>();

			var jObject = JObject.Load(reader);

			foreach (var child in jObject.Children())
			{
				var jProperty = child as JProperty;
				if(jProperty!=null)
				{
					var symbol = jProperty.Name;
					var amount = decimal.Parse(jProperty.Value.ToString());
					currencies.Add(new Currency { Amount = amount, Symbol = new CurrencySymbol(symbol) });
				}
				Console.WriteLine(child);
			}

			return currencies;
		}



		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			//To be implemented if necessary
		}
	}
}
