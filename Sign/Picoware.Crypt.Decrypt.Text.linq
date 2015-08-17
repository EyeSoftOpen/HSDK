<Query Kind="Program">
  <Namespace>System.Security.Cryptography</Namespace>
</Query>

void Main()
{
	Console.WriteLine("Insert text to crypt or decrypt");	
	var text = Console.ReadLine();
	
	var password = Util.GetPassword("picoware.strong.password");
	
	Console.WriteLine();
	
	var rgbSalt = Encoding.Unicode.GetBytes("Picoware");
	
	try
	{
		Encription.Decrypt(text, password, rgbSalt).Dump("Decrypted");
	}
	catch
	{
		Encription.Encrypt(text, password, rgbSalt).Dump("Encrypted");
	}
}

public static class Encription
{
	private const int BufferSize = 4096;

	// Encrypt a byte array into a byte array using a key and an IV 
	public static byte[] Encrypt(byte[] clearData, byte[] key, byte[] iv)
	{
		// Create a MemoryStream to accept the encrypted bytes 
		using (var memoryStream = new MemoryStream())
		{
			using (var encryptionAlgorithm = Rijndael.Create())
			{
				encryptionAlgorithm.Key = key;
				encryptionAlgorithm.IV = iv;

				// Create a CryptoStream through which we are going to be
				// pumping our data. 
				// CryptoStreamMode.Write means that we are going to be
				// writing data to the stream and the output will be written
				// in the MemoryStream we have provided. 
				using (var cs = new CryptoStream(memoryStream, encryptionAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
				{
					// Write the data and make it do the encryption 
					cs.Write(clearData, 0, clearData.Length);

					// Close the crypto stream (or do FlushFinalBlock). 
					// This will tell it that we have done our encryption and
					// there is no more data coming in, 
					// and it is now a good time to apply the padding and
					// finalize the encryption process. 
					cs.Close();

					// Now get the encrypted data from the MemoryStream.
					// Some people make a mistake of using GetBuffer() here,
					// which is not the right way. 
					var encryptedData = memoryStream.ToArray();

					return encryptedData;
				}
			}
		}
	}

	// Encrypt a string into a string using a password 
	// Uses Encrypt(byte[], byte[], byte[]) 
	public static string Encrypt(string clearText, string password, byte[] rgbSalt = null)
	{
		// First we need to turn the input string into a byte array. 
		var clearBytes = Encoding.Unicode.GetBytes(clearText);

		// Then, we need to turn the password into Key and IV 
		// We are using salt to make it harder to guess our key
		// using a dictionary attack - 
		// trying to guess a password by enumerating all possible words. 
		using (var pdb = SaltKey(password, rgbSalt))
		{
			var encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));

			// Now we need to turn the resulting byte array into a string. 
			// A common mistake would be to use an Encoding class for that.
			// It does not work because not all byte values can be
			// represented by characters. 
			// We are going to be using Base64 encoding that is designed
			// exactly for what we are trying to do.
			return Convert.ToBase64String(encryptedData);
		}
	}

