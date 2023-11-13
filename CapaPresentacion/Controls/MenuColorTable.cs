using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CapaPresentacion.Controls
{
    public class MenuColorTable : ProfessionalColorTable
    {
        //fields
        private Color backColor;
        private Color leftColumnColor;
        private Color borderColor;
        private Color menuIteamBorderColor;
        private Color menuItemSelectedColor;

        public MenuColorTable(bool isMainMenu, Color primaryColor)
        {
            if (isMainMenu)
            {
                backColor = Color.FromArgb(37, 29, 60);
                leftColumnColor = Color.FromArgb(32, 33, 51);
                borderColor = Color.FromArgb(32, 33, 51);
                menuIteamBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;

            }
            else
            {
                backColor = Color.White;
                leftColumnColor = Color.LightGray;
                borderColor = Color.LightGray;
                menuIteamBorderColor = primaryColor;
                menuItemSelectedColor = primaryColor;
            }
        }

        public override Color ToolStripDropDownBackground
        {
            get
            {
                return backColor;
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return borderColor;
            }
        }

        public override Color MenuItemBorder
        {
            get
            {
                return menuIteamBorderColor;
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return menuItemSelectedColor;
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return leftColumnColor;
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return leftColumnColor;
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return leftColumnColor;
            }
        }


    }
}
