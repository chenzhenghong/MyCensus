using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace MyCensus.Helpers
{
    /// <summary>
    /// ������������
    /// </summary>
    public partial class CommonHelper
    {

        public static string ConvetToSeconds(int duration)
        {
            TimeSpan ts = new TimeSpan(0, 0, Convert.ToInt32(duration));
            string str = "";
            if (ts.Hours > 0)
            {
                str = ts.Hours.ToString() + "ʱ" + ts.Minutes.ToString() + "��" + ts.Seconds + "��";
            }
            if (ts.Hours == 0 && ts.Minutes > 0)
            {
                str = ts.Minutes.ToString() + "��" + ts.Seconds + "��";
            }
            if (ts.Hours == 0 && ts.Minutes == 0)
            {
                str = ts.Seconds + "��";
            }
            return str;
        }

        /// <summary>
        /// 166��189��199 ����
        /// </summary>
        /// <param name="str_handset"></param>
        /// <param name="stand"></param>
        /// <returns></returns>
        public static bool IsPhoneNo(string str_handset, bool stand)
        {
            if (stand)
                return Regex.IsMatch(str_handset, "^(0\\d{2,3}\\d{7,8}(\\d{3,5}){0,1})|(((19[0-9])|(18[0-9])|(16[0-9])|(13[0-9])|(15([0-3]|[5-9]))|(18[0-9])|(17[0-9])|(14[0-9]))\\d{8})$");
            else
                return Regex.IsMatch(str_handset, "^(0\\d{2,3}-?\\d{7,8}(-\\d{3,5}){0,1})|(((19[0-9])|(18[0-9])|(16[0-9])|(13[0-9])|(15([0-3]|[5-9]))|(18[0-9])|(17[0-9])|(14[0-9]))\\d{8})$");
        }

        /// <summary>
        /// �� Stream ת�� byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // ���õ�ǰ����λ��Ϊ���Ŀ�ʼ 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// �� byte[] ת�� Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }


        public static string EnsureSubscriberEmailOrThrow(string email) {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if(!IsValidEmail(output)) {
                throw new Exception("Email is not valid.");
            }

            return output;
        }

        /// <summary>
        /// ��֤һ���ַ�������Ч��e-mail��ʽ
        /// </summary>
        /// <param name="email">Email</param>
        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
                return false;

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// ��֤һ���ַ�������Ч���ֻ������ʽ
        /// </summary>
        /// <param name="email">mobileNumber</param>
        /// <returns></returns>
        public static bool IsValidMobileNumber(string mobileNumber)
        {
            if (String.IsNullOrEmpty(mobileNumber))
                return false;

            mobileNumber = mobileNumber.Trim();
            var result = Regex.IsMatch(mobileNumber, "^0?(13|15|18)[0-9]{9}$", RegexOptions.IgnoreCase);

            return result;
        }

        /// <summary>
        /// ������ɵ����ִ���
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>Result string</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }


        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="length">���ɳ���</param>
        /// <returns></returns>
        public static string GenerateNumber(int Length)
        {
            return Number(Length, false);
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Number(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < Length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }

        /// <summary>
        /// ���������ĸ������
        /// </summary>
        /// <param name="IntStr">���ɳ���</param>
        /// <returns></returns>
        public static string GenerateStr(int Length)
        {
            return GenerateStr(Length, false);
        }
        /// <summary>
        /// ���������ĸ������
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string GenerateStr(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }


        /// <summary>
        /// �����������ĸ�����
        /// </summary>
        /// <param name="IntStr">���ɳ���</param>
        /// <returns></returns>
        public static string GenerateStrchar(int Length)
        {
            return Str_char(Length, false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static string GenerateRegmutouSoft()
        {
            string tempstr = "";
            int[] numArray = new int[16];
            for (int m = 0; m < 16; m++)
            {
                if ((m > 0) && ((m % 4) == 0))
                {
                    tempstr = tempstr + "-";
                }
                tempstr = tempstr + "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"[GenerateRandomInteger(0,36)];
            }
            return tempstr;
        }

        /// <summary>
        /// �����������ĸ�����
        /// </summary>
        /// <param name="Length">���ɳ���</param>
        /// <param name="Sleep">�Ƿ�Ҫ������ǰ����ǰ�߳���ֹ�Ա����ظ�</param>
        /// <returns></returns>
        public static string Str_char(int Length, bool Sleep)
        {
            if (Sleep)
                System.Threading.Thread.Sleep(3);
            char[] Pattern = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Length; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];
            }
            return result;
        }


        /// <summary>
        /// ����һ�������������������ָ���ķ�Χ
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = 2147483647)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// ȷ��һ���ַ����������������󳤶�
        /// </summary>
        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
                return str;

            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// ȷ��һ���ַ�����С����󳤶�
        /// </summary>
        public static bool BetweenLength(string str, int minLength, int maxLength)
        {
            if (String.IsNullOrEmpty(str))
                return false;
            return (str.Length >= minLength && str.Length <= maxLength);
        }

        /// <summary>
        /// ȷ��һ���ַ���Ϊ�û�����ʽ
        /// </summary>
        public static bool UserNameFormat(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;
            str = str.Trim();
            var result = Regex.IsMatch(str, "^[A-Za-z0-9_\\-\\u4e00-\\u9fa5]+$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// ȷ��һ���ַ���ֻ��������
        /// </summary>
        public static bool FullNumber(string str)
        {
            if (String.IsNullOrEmpty(str))
                return false;
            str = str.Trim();
            var result = Regex.IsMatch(str, "^[0-9]+$", RegexOptions.IgnoreCase);
            return result;
        }

        /// <summary>
        /// ȷ��һ���ַ���ֻ������ֵ
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Input string with only numeric values, empty string if input is null/empty</returns>
        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
                return string.Empty;

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                    result.Append(c);
            }
            return result.ToString();
        }

        /// <summary>
        /// ȷ��һ���ַ�����Ϊnull
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Result</returns>
        public static string EnsureNotNull(string str)
        {
            if (str == null)
                return string.Empty;

            return str;
        }

        /// <summary>
        /// ��ʾָ�����ַ����Ƿ���null����ַ���
        /// </summary>
        /// <param name="stringsToValidate">Array of strings to validate</param>
        /// <returns>Boolean</returns>
        public static bool AreNullOrEmpty(params string[] stringsToValidate) {
            bool result = false;
            Array.ForEach(stringsToValidate, str => {
                if (string.IsNullOrEmpty(str)) result = true;
            });
            return result;
        }




        /// Convert enum for front-end
        /// </summary>
        /// <param name="str">Input string</param>
        /// <returns>Converted string</returns>
        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
                if (c.ToString() != c.ToString().ToLower())
                    result += " " + c.ToString();
                else
                    result += c.ToString();
            return result;
        }

        /// <summary>  
        /// GMTʱ��ת�ɱ���ʱ��  
        /// </summary>  
        /// <param name="gmt">�ַ�����ʽ��GMTʱ��</param>  
        /// <returns></returns>  
        public static DateTime GMT2Local(string gmt)
        {
            DateTime dt = DateTime.MinValue;
            try
            {
                string pattern = "";
                if (gmt.IndexOf("+0") != -1)
                {
                    gmt = gmt.Replace("GMT", "");
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss zzz";
                }
                if (gmt.ToUpper().IndexOf("GMT") != -1)
                {
                    pattern = "ddd, dd MMM yyyy HH':'mm':'ss 'GMT'";
                }
                if (pattern != "")
                {
                    dt = DateTime.ParseExact(gmt, pattern, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.AdjustToUniversal);
                    dt = dt.ToLocalTime();
                }
                else
                {
                    dt = Convert.ToDateTime(gmt);
                }
            }
            catch
            {
            }
            return dt;
        }


        /// <summary>
        /// Html��ʽ��JS
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string HtmlToJs(string source)
        {
            return String.Format("{0}", String.Join("", source.Replace("\\", "\\\\")
            .Replace("/", "\\/")
            .Replace("'", "\\'")
            .Replace("\"", "\\\"")
            .Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            ));
        }

        public static string UnicodeToGB(string text)
        {
            System.Text.RegularExpressions.MatchCollection mc = System.Text.RegularExpressions.Regex.Matches(text, "\\\\u([\\w]{4})");
            if (mc != null && mc.Count > 0)
            {
                foreach (System.Text.RegularExpressions.Match m2 in mc)
                {
                    string v = m2.Value;
                    string word = v.Substring(2);
                    byte[] codes = new byte[2];
                    int code = Convert.ToInt32(word.Substring(0, 2), 16);
                    int code2 = Convert.ToInt32(word.Substring(2), 16);
                    codes[0] = (byte)code2;
                    codes[1] = (byte)code;
                    text = text.Replace(v, Encoding.Unicode.GetString(codes)).Replace("\\\\\\", "\\").Replace("\\\\", "\\");

                }
            }
            return (text.Replace("\\r\\n", "")).Replace("\\r", "");
        }


        /// <summary>
        /// �����ַ����е�html����
        /// </summary>
        /// <param name="Str"></param>
        /// <returns>���ع���֮����ַ���</returns>
        public static string LostHTML(string Str)
        {
            string Re_Str = "";
            if (Str != null)
            {
                if (Str != string.Empty)
                {
                    string Pattern = "<\\/*[^<>]*>";
                    Re_Str = Regex.Replace(Str, Pattern, "");
                }
            }
            return (Re_Str.Replace("\\r\\n", "")).Replace("\\r", "");
        }


        public static string Filter(string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                //throw new Exception("�ַ����к��зǷ��ַ�!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }


        public static string FilterHTML(string html)
        {
            if (html == null)
                return "";
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\S]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\S]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex6 = new System.Text.RegularExpressions.Regex(@"\<img[^\>]+\>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex7 = new System.Text.RegularExpressions.Regex(@"</p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex8 = new System.Text.RegularExpressions.Regex(@"<p>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex9 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //����<script></script>���
            html = regex2.Replace(html, ""); //����href=javascript: (<A>) ����
            html = regex3.Replace(html, " _disibledevent="); //���������ؼ���on...�¼�
            html = regex4.Replace(html, ""); //����iframe
            html = regex5.Replace(html, ""); //����frameset
            html = regex6.Replace(html, ""); //����frameset
            html = regex7.Replace(html, ""); //����frameset
            html = regex8.Replace(html, ""); //����frameset
            html = regex9.Replace(html, "");
            html = html.Replace(" ", "");
            html = html.Replace("</strong>", "");
            html = html.Replace("<strong>", "");
            return html;
        }



        public static string Lost(string chr)
        {
            if (chr == null || chr == string.Empty)
            {
                return "";
            }
            else
            {
                chr = chr.Remove(chr.LastIndexOf(","));
                return chr;
            }
        }


        public static bool IsGuidByReg(string strSrc)
        {
            Regex reg = new Regex("^[A-F0-9]{8}(-[A-F0-9]{4}){3}-[A-F0-9]{12}$", RegexOptions.Compiled);
            return reg.IsMatch(strSrc);
        }

        public static void WriteFiles(string input, string fpath, Encoding encoding)
        {
            //using (FileStream fs = new FileStream(fname, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            //������
            using (FileStream fs = new FileStream(fpath, FileMode.Create, FileAccess.Write))
            {
                if (encoding == null)
                    throw new ArgumentNullException("encoding");
                ///�������洴�����ļ�������д������
                StreamWriter w = new StreamWriter(fs);
                ///����д����������ʼλ��Ϊ�ļ�����ĩβ
                w.BaseStream.Seek(0, SeekOrigin.End);
                w.Write(input);
                ///��ջ��������ݣ����ѻ���������д�������
                w.Flush();
                ///�ر�д������
                w.Close();
                //
                fs.Close();
            }
        }


        public static void WriteLog(string input, string fn)
        {
            ///ָ����־�ļ���Ŀ¼
            string logPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\log\\";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            if (string.IsNullOrEmpty(fn))
                fn = "RS";
            string fname = logPath + "" + fn + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            ///�����ļ���Ϣ����
            //FileInfo finfo = new FileInfo(fname);
            ///����ֻд�ļ���
            using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                ///�������洴�����ļ�������д������
                StreamWriter w = new StreamWriter(fs, Encoding.UTF8);
                ///����д����������ʼλ��Ϊ�ļ�����ĩβ
                w.BaseStream.Seek(0, SeekOrigin.End);
                //w.Write("��" + fn + "��");
                ///д�뵱ǰϵͳʱ�䲢����
                //w.Write("{0} {1} \r\n", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString());
                ///д��------------------------------------��������
                ///д����־���ݲ�����
                w.Write(input + "\r\n");
                ///��ջ��������ݣ����ѻ���������д�������
                w.Flush();
                ///�ر�д������
                w.Close();

                fs.Close();
            }
        }

        /// <summary>
        /// ��ȡʱ���
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time, int length = 13)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString().Substring(0, length);
        }
        /// <summary>  
        /// ��c# DateTimeʱ���ʽת��ΪUnixʱ�����ʽ  
        /// </summary>  
        /// <param name="time">ʱ��</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTimeToUtc(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //��10000����Ϊ13λ      
            return t;
        }
        /// <summary>        
        /// ʱ���תΪC#��ʽʱ��        
        /// </summary>        
        /// <param name=��timeStamp��></param>        
        /// <returns></returns>        
        public static DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZoneInfo.ConvertTimeToUtc(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// ʱ���תΪC#��ʽʱ��10λ
        /// </summary>
        /// <param name="timeStamp">Unixʱ�����ʽ</param>
        /// <returns>C#��ʽʱ��</returns>
        public static DateTime GetDateTimeFrom1970Ticks(long curSeconds)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return dtStart.AddSeconds(curSeconds);
        }

        /// <summary>
        /// ��֤ʱ���
        /// </summary>
        /// <param name="time"></param>
        /// <param name="interval">��ֵ�����ӣ�</param>
        /// <returns></returns>
        public static bool IsTime(long time, double interval)
        {
            DateTime dt = GetDateTimeFrom1970Ticks(time);
            //ȡ����ʱ��
            DateTime dt1 = DateTime.Now.AddMinutes(interval);
            DateTime dt2 = DateTime.Now.AddMinutes(interval * -1);
            if (dt > dt2 && dt < dt1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �ж�ʱ����Ƿ���ȷ����֤ǰ8λ��
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool IsTime(string time)
        {
            string str = GetTimeStamp(DateTime.Now, 8);
            if (str.Equals(time))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
