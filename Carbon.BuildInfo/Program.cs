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
			var carbonPath = CommandLineEx.GetArgumentResult("-carbon");
			var outputPath = CommandLineEx.GetArgumentResult("-o");

			var carbon = Assembly.LoadFrom($"{carbonPath}.dll");
			var modules = Assembly.LoadFrom($"{carbonPath}.Modules.dll");
			var informationalVersion = carbon.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
			var version = carbon.GetName().Version.ToString();
			var moduleList = new List<Info.Module>();

			foreach (var type in modules.GetTypes().Where(x => x.Namespace == "Carbon.Modules" && x.BaseType != null && x.BaseType.Name.Contains("CarbonModule")))
			{
				var act = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);

				moduleList.Add(new Info.Module
				{
					Name = (string)type.GetProperty("Name").GetValue(act),
					EnabledByDefault = (bool)type.GetProperty("EnabledByDefault").GetValue(act),
					Disabled = (bool)type.GetProperty("Disabled").GetValue(act)

				});
			}

			var result = JsonConvert.SerializeObject(new Info
			{
				InformationalVersion = informationalVersion,
				Version = version,
				Modules = moduleList
			}, Formatting.Indented);
			File.WriteAllText(outputPath, result);
        }

	}
}
