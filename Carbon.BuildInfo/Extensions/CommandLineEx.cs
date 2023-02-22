using System;

/*
 *
 * Copyright (c) 2022-2023 Carbon Community 
 * All rights reserved.
 *
 */

namespace Carbon.Extensions
{
	public static class CommandLineEx
	{
		public static string GetArgumentResult(this string[] args, string argument, string Default = null)
		{
			var result = string.Empty;


			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == argument)
				{
					return string.IsNullOrEmpty(args[i + 1]) ? result = Default : result = args[i + 1];
				}
			}

			if (string.IsNullOrEmpty(result))
			{
				result = Default;
			}

			return result;
		}

		public static string GetArgumentResult(this string argument, string Default = null)
		{
			var args = Environment.GetCommandLineArgs();

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == argument)
				{
					return string.IsNullOrEmpty(args[i + 1]) ? Default : args[i + 1];
				}
			}

			return Default;
		}

		public static bool GetArgumentExists(this string[] args, string argument)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == argument)
				{
					return true;
				}
			}

			return false;
		}

		public static bool GetArgumentExists(this string argument)
		{
			var args = Environment.GetCommandLineArgs();

			for (int i = 0; i < args.Length; i++)
			{
				if (args[i] == argument)
				{
					return true;
				}
			}

			return false;
		}
	}
}
