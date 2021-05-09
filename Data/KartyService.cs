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
    public class KartyService
    {

        public Task<Karty[]> AllCards(string path)
        {
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});
			List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
			vyber = vyber.OrderBy(x => x.Name).ToList();
				foreach(var card in vyber)
                {
					card.pocet = vyber.Where(c => c.Name == card.Name).Where(c => c.Set == card.Set).Where(c => c.Effects == card.Effects).ToList().Count();
                }
				var zoznam = vyber.GroupBy(p => new { p.Name, p.Effects ,p.Set})
						   .Select(grp => grp.First())
						   .ToList();
				return Task.FromResult(zoznam.Select(c => new Karty
				{
					Url = (c.Layout == Scryfall.API.Models.Layouts.ModalDfc || c.Layout == Scryfall.API.Models.Layouts.Transform) ? c.CardFaces[0].ImageUris.Normal + '|' + c.CardFaces[1].ImageUris.Normal : c.ImageUris.Normal,
					//string.ReferenceEquals(c.ImageUris.Normal, null)?new Uri("https://upload.wikimedia.org/wikipedia/en/thumb/a/aa/Magic_the_gathering-card_back.jpg/220px-Magic_the_gathering-card_back.jpg"):new Uri(c.ImageUris.Normal),
					Name = (!string.IsNullOrWhiteSpace(c.Effects)) ? c.pocet + "x : " + c.Name + " - " + c.Set + " - " + c.Effects : c.pocet + "x : " + c.Name + " - " +c.Set
					//string.ReferenceEquals(c.ImageUris.Normal, null)?c.Name+"\r"+c.OracleText : c.Name
				}).ToArray());

			//.Where(c => !string.ReferenceEquals(c.Text, null))
			//.Where(c => c.Text.ToLower().Contains("reach".ToLower()))
		}

		public Task<SetList[]> ListOfSets(string path)
        {
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});
			List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
			var zoznamset = vyber.GroupBy(p => new { p.Set })
				.Select(grp => grp.First())
						   .ToList();
			List<Blazor.Data.SetList> lista = new List<Blazor.Data.SetList>();
			foreach (var abd in zoznamset)
			{
				lista.Add(new Blazor.Data.SetList(abd.Set.ToString(), vyber.Where(c => c.Set.ToString() == abd.Set.ToString()).Select(c => c.SetName).First().ToString()));
			}
			lista.Add(new Blazor.Data.SetList("0",""));
			lista=lista.OrderBy(x => x.Code).ToList();
			return Task.FromResult(lista.Select(c => new Blazor.Data.SetList
			(c.Code,c.Name)).ToArray());
		}

		public Task<Karty[]> FindCards(string path, bool Singleton, Filter filters, bool group)
		{
			var subor = File.ReadAllText(@path);
			var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
			{
				TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
				NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
			});
			List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
			//Card Name, Card Type, Card Text
			vyber = (!string.IsNullOrWhiteSpace(filters.Name)) ? vyber.Where(c => c.Name.ToLower().Contains((filters.Name).ToLower())).ToList() : vyber.ToList();
			vyber = (!string.IsNullOrWhiteSpace(filters.CardType)) ? vyber.Where(c => c.TypeLine.ToLower().Contains((filters.CardType).ToLower())).ToList() : vyber.ToList();
			vyber = (!string.IsNullOrWhiteSpace(filters.Text)) ? vyber.Where(c => !string.ReferenceEquals(c.OracleText, null)).Where(c => c.OracleText.ToLower().Contains((filters.Text).ToLower())).ToList() : vyber.ToList();
			//Legendary
			vyber = (filters.Legendary) ? vyber.Where(c => c.TypeLine.ToLower().Contains("Legendary".ToLower())).ToList() : vyber.ToList();
			vyber = (filters.Snow) ? vyber.Where(c => c.TypeLine.ToLower().Contains("Snow".ToLower())).ToList() : vyber.ToList();
			//Rarity
			List<Scryfall.API.Models.Card> vzacnost = new List<Scryfall.API.Models.Card>();
			if (filters.Rarita.Common) vzacnost.AddRange(vyber.Where(c => c.Rarity == Scryfall.API.Models.Rarity.Common).ToList());
			if (filters.Rarita.Uncommon) vzacnost.AddRange(vyber.Where(c => c.Rarity == Scryfall.API.Models.Rarity.Uncommon).ToList());
			if (filters.Rarita.Rare) vzacnost.AddRange(vyber.Where(c => c.Rarity == Scryfall.API.Models.Rarity.Rare).ToList());
			if (filters.Rarita.Mythic) vzacnost.AddRange(vyber.Where(c => c.Rarity == Scryfall.API.Models.Rarity.Mythic).ToList());
			vyber = ((filters.Rarita.Common) || (filters.Rarita.Uncommon) || (filters.Rarita.Rare) || (filters.Rarita.Mythic)) ? vzacnost.ToList() : vyber.ToList();
			//Type
			if (filters.Typy.Artifact) vyber = (vyber.Where(c => c.TypeLine.ToLower().Contains("Artifact".ToLower())).ToList());
			if (filters.Typy.Land) vyber = (vyber.Where(c => c.TypeLine.ToLower().Contains("Land".ToLower())).ToList());
			if (filters.Typy.Instant) vyber = (vyber.Where(c => c.TypeLine.ToLower().Contains("Instant".ToLower())).ToList());
			if (filters.Typy.Creature) vyber = (vyber.Where(c => c.TypeLine.ToLower().Contains("Creature".ToLower())).ToList());
			if (filters.Typy.Planeswalker) vyber = (vyber.Where(c => c.TypeLine.ToLower().Contains("Planeswalker".ToLower())).ToList());
			if (filters.Typy.Sorcery) vyber = (vyber.Where(c => c.TypeLine.ToLower().Contains("Sorcery".ToLower())).ToList());
			if (filters.Typy.Enchantment) vyber = (vyber.Where(c => c.TypeLine.ToLower().Contains("Enchantment".ToLower())).ToList());
			////Colors
			if (filters.Colors.White == 1) vyber = (vyber.Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.W))).ToList(); else if (filters.Colors.White == 3) vyber = (vyber.Where(c => !c.ColorIdentity.Contains(Scryfall.API.Models.Colors.W))).ToList();
			if (filters.Colors.Black == 1) vyber = (vyber.Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.B))).ToList(); else if (filters.Colors.Black == 3) vyber = (vyber.Where(c => !c.ColorIdentity.Contains(Scryfall.API.Models.Colors.B))).ToList();
			if (filters.Colors.Blue == 1) vyber = (vyber.Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.U))).ToList(); else if (filters.Colors.Blue == 3) vyber = (vyber.Where(c => !c.ColorIdentity.Contains(Scryfall.API.Models.Colors.U))).ToList();
			if (filters.Colors.Red == 1) vyber = (vyber.Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.R))).ToList(); else if (filters.Colors.Red == 3) vyber = (vyber.Where(c => !c.ColorIdentity.Contains(Scryfall.API.Models.Colors.R))).ToList();
			if (filters.Colors.Green == 1) vyber = (vyber.Where(c => c.ColorIdentity.Contains(Scryfall.API.Models.Colors.G))).ToList(); else if (filters.Colors.Green == 3) vyber = (vyber.Where(c => !c.ColorIdentity.Contains(Scryfall.API.Models.Colors.G))).ToList();
			//numbers of colors
			List<Scryfall.API.Models.Card> numbers = new List<Scryfall.API.Models.Card>();
			if (filters.Colorless) numbers.AddRange(vyber.Where(c => c.ColorIdentity.Count() == 0).ToList());
			if (filters.Monocolor) numbers.AddRange(vyber.Where(c => c.ColorIdentity.Count() == 1).ToList());
			if (filters.Bicolor) numbers.AddRange(vyber.Where(c => c.ColorIdentity.Count() == 2).ToList());
			if (filters.Tricolor) numbers.AddRange(vyber.Where(c => c.ColorIdentity.Count() == 3).ToList());
			if (filters.Fourcolor) numbers.AddRange(vyber.Where(c => c.ColorIdentity.Count() == 4).ToList());
			if (filters.Fivecolor) numbers.AddRange(vyber.Where(c => c.ColorIdentity.Count() == 5).ToList());
			vyber = ((filters.Colorless) || (filters.Monocolor) || (filters.Bicolor) || (filters.Tricolor) || (filters.Fourcolor) || (filters.Fivecolor)) ? numbers.ToList() : vyber.ToList();
			if (filters.Edition != "0") vyber = (vyber.Where(c => c.Set == filters.Edition)).ToList();
			if (filters.Foil) vyber = (vyber.Where(c => !string.ReferenceEquals(c.Effects, null)).Where(c => c.Effects.Contains("F"))).ToList();
			if (filters.Showcase) vyber = (vyber.Where(c => !string.ReferenceEquals(c.Effects, null)).Where(c => c.Effects.Contains("S"))).ToList();
			if (filters.ManaCost == "1") vyber = (vyber.Where(c => c.Cmc == 1)).ToList();
			if (filters.ManaCost == "2") vyber = (vyber.Where(c => c.Cmc == 2)).ToList();
			if (filters.ManaCost == "3") vyber = (vyber.Where(c => c.Cmc == 3)).ToList();
			if (filters.ManaCost == "4") vyber = (vyber.Where(c => c.Cmc == 4)).ToList();
			if (filters.ManaCost == "5") vyber = (vyber.Where(c => c.Cmc == 5)).ToList();
			if (filters.ManaCost == "6") vyber = (vyber.Where(c => c.Cmc > 5)).ToList();
			vyber = vyber.OrderBy(x => x.Name).ToList();
			List<Scryfall.API.Models.Card> zoznam = new List<Scryfall.API.Models.Card>();
			if (group)
			{
				foreach (var card in vyber)
				{
					card.pocet = (!Singleton) ? vyber.Where(c => c.Name == card.Name).Where(c => c.Set == card.Set).Where(c => c.Effects == card.Effects).ToList().Count() : vyber.Where(c => c.Name == card.Name).ToList().Count();
				}

				zoznam = (Singleton) ? vyber.GroupBy(p => new { p.Name })
							   .Select(grp => grp.First())
							   .ToList()
							   :
							   vyber.GroupBy(p => new { p.Name, p.Effects, p.Set })
							   .Select(grp => grp.First())
							   .ToList();
			}
			else
			{
				int i = 0;
				foreach (var card in vyber)
				{
					card.poradie = i;
					i++;
				}
				zoznam = vyber.ToList();
			}
			return Task.FromResult(zoznam.Select(c => new Karty
			{
				Url = (c.Layout == Scryfall.API.Models.Layouts.ModalDfc || c.Layout == Scryfall.API.Models.Layouts.Transform) ? c.CardFaces[0].ImageUris.Normal + '|' + c.CardFaces[1].ImageUris.Normal : c.ImageUris.Normal,
				//string.ReferenceEquals(c.ImageUris.Normal, null)?new Uri("https://upload.wikimedia.org/wikipedia/en/thumb/a/aa/Magic_the_gathering-card_back.jpg/220px-Magic_the_gathering-card_back.jpg"):new Uri(c.ImageUris.Normal),
				Name = group ? (!string.IsNullOrWhiteSpace(c.Effects)) ? ((!Singleton) ? c.pocet + "x : " + c.Name + " - " + c.Set + " - " + c.Effects : c.pocet + "x : " + c.Name) : c.pocet + "x : " + c.Name + " - " + c.Set : (!string.IsNullOrWhiteSpace(c.Effects)) ? c.poradie + ":" + c.Name + "-" + c.Set + "-" + c.Effects : c.poradie + ":" + c.Name + "-" + c.Set + "-"
				//string.ReferenceEquals(c.ImageUris.Normal, null)?c.Name+"\r"+c.OracleText : c.Name
			}).ToArray());

			//.Where(c => !string.ReferenceEquals(c.Text, null))
			//.Where(c => c.Text.ToLower().Contains("reach".ToLower()))
		}
    
		public Task<string[]> Search(string name)
        {
			List<string> baliky = new List<string>();
			

			return Task.FromResult(baliky.ToArray());
        }
	
	}
}
