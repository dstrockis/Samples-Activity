using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samples_Activity.Models
{
    public static class Globals
    {
        public static string GitHubAppName 
        { 
            get
            {
                return "AADSamplesActivity";
            }
        }

        public static string GitHubOrg
        {
            get
            {
                return "AzureADSamples";
            }
        }

        public static string GitHubUriBase
        {
            get
            {
                return "https://api.github.com";
            }
        }

        public static string GitHubClientID
        {
            get
            {
                return "64eec3ad37d4e83fc4ae";
            }
        }

        public static string GitHubClientSecret
        {
            get
            {
                return "6a844ce7f938b7702abaeee2ffc346c3ed02c871";
            }
        }

        public static string GitHubAuthServerUriBase
        {
            get
            {
                return "https://github.com";
            }
        }
        public static string GitHubAuthPath
        {
            get
            {
                return "/login/oauth/authorize";
            }
        }
        public static string GitHubTokenPath
        {
            get
            {
                return "/login/oauth/access_token";
            }
        }

        public static int numWeeksToInclude
        {
            get
            {
                return 10;
            }
        }

        public static string mailUser
        {
            get
            {
                return "d.l.strockis@gmail.com";
            }
        }

        public static string mailUserPwd
        {
            get
            {
                return "@Mammoth2";
            }
        }


    }

}