using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Carbon.Extensions;
using Newtonsoft.Json;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community 
 * All rights reserved.
 *
 */

namespace Carbon.BuildInfo
{
    internal class Program
    {
        static void Main ( string [] args )
        {
			var carbon = CommandLineEx.GetArgumentResult("-carbon");
			var output = CommandLineEx.GetArgumentResult("-o");

			var assembly = Assembly.LoadFrom(carbon);
			var informationalVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
			var version = assembly.GetName().Version.ToString();
			var module = assembly.GetType("Carbon.Base.BaseModule");
			var modules = new List<Info.Module>();

			foreach(var type in assembly.GetTypes().Where(x => x.IsSubclassOf(module) && x.Namespace == "Carbon.Modules"))
			{
				var act = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);
				modules.Add(new Info.Module
				{
					Name = (string)type.GetProperty("Name").GetValue(act),
					EnabledByDefault = (bool)type.GetProperty("EnabledByDefault").GetValue(act)
				});
			}

			var result = JsonConvert.SerializeObject(new Info
			{
				InformationalVersion = informationalVersion,
				Version = version,
				Modules = modules
			}, Formatting.Indented);
			File.WriteAllText(output, result);
        }
    }
}
