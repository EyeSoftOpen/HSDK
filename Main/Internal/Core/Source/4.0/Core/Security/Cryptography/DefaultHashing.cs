namespace EyeSoft.Security.Cryptography
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;

    internal class DefaultHashing
    {
        private readonly IDictionary<string, HashAlgoritmRegistration> hashAlgorithmDictionary =
            new Dictionary<string, HashAlgoritmRegistration>();

        private readonly IDictionary<string, Func<IHashAlgorithm>> systmeHashAlgorithmDictionary =
            new Dictionary<string, Func<IHashAlgorithm>>
            {
                { HashAlgorithms.Md5, Create<MD5> },
                { HashAlgorithms.Sha1, Create<SHA1> },
                { HashAlgorithms.Sha256, Create<SHA256> },
                { HashAlgorithms.Sha384, Create<SHA384> },
                { HashAlgorithms.Sha512, Create<SHA512> }
            };

        private readonly Singleton<Func<IHashAlgorithm>> defaultHashAlgorithm;

        private readonly Singleton<IHashAlgorithmFactory> factory =
            new Singleton<IHashAlgorithmFactory>(() => new HashAlgorithmFactoryWrapper());

        public DefaultHashing()
        {
            defaultHashAlgorithm = new Singleton<Func<IHashAlgorithm>>(() => () => Sha1);
        }

        public Func<IHashAlgorithm> Default
        {
            get { return defaultHashAlgorithm.Instance; }
        }

        public IHashAlgorithm Md5
        {
            get { return Create(HashAlgorithms.Md5); }
        }

        public IHashAlgorithm Sha1
        {
            get { return Create(HashAlgorithms.Sha1); }
        }

        public IHashAlgorithm Sha256
        {
            get { return Create(HashAlgorithms.Sha256); }
        }

        public IHashAlgorithm Sha384
        {
            get { return Create(HashAlgorithms.Sha384); }
        }

        public IHashAlgorithm Sha512
        {
            get { return Create(HashAlgorithms.Sha512); }
        }

        public IHashAlgorithm Ripemd160
        {
            get { return Create(HashAlgorithms.Ripemd160); }
        }

        public void SetHashAlgorithm(Func<IHashAlgorithm> hashAlgorithm)
        {
            defaultHashAlgorithm.Set(() => hashAlgorithm);
        }

        public void SetHashAlgorithmFactory(IHashAlgorithmFactory hashAlgorithmFactory)
        {
            factory.Set(() => hashAlgorithmFactory);
        }

        public void Register<T>() where T : IHashAlgorithm, new()
        {
            var providerName = typeof(T).Name.Replace("HashAlgorithm", null);

            Register<T>(providerName);
        }

        public void Register<T>(string providerName) where T : IHashAlgorithm, new()
        {
            if (HashAlgorithms.All.Any(p => p.IgnoreCaseEquals(providerName)))
            {
                var message = $"The provider name is a system hash algorithm and cannot be used. Provider name: {providerName}.";

                throw new ArgumentException(message);
            }

            var hashingType = typeof(T);

            var providerTypeAlreadyRegistered = hashAlgorithmDictionary.Any(p => p.Value.HashingType == hashingType);

            if (providerTypeAlreadyRegistered)
            {
                var message = $"The hash algorithm with the name {providerName} and type {hashingType} is already registered.";

                throw new ArgumentException(message);
            }

            hashAlgorithmDictionary.Add(providerName, new HashAlgoritmRegistration(() => new T(), hashingType));
        }

        public IHashAlgorithm Create(string providerName)
        {
            if (systmeHashAlgorithmDictionary.ContainsKey(providerName))
            {
                return systmeHashAlgorithmDictionary[providerName]();
            }

            if (!hashAlgorithmDictionary.ContainsKey(providerName))
            {
                throw new ArgumentException(string.Format("The provider '{0}' is not known.", providerName));
            }

            return hashAlgorithmDictionary[providerName].Create();
        }

        public void Reset()
        {
            hashAlgorithmDictionary.Clear();
        }

        private static IHashAlgorithm Create<THashAlgorithm>() where THashAlgorithm : HashAlgorithm
        {
            return new HashAlgorithmWrapper(HashAlgorithm.Create(typeof(THashAlgorithm).Name));
        }

        private class HashAlgoritmRegistration
        {
            public HashAlgoritmRegistration(Func<IHashAlgorithm> create, Type hashingType)
            {
                Create = create;
                HashingType = hashingType;
            }

            public Func<IHashAlgorithm> Create { get; private set; }

            public Type HashingType { get; private set; }
        }
    }
}