using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Keybase.Test
{
    [TestClass]
    public class MerkleTest
    {
        [TestMethod]
        public void GetMerkleRootGetsAResponse()
        {
            var root = Merkle.Root(null, null);

            Assert.IsNotNull(root.hash);
            Assert.IsNotNull(root.payload_json);
            Assert.IsNotNull(root.ctime_string);
        }
    }
}
