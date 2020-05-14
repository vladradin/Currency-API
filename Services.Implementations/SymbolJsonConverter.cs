using System;
using Bussiness.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Services.Implementations
{
	internal class SymbolJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return false;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var symbol = JToken.Load(reader);
			return new CurrencySymbol(symbol.ToString());
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			//to be implemented if necessary
		}
	}
}