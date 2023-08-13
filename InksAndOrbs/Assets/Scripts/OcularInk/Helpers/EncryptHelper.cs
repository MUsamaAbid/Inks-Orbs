using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class EncryptHelper
{
    public static string CreateHash(string data)
    {
        byte[] dataByte = Encoding.UTF8.GetBytes(data);
        SHA256Managed SHA256 = new SHA256Managed();

        byte[] hashValue = SHA256.ComputeHash(dataByte);

        return GetHexStringFromHash(hashValue);
    }

    public static bool VerifyHash(string data, string hash)
    {
        string defaultHash = "initial_hash";
        string hashValue = CreateHash(data);

        return hashValue == hash || hashValue == defaultHash;
    }

    private static string GetHexStringFromHash(byte[] hash)
    {
        string hexString = String.Empty;

        foreach (byte b in hash)
        {
            hexString += b.ToString("x2");
        }

        return hexString;
    }
}