﻿using ASPNetCore.Localization.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCore.Localization.Data.SeedData
{
    public static class en_US
    {
        public static List<Resource> GetList()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return JsonConvert.DeserializeObject<List<Resource>>(File.ReadAllText("Languages/en-US.json"), jsonSerializerSettings);
        }
    }
}
