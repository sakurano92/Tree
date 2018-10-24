﻿using System;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Summary description for EncryptionHandler
/// </summary>
public class EncryptionHandler
{
	public EncryptionHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string EncodePassword(string originalPassword)
    {
        //Declarations
        Byte[] originalBytes;
        Byte[] encodedBytes;
        MD5 md5;

        //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
        md5 = new MD5CryptoServiceProvider();
        originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
        encodedBytes = md5.ComputeHash(originalBytes);

        //Convert encoded bytes back to a 'readable' string
        return BitConverter.ToString(encodedBytes);
    }
}
