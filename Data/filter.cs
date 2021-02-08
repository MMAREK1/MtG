namespace Blazor.Data
{
	public class Filter
	{
		public string Name { get; set; }
		public string CardType { get; set; }
		public string Text {get;set;}
		public bool Legendary {get;set;}
		public bool Snow { get; set; }
		public Rarity Rarita {get;set;}
		public Type Typy {get;set;}
		public Color Colors {get;set;}
		public bool Colorless {get;set;}
		public bool Monocolor {get;set;}
		public bool Bicolor {get;set;}
		public bool Tricolor {get;set;}
		public bool Fourcolor {get;set;}
		public bool Fivecolor {get;set;}
	}
}