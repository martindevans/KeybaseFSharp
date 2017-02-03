﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Keybase.Test
{
    [TestClass]
    public class KeyTest
    {
        private readonly string _key = @"-----BEGIN PGP PUBLIC KEY BLOCK-----
Version: GnuPG v2.0.22 (MingW32)

mQINBFPGcdcBEAC6PhFMW3VHraR7pXi5PVJ4vnuEqNqOl5T2ADPpHmOTZNGPHtRl
zOcibk6+vgVt6y0Ufr2yyqihMlaJ38t4aYAW9VChYGvd0PO3wCu5vcTby6jmzB3L
u/WrfdH2C/zLDqy3ZgTxGBHm1ES0Gw/FqA67g8hdIE4xtrX9y2H5NpatYNugPsqf
U3FfT79rSklNhdQ1ZLrjon9nikofMqur7/ZzvoSGn/JHcTLBCtuZ/74Xyun5Um6D
zw43xKQ9CfSkHWx8U7kLy0VkVR614RUvomhdWyefmwpB8rdEZNEXvScRXWaOeGsU
dC8vNHWcCv/CmmjSNzVoPtiGfLT0pbDptFMHJTuj/S5u0TzMB+KJ4VSEnHFauqlB
WNIwC7FRnF7OL3ieY0jtv0mkxJivxF9C4NzOR7f5gqtWhBeuWTiMuTrgIk+uXo7W
fSnFnGSr7oeRLHlphBqNYw4QM4rAdwAvd6GStHrB7sK/JRPV2DycnZ8OkWSdLymu
I7cXlrh3YdLJSG6BEkmj/svR4uexrjv22ryATM5yecfeqINZmckTZCKeAqbyLHQG
fQ6vMoGAoQjijOu2JUMLp56ig1jo2COltx3ptuteHqqjs48VWHG1BTWbzCM3haXR
Ytw03NcA2Y+Zjfn5BzVVJ9SE85LahfpPCg/0QNrEBVV3QERBmp+3JmiQkwARAQAB
tD5NYXJ0aW4gRXZhbnMgKGh0dHA6Ly9tYXJ0aW5kZXZhbnMubWUpIDxtYXJ0aW5k
ZXZhbnNAZ21haWwuY29tPokCOQQTAQIAIwUCU8Zx1wIbDwcLCQgHAwIBBhUIAgkK
CwQWAgMBAh4BAheAAAoJEBZC808PbT67r9sQALQhnqCry5sabuvyvfaV4Hcf9Mms
rHdkws66vf6vVwLtYo7+9yHX45CjLVJkd+yHIWCKXcvB6Bt53PtMU2gZVL0VE6vq
HTSx+qFFeKWBf+zjsGwWnJPngMEogzl2NiPQ7lbzmRjj6an7MczXvGLYTmb1Pai0
IGDM3Wnp6c5vJhW4+adzgLXoEiE7hskOyrO/6pLeSRGfZpQeCMgNzIVbKmM6h+xf
4hVVDet6jOFLEsylmuCain6hUzjsfq+4dRrOLiRIqXE8N1NBahUtIqMAlPehGKNo
oFf1t8AHlfYx+bG2K226CUvlgc6I40kh/xZOKn+ZJbEn+1zXORQtB6rCAinGxuqz
Du6quRVPw0HJqotCldOdQqmW+wdZZAQZ/oCy0uxUBJRO6187fdPykbAGvPMqVc4f
m5LRy6O6CEZ2hwXfasKOXjsOFzHSTcpLOxlaPkppGPUsi8kUsxav1S3C/Q9yV4tL
pNMNYOUbepX6xX42/3gGurSiJbnSLX9t+iAAxMcqcEL3WSDsdChfuSe+uxdKv+/5
hbl/eDyk5eZIuNo2eGzYKYTNRp/ub0xrIOByUsj1J0eJk22zsBwH8U/lmF4kUcWs
TiuJR547GHeKAk6VynaVMBUc2NC0QAlgZeNpIX1HPl4gDLL1hprr4GZsGsqxRKfQ
R2Ohfw1Ks9GxTScI
=EAfl
-----END PGP PUBLIC KEY BLOCK-----
".Replace("\r\n", "\n");

        [TestMethod]
        public void GetKeyForExistingUserGetsCorrectKey()
        {
            var result = Key.Key("martindevans");

            Assert.IsTrue(result.StartsWith(_key));

            Assert.AreEqual(_key, result);
        }

        [TestMethod]
        public void GetKeyForNonExistingUserReturnsNull()
        {
            var result = Key.Key(UserLookupTest.RandomString());
            Assert.IsNull(result);
        }
    }
}
