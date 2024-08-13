using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ArthiPOS.shop;
using CommonUtilities;
using System.Diagnostics;

namespace ArthiPOS.Utill
{
    public class EncrypDecrypt
    {
        #region Encrypt Decrypt and convert into RAR file
        public static bool IsBakFile(string filePath)
        {
            return Path.GetExtension(filePath).Equals(".bak", StringComparison.OrdinalIgnoreCase);
        }
        public static void EncryptFileToRar(string sourceFile, string rarFile, string password)
        {
            string arguments = $"a -hp{password} {rarFile} {sourceFile}";

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "Rar.exe", // Ensure Rar.exe is in your PATH or provide the full path
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(output);
            }
        }

        public static bool IsEncryptedRarFile(string rarFile, string password)
        {
            string arguments = $"t -p{password} {rarFile}";

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "Rar.exe", // Ensure Rar.exe is in your PATH or provide the full path
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output.Contains("All OK");
            }
        }

        public static void DecryptRarFile(string rarFile, string extractPath, string password)
        {
            string arguments = $"x -p{password} {rarFile} {extractPath}";

            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "Rar.exe", // Ensure Rar.exe is in your PATH or provide the full path
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Console.WriteLine(output);
            }
        }
        #endregion
        // Rfc2898DeriveBytes constants:
        public readonly byte[] salt = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; // Must be at least eight bytes.  MAKE THIS SALTIER!
        public const int iterations = 1042; // Recommendation is >= 1000.

        public bool IsFileSecure=false;
        public EncrypDecrypt()
        {
            IsFileSecure = RegistryAccess.GetStringSecurityValue(Const.SECURITYKEY, false);
        }

        /// <summary>Decrypt a file.</summary>
        /// <remarks>NB: "Padding is invalid and cannot be removed." is the Universal CryptoServices error.  Make sure the password, salt and iterations are correct before getting nervous.</remarks>
        /// <param name="sourceFilename">The full path and name of the file to be decrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        /// <param name="password">The password for the decryption.</param>
        /// <param name="salt">The salt to be applied to the password.</param>
        /// <param nam
        /// e="iterations">The number of iterations Rfc2898DeriveBytes should use before generating the key and initialization vector for the decryption.</param>
        public void DecryptDatabase(string key)
        {
            if (!IsFileSecure)
                return;
            string path = ".\\db\\db_pt.mdf";
            string dpath = ".\\db\\db_pt";
            string path1 = ".\\db\\db_pt_log.mdf";
            string dpath1 = ".\\db\\db_pt_log";
            DecryptFile(dpath, path, key, salt, 1);
            DecryptFile(dpath1, path1, key, salt, 1);
        }
        public void EncryptDatabase(string key)
        {
            if (!IsFileSecure)
                return;
            string path = ".\\db\\db_pt.mdf";
            string dpath = ".\\db\\db_pt";
            string path1 = ".\\db\\db_pt_log.mdf";
            string dpath1 = ".\\db\\db_pt_log";
            EncryptFile(path,dpath, key, salt, 1);
            EncryptFile(path1,dpath1, key, salt, 1);
        }

        public void DecryptFile(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    try
                    {
                        using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            source.CopyTo(cryptoStream);
                        }
                    }
                    catch (CryptographicException exception)
                    {
                        if (exception.Message == "Padding is invalid and cannot be removed.")
                            throw new ApplicationException("Universal Microsoft Cryptographic Exception (Not to be believed!)", exception);
                        else
                            throw;
                    }
                }
            }
        }

        /// <summary>Encrypt a file.</summary>
        /// <param name="sourceFilename">The full path and name of the file to be encrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        /// <param name="password">The password for the encryption.</param>
        /// <param name="salt">The salt to be applied to the password.</param>
        /// <param name="iterations">The number of iterations Rfc2898DeriveBytes should use before generating the key and initialization vector for the decryption.</param>
        public void EncryptFile(string sourceFilename, string destinationFilename, string password, byte[] salt, int iterations)
        {
            AesManaged aes = new AesManaged();
            aes.BlockSize = aes.LegalBlockSizes[0].MaxSize;
            aes.KeySize = aes.LegalKeySizes[0].MaxSize;
            // NB: Rfc2898DeriveBytes initialization and subsequent calls to   GetBytes   must be eactly the same, including order, on both the encryption and decryption sides.
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations);
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Mode = CipherMode.CBC;
            ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);

            using (FileStream destination = new FileStream(destinationFilename, FileMode.CreateNew, FileAccess.Write, FileShare.None))
            {
                using (CryptoStream cryptoStream = new CryptoStream(destination, transform, CryptoStreamMode.Write))
                {
                    using (FileStream source = new FileStream(sourceFilename, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        source.CopyTo(cryptoStream);
                    }
                }
            }
        }
    }
}
