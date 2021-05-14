// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace Blazor.Pages
{
    #line hidden
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\priestor\MTG\Blazor\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\priestor\MTG\Blazor\_Imports.razor"
using Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\priestor\MTG\Blazor\_Imports.razor"
using Blazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\priestor\MTG\Blazor\Pages\Karty.razor"
using Blazor.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\priestor\MTG\Blazor\Pages\Karty.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\priestor\MTG\Blazor\Pages\Karty.razor"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Karty")]
    public partial class Karty : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 164 "C:\priestor\MTG\Blazor\Pages\Karty.razor"
       
    private Blazor.Data.Karty[] cards;
	private int CurrentPage { get; set; } = 1;
    private int RecordsPerPage = 150;
	private List<Blazor.Data.Choice> Choices = new List<Blazor.Data.Choice>{ new Blazor.Data.Choice(1,"Je"),new Blazor.Data.Choice(2,"Moze"),new Blazor.Data.Choice(3,"Nie Je")};
	private List<Blazor.Data.SetList> Sets = new List<Blazor.Data.SetList>{};
	private string[] fileEntries;
	private List<string> files=new List<string>();
	private string subor;
	private string set;
	private string cardName;
	private string cardType;
	private string cardText;
	private int count =0;
	private bool Common;
	private bool Uncommon;
	private bool Rare;
	private bool Mythic;
	private bool Legendary;
	private bool Snow;
	private bool Artifact;
	private bool Land;
	private bool Instant;
	private bool Creature;
    private bool Planeswalker;
	private bool Sorcery;
	private bool Enchantment;
	private int White = 2;
	private int Black = 2;
	private int Blue = 2;
	private int Red = 2;
	private int Green = 2;
	private bool Colorless;
	private bool Monocolor;
	private bool Bicolor;
	private bool Tricolor;
	private bool Fourcolor;
	private bool Fivecolor;
	private bool Foil;
	private bool Showcase;
	private bool Singleton;
	private string Mana;
	bool flipped=true;
    void FlipMe()
    {
        flipped = !flipped;
    }
    string flipCss => flipped ? "front" : "back";


    void MoveNext()
    {
        if (CurrentPage < NumberOfPages())
        {
            CurrentPage++;
        }
    }

    void MovePrevious()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
        }

    }

    string DisablePrevious {
        get {
            if (CurrentPage == 1) { return "disabled"; }
            return ""; 
        }
    }
    string DisableNext {
        get {
            if (CurrentPage >= NumberOfPages()) { return "disabled"; }
            return ""; 
        }
    }


    int NumberOfPages()
    {
        return (int)(Math.Ceiling((cards.Count()/(double)RecordsPerPage)));
    }


    List<Blazor.Data.Karty> GetImageToShow()
    {
        int skip = (CurrentPage - 1) * RecordsPerPage;

        return cards.Skip(skip).Take(RecordsPerPage).ToList();
    }

	private async Task Changed(ChangeEventArgs suborEvent)
    {
		set="0";
		Sets.Clear();
		subor=suborEvent.Value.ToString();
		Sets.AddRange(await CardService.ListOfSets(subor));
    }

    protected override async Task OnInitializedAsync()
    {
		fileEntries = Directory.GetFiles(@"json\");
		files = fileEntries.ToList();
		Common=false;
		Uncommon=false;
		Rare=false;
		Mythic=false;
		Legendary=false;
		Snow=false;
		Artifact=false;
		Land=false;
		Instant=false;
		Creature=false;
		Planeswalker=false;
		Sorcery=false;
		Enchantment=false;
		White = 2;
		Black = 2;
		Blue = 2;
		Red = 2;
		Green = 2;
		Colorless=false;
		Monocolor=false;
		Bicolor=false;
		Tricolor=false;
		Fourcolor=false;
		Fivecolor=false;
		Showcase=false;
		Singleton=false;
		Foil=false;
		set="0";
		cardName="";
		cardType="";
		cardText="";
		CurrentPage=1;
		subor=fileEntries[0];
		Mana="0";
        cards = await CardService.AllCards(subor);
		Sets.Clear();
		Sets.AddRange(await CardService.ListOfSets(subor));
		count=cards.Length;
    }
	
	string whiteId;

    string WhiteID
    {
        get => whiteId;
        set
        {
            whiteId = value;

        }
    }
	
	private async Task FindCards()
    {
		CurrentPage=1;
        cards = await CardService.FindCards(subor,Singleton, new Blazor.Data.Filter{
			Name=!string.IsNullOrWhiteSpace(cardName)?cardName:null,
			CardType=!string.IsNullOrWhiteSpace(cardType)?cardType:null,
			Text=!string.IsNullOrWhiteSpace(cardText)?cardText:null,
			Legendary=Legendary,
			Snow=Snow,
			Rarita= new Blazor.Data.Rarity{
			Common=Common,
			Uncommon=Uncommon,
			Rare=Rare,
			Mythic=Mythic
			},
			Typy=new Blazor.Data.Type{
				Artifact=Artifact,
				Land=Land,
				Instant=Instant,
				Creature=Creature,
				Planeswalker=Planeswalker,
				Sorcery=Sorcery,
				Enchantment=Enchantment
			},
			Colors= new Blazor.Data.Color{
			White= White,
			Black=Black,
			Blue=Blue,
			Red=Red, 
			Green=Green,
			},
			Colorless=Colorless,
			Monocolor=Monocolor,
			Bicolor=Bicolor,
			Tricolor=Tricolor,
			Fourcolor=Fourcolor,
			Fivecolor=Fivecolor,
			Foil=Foil,
			Showcase=Showcase,
			Edition=set,
			ManaCost=Mana
		},true);
       count=cards.Length; 
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private KartyService CardService { get; set; }
    }
}
#pragma warning restore 1591