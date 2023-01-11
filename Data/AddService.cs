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
		public Task<string> AddListCards(string path, List<string> pridat)
		{
			Console.WriteLine("Zaciatok");
			Console.WriteLine(path);
			Console.WriteLine(pridat.Count());
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});

			List<Scryfall.API.Models.Card> zoznam = karty.ToList<Scryfall.API.Models.Card>();

			var scryfall = new ScryfallClient();
			int i = 0;
			foreach (string zaznam in pridat)
			{
				if (i != 0)
				{
					i++;
					string[] riadok = zaznam.Split(';');
					var card = scryfall.Cards.GetNamed((riadok[0].IndexOf("/")>0)?riadok[0].Substring(0, riadok[0].IndexOf("/")-1): riadok[0]  , null, riadok[1].Remove(riadok[1].IndexOf(' '),1));
					if (card.CollectorNumber != riadok[4].Remove(riadok[4].IndexOf(' '), 1))
					{
						if (!string.IsNullOrEmpty(riadok[3].Remove(riadok[3].IndexOf(' '), 1)))
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
						if (!string.IsNullOrEmpty(riadok[3].Remove(riadok[3].IndexOf(' '), 1)))
						{
							card.Effects = "F";
						}
					}
					for (int p = 0; p < Int32.Parse(riadok[2].Remove(riadok[2].IndexOf(' '), 1)); p++)
						zoznam.Add(card);

					Console.WriteLine(i + " : " + pridat.Count());
				}
				else
                {
					i++;
                }
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




		public Task<string> ChoosenCards(string pridat, string path)
		{
			Console.WriteLine("Zaciatok Choosen");
			Console.WriteLine(pridat);
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});

			List<Scryfall.API.Models.Card> zoznam = karty.ToList<Scryfall.API.Models.Card>();

			string[] fileEntries = Directory.GetFiles(@"json\");
			
			var subor2 = File.ReadAllText(fileEntries[0]);
			var karty2 = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor2, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});

			List<Scryfall.API.Models.Card> vsetky = karty2.ToList<Scryfall.API.Models.Card>();

			string[] riadky = pridat.Split(':');
			int i = 0;
			foreach (string zaznam in riadky)
			{
				string[] info = zaznam.Split(" _ ");

				var card = info[2].Length==0?
					vsetky.Where(c => c.Name == info[0])
					.Where(c=>c.Set==info[1])
					.FirstOrDefault()
					:
					vsetky.Where(c => c.Name == info[0])
					.Where(c => c.Set == info[1])
					.Where(c => c.Effects == info[2])
					.FirstOrDefault()
					;
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
		public Task<string> MoveCards(string pridat, string from, string to)
		{
			Console.WriteLine("Zaciatok Move");
			Console.WriteLine(pridat);
			var subor = File.ReadAllText(to);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});

			List<Scryfall.API.Models.Card> zoznam = karty.ToList<Scryfall.API.Models.Card>();

			var subor2 = File.ReadAllText(from);
			var karty2 = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor2, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});

			List<Scryfall.API.Models.Card> vsetky = karty2.ToList<Scryfall.API.Models.Card>();

			string[] riadky = pridat.Split(':');
			foreach (string zaznam in riadky)
			{
				string[] info = zaznam.Split(" _ ");

				var card = info[2].Length == 0 ?
					vsetky.Where(c => c.Name == info[0])
					.Where(c => c.Set == info[1])
					.FirstOrDefault()
					:
					vsetky.Where(c => c.Name == info[0])
					.Where(c => c.Set == info[1])
					.Where(c => c.Effects == info[2])
					.FirstOrDefault()
					;
				zoznam.Add(card);
				vsetky.Remove(card);
			}
			JsonSerializer serializer = new JsonSerializer();
			serializer.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
			serializer.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
			serializer.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto;
			serializer.Formatting = Newtonsoft.Json.Formatting.Indented;
			using (StreamWriter sw = new StreamWriter(to))
			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				serializer.Serialize(writer, zoznam.ToArray());
			}
			using (StreamWriter sw = new StreamWriter(from))
			using (JsonWriter writer = new JsonTextWriter(sw))
			{
				serializer.Serialize(writer, vsetky.ToArray());
			}
			return Task.FromResult("Skoncilo");
		}

	}
}
