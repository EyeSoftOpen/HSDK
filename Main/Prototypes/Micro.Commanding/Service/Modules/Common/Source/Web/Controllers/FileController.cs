namespace EyeSoft.Accounting.Prototype.Api.Web.Controllers
{
	using System.IO;
	using System.Web;
	using EyeSoft.IO;

	using System;
	using System.Linq;
	using System.Net.Http;
	using System.Threading.Tasks;
	using System.Web.Http;

	public class FileController : ApiController
	{
		[HttpPost]
		public async Task<FileResult> UploadSingleFile(Guid id)
		{
			var uploadFolder = Path.Combine(Path.GetTempPath(), "EyeSoft.Prototype.Micro.Commanding", id.ToString());

			Storage.Directory(uploadFolder).Create();

			var streamProvider = new MultipartFormDataStreamProvider(uploadFolder);
			await Request.Content.ReadAsMultipartAsync(streamProvider);

			foreach (var file in streamProvider.FileData)
			{
				var fileName = file.Headers.ContentDisposition.FileName.Trim('\"');
				fileName = Path.Combine(uploadFolder, fileName);

				var localFileName = Path.Combine(uploadFolder, file.LocalFileName);

				Storage.File(fileName).Directory.Create();

				Storage.File(localFileName).MoveTo(fileName);
			}

			return new FileResult
				{
					FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName),
					Names = streamProvider.FileData.Select(entry => entry.Headers.ContentDisposition.FileName),
					ContentTypes = streamProvider.FileData.Select(entry => entry.Headers.ContentType.MediaType),
					Description = streamProvider.FormData["description"],
					CreatedTimestamp = DateTime.UtcNow,
					UpdatedTimestamp = DateTime.UtcNow,
				};
		}
	}
}