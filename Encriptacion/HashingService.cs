using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Encriptacion
{
    public class HashingService: IHashingService
	{

		//public string Create(string password, string salt)
		//{
		//	var valueBytes = KeyDerivation.Pbkdf2(
		//						password: password,
		//						salt: Encoding.UTF8.GetBytes(salt),
		//						prf: KeyDerivationPrf.HMACSHA512,
		//						iterationCount: 10000,
		//						numBytesRequested: 64);

		//	return Convert.ToBase64String(valueBytes);
		//}

		public string CreateSha256(string password)
		{
			using (SHA256 sha256Hash = SHA256.Create())
			{
				// ComputeHash - returns byte array  
				byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

				// Convert byte array to a string   
				StringBuilder builder = new StringBuilder();
				for (int i = 0; i < bytes.Length; i++)
				{
					builder.Append(bytes[i].ToString("x2"));
				}
				return builder.ToString();
			}
		}
	}
}
