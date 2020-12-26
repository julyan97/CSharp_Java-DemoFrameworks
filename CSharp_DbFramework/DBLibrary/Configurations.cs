using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBLibrary
{
    class Configurations
    {
        private dynamic settings;

        public string this[string field] => settings[field];

        public Configurations()
        {
            settings = Get();
        }

        private dynamic Get()
        {
            using StreamReader r = new StreamReader("config.json");
            var json = r.ReadToEnd();
            dynamic array = JsonConvert.DeserializeObject(json);
            return array;
        }
    }
}
