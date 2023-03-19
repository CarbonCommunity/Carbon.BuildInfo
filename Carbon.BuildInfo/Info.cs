using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbon.BuildInfo
{
	public class Info
	{
		public string InformationalVersion { get; set; }
		public string Version { get; set; }
		public List<Module> Modules { get; set; } = new List<Module>();

		public class Module
		{
			public string Name { get; set; }
			public bool EnabledByDefault { get; set; }
			public bool Disabled { get; set; }
		}
	}
}
