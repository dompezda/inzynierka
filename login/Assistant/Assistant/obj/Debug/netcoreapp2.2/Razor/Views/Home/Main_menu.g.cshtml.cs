#pragma checksum "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0ba25ba5717004211e4548f74e9758a7c20d0214"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0ba25ba5717004211e4548f74e9758a7c20d0214", @"/Views/Home/Main_menu.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5c9736257f157233dcd41cbc09aa3be272f77708", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Main_menu : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Views/Shared/_Layout.cshtml"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 2 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
  
    ViewData["Title"] = "Main_menu";

#line default
#line hidden
            BeginContext(47, 185, true);
            WriteLiteral("\r\n\r\n<nav class=\"navbar\">\r\n\r\n    <h2> Asystent Zakupow </h2>\r\n\r\n</nav>\r\n<div class=\"col-sm-3 text-center\">\r\n    <div class=\"row mt-3\"></div>\r\n    <input type=\"button\" class=\"btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 232, "\"", 290, 3);
            WriteAttributeValue("", 242, "location.href=\'", 242, 15, true);
#line 14 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
WriteAttributeValue("", 257, Url.Action("Load_list", "Home"), 257, 32, false);

#line default
#line hidden
            WriteAttributeValue("", 289, "\'", 289, 1, true);
            EndWriteAttribute();
            BeginContext(291, 151, true);
            WriteLiteral(" value=\"Edytuj liste zakupów\" style=\"margin-bottom:3px; width:200px\" />\r\n    <div class=\"row mt-3\"></div>\r\n    <input type=\"button\" class=\"btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 442, "\"", 502, 3);
            WriteAttributeValue("", 452, "location.href=\'", 452, 15, true);
#line 16 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
WriteAttributeValue("", 467, Url.Action("Create_list", "Home"), 467, 34, false);

#line default
#line hidden
            WriteAttributeValue("", 501, "\'", 501, 1, true);
            EndWriteAttribute();
            BeginContext(503, 148, true);
            WriteLiteral(" value=\"Stworz nowa listę\" style=\" margin-bottom:3px;width:200px\" />\r\n    <div class=\"row mt-3\"></div>\r\n    <input type=\"button\" class=\"btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 651, "\"", 713, 3);
            WriteAttributeValue("", 661, "location.href=\'", 661, 15, true);
#line 18 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
WriteAttributeValue("", 676, Url.Action("Generate_list", "Home"), 676, 36, false);

#line default
#line hidden
            WriteAttributeValue("", 712, "\'", 712, 1, true);
            EndWriteAttribute();
            BeginContext(714, 155, true);
            WriteLiteral(" value=\"Wygeneruj listę zakupów\" style=\" margin-bottom:3px; width:200px\" />\r\n    <div class=\"row mt-3\"></div>\r\n    <input type=\"button\" class=\"btn-primary\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 869, "\"", 929, 3);
            WriteAttributeValue("", 879, "location.href=\'", 879, 15, true);
#line 20 "C:\Users\Dominik\Documents\GitHub\inzynierka\login\Assistant\Assistant\Views\Home\Main_menu.cshtml"
WriteAttributeValue("", 894, Url.Action("Register", "Account"), 894, 34, false);

#line default
#line hidden
            WriteAttributeValue("", 928, "\'", 928, 1, true);
            EndWriteAttribute();
            BeginContext(930, 75, true);
            WriteLiteral(" value=\"Zarejestruj\" style=\" margin-bottom:3px; width:200px\" />\r\n</div>\r\n\r\n");
            EndContext();
            BeginContext(1005, 73, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0ba25ba5717004211e4548f74e9758a7c20d02147030", async() => {
                BeginContext(1045, 29, true);
                WriteLiteral("~/Views/Shared/_Layout.cshtml");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1078, 6, true);
            WriteLiteral("\r\n\r\n\r\n");
            EndContext();
        }
        #pragma warning restore 1998
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
