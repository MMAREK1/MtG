using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scryfall.API;
using Newtonsoft.Json;
using System.IO;

namespace Blazor.Data
{
    public class AddService
    {

        public Task<string> AddCards(string path,string pridat)
        {
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});

			List<Scryfall.API.Models.Card> zoznam = karty.ToList<Scryfall.API.Models.Card>();

			var scryfall = new ScryfallClient();
			string[] riadky = pridat.Split(':');
			foreach (string zaznam in riadky)
			{
				string[] riadok = zaznam.Split(';');
				var card = scryfall.Cards.GetNamed(riadok[0], null, riadok[1]);
				if (card.CollectorNumber != riadok[4])
				{
					if (!string.IsNullOrEmpty(riadok[3]))
					{
						card.Effects = "FS";
					}
					else
					{
						card.Effects = "S";
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(riadok[3]))
					{
						card.Effects = "F";
					}
				}
				for (int p = 0; p < Int32.Parse(riadok[2]); p++)
					zoznam.Add(card);
			}
			zoznam = zoznam.OrderBy(x => x.Name).ToList();
			JsonSerializer serializer = new JsonSerializer();
			serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
			serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
			serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
			serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
			using (StreamWriter sw = new StreamWriter(@path))
			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				serializer.Serialize(writer, zoznam.ToArray());
			}
			return Task.FromResult("Skoncilo");
		}
    }
}
