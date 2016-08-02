using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenIddict;
using CQRSBoilerPlate.Models;
using CQRSBoilerPlate.Entities.DBModels;

namespace CQRSBoilerPlate
{
	public class CustomOpenIddictManager : OpenIddictUserManager<ApplicationUser>
	{
		public CustomOpenIddictManager(
			IServiceProvider services,
			IOpenIddictUserStore<ApplicationUser> store,
			IOptions<IdentityOptions> options,
			ILogger<OpenIddictUserManager<ApplicationUser>> logger,
			IPasswordHasher<ApplicationUser> hasher,
			IEnumerable<IUserValidator<ApplicationUser>> userValidators,
			IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
			ILookupNormalizer keyNormalizer,
			IdentityErrorDescriber errors
			)
			: base(services, store, options, logger, hasher, userValidators, passwordValidators, keyNormalizer, errors)
		{
		}

        public override async Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, IEnumerable<string> scopes)
        {
            var claimsIdentity = await base.CreateIdentityAsync(user, scopes);

            claimsIdentity.AddClaim("first_name", user.FirstName,
                OpenIdConnectConstants.Destinations.AccessToken, 
                OpenIdConnectConstants.Destinations.IdentityToken);

            claimsIdentity.AddClaim("last_name", user.LastName,
               OpenIdConnectConstants.Destinations.AccessToken,
               OpenIdConnectConstants.Destinations.IdentityToken);

            return claimsIdentity;
        }
	}
}