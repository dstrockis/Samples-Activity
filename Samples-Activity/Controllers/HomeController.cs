using DotNetOpenAuth.OAuth2;
using Samples_Activity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Samples_Activity.Controllers
{
    public class HomeController : Controller
    {
        private WebServerClient _webServerClient;
        public ActionResult Index()
        {
            var model = new RepoList(Globals.GitHubOrg, Globals.GitHubAppName);
    
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private void InitializeWebServerClient()
        {
            var authorizationServerUri = new Uri(Globals.GitHubAuthServerUriBase);
            var authorizationServer = new AuthorizationServerDescription
            {
                AuthorizationEndpoint = new Uri(authorizationServerUri, Globals.GitHubAuthPath),
                TokenEndpoint = new Uri(authorizationServerUri, Globals.GitHubTokenPath)
            };
            _webServerClient = new WebServerClient(authorizationServer, Globals.GitHubClientID, Globals.GitHubClientSecret);
        }

    }
}