using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("EyeSoft.Test, PublicKey=" +
                              "0024000004800000140100000602000000240000525341310008000001000100b15645c56b6f2c" +
                              "52ff34036a74184b325041bdd35127b6bdeb6e5fd652148ba27714b15fd654deece4066a6033d0" +
                              "4708ad339c63e11dd2a86e053370042ca45f0cc9f7c3acb315b730de3273c33a7f44c2fb0b1df8" +
                              "1a593199e277e4304d239f8e7fe42fe7e2d660c81137a38a4de77499a68e9251e819cae272b91a" +
                              "fbdefe737dc504da52596c4a79d77f1c9c3ed42c61496d9ab035888956ac075d1c58334bf27413" +
                              "c185f525e5957a8a42ca0a70a7c59ecc6d7018b96e94ca6feb9c04eee35e1c79b40d548b0310ff" +
                              "60ef8b6c3d1423e97ad4ba64109112539e48038851b677284a4f3c1cb685f6f6916d9935b2b55c" +
                              "93074f1c9423f20754ecc08e71ffca")]
namespace EyeSoft.Mapping
{
    using System;
    using System.Reflection;
    using ComponentModel.DataAnnotations;
    using Extensions;

    public class ReferenceMemberInfoMetadata : MemberInfoMetadata
    {
        internal ReferenceMemberInfoMetadata(MemberInfo memberInfo) : base(memberInfo)
        {
            var property = memberInfo as PropertyInfo;

            var type = property != null ? property.PropertyType : (memberInfo as FieldInfo).FieldType;

            if (type.IsPrimitiveType())
            {
                throw new ArgumentException($"Can map only reference types. Name: {memberInfo.Name} Type: {type.FullName}");
            }

            Required = memberInfo.IsRequired();
        }

        public bool Required { get; }
    }
}