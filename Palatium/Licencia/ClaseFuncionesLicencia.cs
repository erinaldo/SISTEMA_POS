using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Palatium.Licencia
{
    class ClaseFuncionesLicencia
    {
        public static bool UseProcessorID = true;
        public static bool UseBaseBoardProduct = true;
        public static bool UseBaseBoardManufacturer = false;
        public static bool UseDiskDriveSignature = false;
        public static bool UseVideoControllerCaption = false;
        public static bool UsePhysicalMediaSerialNumber = true;
        public static bool UseBiosVersion = true;
        public static bool UseBiosManufacturer = false;
        public static bool UseWindowsSerialNumber = false;

        public string sId;
        public string sPass;

        public string GetSystemInfo(string SoftwareName)
        {
            if (UseProcessorID == true)
                SoftwareName += RunQuery("Processor", "ProcessorId");

            if (UseBaseBoardProduct == true)
                SoftwareName += RunQuery("BaseBoard", "Product");

            if (UseBaseBoardManufacturer == true)
                SoftwareName += RunQuery("BaseBoard", "Manufacturer");

            if (UseDiskDriveSignature == true)
                SoftwareName += RunQuery("DiskDrive", "Signature");

            if (UseVideoControllerCaption == true)
                SoftwareName += RunQuery("VideoController", "Caption");

            if (UsePhysicalMediaSerialNumber == true)
                SoftwareName += RunQuery("PhysicalMedia", "SerialNumber");

            if (UseBiosVersion == true)
                SoftwareName += RunQuery("BIOS", "Version");

            if (UseWindowsSerialNumber == true)
                SoftwareName += RunQuery("OperatingSystem", "SerialNumber");

            SoftwareName = RemoveUseLess(SoftwareName);

            if (SoftwareName.Length < 25)
                return GetSystemInfo(SoftwareName);

            string sAyuda = SoftwareName.Substring(0, 25).ToUpper();

            SoftwareName = InverseByBase(sAyuda, 10);

            sId = Boring(SoftwareName);

            sPass = MakePassword(sId, "958");

            return "OK";
        }

        private static string RunQuery(string TableName, string MethodName)
        {
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * from Win32_" + TableName);
            foreach (ManagementObject MO in MOS.Get())
            {
                try
                {
                    return MO[MethodName].ToString();
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
            return "";
        }

        private static string RemoveUseLess(string st)
        {
            char ch;
            for (int i = st.Length - 1; i >= 0; i--)
            {
                ch = char.ToUpper(st[i]);

                if ((ch < 'A' || ch > 'Z') &&
                    (ch < '0' || ch > '9'))
                {
                    st = st.Remove(i, 1);
                }
            }
            return st;
        }

        static public string InverseByBase(string st, int MoveBase)
        {
            StringBuilder SB = new StringBuilder();
            //st = ConvertToLetterDigit(st);
            int c;
            for (int i = 0; i < st.Length; i += MoveBase)
            {
                if (i + MoveBase > st.Length - 1)
                    c = st.Length - i;
                else
                    c = MoveBase;
                SB.Append(InverseString(st.Substring(i, c)));
            }
            return SB.ToString();
        }

        static public string InverseString(string st)
        {
            StringBuilder SB = new StringBuilder();
            for (int i = st.Length - 1; i >= 0; i--)
            {
                SB.Append(st[i]);
            }
            return SB.ToString();
        }

        static public string Boring(string st)
        {
            int NewPlace;
            char ch;
            for (int i = 0; i < st.Length; i++)
            {
                NewPlace = i * Convert.ToUInt16(st[i]);
                NewPlace = NewPlace % st.Length;
                ch = st[i];
                st = st.Remove(i, 1);
                st = st.Insert(NewPlace, ch.ToString());
            }
            return st;
        }

        static public string MakePassword(string st, string Identifier)
        {
            if (Identifier.Length != 3)
                throw new ArgumentException("Identifier must be 3 character length");

            int[] num = new int[3];
            num[0] = Convert.ToInt32(Identifier[0].ToString(), 10);
            num[1] = Convert.ToInt32(Identifier[1].ToString(), 10);
            num[2] = Convert.ToInt32(Identifier[2].ToString(), 10);
            st = Boring(st);
            st = InverseByBase(st, num[0]);
            st = InverseByBase(st, num[1]);
            st = InverseByBase(st, num[2]);

            StringBuilder SB = new StringBuilder();
            foreach (char ch in st)
            {
                SB.Append(ChangeChar(ch, num));
            }
            return SB.ToString();
        }

        static private char ChangeChar(char ch, int[] EnCode)
        {
            ch = char.ToUpper(ch);
            if (ch >= 'A' && ch <= 'H')
                return Convert.ToChar(Convert.ToInt16(ch) + 2 * EnCode[0]);
            else if (ch >= 'I' && ch <= 'P')
                return Convert.ToChar(Convert.ToInt16(ch) - EnCode[2]);
            else if (ch >= 'Q' && ch <= 'Z')
                return Convert.ToChar(Convert.ToInt16(ch) - EnCode[1]);
            else if (ch >= '0' && ch <= '4')
                return Convert.ToChar(Convert.ToInt16(ch) + 5);
            else if (ch >= '5' && ch <= '9')
                return Convert.ToChar(Convert.ToInt16(ch) - 5);
            else
                return '0';
        }
    }
}
