using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using DynamicHub;
using Microsoft.CSharp;
using Newtonsoft.Json;
using Raven.Database.Plugins;

namespace DynamicAssemblyGenerator
{
    public class RuleRemovedTrigger : AbstractDeleteTrigger
    {
        public override void OnDelete (string key, Raven.Abstractions.Data.TransactionInformation transactionInformation)
        {
            //notify Dynamic hub to ignore rule.
            Console.WriteLine("Key: " + key);
            
            base.OnDelete (key, transactionInformation);
        }
    }

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
                    var doc = document.ToString(Formatting.None);
                    var dr = new ServiceStack.Text.JsonSerializer<DynamicRule>().DeserializeFromString(doc);
                    
                    var ass = CodeDomProvider.CreateProvider("CSharp")
                                     .CompileAssemblyFromSource(
                                        new CompilerParameters
	                                    {
                                            GenerateInMemory = false,
                                            GenerateExecutable = false,
	                                        IncludeDebugInformation = false,
	                                        OutputAssembly = "TestAssembly",
                                        },
                                        new[]
                                        {
	                                        @"",
                                            @""
	                                    }).CompiledAssembly;


             sw.Write(document.ToString());                        
                }
            }
        }
    }
}
