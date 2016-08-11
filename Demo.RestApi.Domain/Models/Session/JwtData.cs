using Demo.RestApi.Common.Enums;

namespace Demo.RestApi.Domain.Models.Session
{
	public class JwtData
	{
		public string Id { get; set; }
		public string PhoneNumber { get; set; }

		public bool IsCaptchaRequired { get; set; }

		public ClientProcessStep ClientProcessStep { get; set; }

		//required by the package - should be UNIX date time representaion
		public int exp { get; set; }
	}
}
