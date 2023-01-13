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
	public class ContactsController : SurfaceController
	{
		public ContactsController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Contacts([Bind(Prefix = "contactFormModel")]ContactFormModel model)
		{
			if (!ModelState.IsValid)
				return RedirectToCurrentUmbracoUrl();

			using var mail = new MailSender("", "", 587, "", "");
			mail.SendAsync(model.Email, $"Vi har mottagit din kontaktförfrågan", "Hej vi har mottagit din kontaktförfrågan. Vi återkommer till dig så snart vi kan.").ConfigureAwait(false);
            mail.SendAsync("", $"{model.Name} har skickat en kontaktförfrågan", model.Comment).ConfigureAwait(false);

            if (!string.IsNullOrWhiteSpace(model.RedirectUrl))
				return Redirect(Url.IsLocalUrl(model.RedirectUrl) 
					? model.RedirectUrl 
					: CurrentPage!.AncestorOrSelf(1)!.Url(PublishedUrlProvider));

			return RedirectToCurrentUmbracoUrl();

		}
	}
}
