using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Framework.Model.Util;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            String cultureName = Request.RequestContext.RouteData.Values["language"] != null 
                ? Request.RequestContext.RouteData.Values["language"].ToString() : String.Empty;

            if (String.IsNullOrEmpty(cultureName))
            {
                //set default language
                var defaultLanguage = LanguageProvider.SystemLanguages.Languages.Where(x => x.IsDefault).FirstOrDefault();

                if(defaultLanguage != null)
                {
                    cultureName = defaultLanguage.Culture;
                }
                else
                {
                    //last resort - set to english (US)
                    cultureName = "en-US";
                }
                
            }
            else
            {
                var language = LanguageProvider.SystemLanguages.Languages.Where(x => x.Code == cultureName).FirstOrDefault();
                cultureName = language.Culture;
            }
           

            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

            return base.BeginExecuteCore(callback, state);
           
        }
    }
}