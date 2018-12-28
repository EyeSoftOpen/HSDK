﻿namespace MassTransit.NewIdTests
{
    using System;
    using EyeSoft.SequentialIdentity;
    using NUnit.Framework;


    [TestFixture]
    public class Using_a_new_id
    {
        [Test]
        public void Should_format_just_like_a_default_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", newId.ToString());
        }

        [Test]
        public void Should_format_just_like_a_fancy_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("{00000000-0000-0000-0000-000000000000}", newId.ToString("B"));
        }

        [Test]
        public void Should_format_just_like_a_narrow_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("00000000000000000000000000000000", newId.ToString("N"));
        }

        [Test]
        public void Should_format_just_like_a_parenthesis_guid_formatter()
        {
            var newId = new NewId();

            Assert.AreEqual("(00000000-0000-0000-0000-000000000000)", newId.ToString("P"));
        }

        [Test]
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