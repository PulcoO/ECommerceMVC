#pragma checksum "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\Shared\_ProductDetails__comment.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2519a0f0729e1e4e329b6c23fff010e916c5ea8b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__ProductDetails__comment), @"mvc.1.0.view", @"/Views/Shared/_ProductDetails__comment.cshtml")]
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
#nullable restore
#line 1 "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\_ViewImports.cshtml"
using ECommerceMVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\_ViewImports.cshtml"
using ECommerceMVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2519a0f0729e1e4e329b6c23fff010e916c5ea8b", @"/Views/Shared/_ProductDetails__comment.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be1ab145a02dc2becbcd72c425836480221b8e5e", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__ProductDetails__comment : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ECommerceMVC.Models.Comments.Comment>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\Shared\_ProductDetails__comment.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <article>\r\n        <div class=\"comment-card__header\">\r\n            <div>");
#nullable restore
#line 7 "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\Shared\_ProductDetails__comment.cshtml"
            Write(Model.Client.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n            <div>");
#nullable restore
#line 8 "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\Shared\_ProductDetails__comment.cshtml"
            Write(Model.Product.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div>\r\n        <div class=\"comment-card__body\">\r\n            <div>");
#nullable restore
#line 11 "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\Shared\_ProductDetails__comment.cshtml"
            Write(Model.Content);

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n        </div>\r\n        <div class=\"comment-card__\">\r\n\r\n        </div>\r\n    </article>\r\n");
#nullable restore
#line 17 "D:\Repository\Ecommerce(Poe)\ECommerceMVC\ECommerceMVC\Views\Shared\_ProductDetails__comment.cshtml"
}
else
{

}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ECommerceMVC.Models.Comments.Comment> Html { get; private set; }
    }
}
#pragma warning restore 1591
