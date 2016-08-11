using System;
using Demo.RestApi.Common.Configuration;
using Demo.RestApi.Common.ExtensionMethods;
using Demo.RestApi.Domain.DataProviders;
using Demo.RestApi.Domain.Models.Session;
using JWT;

namespace Demo.RestApi.Domain.Services
{
	public interface IJwtService
	{
		string HandleIncomingToken(string token);
		string CreateToken(JwtData tokenData);
	}

	public class JwtService : IJwtService
	{
		private readonly IRequestDataProvider _requestDataProvider;
		private readonly IConfigurationHelper _configurationHelper;

		public JwtService(IRequestDataProvider requestDataProvider, IConfigurationHelper configurationHelper)
		{
			_requestDataProvider = requestDataProvider;
			_configurationHelper = configurationHelper;
		}

		public string HandleIncomingToken(string token)
		{
			try
			{
				var tokenBody = JsonWebToken.DecodeToObject<JwtData>(token, _configurationHelper.GetString(CommonNames.JWT_KEY));

				// Check if the token is about to expire. If so, create a new token
				var expiryDate = tokenBody.exp.UnixSecondsToDateTime();

				if (expiryDate.AddMinutes(_configurationHelper.GetDouble(CommonNames.JWT_MINUTES_BEFORE_RENEWAL)) > DateTime.Now.ToUniversalTime())
					token = CreateToken(tokenBody);

				_requestDataProvider.CacheItem(CommonNames.JWT_CONTENT, tokenBody);

				return token;
			}
			catch (SignatureVerificationException e)
			{
				e.Data.Add(e.Data.Count + 1, "Failure on token validation");
				throw;
			}
		}

		public string CreateToken(JwtData tokenData)
		{
			// Set the expiration time of the token
			tokenData.exp = DateTime.Now.AddMinutes(_configurationHelper.GetDouble(CommonNames.JWT_LIFE_TIME_IN_MINUTES)).ToUnixDateTime();

			var token = JsonWebToken.Encode(tokenData, _configurationHelper.GetString(CommonNames.JWT_KEY), JwtHashAlgorithm.HS256);

			return token;
		}
	}
}
