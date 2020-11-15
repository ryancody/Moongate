using Moongate.Models.Identity;
using System;

namespace Moongate.Identity.Provider
{
	public class IdentityProvider : IIdentityProvider
	{
		public Id Id { get; set; }
	}
}
