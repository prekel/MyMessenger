using Microsoft.EntityFrameworkCore.Storage;
using MyMessenger.Server;

using NUnit.Framework;

namespace MyMessenger.Server.Tests
{
	[TestFixture]
	public class CryptoTest
	{
		private string Password1;
		private string Password2;
		private string Password3;
		private int Salt1;
		private int Salt2;
		private int Salt3;
		private byte[] Hash1;
		private byte[] Hash2;
		private byte[] Hash3;
		
		[SetUp]
		public void Setup()
		{
			Password1 = "123456";
			Password2 = "123456";
			Password3 = "123457";

			Salt1 = Crypto.GenerateSaltForPassword();
			Salt2 = Crypto.GenerateSaltForPassword();
			Salt3 = Crypto.GenerateSaltForPassword();

			Hash1 = Crypto.ComputePasswordHash(Password1, Salt1);
			Hash2 = Crypto.ComputePasswordHash(Password2, Salt2);
			Hash3 = Crypto.ComputePasswordHash(Password3, Salt3);
		}

		[Test]
		public void ValidTest()
		{
			Assert.IsTrue(Crypto.IsPasswordValid(Password1, Salt1, Hash1));
			Assert.IsTrue(Crypto.IsPasswordValid(Password2, Salt2, Hash2));
			Assert.IsTrue(Crypto.IsPasswordValid(Password3, Salt3, Hash3));
		}

		[Test]
		public void NotValidTest()
		{
			Assert.IsFalse(Crypto.IsPasswordValid("cat", Salt1, Hash1));
			Assert.IsFalse(Crypto.IsPasswordValid(Password1, Salt2, Hash2));
			Assert.IsFalse(Crypto.IsPasswordValid(Password3, Salt2, Hash3));
		}

		[Test]
		public void SaltTest()
		{
			Assert.AreEqual(Password1, Password2);
			Assert.AreEqual(Hash1, Hash1);
			Assert.AreNotEqual(Hash1, Hash2);
		}
	}
}