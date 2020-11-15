using System;
using Moongate.Models.Identity;

namespace Moongate.Identity.Provider
{
	public interface IIdentityProvider
	{
		Id Id { get; set; }	
	}
}
