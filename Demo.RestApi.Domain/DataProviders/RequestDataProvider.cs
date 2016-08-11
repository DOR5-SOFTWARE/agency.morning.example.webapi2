using System.Web;

namespace Demo.RestApi.Domain.DataProviders
{
	public interface IRequestDataProvider
	{
		string GetCookieValue(string key);
		string GetServerVariable(string key);
		string GetQueryStringValue(string key);

		T GetCachedItem<T>(string itemName);
		void CacheItem<T>(string itemName, T item);
		void SetResponseAuthorizationHeader(string value);
		void SetResponseHeader(string name, string value);
	}

	public class RequestDataProvider : IRequestDataProvider
	{
		public HttpRequest Current
		{
			get { return HttpContext.Current.Request; }
		}

		public HttpContext HttpContext
		{
			get { return HttpContext.Current; }
		}

		public string GetCookieValue(string key)
		{
			if (Current.Cookies[key] != null)
				return Current.Cookies[key].Value;

			return null;
		}

		public string GetServerVariable(string key)
		{
			if (Current.ServerVariables[key] != null)
				return Current.ServerVariables[key];

			return null;
		}

		public string GetQueryStringValue(string key)
		{
			if (Current.QueryString[key] != null)
				return Current.QueryString[key];

			return null;
		}

		public T GetCachedItem<T>(string itemName)
		{
			return (T)HttpContext.Current.Items[itemName];
		}

		public void CacheItem<T>(string itemName, T item)
		{
			HttpContext.Current.Items[itemName] = item;
		}

		public void SetResponseAuthorizationHeader(string value)
		{
			SetResponseHeader("Authorization", value);
		}

		public void SetResponseHeader(string name, string value)
		{
			HttpContext.Response.Headers.Set(name, value);
		}
	}
}
