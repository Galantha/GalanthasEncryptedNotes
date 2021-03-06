﻿/*Created by Layla aka Galantha
 * MIT license
 * no warranty
 * 
 * Copyright 2020-12-27
 * 
 * email: gal_0xff@outlook.com
 */

using System;
using System.Text;

namespace GalsPassHolder
{
    public static class GalLib
    {
        public static readonly IFormatProvider inv = System.Globalization.CultureInfo.InvariantCulture;
        private const string versionPrefix = "²";
        private const string verify = "Verify!";
        private const string dsVersionPrefix = "³";


        private static readonly char[] charArray = InitCharArray(); //length is 89 //changing this will break backwards compatiability
        private static char[] InitCharArray()
        {
            //StringBuilder chars = new StringBuilder();
            //for (int i = 32; i <= 254; i++)
            //{
            //    if (i >= 127 && i < 159)
            //        i = 159;
            //    chars.Append((char)i);
            //}
            //return chars.ToString().ToCharArray();

            //the below is more predictable when default system code pages change
            //return " !#$%()*+,-./0123456789:;=?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþ".ToCharArray();
            return " !#$%()*+,-./0123456789:;=?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~".ToCharArray(); //non-keyboard chars removed, xml chars removed
        }

        private const int filePadLength = 42; //answer to the ultimate question of life, the universe, and everything

        public static String GetRandomSalt(int length = 25)
        {
            Random randy = new Random();
            int maxValue = charArray.Length - 1;
            StringBuilder salt = new StringBuilder(50);

            while (salt.Length < length)
                salt.Append(charArray[randy.Next(0, maxValue)]);

            return salt.ToString();
        }
        public static string GetNonRandomSalt(int length = 25)
        {
            int pos = 0;
            StringBuilder salt = new StringBuilder(length);

            while (salt.Length < length)
            {
                if (pos >= charArray.Length)
                    pos = 0;
                salt.Append(charArray[pos]);
                pos += 1;
            }
            return salt.ToString();
        }


        private static byte[] GetHash256(Byte[] bytes, int extraIterations = 10000)
        {
            byte[] ret;
            {
                var mySHA256 = System.Security.Cryptography.SHA256.Create();
                ret = mySHA256.ComputeHash(GetHash512(bytes, extraIterations));
                mySHA256.Clear();
                mySHA256.Dispose();
            } //forces out of scope and into garbage collection
            GC.Collect();
            return ret;
        }
        private static byte[] GetHash256(string salt, string key, int extraIterations = 10000)
        {
            return GetHash256(StringToBytes(salt + salt.Length.ToString(inv) + key + key.Length.ToString(inv) + "whyNot"), extraIterations + salt.Length + key.Length);
        }
        private static byte[] GetHash512(Byte[] bytes, int extraIterations = 10000)
        {
            Byte[] workingBytes;
            {
                int iterations = 100; //always have a few extra, start to see slow downs in some areas if this is to large
                iterations += AddUpArray(bytes); //frequently >1000
                iterations += extraIterations;
                workingBytes = new byte[bytes.Length + 4];
                bytes.CopyTo(workingBytes, 0);
                BitConverter.GetBytes(bytes.Length).CopyTo(workingBytes, bytes.Length);

                var mySHA512 = System.Security.Cryptography.SHA512.Create(); //sha512 is part of sha2, which has no known attacks as of this me writing this
                for (int i = 0; i < iterations; i++)
                    workingBytes = mySHA512.ComputeHash(workingBytes);
                mySHA512.Clear();
                mySHA512.Dispose();
            } //out of scope and into garbage
            GC.Collect();
            return workingBytes;
        }
        public static byte[] GetHash512(string salt, string key, int extraIterations = 10000)
        {
            return GetHash512(StringToBytes(salt + salt.Length.ToString(inv) + key + key.Length.ToString(inv) + "more is better"), extraIterations + salt.Length + key.Length);
        }

