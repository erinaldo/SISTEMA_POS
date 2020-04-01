using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Palatium.Clases
{
    class ClaseRedimension
    {
        float f_HeightRatio = new float();
        float f_WidthRatio = new float();

        public void ResizeForm(Form ObjForm, int DesignerWidth, int DesignerHeight)
        {
            int i_StandardHeight = DesignerHeight;
            int i_StandardWidth = DesignerWidth;
            int i_PresentHeight = Screen.PrimaryScreen.Bounds.Height;
            int i_PresentWidth = Screen.PrimaryScreen.Bounds.Width;
            f_HeightRatio = (Convert.ToSingle(i_PresentHeight) / Convert.ToSingle(i_StandardHeight));
            f_WidthRatio = (Convert.ToSingle(i_PresentWidth) / Convert.ToSingle(i_StandardWidth));
            ObjForm.AutoScaleMode = AutoScaleMode.None;
            ObjForm.Scale(new SizeF(f_WidthRatio, f_HeightRatio));
            foreach (Control c in ObjForm.Controls)
            {
                if (c.HasChildren)
                {
                    ResizeControlStore(c);
                }
                else
                {
                    c.Font = new Font(c.Font.FontFamily, c.Font.Size * f_HeightRatio, c.Font.Style, c.Font.Unit, (Convert.ToByte(0)));
                }
            }
            ObjForm.Font = new Font(ObjForm.Font.FontFamily, ObjForm.Font.Size * f_HeightRatio, ObjForm.Font.Style, ObjForm.Font.Unit, (Convert.ToByte(0)));
        }

        private void ResizeControlStore(Control objCtl)
        {
            if (objCtl.HasChildren)
            {
                foreach (Control cChildren in objCtl.Controls)
                {
                    if (cChildren.HasChildren)
                    {
                        ResizeControlStore(cChildren);
                    }
                    else
                    {
                        cChildren.Font = new Font(cChildren.Font.FontFamily, cChildren.Font.Size * f_HeightRatio, cChildren.Font.Style, cChildren.Font.Unit, (Convert.ToByte(0)));
                    }
                }
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, (Convert.ToByte(0)));
            }
            else
            {
                objCtl.Font = new Font(objCtl.Font.FontFamily, objCtl.Font.Size * f_HeightRatio, objCtl.Font.Style, objCtl.Font.Unit, (Convert.ToByte(0)));
            }
        }

        public void extraerPixelado()
        {
            //Program.iLargoPantalla = Screen.PrimaryScreen.Bounds.Width;
            //Program.iAnchoPantalla = Screen.PrimaryScreen.Bounds.Height;

            Program.iLargoPantalla = 1366;
            Program.iAnchoPantalla = 768;
        }
    }
}
