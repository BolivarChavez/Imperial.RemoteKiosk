using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RemoteKiosk.Modelos
{
    public class Cifrado
    {
        private static Rfc2898DeriveBytes _Rfc2898DeriveBytes;
        private static Byte[] _Byte;

        public string Encriptar(string Valor)
        {
            try
            {
                string _Key = ConfigurationManager.AppSettings["Llave_cifrado"];
                using (RijndaelManaged _RijndaelManaged = new RijndaelManaged())
                {
                    _Rfc2898DeriveBytes = new Rfc2898DeriveBytes(_Key, new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
                    _RijndaelManaged.Key = _Rfc2898DeriveBytes.GetBytes(_RijndaelManaged.Key.Length);
                    _RijndaelManaged.IV = _Rfc2898DeriveBytes.GetBytes(_RijndaelManaged.IV.Length);
                    MemoryStream _MemoryStream = new MemoryStream();
                    CryptoStream _CryptoStream = new CryptoStream(_MemoryStream, _RijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write);
                    _Byte = Encoding.UTF8.GetBytes(Valor);
                    _CryptoStream.Write(_Byte, 0, _Byte.Length);
                    _CryptoStream.Close();
                    return Convert.ToBase64String(_MemoryStream.ToArray());
                }
            }
            catch { return string.Empty; }
        }

        public string DesEncriptar(string Valor)
        {
            try
            {
                string _Key = ConfigurationManager.AppSettings["Llave_cifrado"];
                using (RijndaelManaged _RijndaelManaged = new RijndaelManaged())
                {
                    _Rfc2898DeriveBytes = new Rfc2898DeriveBytes(_Key, new Byte[] { 1, 2, 3, 4, 5, 6, 7, 8 });
                    _RijndaelManaged.Key = _Rfc2898DeriveBytes.GetBytes(_RijndaelManaged.Key.Length);
                    _RijndaelManaged.IV = _Rfc2898DeriveBytes.GetBytes(_RijndaelManaged.IV.Length);
                    MemoryStream _MemoryStream = new MemoryStream();
                    CryptoStream _CryptoStream = new CryptoStream(_MemoryStream, _RijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write);
                    _Byte = Convert.FromBase64String(Valor);
                    _CryptoStream.Write(_Byte, 0, _Byte.Length);
                    _CryptoStream.Close();
                    return Encoding.UTF8.GetString(_MemoryStream.ToArray());
                }
            }
            catch { return string.Empty; }
        }

    }
}