	// Encrypt bytes into bytes using a password 
	// Uses Encrypt(byte[], byte[], byte[]) 
	public static byte[] Encrypt(byte[] clearData, string password, byte[] rgbSalt = null)
	{
		// We need to turn the password into Key and IV. 
		// We are using salt to make it harder to guess our key
		// using a dictionary attack - 
		// trying to guess a password by enumerating all possible words. 
		using (var pdb = SaltKey(password, rgbSalt))
		{
			return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));
		}
	}

	// Encrypt a file into another file using a password 
	public static void Encrypt(string fileIn, string fileOut, string password, byte[] rgbSalt)
	{
		// First we are going to open the file streams 
		using (var fileStreamIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read))
		{
			using (var fileStreamOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write))
			{
				using (var encryptionAlgorithm = Rijndael.Create())
				{
					using (var pdb = SaltKey(password, rgbSalt))
					{
						encryptionAlgorithm.Key = pdb.GetBytes(32);
						encryptionAlgorithm.IV = pdb.GetBytes(16);
					}

					// Now create a crypto stream through which we are going
					// to be pumping data. 
					// Our fileOut is going to be receiving the encrypted bytes. 
					using (var cryptoStream = new CryptoStream(fileStreamOut, encryptionAlgorithm.CreateEncryptor(), CryptoStreamMode.Write))
					{
						// Now will will initialize a buffer and will be processing
						// the input file in chunks. 
						// This is done to avoid reading the whole file (which can be huge) into memory.
						var buffer = new byte[BufferSize];

						int bytesRead;

						do
						{
							// read a chunk of data from the input file 
							bytesRead = fileStreamIn.Read(buffer, 0, BufferSize);

							// encrypt it 
							cryptoStream.Write(buffer, 0, bytesRead);
						}
						while (bytesRead != 0);
					}
				}
			}
		}
	}

	// Decrypt a byte array into a byte array using a key and an IV 
	public static byte[] Decrypt(byte[] cipherData, byte[] key, byte[] iv)
	{
		// Create a MemoryStream that is going to accept the
		// decrypted bytes 
		using (var memoryStream = new MemoryStream())
		{
			using (var encryptionAlgorithm = Rijndael.Create())
			{
				encryptionAlgorithm.Key = key;
				encryptionAlgorithm.IV = iv;

				// Create a CryptoStream through which we are going to be
				// pumping our data. 
				// CryptoStreamMode.Write means that we are going to be
				// writing data to the stream 
				// and the output will be written in the MemoryStream
				// we have provided. 
				using (var cryptoStream = new CryptoStream(memoryStream, encryptionAlgorithm.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cryptoStream.Write(cipherData, 0, cipherData.Length);
				}
			}

			// Now get the decrypted data from the MemoryStream. 
			// Some people make a mistake of using GetBuffer() here,
			// which is not the right way. 
			var decryptedData = memoryStream.ToArray();

			return decryptedData;
		}
	}

	// Decrypt a string into a string using a password 
	// Uses Decrypt(byte[], byte[], byte[]) 
	public static string Decrypt(string cipherText, string password, byte[] rgbSalt)
	{
		// First we need to turn the input string into a byte array. 
		// We presume that Base64 encoding was used 
		var cipherBytes = Convert.FromBase64String(cipherText);

		// Then, we need to turn the password into Key and IV 
		// We are using salt to make it harder to guess our key
		// using a dictionary attack - 
		// trying to guess a password by enumerating all possible words. 
		using (var pdb = SaltKey(password, rgbSalt))
		{
			var decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));

			// Now we need to turn the resulting byte array into a string. 
			// A common mistake would be to use an Encoding class for that.
			// It does not work 
			// because not all byte values can be represented by characters. 
			// We are going to be using Base64 encoding that is 
			// designed exactly for what we are trying to do. 
			return System.Text.Encoding.Unicode.GetString(decryptedData);
		}
	}

	// Decrypt bytes into bytes using a password 
	// Uses Decrypt(byte[], byte[], byte[]) 
	public static byte[] Decrypt(byte[] cipherData, string password, byte[] rgbSalt)
	{
		// We need to turn the password into Key and IV. 
		// We are using salt to make it harder to guess our key
		// using a dictionary attack - 
		// trying to guess a password by enumerating all possible words. 
		using (var pdb = SaltKey(password, rgbSalt))
		{
			return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
		}
	}

	// Decrypt a file into another file using a password 
	public static void Decrypt(string fileIn, string fileOut, string password, byte[] rgbSalt)
	{
		// First we are going to open the file streams 
		using (var fsIn = new FileStream(fileIn, FileMode.Open, FileAccess.Read))
		{
			using (var fsOut = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write))
			{
				using (var encryptionAlgorithm = Rijndael.Create())
				{
					using (var pdb = SaltKey(password, rgbSalt))
					{
						encryptionAlgorithm.Key = pdb.GetBytes(32);
						encryptionAlgorithm.IV = pdb.GetBytes(16);
					}

					// Now create a crypto stream through which we are going
					// to be pumping data. 
					// Our fileOut is going to be receiving the Decrypted bytes. 
					using (var cryptoStream = new CryptoStream(fsOut, encryptionAlgorithm.CreateDecryptor(), CryptoStreamMode.Write))
					{
						// Now will will initialize a buffer and will be 
						// processing the input file in chunks. 
						// This is done to avoid reading the whole file (which can be huge) into memory.
						var buffer = new byte[BufferSize];

						int bytesRead;

						do
						{
							// read a chunk of data from the input file 
							bytesRead = fsIn.Read(buffer, 0, BufferSize);

							// Decrypt it 
							cryptoStream.Write(buffer, 0, bytesRead);
						}
						while (bytesRead != 0);
					}
				}
			}
		}
	}
	
	private static Rfc2898DeriveBytes SaltKey(string password, byte[] rgbSalt)
	{
		if (rgbSalt != null)
		{
			return new Rfc2898DeriveBytes(password, rgbSalt);
		}
		
		return new Rfc2898DeriveBytes(password, 16);
	}
}