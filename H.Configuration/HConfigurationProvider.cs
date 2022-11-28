using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace HConfiguration
{
    public class HConfigurationProvider : FileConfigurationProvider
    {
        public HConfigurationProvider(HConfigurationSource source) : base(source)
        {

        }

        public override void Load(Stream stream)
        {
            var data=new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
           
            //stream.
            //var json = JObject.Parse;
            base.Data= data;
        }
    }
}
