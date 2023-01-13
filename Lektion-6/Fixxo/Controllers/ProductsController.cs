using Fixxo.Contexts;
using Fixxo.Models;
using Fixxo.Services;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Fixxo.Controllers
{
    public class ProductsController : SurfaceController
    {
        private readonly DataContext _context;

        public ProductsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, DataContext context) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _context = context;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind(Prefix = "productModel")] ProductModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToCurrentUmbracoUrl();

            // save to database
            _context.Add(model);
            await _context.SaveChangesAsync();


            if (!string.IsNullOrWhiteSpace(model.RedirectUrl))
                return Redirect(Url.IsLocalUrl(model.RedirectUrl)
                    ? model.RedirectUrl
                    : CurrentPage!.AncestorOrSelf(1)!.Url(PublishedUrlProvider));

            return RedirectToCurrentUmbracoUrl();
        }
    }
}
