using System;

namespace Demo.RestApi.Common.ExtensionMethods
{
	public static class DateTimeExtensionMethods
	{
		public static int ToUnixDateTime(this DateTime dateTime)
		{
			var startDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (int)Math.Round((dateTime.ToUniversalTime() - startDate).TotalSeconds);
		}
	}
}
