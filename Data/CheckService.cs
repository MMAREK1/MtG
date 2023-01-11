using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.Data
{
    public class CheckService
    {
        public Task<string[]> FindMatch(string hladam)
        {
            List<string> vlastnim = new List<string>();

            var subor = File.ReadAllText("json\\All.json");
            var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });
            List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
            List<string> hladaneKarty = hladam.Split(';').ToList();
            foreach(string karta in hladaneKarty)
            {
                var pocet = vyber.Where(c => c.Name == karta).ToList();
                if (pocet.Count() > 1)
                {
                    int navyse = pocet.Count() - 1;
                    vlastnim.Add(karta + " : "+ pocet.Select(c=>c.Rarity).FirstOrDefault()+ " : " + navyse + "-krat");
                }
            }

            return Task.FromResult(vlastnim.ToArray());
        }
        public Task<string[]> FindMam(string hladam)
        {
            List<string> vlastnim = new List<string>();

            var subor = File.ReadAllText("json\\All.json");
            var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });
            List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
            List<string> hladaneKarty = hladam.Split(';').ToList();
            foreach (string karta in hladaneKarty)
            {
                var pocet = vyber.Where(c => c.Name == karta).ToList();
                if (pocet.Count() > 0)
                {
                    vlastnim.Add(karta + " : " + pocet.Count + "-krat");
                }
            }

            return Task.FromResult(vlastnim.ToArray());
        }
        public Task<string[]> FindNemam(string hladam)
        {
            List<string> nevlastnim = new List<string>();

            var subor = File.ReadAllText("json\\All.json");
            var karty = JsonConvert.DeserializeObject<Scryfall.API.Models.Card[]>(subor, new Newtonsoft.Json.JsonSerializerSettings
            {
                TypeNameHandling = Newtonsoft.Json.TypeNameHandling.Auto,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            });
            List<Scryfall.API.Models.Card> vyber = karty.ToList<Scryfall.API.Models.Card>();
            List<string> hladaneKarty = hladam.Split(';').ToList();
            foreach (string karta in hladaneKarty)
            {
                var pocet = vyber.Where(c => c.Name == karta).ToList();
                if (pocet.Count() == 0)
                {
                    nevlastnim.Add(karta);
                }
            }

            return Task.FromResult(nevlastnim.ToArray());
        }

    }


}
