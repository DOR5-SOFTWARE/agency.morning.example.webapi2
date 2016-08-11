using System.Collections.Generic;

namespace Demo.RestApi.Domain.Models.API.Responses
{
	public class ValidationResponse
	{
		public Dictionary<string, string[]> ValidationMessages { get; set; }
	}
}
