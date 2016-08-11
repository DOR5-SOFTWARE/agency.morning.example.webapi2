using System;

namespace Demo.RestApi.Common.ExtensionMethods
{
	public static class IntExtensionMethods
	{
		public static DateTime UnixSecondsToDateTime(this int number)
		{
			var startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return startDate.AddSeconds(number);
		}
	}
}
