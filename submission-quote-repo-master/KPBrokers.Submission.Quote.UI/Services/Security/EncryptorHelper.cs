using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace KPBrokers.Submission.Quote.UI.Services.Security
{
    public static class EncryptorHelper
    {
        /// <summary>
        /// Encrypts the specified source data.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <returns></returns>
        /// <exception cref="StringEncryptorException">Unable to encrypt data.</exception>
        public static string Encrypt(string sourceData)
        {
            // set key and initialization vector values
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            try
            {
                // convert data to byte array
                byte[] sourceDataBytes =
                  System.Text.ASCIIEncoding.ASCII.GetBytes(sourceData);

                // get target memory stream
                MemoryStream tempStream = new MemoryStream();

                // get encryptor and encryption stream
#pragma warning disable SYSLIB0021 // Type or member is obsolete
                DESCryptoServiceProvider encryptor = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
                CryptoStream encryptionStream = new CryptoStream(tempStream, encryptor.CreateEncryptor(key, iv),
                    CryptoStreamMode.Write);

                // encrypt data
                encryptionStream.Write(sourceDataBytes, 0, sourceDataBytes.Length);
                encryptionStream.FlushFinalBlock();

                // put data into byte array
                byte[] encryptedDataBytes = tempStream.GetBuffer();

                // convert encrypted data into string
                //return Convert.ToBase64String(encryptedDataBytes, 0,(int)tempStream.Length);
                return Encode(Convert.ToBase64String(encryptedDataBytes, 0, (int)tempStream.Length));
            }
            catch
            {
                throw new Exception("Unable to encrypt data.");
            }
        }

        /// <summary>
        /// Decrypts the specified source data.
        /// </summary>
        /// <param name="sourceData">The source data.</param>
        /// <returns></returns>
        /// <exception cref="StringEncryptorException">Unable to decrypt data.</exception>
        public static string Decrypt(string sourceData)
        {
            sourceData = Decode(sourceData);

            // set key and initialization vector values
            byte[] key = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] iv = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            try
            {
                // convert data to byte array
                byte[] encryptedDataBytes = Convert.FromBase64String(sourceData);

                // get source memory stream and fill it 
                MemoryStream tempStream = new MemoryStream(encryptedDataBytes, 0, encryptedDataBytes.Length);

                // get decryptor and decryption stream 
#pragma warning disable SYSLIB0021 // Type or member is obsolete
                DESCryptoServiceProvider decryptor = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // Type or member is obsolete
                CryptoStream decryptionStream = new CryptoStream(tempStream, decryptor.CreateDecryptor(key, iv), CryptoStreamMode.Read);

                // decrypt data 
                StreamReader allDataReader =  new StreamReader(decryptionStream);
                return allDataReader.ReadToEnd();
            }
            catch
            {
                throw new Exception("Unable to decrypt data.");
            }
        }


        /// <summary>
        /// Encodes the specified encode string.
        /// </summary>
        /// <param name="encodeString">The encode string.</param>
        /// <returns></returns>
        private static string Encode(string encodeString)
        {
            try
            {
                byte[] encoded = System.Text.Encoding.UTF8.GetBytes(encodeString);
                return Convert.ToBase64String(encoded);
            }
            catch
            {
                throw new Exception("Unable to decrypt data.");
            }
        }

        /// <summary>
        /// Decodes the specified decode string.
        /// </summary>
        /// <param name="decodeString">The decode string.</param>
        /// <returns></returns>
        private static string Decode(string decodeString)
        {
            try
            {
                byte[] encoded = Convert.FromBase64String(decodeString);
                return System.Text.Encoding.UTF8.GetString(encoded);
            }
            catch
            {
                throw new Exception("Unable to decrypt data.");
            }
        }      
    }
}
