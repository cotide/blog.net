/*

using System.Web.Mvc;
using System.Web.WebPages;
using RazorGenerator.Mvc;

namespace Cotide.Portal.CastleWindsor
{
    public class PreApplicationStartCode
    {
        private static bool _isStarting;
        public static void PreStart()
        {
            if (!_isStarting)
            {
                _isStarting = true;
                var engine = new PrecompiledMvcEngine(
                    typeof(PreApplicationStartCode).Assembly);
                ViewEngines.Engines.Add(engine);
                VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
            }
        }
    }
}*/