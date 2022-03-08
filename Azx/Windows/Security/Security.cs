using System;
using System.Collections.Generic;
using System.Text;

namespace Azx.Windows.Security
{
    /// <summary>
    /// Ali Azimzadeh 88/08/27  Version 1.0.0
    /// </summary>
    public class Security : Object
    {

        public Security() : base()
        {
        }

        protected byte[] bytBox = new byte[lngBoxLen];
        protected static long lngBoxLen = 0xffL;
        private string _encryptionKeyAscii = "";

        private string _cryptedText = "";
        protected string CryptedText
        {
            get
            {
                return (_cryptedText);
            }
            set
            {
                if (_cryptedText != value)
                {
                    _cryptedText = value;
                }
            }
        }

        private string _encriptionKey = "";
        protected string EncryptionKey
        {
            get
            {
                return (_encriptionKey);
            }
            set
            {
                if (_encriptionKey != value)
                {
                    _encriptionKey = value;
                    long num = 0L;
                    Encoding ascii = Encoding.ASCII;
                    Encoding unicode = Encoding.Unicode;
                    byte[] bytes = Encoding.Convert(unicode, ascii, unicode.GetBytes(this._encriptionKey));
                    char[] chars = new char[ascii.GetCharCount(bytes, 0, bytes.Length)];
                    ascii.GetChars(bytes, 0, bytes.Length, chars, 0);
                    this._encryptionKeyAscii = new string(chars);
                    long length = this._encriptionKey.Length;
                    for (long i = 0L; i < lngBoxLen; i += 1L)
                    {
                        this.bytBox[(int)((IntPtr)i)] = (byte)i;
                    }
                    for (long j = 0L; j < lngBoxLen; j += 1L)
                    {
                        num = (long)(((num + this.bytBox[(int)((IntPtr)j)]) + chars[(int)((IntPtr)(j % length))]) % ((long)lngBoxLen));
                        byte num5 = this.bytBox[(int)((IntPtr)j)];
                        this.bytBox[(int)((IntPtr)j)] = this.bytBox[(int)((IntPtr)num)];
                        this.bytBox[(int)((IntPtr)num)] = num5;
                    }
                }
            }
        }

        private string _encriptionText = "";
        protected string EncriptionText
        {
            get
            {
                return (_encriptionText);
            }
            set
            {
                if (_encriptionText != value)
                {
                    _encriptionText = value;
                }
            }
        }

