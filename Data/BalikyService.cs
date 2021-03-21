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
    public class BalikyService
    {
		public Task<Stats> DeckStats(string path)
		{
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});
			List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
			return Task.FromResult(new Stats
			{
				ColorLess = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Count() == 0).Count(),
				White = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.W)).Where(c => c.ColorIdentity.Count() == 1).Count(),
				Black = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.B)).Where(c => c.ColorIdentity.Count() == 1).Count(),
				Blue = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.U)).Where(c => c.ColorIdentity.Count() == 1).Count(),
				Red = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.R)).Where(c => c.ColorIdentity.Count() == 1).Count(),
				Green = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.G)).Where(c => c.ColorIdentity.Count() == 1).Count(),
				BiColor = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Count() == 2).Count(),
				ThreeColor = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Count() == 3).Count(),
				FourColor = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Count() == 4).Count(),
				FiveColor = vyber.Where(c => !c.TypeLine.ToLower().Contains("Land".ToLower())).Where(c => c.ColorIdentity.Count() == 5).Count(),
				Zero = vyber.Where(c => c.Cmc == 0).Count(),
				One = vyber.Where(c => c.Cmc == 1).Count(),
				Two = vyber.Where(c => c.Cmc == 2).Count(),
				Three = vyber.Where(c => c.Cmc == 3).Count(),
				Four = vyber.Where(c => c.Cmc == 4).Count(),
				Five = vyber.Where(c => c.Cmc == 5).Count(),
				Six = vyber.Where(c => c.Cmc == 6).Count(),
				More = vyber.Where(c => c.Cmc > 6).Count(),
				Creature = new Blazor.Data.Stats2{
					All   =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Count(),
 					One   =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Where(c => c.Cmc == 1).Count(),
					Two   =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Where(c => c.Cmc == 2).Count(),
					Three =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Where(c => c.Cmc == 3).Count(),
					Four  =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Where(c => c.Cmc == 4).Count(),
					Five  =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Where(c => c.Cmc == 5).Count(),
					Six   =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Where(c => c.Cmc == 6).Count(),
					More  =(vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList()).Where(c => c.Cmc  > 6).Count()
					},
				Instant = new Blazor.Data.Stats2
				{
					All = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Count(),
					One = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Where(c => c.Cmc == 1).Count(),
					Two = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Where(c => c.Cmc == 2).Count(),
					Three = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Where(c => c.Cmc == 3).Count(),
					Four = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Where(c => c.Cmc == 4).Count(),
					Five = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Where(c => c.Cmc == 5).Count(),
					Six = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Where(c => c.Cmc == 6).Count(),
					More = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList()).Where(c => c.Cmc > 6).Count()
				},
				Land = (vyber.Where(c => c.TypeLine.ToLower().Contains("Land".ToLower())).ToList()).Count(),
				Sorcery = new Blazor.Data.Stats2
				{
					All = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Count(),
					One = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Where(c => c.Cmc == 1).Count(),
					Two = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Where(c => c.Cmc == 2).Count(),
					Three = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Where(c => c.Cmc == 3).Count(),
					Four = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Where(c => c.Cmc == 4).Count(),
					Five = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Where(c => c.Cmc == 5).Count(),
					Six = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Where(c => c.Cmc == 6).Count(),
					More = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList()).Where(c => c.Cmc > 6).Count()
				},
				Enchant = new Blazor.Data.Stats2
				{
					All = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Count(),
					One = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Where(c => c.Cmc == 1).Count(),
					Two = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Where(c => c.Cmc == 2).Count(),
					Three = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Where(c => c.Cmc == 3).Count(),
					Four = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Where(c => c.Cmc == 4).Count(),
					Five = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Where(c => c.Cmc == 5).Count(),
					Six = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Where(c => c.Cmc == 6).Count(),
					More = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchant".ToLower())).ToList()).Where(c => c.Cmc > 6).Count()
				},
				Artifact = new Blazor.Data.Stats2
				{
					All = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Count(),
					One = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Where(c => c.Cmc == 1).Count(),
					Two = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Where(c => c.Cmc == 2).Count(),
					Three = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Where(c => c.Cmc == 3).Count(),
					Four = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Where(c => c.Cmc == 4).Count(),
					Five = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Where(c => c.Cmc == 5).Count(),
					Six = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Where(c => c.Cmc == 6).Count(),
					More = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList()).Where(c => c.Cmc > 6).Count()
				},
				Planeswalker = (vyber.Where(c => c.TypeLine.ToLower().Contains("Planeswalker".ToLower())).ToList()).Count()
			}) ;

			//.Where(c => !string.ReferenceEquals(c.Text, null))
			//.Where(c => c.Text.ToLower().Contains("reach".ToLower()))
		}

		public Task<Karty[]> RandomHand(string path)
        {
			int temp;
			int[] lotto = new int[10];

			Random rand = new Random();

			for (int i = 0; i < 10; i++)
			{
				do
				{
					temp = rand.Next(0, 100);
				}
				while (lotto.Contains(temp));
				lotto[i] = temp;
			}
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});
			List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
			Scryfall.API.Models.Card[] pole = vyber.ToArray();
			List < Scryfall.API.Models.Card> nahoda = new List<Scryfall.API.Models.Card>();
			for(int i =0; i<7;i++)
            {
				nahoda.Add(pole[lotto[i]]); 
            }
			return Task.FromResult(nahoda.Select(c => new Karty
			{
				Url = (c.Layout == Scryfall.API.Models.Layouts.ModalDfc || c.Layout == Scryfall.API.Models.Layouts.Transform) ? c.CardFaces[0].ImageUris.Normal + '|' + c.CardFaces[1].ImageUris.Normal : c.ImageUris.Normal,
				//string.ReferenceEquals(c.ImageUris.Normal, null)?new Uri("https://upload.wikimedia.org/wikipedia/en/thumb/a/aa/Magic_the_gathering-card_back.jpg/220px-Magic_the_gathering-card_back.jpg"):new Uri(c.ImageUris.Normal),
				Name = (!string.IsNullOrWhiteSpace(c.Effects)) ? c.pocet + "x : " + c.Name + " - " + c.Effects : c.pocet + "x : " + c.Name
				//string.ReferenceEquals(c.ImageUris.Normal, null)?c.Name+"\r"+c.OracleText : c.Name
			}).ToArray());

		}


	}
}
