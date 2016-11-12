using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Enterprise.Component.Common.Encrypt;
using NUnit.Framework;

namespace Enterprise.XUnits.CommenTests
{
    [TestFixture]
    public class DesHelperTests
    {
        [Test]
        public void TestEncrypt()
        {
            Assert.IsNotNull(DesHelper.DES_Encryption("daniao"));
            Assert.AreEqual("daniao", DesHelper.DES_Decryption(DesHelper.DES_Encryption("daniao")));
        }
    }
}
