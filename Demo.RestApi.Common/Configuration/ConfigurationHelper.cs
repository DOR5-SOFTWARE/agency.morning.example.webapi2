using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace Demo.RestApi.Common.Configuration
{
	public interface IConfigurationHelper
	{
		string GetConnectionString(string name);
		string GetString(string key);
		int GetInt(string key);
		long GetLong(string key);
		float GetFloat(string key);
		double GetDouble(string key);
		decimal GetDecimal(string key);
		bool GetBool(string key);
		Uri GetUri(string key);
		T[] GetArray<T>(string key);
		List<T> GetList<T>(string key);
		IEnumerable<T> GetEnumerable<T>(string key);
	}

	public class ConfigurationHelper : IConfigurationHelper
	{
		public string GetConnectionString(string name)
		{
			if (ConfigurationManager.ConnectionStrings[name] == null || string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[name].ConnectionString))
				throw new ConfigurationErrorsException(
					String.Format("Connection string '{0}' is missing from ConnectionStrings in the configuration. Add it to your config file", name));

			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}

		public string GetString(string key)
		{
			var val = ConfigurationManager.AppSettings[key];

			if (string.IsNullOrEmpty(val))
				throw new ConfigurationErrorsException(
					String.Format("Key '{0}' is missing from appSettings in the configuration. Add it to your config file", key));

			return val;
		}

		public int GetInt(string key)
		{
			var val = GetString(key);

			int result;
			if (!int.TryParse(val, out result))
				throw new ConfigurationErrorsException(String.Format("Value of key '{0}' was expected to be int but wasn't: {1}", key, val));

			return result;
		}

		public long GetLong(string key)
		{
			var val = GetString(key);

			long result;
			if (!long.TryParse(val, out result))
				throw new ConfigurationErrorsException(String.Format("Value of key '{0}' was expected to be long but wasn't: {1}", key, val));

			return result;
		}

		public float GetFloat(string key)
		{
			var val = GetString(key);

			float result;
			if (!float.TryParse(val, out result))
				throw new ConfigurationErrorsException(String.Format("Value of key '{0}' was expected to be float but wasn't: {1}", key, val));

			return result;
		}

		public double GetDouble(string key)
		{
			var val = GetString(key);

			double result;
			if (!double.TryParse(val, out result))
				throw new ConfigurationErrorsException(String.Format("Value of key '{0}' was expected to be double but wasn't: {1}", key, val));

			return result;
		}

		public decimal GetDecimal(string key)
		{
			var val = GetString(key);

			decimal result;
			if (!decimal.TryParse(val, out result))
				throw new ConfigurationErrorsException(String.Format("Value of key '{0}' was expected to be decimal but wasn't: {1}", key, val));

			return result;
		}

		public bool GetBool(string key)
		{
			var val = GetString(key);

			bool result;
			if (!bool.TryParse(val, out result))
				throw new ConfigurationErrorsException(String.Format("Value of key '{0}' was expected to be bool but wasn't: {1}", key, val));

			return result;
		}

		public Uri GetUri(string key)
		{
			var val = GetString(key);

			Uri result;

			if (!Uri.TryCreate(val, UriKind.Absolute, out result))
				throw new ConfigurationErrorsException(
					String.Format("Value of key '{0}' is not a valid URI: '{1}'", key, val));

			return result;
		}

		public T[] GetArray<T>(string key)
		{
			return GetEnumerable<T>(key).ToArray();
		}

		public List<T> GetList<T>(string key)
		{
			return GetEnumerable<T>(key).ToList();
		}

		public IEnumerable<T> GetEnumerable<T>(string key)
		{
			var val = GetString(key);

			try
			{
				return val.Split(',').Select(x => (T)Convert.ChangeType(x.Trim(), typeof (T), CultureInfo.InvariantCulture));
			}
			catch (Exception)
			{
				throw new ConfigurationErrorsException(
					String.Format("Values of array in key '{0}' were expected to be of type {1} but weren't", key, typeof (T).FullName));
			}
		}
	}
}
