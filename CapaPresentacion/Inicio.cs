using CapaPresentacion.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(dropdownMenu1, sender);
        }
        private void btnVenta_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(dropdownMenu2, sender);

        }
        private void btnIngreso_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(dropdownMenu3, sender);
        }

        private void btnGeneraReportes_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(dropdownMenu4, sender);
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(dropdownMenu5, sender);
        }

        private void Open_DropDownMenu(DropdownMenu dropdownMenu, object sender)
        {
            Control control = (Control)sender;
            dropdownMenu.VisibleChanged += new EventHandler((sender2, ev) => DropdownMenu_VisibleChanged(sender2, ev, control));
            dropdownMenu.Show(control, control.Width, 0);
        }

        private void Open_DropDownMenu2(DropdownMenu dropdownMenu, object sender)
        {
            Control control = (Control)sender;
            dropdownMenu.VisibleChanged += new EventHandler((sender2, ev) => DropdownMenu_VisibleChanged(sender2, ev, control));
            dropdownMenu.Show(control, control.Width - dropdownMenu.Width, control.Height);
        }
        
        private void DropdownMenu_VisibleChanged(object sender, EventArgs e, Control ctrl)
        {
            DropdownMenu dropdownMenu = (DropdownMenu)sender;
            if (!DesignMode)
            {
                if (dropdownMenu.Visible)
                    ctrl.BackColor = Color.FromArgb(252, 163, 17);
                else ctrl.BackColor = Color.FromArgb(240, 240, 240);
            }
        }

    }
}
