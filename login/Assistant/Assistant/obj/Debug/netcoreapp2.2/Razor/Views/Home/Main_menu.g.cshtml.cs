#pragma checksum "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "588420380145ae9bf08cc9d70e4969d016f589b2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Main_menu), @"mvc.1.0.view", @"/Views/Home/Main_menu.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Main_menu.cshtml", typeof(AspNetCore.Views_Home_Main_menu))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\_ViewImports.cshtml"
using Assistant;

#line default
#line hidden
#line 2 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\_ViewImports.cshtml"
using Assistant.Models;

#line default
#line hidden
#line 1 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"588420380145ae9bf08cc9d70e4969d016f589b2", @"/Views/Home/Main_menu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc8d0bc7d1d0e3ca7aafbc931c934509822bc9d6", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Main_menu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 4 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
  
    ViewData["Title"] = "Main_menu";

#line default
#line hidden
            BeginContext(181, 185, true);
            WriteLiteral("\r\n\r\n<nav class=\"navbar\">\r\n\r\n    <h2> Asystent Zakupow </h2>\r\n\r\n</nav>\r\n<div class=\"col-sm-3 text-center\">\r\n    <div class=\"row mt-3\"></div>\r\n    <input type=\"button\" class=\"btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 366, "\"", 424, 3);
            WriteAttributeValue("", 376, "location.href=\'", 376, 15, true);
#line 16 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
WriteAttributeValue("", 391, Url.Action("Load_list", "Home"), 391, 32, false);

#line default
#line hidden
            WriteAttributeValue("", 423, "\'", 423, 1, true);
            EndWriteAttribute();
            BeginContext(425, 151, true);
            WriteLiteral(" value=\"Edytuj liste zakupów\" style=\"margin-bottom:3px; width:200px\" />\r\n    <div class=\"row mt-3\"></div>\r\n    <input type=\"button\" class=\"btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 576, "\"", 634, 3);
            WriteAttributeValue("", 586, "location.href=\'", 586, 15, true);
#line 18 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
WriteAttributeValue("", 601, Url.Action("List_name", "Home"), 601, 32, false);

#line default
#line hidden
            WriteAttributeValue("", 633, "\'", 633, 1, true);
            EndWriteAttribute();
            BeginContext(635, 148, true);
            WriteLiteral(" value=\"Stworz nowa listę\" style=\" margin-bottom:3px;width:200px\" />\r\n    <div class=\"row mt-3\"></div>\r\n    <input type=\"button\" class=\"btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 783, "\"", 847, 3);
            WriteAttributeValue("", 793, "location.href=\'", 793, 15, true);
#line 20 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
WriteAttributeValue("", 808, Url.Action("Select_list", "Generate"), 808, 38, false);

#line default
#line hidden
            WriteAttributeValue("", 846, "\'", 846, 1, true);
            EndWriteAttribute();
            BeginContext(848, 89, true);
            WriteLiteral(" value=\"Wygeneruj listę zakupów\" style=\" margin-bottom:3px; width:200px\" />\r\n</div>\r\n\r\n\r\n");
            EndContext();
            BeginContext(938, 39, false);
#line 24 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
Write(await Html.PartialAsync("FrequentList"));

#line default
#line hidden
            EndContext();
            BeginContext(977, 4, true);
            WriteLiteral("\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<IdentityUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<IdentityUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
