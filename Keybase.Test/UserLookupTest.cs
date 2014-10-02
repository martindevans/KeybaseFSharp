using System;
using System.Linq;
using Keybase.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Keybase.Test
{
    [TestClass]
    public class UserLookupTest
    {
        private static void AssertMartindevans(Lookup.UserResponse response)
        {
            Assert.AreEqual(0, response.Status.Code);
            Assert.AreEqual("OK", response.Status.Name);

            Assert.AreEqual(1, response.Them.Length);

            Assert.AreEqual("55314fbf0ace243fe66afcf5cd60be00", response.Them[0].Id);
            Assert.AreEqual("martindevans", response.Them[0].Basics.Username);
            Assert.AreEqual(1410965719, response.Them[0].Basics.CTime);
            Assert.AreEqual(1410965719, response.Them[0].Basics.MTime);
            Assert.AreEqual(22, response.Them[0].Basics.IdVersion);
            Assert.AreEqual(1412036434, response.Them[0].Basics.LastIdChange);
        }

        private static void AssertNobody(Lookup.UserResponse response)
        {
            Assert.IsTrue(response.Them.Length == 0 || response.Them.All(a => a == null));
        }

        private static readonly Random _r = new Random();
        public static string RandomString(int length = 20)
        {
            const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(
                Enumerable.Range(0, length)
                .Select(_ => CHARS[_r.Next(CHARS.Length)])
                .ToArray()
            );
        }

        [TestMethod]
        public void LookupByUsernameFindsExistingUser()
        {
            AssertMartindevans(Lookup.Username(new[] { "martindevans" }));
        }

        [TestMethod]
        public void LookupByUsernameDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.Username(new[] {RandomString(10)}));
        }

        [TestMethod]
        public void LookupByDomainFindsExistingUser()
        {
            AssertMartindevans(Lookup.Domain("martindevans.me"));
        }

        [TestMethod]
        public void LookupByDomainDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.Domain(RandomString() + ".THISISNOTAREALGTLD"));
        }

        [TestMethod]
        public void LookupByTwitterFindsExistingUser()
        {
            AssertMartindevans(Lookup.Twitter("martindevans"));
        }

        [TestMethod]
        public void LookupByTwitterDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.Twitter(RandomString()));
        }

        [TestMethod]
        public void LookupByGithubFindsExistingUser()
        {
            AssertMartindevans(Lookup.Github("martindevans"));
        }

        [TestMethod]
        public void LookupByGithubDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.Github(RandomString()));
        }

        [TestMethod]
        public void LookupByRedditFindsExistingUser()
        {
            AssertMartindevans(Lookup.Reddit("martindevans"));
        }

        [TestMethod]
        public void LookupByRedditDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.Reddit(RandomString()));
        }

        [TestMethod]
        public void LookupByHackerNewsFindsExistingUser()
        {
            //I haven't got enough karma to prove my HN account :'(
            //AssertMartindevans(Lookup.HackerNews("martindevans"));
        }

        [TestMethod]
        public void LookupByHackerNewsDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.HackerNews(RandomString()));
        }

        [TestMethod]
        public void LookupByCoinbaseFindsExistingUser()
        {
            //I don't have a coinbase account
            //AssertMartindevans(Lookup.Coinbase("martindevans"));
        }

        [TestMethod]
        public void LookupByCoinbaseDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.Coinbase(RandomString()));
        }

        [TestMethod]
        public void LookupByFingerprintFindsExistingUser()
        {
            AssertMartindevans(Lookup.KeyFingerprint("729906c47f913d1a0ea7b0001642f34f0f6d3ebb"));
        }

        [TestMethod]
        public void LookupByFingerprintDoesNotFindNonExistantUser()
        {
            AssertNobody(Lookup.KeyFingerprint("0000000000000000000000000000000000000000"));
        }
    }
}
