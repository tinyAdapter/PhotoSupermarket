﻿using PhotoSupermarket.Core.Model;
using PhotoSupermarket.Core.Util;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhotoSupermarket.Core.Compression
{
    public class HuffmanCompression
    {
        private readonly char[] data;
        public readonly Dictionary<char, string> zipCode;

        public HuffmanCompression(byte[] originData)
        {
            data = BytesToChars(originData);
            zipCode = new BuildHuffTree(data).generateZipcode();
        }

        private char[] BytesToChars(byte[] bytes)
        {
            char[] result = new char[bytes.Length];

            for (int i = 0; i < bytes.Length; i++)
                result[i] = (char)bytes[i];

            return result;
        }

        public void Compress(String fileSavingPath)
        {
            FileStream fs = File.OpenWrite(fileSavingPath);

            StoreDictionary(fs);

            StoreCompressedData(fs);

            fs.Close();
        }

        private void StoreDictionary(FileStream fs)
        {
            if (zipCode.Count < 1)
                throw new IndexOutOfRangeException("字典长度非正数");
            else if (zipCode.Count > 256)
                throw new IndexOutOfRangeException("字典长度大于256");
            else
            {

                //在第一个字节写入字典长度（count-1）
                fs.Write(new byte[] { (byte)(zipCode.Count - 1) }, 0, 1);

                //写入字典
                foreach (var v in zipCode)
                {
                    fs.Write(new byte[] { (byte)v.Key }, 0, 1);

                    int codeLength = v.Value.Length;
                    fs.Write(new byte[] { (byte)codeLength }, 0, 1);

                    string temp = v.Value;
                    for (int i = 0; temp.Length > 8;i++)
                    {
                        byte fullByte = Convert.ToByte(temp.Substring(0, 8),2);
                        fs.Write(new byte[] { fullByte }, 0, 1);
                        temp = temp.Substring(8);
                    }
                    byte code = Convert.ToByte(temp, 2);
                    fs.Write(new byte[] { code }, 0, 1);

                    //Console.WriteLine((int)v.Key + "&" + v.Value.Length + "&" + (int)code);
                }
            }
        }

        private void StoreCompressedData(FileStream fs)
        {
            string temp = "";
            foreach (char c in data)
            {
                zipCode.TryGetValue(c, out string value);
                temp += value ?? throw new NullReferenceException("字典中没有找到响应值");
                //Console.WriteLine("the value:" + value);
                if (temp.Length >= 8)
                {
                    byte oneByte = Convert.ToByte(temp.Substring(0, 8), 2);
                    temp = temp.Remove(0, 8);
                    fs.Write(new byte[] { oneByte }, 0, 1);
                    //Console.WriteLine("the byte:" + Convert.ToString((int)oneByte, 2));
                }
            }

            if (temp.Length > 0)
            {
                fs.Write(new byte[] { (byte)(Convert.ToInt32(temp, 2) << (8 - temp.Length)) }, 0, 1);
                fs.Write(new byte[] { (byte)(8 - temp.Length) }, 0, 1);
            }
            else if (temp.Length == 0)
                fs.Write(new byte[] { 0 }, 0, 1);
        }
    }
}