        protected bool Decrypt()
        {
            bool flag = true;
            try
            {
                EncriptionText = CryptedText;
                CryptedText = "";
                if (flag = Encrypt())
                {
                    EncriptionText = CryptedText;
                }
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        protected bool Encrypt()
        {
            bool flag = true;
            try
            {
                long num = 0L;
                long num2 = 0L;
                Encoding encoding = Encoding.Default;
                byte[] bytes = encoding.GetBytes(EncriptionText);
                byte[] buffer2 = new byte[bytes.Length];
                byte[] array = new byte[lngBoxLen];
                bytBox.CopyTo(array, 0);
                int length = bytes.Length;
                for (long i = 0L; i < bytes.Length; i += 1L)
                {
                    num = (num + 1L) % lngBoxLen;
                    num2 = (long)((num2 + array[(int)((IntPtr)num)]) % ((long)lngBoxLen));
                    byte num4 = array[(int)((IntPtr)num)];
                    array[(int)((IntPtr)num)] = array[(int)((IntPtr)num2)];
                    array[(int)((IntPtr)num2)] = num4;
                    byte num5 = bytes[(int)((IntPtr)i)];
                    byte num6 = array[(int)((IntPtr)(((long)(array[(int)((IntPtr)num)] + array[(int)((IntPtr)num2)])) % lngBoxLen))];
                    buffer2[(int)((IntPtr)i)] = (byte)(num5 ^ num6);
                }
                char[] chars = new char[encoding.GetCharCount(buffer2, 0, buffer2.Length)];
                encoding.GetChars(buffer2, 0, buffer2.Length, chars, 0);
                CryptedText = new string(chars);
            }
            catch
            {
                flag = false;
            }
            return flag;
        }

        public string GetEncriptionCode(string encriptionText, string encriptionKey)
        {
            EncriptionText = encriptionText;
            EncryptionKey = encriptionKey;
            Encrypt();
            return (CryptedText);
        }

        public string GetDecriptionCode(string encriptedText, string encriptedKey)
        {
            CryptedText = encriptedText;
            EncryptionKey = encriptedKey;
            Decrypt();
            return (EncriptionText);
        }
    }
    ///// <summary>
    ///// Ali Azimzadeh 88/08/27  Version 1.0.0
    ///// </summary>
    //public  class Security
    //{
    //    private  byte[] bytBox = new byte[lngBoxLen];
    //    private  static long lngBoxLen = 0xffL;
    //    private  string _encryptionKeyAscii = "";

    //    //public Security()
    //    //{
    //    //}

    //    private  string _cryptedText = "";
    //    public  string CryptedText
    //    {
    //        get
    //        {
    //            return (_cryptedText);
    //        }
    //        set
    //        {
    //            if (_cryptedText != value)
    //            {
    //                _cryptedText = value;
    //            }
    //        }
    //    }

    //    private  string _encriptionKey = "";
    //    public  string EncryptionKey
    //    {
    //        get
    //        {
    //            return (_encriptionKey);
    //        }
    //        set
    //        {
    //            if (_encriptionKey != value)
    //            {
    //                _encriptionKey = value;
    //                long num = 0L;
    //                Encoding ascii = Encoding.ASCII;
    //                Encoding unicode = Encoding.Unicode;
    //                byte[] bytes = Encoding.Convert(unicode, ascii, unicode.GetBytes(_encriptionKey));
    //                char[] chars = new char[ascii.GetCharCount(bytes, 0, bytes.Length)];
    //                ascii.GetChars(bytes, 0, bytes.Length, chars, 0);
    //                _encryptionKeyAscii = new string(chars);
    //                long length = _encriptionKey.Length;
    //                for (long i = 0L; i < lngBoxLen; i += 1L)
    //                {
    //                    bytBox[(int)((IntPtr)i)] = (byte)i;
    //                }
    //                for (long j = 0L; j < lngBoxLen; j += 1L)
    //                {
    //                    num = (long)(((num + bytBox[(int)((IntPtr)j)]) + chars[(int)((IntPtr)(j % length))]) % ((long)lngBoxLen));
    //                    byte num5 = bytBox[(int)((IntPtr)j)];
    //                    bytBox[(int)((IntPtr)j)] = bytBox[(int)((IntPtr)num)];
    //                    bytBox[(int)((IntPtr)num)] = num5;
    //                }
    //            }
    //        }
    //    }

    //    private  string _encriptionText = "";
    //    public  string EncriptionText
    //    {
    //        get
    //        {
    //            return (_encriptionText);
    //        }
    //        set
    //        {
    //            if (_encriptionText != value)
    //            {
    //                _encriptionText = value;
    //            }
    //        }
    //    }

    //    public  bool Decrypt()
    //    {
    //        bool flag = true;
    //        try
    //        {
    //            EncriptionText = CryptedText;
    //            CryptedText = "";
    //            if (flag = Encrypt())
    //            {
    //                EncriptionText = CryptedText;
    //            }
    //        }
    //        catch
    //        {
    //            flag = false;
    //        }
    //        return flag;
    //    }

    //    public  bool Encrypt()
    //    {
    //        bool flag = true;
    //        try
    //        {
    //            long num = 0L;
    //            long num2 = 0L;
    //            Encoding encoding = Encoding.Default;
    //            byte[] bytes = encoding.GetBytes(EncriptionText);
    //            byte[] buffer2 = new byte[bytes.Length];
    //            byte[] array = new byte[lngBoxLen];
    //            bytBox.CopyTo(array, 0);
    //            int length = bytes.Length;
    //            for (long i = 0L; i < bytes.Length; i += 1L)
    //            {
    //                num = (num + 1L) % lngBoxLen;
    //                num2 = (long)((num2 + array[(int)((IntPtr)num)]) % ((long)lngBoxLen));
    //                byte num4 = array[(int)((IntPtr)num)];
    //                array[(int)((IntPtr)num)] = array[(int)((IntPtr)num2)];
    //                array[(int)((IntPtr)num2)] = num4;
    //                byte num5 = bytes[(int)((IntPtr)i)];
    //                byte num6 = array[(int)((IntPtr)(((long)(array[(int)((IntPtr)num)] + array[(int)((IntPtr)num2)])) % lngBoxLen))];
    //                buffer2[(int)((IntPtr)i)] = (byte)(num5 ^ num6);
    //            }
    //            char[] chars = new char[encoding.GetCharCount(buffer2, 0, buffer2.Length)];
    //            encoding.GetChars(buffer2, 0, buffer2.Length, chars, 0);
    //            CryptedText = new string(chars);
    //        }
    //        catch
    //        {
    //            flag = false;
    //        }
    //        return flag;
    //    }
    //}
}
