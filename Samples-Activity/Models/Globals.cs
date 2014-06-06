﻿using System;
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
    }

}