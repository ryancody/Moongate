using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjection
{
	public class Services
	{
		private Dictionary<Type, object> services = new Dictionary<Type, object>();

		public T GetService<T> ()
		{
			if (services.TryGetValue(typeof(T), out var service))
			{
				if (service is T s)
				{
					return s;
				}
				else
				{
					throw new Exception($"Error retrieving service as proper type: {typeof(T).Name}");
				}
			}

			throw new Exception($"Couldn't find service of type {typeof(T).Name}");
		}

		public void AddService<T>(T service)
		{
			services.Add(typeof(T), service);
		}
	}
}
