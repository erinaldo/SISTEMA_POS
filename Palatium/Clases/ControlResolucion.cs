using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Palatium.Clases
{
    class ControlResolucion
    {
        [DllImport("user32.dll")]
        private static extern bool EnumDisplaySettings([MarshalAs(UnmanagedType.LPTStr)] string lpszDeviceName, Int32 iModeNum, ref DEVMODE lpDevMode
        );

        [DllImport("user32.dll")]
        private static extern Int32 ChangeDisplaySettings(ref DEVMODE lpDevMode, Int32 dwFlags
        );

        private const Int32 DM_BITSPERPEL = 0x40000;
        private const Int32 DM_PELSWIDTH = 0x80000;
        private const Int32 DM_PELSHEIGHT = 0x100000;

        private const Int32 DISP_CHANGE_SUCCESSFUL = 0;

        [StructLayout(LayoutKind.Sequential)]
        private struct POINTL
        {
            public Int32 x;
            public Int32 y;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct DEVMODE_union1
        {
            // struct {
            [FieldOffset(0)]
            public Int16 dmOrientation;
            [FieldOffset(2)]
            public Int16 dmPaperSize;
            [FieldOffset(4)]
            public Int16 dmPaperLength;
            [FieldOffset(6)]
            public Int16 dmPaperWidth;
            // }
            [FieldOffset(0)]
            public POINTL dmPosition;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct DEVMODE_union2
        {
            [FieldOffset(0)]
            public Int32 dmDisplayFlags;
            [FieldOffset(0)]
            public Int32 dmNup;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct DEVMODE
        {
            private const Int32 CCDEVICENAME = 32;
            private const Int32 CCFORMNAME = 32;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCDEVICENAME)]
            public string dmDeviceName;
            public Int16 dmSpecVersion;
            public Int16 dmDriverVersion;
            public Int16 dmSize;
            public Int16 dmDriverExtra;
            public Int32 dmFields;
            public DEVMODE_union1 u1;
            public Int16 dmScale;
            public Int16 dmCopies;
            public Int16 dmDefaultSource;
            public Int16 dmPrintQuality;
            public Int16 dmColor;
            public Int16 dmDuplex;
            public Int16 dmYResolution;
            public Int16 dmTTOption;
            public Int16 dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCFORMNAME)]
            public string dmFormName;
            public Int16 dmUnusedPadding;
            public Int16 dmBitsPerPel;
            public Int32 dmPelsWidth;
            public Int32 dmPelsHeight;
            public DEVMODE_union2 u2;
            public Int32 dmDisplayFrequency;
            public Int32 dmICMMethod;
            public Int32 dmICMIntent;
            public Int32 dmMediaType;
            public Int32 dmDitherType;
            public Int32 dmReserved1;
            public Int32 dmReserved2;
            public Int32 dmPanningWidth;
            public Int32 dmPanningHeight;
        }

        DEVMODE dm;
        
        public bool SetResolution(Int32 Width, Int32 Height, Int16 BitsPerPixel)
        {
            

            if (!EnumDisplaySettings(null, 0, ref dm))
            {
                return false;
            }

            else
            {
                {
                    var withBlock = dm;
                    withBlock.dmFields = DM_PELSWIDTH | DM_PELSHEIGHT | DM_BITSPERPEL;
                    withBlock.dmPelsWidth = Width;
                    withBlock.dmPelsHeight = Height;
                    withBlock.dmBitsPerPel = BitsPerPixel;
                }
                return (ChangeDisplaySettings(ref dm, 0) == DISP_CHANGE_SUCCESSFUL);
            }
        }
    }
}
