using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApiContrib.Formatting.Jsonp;

namespace Demo.RestApi.Web.App_Start
{
	public static class WebApiConfig
	{
		public static void ConfigureWebApiRouting(HttpConfiguration config)
		{
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "{controller}/{action}"
				);
		}

		public static void ConfigureDataFormatters(HttpConfiguration config)
		{
			config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

			GlobalConfiguration.Configuration.AddJsonpFormatter();

			var jsonSetting = new JsonSerializerSettings();

			jsonSetting.Converters.Add(new StringEnumConverter());
			jsonSetting.Formatting = Formatting.Indented;
			jsonSetting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

			GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings = jsonSetting;
		}
	}
}
