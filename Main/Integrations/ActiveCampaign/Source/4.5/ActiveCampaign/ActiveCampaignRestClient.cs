namespace EyeSoft.ActiveCampaign
{
	using System;
	using System.Text;

	using EyeSoft.ActiveCampaign.Commanding;
	using EyeSoft.ActiveCampaign.Extensions;
	using EyeSoft.ActiveCampaign.Helpers;
	using EyeSoft.ActiveCampaign.Query;

	public abstract class ActiveCampaignRestClient : IDisposable
	{
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

			var json = client.DownloadString(urlAction);

			var result = GetResult<TResult>(action, json);

			return result;
		}

		internal TResult ExecutePostRequest<TResult>(string action, ActiveCampaignRequest request) where TResult : ActiveCampaignResponse
		{
			var urlAction = baseAddress;

			SetRequestQueryValues(action, request);

			var requestData = request.GetNamedValueCollection();

			var client = connection.WebClient;

			var byteResult = client.UploadValues(urlAction, requestData);

			var json = Encoding.Default.GetString(byteResult);

			var result = GetResult<TResult>(action, json);

			return result;
		}

		private static TResult GetResult<TResult>(string action, string json)
		{
			var response = JsonConvertWrapper.DeserializeObject<ActiveCampaignResponse>(json);

			if (response.ResultCode == 0)
			{
				return default(TResult);
			}

			if (response.ResultCode != 1)
			{
				throw new InvalidOperationException($"Error on request '{action}'. Message: {response.ResultMessage}");
			}

			var result = JsonConvertWrapper.DeserializeObject<TResult>(json);

			return result;
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