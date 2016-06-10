namespace EyeSoft.ActiveCampaign
{
	using System;
	using System.Text;

	using EyeSoft.ActiveCampaign.Commanding;
	using EyeSoft.ActiveCampaign.Extensions;
	using EyeSoft.ActiveCampaign.Helpers;
	using EyeSoft.ActiveCampaign.Query;

	using Newtonsoft.Json;

	public abstract class ActiveCampaignRestClient : IDisposable
	{
		private readonly JsonSerializerSettings settings = new JsonSerializerSettings
		{
			ContractResolver = new UnderscoreMappingResolver(),
			NullValueHandling = NullValueHandling.Ignore
		};

		private readonly ActiveCampaignConnection connection;

		private readonly string baseAddress;

		protected ActiveCampaignRestClient(ActiveCampaignConnection connection)
		{
			this.connection = connection;
			baseAddress = $"{connection.Account}/admin/api.php?";
		}

		internal TResult ExecuteGetRequest<TResult>(string action, ActiveCampaignRequest requestData)
		{
			var queryParameters = GetQueryString(action, requestData);

			var urlAction = $"{baseAddress}&{queryParameters}";

			var client = connection.WebClient;

			var result = client.DownloadString(urlAction);

			return GetResult<TResult>(action, result);
		}

		internal TResult ExecutePostRequest<TResult>(string action, ActiveCampaignRequest request) where TResult : ActiveCampaignResponse
		{
			var urlAction = baseAddress;

			SetRequestQueryValues(action, request);

			var requestData = request.GetNamedValueCollection();

			var client = connection.WebClient;

			var byteResult = client.UploadValues(urlAction, requestData);

			var result = Encoding.Default.GetString(byteResult);

			return GetResult<TResult>(action, result);
		}

		private TResult GetResult<TResult>(string action, string source)
		{
			var result = JsonConvert.DeserializeObject<ActiveCampaignResponse>(source, settings);

			if (result.ResultCode == 0)
			{
				return default(TResult);
			}

			if (result.ResultCode != 1)
			{
				throw new InvalidOperationException($"Error on request '{action}'. Message: {result.ResultMessage}");
			}

			return JsonConvert.DeserializeObject<TResult>(source, settings);
		}

		private string GetQueryString(string action, ActiveCampaignRequest request)
		{
			SetRequestQueryValues(action, request);

			return request.ToQueryString().ToLower();
		}

		private void SetRequestQueryValues(string action, ActiveCampaignRequest request)
		{
			request.ApiKey = connection.ApiKey;
			request.ApiAction = action;
			request.ApiOutput = "json";
		}

		public void Dispose()
		{
			connection.Dispose();
		}
	}
}