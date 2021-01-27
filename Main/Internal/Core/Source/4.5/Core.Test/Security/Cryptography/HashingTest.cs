namespace EyeSoft.Core.Test.Security.Cryptography
{
    using System;
    using System.Security.Cryptography;
    using Core.IO;
    using Core.Security.Cryptography;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class HashingTest
	{
		private const string FakeProviderName = "Fake";
		private const string FakeExpectedHash = "RgBhAGsAZQA=";

		private const string ClearText = "Hello";
		private const string Sha1Base64ExpectedHash = "9/+ei3uy4Jtwk1pdeF4MxdnQq/A=";

		[TestInitialize]
		public void Initialize()
		{
			Hashing.Reset();
		}

		[TestMethod]
		public void UseAnUnknowHashAlgorithmExpectedException()
		{
			Executing.This(() => VerifyBase64HashAlgorithm("Unknow", null))
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void VerifyCustomHashAlgorithm()
		{
			Hashing.Register<FakeHashAlgorithm>(FakeProviderName);

			VerifyBase64HashAlgorithm(FakeProviderName, FakeExpectedHash);
		}

		[TestMethod]
		public void VerifyCustomHashAlgorithmRegisteredUsingTypeOnly()
		{
			Hashing.Register<FakeHashAlgorithm>();

			VerifyBase64HashAlgorithm(FakeProviderName, FakeExpectedHash);
		}

		[TestMethod]
		public void RegisterAnHashAlgorithmMoreThanOneThrowException()
		{
			Hashing.Register<FakeHashAlgorithm>(FakeProviderName);

			Executing
				.This(() => Hashing.Register<FakeHashAlgorithm>("Fake1"))
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void RegisterAnHashAlgorithmWithAnExistingProviderName()
		{
			Executing
				.This(() => Hashing.Register<FakeHashAlgorithm>(HashAlgorithms.Md5))
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void RegisterAnHashAlgorithmWithAnExistingProviderNameIgnoringCase()
		{
			Executing
				.This(() => Hashing.Register<FakeHashAlgorithm>("Md5"))
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void ComputeHexHashWithMd5Encoding()
		{
			var hex = Hashing.Md5.ComputeHexWithEncoding("value");
			hex.Should().Be.EqualTo("2063c1608d6e0baf80249c42e2be5804");
		}

		[TestMethod]
		public void VerifyMd5Algorithm()
		{
			const string ExpectedHash = "ixqZU8RhEpaoJ6v4xHgE1w==";
			VerifyBase64HashAlgorithm(HashAlgorithms.Md5, ExpectedHash);
		}

		[TestMethod]
		public void VerifyDefaultAlgorithm()
		{
			Hashing.ComputeHashString(ClearText).Should().Be.EqualTo("����{���p�Z]x^��Ы�");
		}

		[TestMethod]
		public void VerifyDefaultAlgorithmBase64()
		{
			Hashing.ComputeHashBase64String(ClearText).Should().Be.EqualTo(Sha1Base64ExpectedHash);
		}

		[TestMethod]
		public void VerifyDefaultAlgorithmHex()
		{
			Hashing.ComputeHex(ClearText).Should().Be.EqualTo("f7ff9e8b7bb2e09b70935a5d785e0cc5d9d0abf0");
		}

		[TestMethod]
		public void VerifySha1Algorithm()
		{
			VerifyBase64HashAlgorithm(HashAlgorithms.Sha1, Sha1Base64ExpectedHash);
		}

		[TestMethod]
		public void VerifySha256Algorithm()
		{
			const string ExpectedHash = "GF+NsyJx/iX1Yab8k4suJkMG7DBO2lGAB9F2SCY4GWk=";
			VerifyBase64HashAlgorithm(HashAlgorithms.Sha256, ExpectedHash);
		}

		[TestMethod]
		public void VerifySha384Algorithm()
		{
			const string ExpectedHash = "NRn+WtLFlu/j4nam81G4/AsD24YXgkkNRfdZjr0Ktf1VIO0QLzjEpeyDTphmgDX8";
			VerifyBase64HashAlgorithm(HashAlgorithms.Sha384, ExpectedHash);
		}

		[TestMethod]
		public void VerifySha512Algorithm()
		{
			const string ExpectedHash = "NhX4DJ0pPtdAJof5SyLVjlKbjMeRb4+sf933+9WvTPd309eVp6AKFr9+fz+5Vh7puq5IDan+ehh2nnGIawPzFQ==";
			VerifyBase64HashAlgorithm(HashAlgorithms.Sha512, ExpectedHash);
		}

		[TestMethod]
		public void CompareFileHashWithTextHash()
		{
			const string Path = "Test.txt";
			const string Contents = "Hello! è";
			const string ExpectedSha1 = "c42b73d217dfd2b7a2e701f1961fb3b8504ed369";

			Storage.Reset(() => new FileSystemStorage());

			Storage.WriteAllText(Path, Contents);
			var text = Storage.ReadAllText(Path);

			text.Should().Be.EqualTo(Contents);

			var textHash = Hashing.ComputeHex(text);
			var fileHash = Storage.File(Path).Hash();

			textHash.Should().Be.EqualTo(ExpectedSha1);
			fileHash.Should().Be.EqualTo(ExpectedSha1);
		}

		private void VerifyBase64HashAlgorithm(string providerName, string expectedHash)
		{
			var computedHash = Hashing.Create(providerName).ComputeHashBase64String(ClearText);

			computedHash.Should().Be.EqualTo(expectedHash);
		}

		private class FakeHashAlgorithm : HashAlgorithm, IHashAlgorithm
		{
			public override void Initialize()
			{
			}

			protected override void HashCore(byte[] array, int ibStart, int cbSize)
			{
			}

			protected override byte[] HashFinal()
			{
				return FakeProviderName.ToByteArray();
			}
		}
	}
}