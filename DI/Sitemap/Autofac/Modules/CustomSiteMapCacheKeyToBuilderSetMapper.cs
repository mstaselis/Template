using MvcSiteMapProvider.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.DI.Sitemap.Autofac.Modules
{
    public class CustomSiteMapCacheKeyToBuilderSetMapper
     : ISiteMapCacheKeyToBuilderSetMapper
    {
        public virtual string GetBuilderSetName(string cacheKey)
        {
            switch (cacheKey)
            {
                case "sitemap://AdminSiteMapProvider":
                    return "AdminMenu";
                case "sitemap://AppSiteMapProvider":
                    return "default";
                default:
                    return "default";
            }
        }
    }
}