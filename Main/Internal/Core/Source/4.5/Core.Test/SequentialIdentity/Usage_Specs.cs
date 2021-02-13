namespace EyeSoft.Core.Test.SequentialIdentity
{
    using System;
    using EyeSoft.SequentialIdentity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Using_a_new_id
    {
        [TestMethod]
        public void Should_format_just_like_a_default_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", newId.ToString());
        }

        [TestMethod]
        public void Should_format_just_like_a_fancy_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("{00000000-0000-0000-0000-000000000000}", newId.ToString("B"));
        }

        [TestMethod]
        public void Should_format_just_like_a_narrow_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("00000000000000000000000000000000", newId.ToString("N"));
        }

        [TestMethod]
        public void Should_format_just_like_a_parenthesis_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("(00000000-0000-0000-0000-000000000000)", newId.ToString("P"));
        }

        [TestMethod]
        public void Should_work_from_guid_to_newid_to_guid()
        {
            Guid g = Guid.NewGuid();

            var n = new NewId(g.ToByteArray());

            string gs = g.ToString("d");
            string ns = n.ToString("d");

            Assert.AreEqual(gs, ns);
        }
    }
}