﻿using Microsoft.Extensions.DependencyInjection;

namespace Moongate.IO
{
	public static class ServiceConfiguration
	{
		public static void AddFarspeakerService(this IServiceCollection services)
		{
			services.AddSingleton<Input>();
			services.AddSingleton<Output>();
			services.AddSingleton<Farspeaker>();
		}
	}
}
