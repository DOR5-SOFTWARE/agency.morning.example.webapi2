using System.ComponentModel.DataAnnotations;

namespace Demo.RestApi.Domain.Models.API
{
	public class DemoAuthenticationModel
	{
		[Required]
		public string Id { get; set; }

		[Required]
		public string MobilePhone { get; set; }
	}
}
