#pragma checksum "c:\priestor\MTG\Blazor\Pages\Add.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3e90571df8e58205befee4b5c5428e66e5443b1f"
// <auto-generated/>
#pragma warning disable 1591
namespace Blazor.Pages
{
    #line hidden
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "c:\priestor\MTG\Blazor\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "c:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "c:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "c:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "c:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "c:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "c:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "c:\priestor\MTG\Blazor\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "c:\priestor\MTG\Blazor\_Imports.razor"
using Blazor;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "c:\priestor\MTG\Blazor\_Imports.razor"
using Blazor.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "c:\priestor\MTG\Blazor\Pages\Add.razor"
using Blazor.Data;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "c:\priestor\MTG\Blazor\Pages\Add.razor"
using System.IO;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "c:\priestor\MTG\Blazor\Pages\Add.razor"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Add")]
    public partial class Add : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Pridat Karty</h1>\r\n\r\n<br>\r\n");
            __builder.OpenElement(1, "div");
            __builder.AddAttribute(2, "class", "container");
            __builder.OpenElement(3, "div");
            __builder.AddAttribute(4, "class", "row");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "col-xs-6 col-sm-3 col-md-3");
            __builder.OpenElement(7, "table");
            __builder.OpenElement(8, "textarea");
            __builder.AddAttribute(9, "class", "form-control");
            __builder.AddAttribute(10, "maxlength", "255");
            __builder.AddAttribute(11, "style", "width:600px;");
            __builder.AddAttribute(12, "id", "Comments");
            __builder.AddAttribute(13, "required");
            __builder.AddAttribute(14, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 18 "c:\priestor\MTG\Blazor\Pages\Add.razor"
         zoznam

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(15, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => zoznam = __value, zoznam));
            __builder.SetUpdatesAttributeName("value");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\r\n\r\n<br>\r\n");
            __builder.OpenElement(17, "center");
            __builder.AddMarkupContent(18, "\r\n    Format : Kaya\'s Onslaught;KHM;1;;18:Spectral Steel;KHM;1;;30 - <br>\"Nazov Kary\";\"Kod set\";\"pocet\";\"F(ak je karta foil inak prazdne)\";\"cislokarty\":\"Nazov Kary\";\"Kod set\";\"pocet\";\"F(ak je karta foil inak prazdne)\";\"cislokarty\"\r\n    <br>\r\n");
            __builder.OpenElement(19, "select");
            __builder.AddAttribute(20, "name", "Zdroj");
            __builder.AddAttribute(21, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 29 "c:\priestor\MTG\Blazor\Pages\Add.razor"
                             subor

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(22, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => subor = __value, subor));
            __builder.SetUpdatesAttributeName("value");
#nullable restore
#line 30 "c:\priestor\MTG\Blazor\Pages\Add.razor"
     foreach(var file in files)
    {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(23, "option");
            __builder.AddAttribute(24, "value", 
#nullable restore
#line 32 "c:\priestor\MTG\Blazor\Pages\Add.razor"
                          file

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(25, 
#nullable restore
#line 32 "c:\priestor\MTG\Blazor\Pages\Add.razor"
                                  file.Substring(file.IndexOf('\\')+1)

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
#nullable restore
#line 33 "c:\priestor\MTG\Blazor\Pages\Add.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(26, "\r\n");
            __builder.OpenElement(27, "button");
            __builder.AddAttribute(28, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 35 "c:\priestor\MTG\Blazor\Pages\Add.razor"
                  AddCards

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(29, "style", "display: inline-block;");
            __builder.AddContent(30, "Add");
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n<br>");
            __builder.CloseElement();
            __builder.AddMarkupContent(32, "\r\n<br>");
#nullable restore
#line 40 "c:\priestor\MTG\Blazor\Pages\Add.razor"
 if (koniec != null)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(33, "<p><em>Skoncilo</em></p>");
#nullable restore
#line 43 "c:\priestor\MTG\Blazor\Pages\Add.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 46 "c:\priestor\MTG\Blazor\Pages\Add.razor"
       	
	private string koniec;
	private string zoznam;
	private List<string> files=new List<string>();
	private string subor;
	private string[] fileEntries;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await jsRuntime.InvokeAsync<bool>("ResizeTextArea", "Comments");
    }

    
    protected override async Task OnInitializedAsync()
    {
		fileEntries = Directory.GetFiles(@"json\");
		files = fileEntries.ToList();
        subor=fileEntries[0];
    }
	
	private async Task AddCards()
    {
        koniec = await PridatService.AddCards(subor, zoznam);
        zoznam=null;
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime jsRuntime { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private AddService PridatService { get; set; }
    }
}
#pragma warning restore 1591