using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Modales;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static Usuario usuarioActual;
        private static Button menuActivo = null;
        private static Form formularioActivo = null;
        private ToolTip toolTip1;

        public Inicio(Usuario objUsuario = null)
        {
            if (objUsuario == null)
            {
                usuarioActual = new Usuario()
                {
                    oDatosPersona = new Datos_Persona { Nombre = "ADMIN" },
                    IdUsuario = 2
                };
            }
            else
            {
                usuarioActual = objUsuario;
            }


            InitializeComponent();

            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btnBack, "Volver a la pantalla de login");
        }

        //Dropdown menu
        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(ddmAdministracion, sender);
        }
        private void btnVenta_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(ddmVenta, sender);

        }
        private void btnIngreso_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(ddmCompra, sender);
        }

        private void btnGeneraReportes_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(ddmReportes, sender);
        }

        private void btnDevolucion_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(ddmDevoluciones, sender);
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

        public Image byteToImagege(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageByte, 0, imageByte.Length);
            Image image = new Bitmap(ms);

            return image;
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = new CN_OtrosDatos().obtenerLogo(out obtenido);

            if (obtenido)
                picLogo.Image = byteToImagege(byteimage);

            List<Permiso> listaPermisos = new CN_Permiso().Listar(usuarioActual.IdUsuario);
            foreach (Button botonMenu in menu.Controls.OfType<Button>())
            {
                bool encontrado = listaPermisos.Any(m => m.NombreMenu == botonMenu.Name);
                if (!encontrado)
                {
                    botonMenu.Visible = false;
                }
            }

            lblUsuario.Text = usuarioActual.oDatosPersona.Nombre + " " + usuarioActual.oDatosPersona.Apellido;

        }

        private void AbrirFormulario(Button menu, Form formulario)
        {
            if (menuActivo != null)
            {
                menuActivo.BackColor = Color.FromArgb(240, 240, 240);
            }

            menu.BackColor = Color.FromArgb(252, 163, 17);
            menuActivo = menu;

            if (formularioActivo != null)
            {
                formularioActivo.Close();
            }

            formularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.WhiteSmoke;
            contenedor.Controls.Add(formulario);
            formulario.Show();



        }
        //menu Usuario
        private void menuUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((Button)sender, new FrmUsuario());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);

        }
        //menu Administracion 
        private void SubMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuadministracion, new FrmProducto());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void SubMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuadministracion, new FrmNegocio());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }
        //menu venta
        private void SubMenuRegVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmVentaRegistro(usuarioActual));
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }



        //menu Ingreso
        private void SubMenuRegIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmCompra(usuarioActual));
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void SubMenuDetIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmDetalleCompra());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void SubMenuAbonoIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmAbonoCompra());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }
        //menu Cliente
        private void menuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuclientes, new FrmCliente());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }
        //menu proveedor
        private void menuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuproveedores, new FrmProveedor());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }
        //menu Genera Reportes
        private void SubMenuRepVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new FrmReporteVentas());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void SubMenuRepIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new FrmReportesCompra());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void menuServicioApartado_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuapartado, new FrmApartado());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void devolverVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menudevolución, new FrmDevolucionVenta());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void SubMenuDevlProv_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menudevolución, new FrmDevolucionCompra());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea salir del sistema?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void panelGerencialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new frmPanelGestion());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void menuabono_Click(object sender, EventArgs e)
        {
            Open_DropDownMenu(ddmAbono, sender);
        }

        private void abonoVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuabono, new FrmAbonoVenta());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void abonoCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuabono, new FrmAbonoCompra());
            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void SubMenuDetVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmDetalleVenta());

            menuacercade.ForeColor = Color.FromArgb(252, 163, 17);
        }

        private void menuacercade_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuacercade, new FrmAcercaDe());

            menuacercade.ForeColor = Color.Black;
        }
    }
    
}
