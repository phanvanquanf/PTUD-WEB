using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace aznews.Utilities
{
    public class Functions
    {
        public static int _MaNguoiDung = 0;
        public static string _TenNguoiDung = string.Empty;
        public static string _TenDangNhap = string.Empty;
        public static string _Email = string.Empty;
        public static string _Message = string.Empty;

        public static string TitleSlugGeneration(string type, string? title, long id)
        {
            return type + "-" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString() + ".vn";
        }

        public static string getCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string MD5Hash(string text)
        {
            // Kiểm tra null hoặc chuỗi rỗng
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text), "Input cannot be null or empty.");

            // Sử dụng MD5.Create() thay vì MD5CryptoServiceProvider
            using (MD5 md5 = MD5.Create())  // Thay vì MD5CryptoServiceProvider
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(text);  // Chuyển chuỗi thành byte array
                byte[] hashBytes = md5.ComputeHash(inputBytes);  // Băm dữ liệu

                // Chuyển kết quả băm thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));  // Định dạng dưới dạng hexadecimal
                }

                return sb.ToString();  // Trả về chuỗi kết quả băm
            }
        }

        public static string MD5Password(string? text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            string str = MD5Hash(text);  // Băm lần đầu
            for (int i = 0; i <= 5; i++)
            {
                str = MD5Hash(str + str);  // Băm kết quả trước đó 6 lần
            }

            return str;
        }

        public static bool IsLogin()
        {
            if ((Functions._MaNguoiDung <= 0) || string.IsNullOrEmpty(Functions._TenDangNhap) || string.IsNullOrEmpty(Functions._Email))
                return false;
            return true;
        }
    }
}