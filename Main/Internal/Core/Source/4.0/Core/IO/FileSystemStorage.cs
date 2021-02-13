namespace EyeSoft.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class FileSystemStorage : DefaultStorage
    {
        public override IFileInfo File(string path)
        {
            return new FileInfoWrapper(path);
        }

        public override IDirectoryInfo Directory(string path)
        {
            return new DirectoryInfoWrapper(path);
        }

        public override void WriteAllBytes(string path, byte[] bytes)
        {
            System.IO.File.WriteAllBytes(path, bytes);
        }

        public override void WriteAllText(string path, string contents, Encoding encoding = null, bool overwrite = false)
        {
            if (overwrite)
            {
                System.IO.File.Delete(path);
            }

            encoding = encoding ?? Encoding.Default;

            System.IO.File.WriteAllText(path, contents, encoding);
        }

        public override Stream OpenWrite(string path)
        {
            return System.IO.File.OpenWrite(path);
        }

        public override Stream OpenRead(string path)
        {
            return System.IO.File.OpenRead(path);
        }

        public override IEnumerable<IFileInfo> GetFiles(string path, string searchPattern)
        {
            return
                new DirectoryInfo(path)
                    .GetFiles(searchPattern)
                    .Select(f => new FileInfoWrapper(f.FullName));
        }

        public override string ReadAllText(string path, Encoding encoding)
        {
            var text = System.IO.File.ReadAllText(path, Encoding.Default);

            return text;
        }

        public override byte[] ReadAllBytes(string path)
        {
            return System.IO.File.ReadAllBytes(path);
        }

        public override void SaveStreamToFile(Stream stream, string path)
        {
            if (stream.Length == 0)
            {
                return;
            }

            using (var writeStream = OpenWrite(path))
            {
                var bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, bytesInStream.Length);

                writeStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        public override bool Exists(string path)
        {
            return System.IO.File.Exists(path);
        }

        public override void Delete(string path)
        {
            System.IO.File.Delete(path);
        }
    }
}