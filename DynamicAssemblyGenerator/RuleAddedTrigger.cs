using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Raven.Database.Plugins;

namespace DynamicAssemblyGenerator
{
    public class RuleAddedTrigger : AbstractPutTrigger
    {
        public override void AfterCommit (string key, Raven.Json.Linq.RavenJObject document, Raven.Json.Linq.RavenJObject metadata, Guid etag)
        {
            using (var sw = new StreamWriter (File.Open ("c:\\test", FileMode.OpenOrCreate)))
            {
                sw.Write (metadata["Raven-Clr-Type"] + Environment.NewLine);
                base.AfterCommit (key, document, metadata, etag);
                if (metadata["Raven-Clr-Type"].ToString() == "DynamicHub.DynamicRule, DynamicHub")
                {
                    sw.Write(document.ToString());                        
                }
            }
        }
    }
}