        public static string EncryptString(string salt, string key, string plainText, int extraHashIterations = 10000)
        {
            const string version = "00001";
            byte[] result;
            {
                if (string.IsNullOrWhiteSpace(salt))
                    throw new ArgumentNullException(nameof(salt), "salt is not allowed to be empty");
                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException(nameof(key), "key is not allowed to be empty");

                byte[] ky = GetHash256(salt, key, extraHashIterations);
                byte[] iv = GetHash256(key, salt, (extraHashIterations + 1) / 2); 

                string textToWrite = GetRandomSalt(10) + verify + plainText; //randomizing the input just a touch //verify lets us verify decryption was correct

                using (var rij = new System.Security.Cryptography.RijndaelManaged())
                {
                    try
                    {
                        rij.KeySize = 256;
                        rij.BlockSize = 256; //this is why not using AES

                        using (var encryptor = rij.CreateEncryptor(ky, iv))
                        {
                            using (var ms = new System.IO.MemoryStream())
                            {
                                using (var cs = new System.Security.Cryptography.CryptoStream(ms, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                                {
                                    using (var sw = new System.IO.StreamWriter(cs))
                                    {
                                        sw.Write(textToWrite);
                                    }
                                }
                                result = ms.ToArray();
                            }
                        }
                    }
                    finally
                    {
                        rij.Clear();
                    }
                }

                WipeByteArray(ky);
                WipeByteArray(iv);
            } //out of scope and into garbage
            GC.Collect();

            string encrypted = versionPrefix + version + BytesToStrStorage(result);
            return encrypted;
        }

        public static string DecryptString(string salt, string key, string encryptedText, int extraHashIterations = 10000)
        {
            string version;
            string plainText = "";
            {
                if (string.IsNullOrWhiteSpace(salt))
                    throw new ArgumentNullException(nameof(salt), "salt is not allowed to be empty");
                if (string.IsNullOrWhiteSpace(key))
                    throw new ArgumentNullException(nameof(key), "key is not allowed to be empty");

                byte[] ky = GetHash256(salt, key, extraHashIterations);
                byte[] iv = GetHash256(key, salt, (extraHashIterations + 1) / 2);


                byte[] encryptedBytes;
                if (encryptedText.Substring(0, 1) != versionPrefix) //version prefix was my way of adding versioning to a function that did not have it orginally
                    version = "00000";
                else
                    version = encryptedText.Substring(1, 5);

                switch (version) //yea, so the first version of the function needed a verify step, so then I realized I needed versioning with a graceful way to transistion between versions.
                {
                    case "00000":
                        encryptedBytes = StrStorageToBytes(encryptedText);
                        break;
                    case "00001":
                        encryptedBytes = StrStorageToBytes(encryptedText.Substring(1 + 5)); //1 for the prefix and 5 for the version
                        break;
                    default:
                        throw new Exception("Decrypt String unsupported version!");
                }

                using (var rij = new System.Security.Cryptography.RijndaelManaged())
                {
                    try
                    {
                        rij.KeySize = 256;
                        rij.BlockSize = 256; //this is why not using AES

                        using (var decryptor = rij.CreateDecryptor(ky, iv))
                        {
                            using (var ms = new System.IO.MemoryStream(encryptedBytes))
                            {
                                using (var cs = new System.Security.Cryptography.CryptoStream(ms, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                                {
                                    using (var sr = new System.IO.StreamReader(cs))
                                    {
                                        plainText = sr.ReadToEnd();
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        rij.Clear();
                    }
                }

                WipeByteArray(ky);
                WipeByteArray(iv);
            }//out of scope and into garbage
            GC.Collect();

            switch (version)
            {
                case "00000":
                    return plainText.Substring(10); //remember the first 10 chars are just randomized for fun
                case "00001":
                    if (plainText.Substring(10, verify.Length) != verify)
                        throw new Exception("decryption verification failed!");
                    else
                        return plainText.Substring(10 + verify.Length);
                default:
                    throw new Exception("Decrypt String unsupported version!");
            }
        }

        public static void EncryptDataSetToFile<T>(T ds, string file, string key, Boolean noAsync = true) where T : System.Data.DataSet
        {
            const string version = "0000A";
            {
                String strDataSet;
                using (var ms = new System.IO.MemoryStream())
                {
                    lock (ds)
                    {
                        ds.WriteXml(ms);
                    }
                    ms.Position = 0;
                    using (var sr = new System.IO.StreamReader(ms))
                    {
                        strDataSet = verify + sr.ReadToEnd();
                    }
                }

                string[] paramArray = { strDataSet, file, key, version };

                if (noAsync)
                    EncryptDataSetToFileAsyncHelper(paramArray);
                else
                {
                    var t = new System.Threading.Thread(EncryptDataSetToFileAsyncHelper);
                    t.Start(paramArray);
                }
            } //out of scope into the garbage, the thread will persist though because it has a system referance
            GC.Collect();
        }

        private static void EncryptDataSetToFileAsyncHelper(object paramArrayObject)
        {
            {
                string[] paramArray = (string[])paramArrayObject;

                string strDataSet = paramArray[0];
                string file = paramArray[1];
                string key = paramArray[2];
                string version = paramArray[3];

                string salt = GetRandomSalt(filePadLength);
                string dsEncrypted = EncryptString(salt, key, strDataSet, 20000); //this is mildly expensive, along with VerifyEncryptedFile below //threading this reduces UI lag
                String tmpFile = GetTemporaryFileName(file);

                using (var sw = System.IO.File.CreateText(tmpFile))
                {
                    sw.Write(dsVersionPrefix);
                    sw.Write(version);
                    sw.Write(salt);
                    sw.Write(dsEncrypted);
                    sw.Flush();
                    sw.Close();
                }

                if (VerifyEncryptedFile(tmpFile, key) == false)
                {
                    //panic!
                    throw new Exception("File Verification Failed!");
                }
                else
                {
                    System.IO.File.Copy(tmpFile, file, true); //this is done because I do not want to delete a good previous file until I know I have a good current file
                    System.IO.File.Delete(tmpFile);
                }

                paramArray = null;
                paramArrayObject = null;
                strDataSet = "";
                key = "";
            } //out of scope into the garbage
            GC.Collect();
        }

        private static string GetTemporaryFileName(String fileName)
        {
            var n = DateTime.Now;
            var f = "00";
            return fileName + "." + n.Year.ToString("#" + f, inv) + n.Month.ToString(f, inv) + n.Day.ToString(f, inv) + n.Hour.ToString(f, inv) + n.Minute.ToString(f, inv) + n.Second.ToString(f, inv) + n.Millisecond.ToString(f, inv) + ".tmp";
        }

        private static bool VerifyEncryptedFile(string file, string key)
        {
            {
                Tuple<string, string> prep;
                try
                {
                    prep = getStringFromFileForDataSet(file, key);
                }
                catch (Exception)
                {
                    return false;
                }
                var version = prep.Item1;
                var plainText = prep.Item2;

                switch (version)
                {
                    case "00000":
                        return true;
                    case "0000A":
                        if (plainText.Substring(0, verify.Length) != verify)
                            return false;
                        break;
                    default:
                        throw new Exception("VerifyEncryptedFile unsupported version!");
                }
            }//out of scope and into garbage
            GC.Collect();
            return true;
        }

        private static Tuple<string, string> getStringFromFileForDataSet(string file, string key)
        {
            String data;
            using (var fs = System.IO.File.OpenRead(file))
            {
                using (var sr = new System.IO.StreamReader(fs))
                {
                    data = sr.ReadToEnd();
                }
            }

            string version;
            if (data.Substring(0, 1) == dsVersionPrefix)
                version = data.Substring(1, 5);
            else
                version = "00000";

            string salt;
            string encryptedData;
            switch (version)
            {
                case "00000":
                    salt = data.Substring(0, filePadLength);
                    encryptedData = data.Substring(filePadLength);
                    break;
                case "0000A":
                    salt = data.Substring(1 + 5, filePadLength);
                    encryptedData = data.Substring(filePadLength + 1 + 5);
                    break;
                default:
                    throw new Exception("VerifyEncryptedFile unsupported version!");
            }
            return new Tuple<string, string>(version, DecryptString(salt, key, encryptedData, 20000));
        }

        public static T DecryptDataSetFromFile<T>(string file, string key) where T : System.Data.DataSet, new()
        {
            T dataSet;
            {
                Tuple<string, string> prep = getStringFromFileForDataSet(file, key);
                string version = prep.Item1;
                string decryptedData;
                switch (version)
                {
                    case "00000":
                        decryptedData = prep.Item2;
                        break;
                    case "0000A":
                        if (prep.Item2.Substring(0, verify.Length) != verify)
                            throw new Exception("File decryption verification failed!");
                        else
                            decryptedData = prep.Item2.Substring(verify.Length);
                        break;
                    default:
                        throw new Exception("VerifyEncryptedFile unsupported version!");
                }

                dataSet = new T();
                using (var ms = new System.IO.MemoryStream())
                {
                    using (var sr = new System.IO.StreamWriter(ms))
                    {
                        sr.Write(decryptedData);
                        sr.Flush();
                        ms.Position = 0;
                        dataSet.ReadXml(ms);
                    }
                }
            }//out of scope and into garbage
            GC.Collect();
            return dataSet;
        }

        public static string BytesToStrStorage(byte[] bytes)
        {
            //Convert.ToBase64String uses A-Z  a-z  0-9  +/=
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
        private static byte[] StrStorageToBytes(string encoded)
        {
            return Convert.FromBase64String(encoded);
        }
        private static byte[] StringToBytes(string convertMe)
        {
            return System.Text.Encoding.Unicode.GetBytes(convertMe);
        }
        private static string BytesToString(byte[] bytes)
        {
            return System.Text.Encoding.Unicode.GetString(bytes);
        }
        private static int AddUpArray(byte[] bytes)
        {
            int result = 0;
            foreach (Byte b in bytes)
                result += Convert.ToInt32(b);
            return result;
        }
        private static byte[] TruncateByteArray(byte[] bytes, int newSize)
        {
            byte[] newArray = new byte[newSize];
            for (int i = 0; i < newSize; i++)
                newArray[i] = bytes[i];
            return newArray;
        }
        private static void WipeByteArray(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = 0;
        }
    }
}
