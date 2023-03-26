using FluorineFx.AMF3;
using MspLib.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MspLib.Autorization
{
    internal class ChecksumCalculator
    {
        public static string createChecksum(object[] param1)
        {
            SHA1 sHA1 = SHA1.Create();
            byte[] hashBytes = new byte[0];
            string loc2 = fromArray(param1);
            string loc3 = getEndData(param1);
            string loc4 = getLogData();
            hashBytes = Encoding.UTF8.GetBytes(loc2 + loc3 + loc4);
            hashBytes = sHA1.ComputeHash(hashBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        public static string generateID()
        {
            Random random = new Random();
            string loc1 = "";
            do
            {
                loc1 += random.Next(0, int.MaxValue).ToString("X");
            }
            while (loc1.Length < 48);

            loc1 = loc1.Substring(0, 46);
            return Convert.ToBase64String(Encoding.Default.GetBytes(loc1));

        }
        public static string fileNamePartFromId(int param1)
        {
            string _loc2_ = "" + ((int)(param1 / 1000000000)) + "_";
            _loc2_ += ((int)(param1 / 1000000) % 1000) + "_";
            _loc2_ += ((int)(param1 / 1000) % 1000) + "_";
            return _loc2_ + (param1 % 1000);
        }
        private static string getLogData()
        {
            return "$CuaS44qoi0Mp2qp";
        }
        private static string fromArray(object[] param1)
        {
            if (param1 == null)
            {
                return "";
            }
            string loc2 = "";
            foreach (object loc3 in param1)
            {
                loc2 += fromObjectInner(loc3);
            }
            return loc2;
        }

        private static string fromObject(object param1)
        {
            string[] loc3 = (from C in param1.GetType().GetProperties()
                             select C.Name).ToArray();
            Array.Sort(loc3);
            string loc2 = "";
            foreach (string loc4 in loc3)
            {
                loc2 += fromObjectInner(param1.GetType().GetProperty(loc4).GetValue(param1, null));
            }
            return loc2;
        }

        private static string fromObjectInner(object param1)
        {
            DateTime loc2;
            if (param1 == null || param1 is TicketHeader)
            {
                return "";
            }
            if (param1 is int || param1 is string)
            {
                return param1.ToString();
            }
            if (param1 is long)
            {
                return fromNumber((long)param1);
            }
            if (param1 is bool)
            {
                return (bool)param1 ? "True" : "False";
            }
            if (param1 is DateTime)
            {
                loc2 = (DateTime)param1;
                return loc2.Year.ToString() + loc2.AddMonths(-1).Month.ToString() + loc2.Day.ToString();
            }
            if (param1 is byte[])
            {
                return fromByteArray((byte[])param1);
            }
            if (param1 is ByteArray)
            {
                return fromAMFByteArray((ByteArray)param1);
            }
            if (param1 is object[])
            {
                return fromArray((object[])param1);
            }
            if (param1 is ArrayCollection)
            {
                return fromArrayCollection((ArrayCollection)param1);
            }
            if (param1 is object)
            {
                return fromObject(param1);
            }
            return "";
        }

        private static string fromNumber(long param1)
        {
            string loc2 = param1.ToString();
            int loc3 = loc2.IndexOf('.');
            if (loc3 >= 0 && loc2.Length > loc3 + 5)
            {
                return loc2.Substring(0, loc3 + 5);
            }
            return loc2;
        }

        private static string fromByteArray(byte[] param1)
        {
            int loc2 = 20;
            if (param1.Length <= loc2)
            {
                return BitConverter.ToString(param1).Replace("-", "").ToLower();
            }
            MemoryStream memoryStream = new MemoryStream(param1);
            MemoryStream loc3 = new MemoryStream();
            int loc4 = param1.Length / loc2;
            int loc5 = 0;
            while (loc5 < loc2)
            {
                memoryStream.Position = loc4 * loc5;
                loc3.WriteByte((byte)memoryStream.ReadByte());
                loc5++;
            }
            return BitConverter.ToString(loc3.ToArray()).Replace("-", "").ToLower();
        }
        private static string fromAMFByteArray(ByteArray param1)
        {
            int loc2 = 20;

            if (param1.Length <= loc2)
            {
                return BitConverter.ToString(param1.ToArray()).Replace("-", "").ToLower();
            }
            MemoryStream loc3 = new MemoryStream();
            int loc4 = (int)param1.Length / loc2;
            int loc5 = 0;
            while (loc5 < loc2)
            {
                param1.Position = (uint)(loc4 * loc5);
                loc3.WriteByte(param1.ReadByte());
                loc5++;
            }
            return BitConverter.ToString(loc3.ToArray()).Replace("-", "").ToLower();
        }
        private static string fromArrayCollection(ArrayCollection o)
        {
            string text = "";
            foreach (object item in o)
            {
                text += fromObjectInner(item);
            }
            return text;
        }

        private static string getEndData(object[] param1)
        {
            string loc3 = null;
            try
            {
                foreach (object loc2 in param1)
                {
                    if (loc2 is TicketHeader)
                    {
                        loc3 = ((TicketHeader)loc2).Ticket;
                        return loc3.Split(',').Last().Substring(0, 5);
                    }
                }
            }
            catch
            {
            }
            return "v1n3g4r";
        }
    }
}
