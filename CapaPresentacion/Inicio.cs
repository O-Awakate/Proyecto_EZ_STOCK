using CapaEntidad;
using CapaNegocio;
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
        private static Usuario usuarioActual;
        private static Button menuActivo = null;
        private static Form formularioActivo = null;

        public Inicio(Usuario objUsuario = null)
        {
            if (objUsuario == null)
            {
                usuarioActual = new Usuario()
                {
                    
                    oDatosPersona = new Datos_Persona { Nombre = "ADMIN" },IdUsuario =2
                };
            }
            else
            {
                usuarioActual = objUsuario;
            }
           

            InitializeComponent();
        }

        //Dropdown menu
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
            Open_DropDownMenu(SubMenuDevlVenta, sender);
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

        private void Inicio_Load(object sender, EventArgs e)
        {
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

        }
        //menu Administracion 
        private void SubMenuCategoria_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuadministracion, new FrmCategoria());
        }

        private void SubMenuProducto_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuadministracion, new FrmProducto());
        }

        private void SubMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuadministracion, new FrmNegocio());
        }
        //menu venta
        private void SubMenuRegVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmVentaRegistro());
        }

        private void SubMenuDetVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmDetalleVenta());
        }

        private void SubMenuAbonoVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuventas, new FrmAbonoVenta());
        }
        //menu Ingreso
        private void SubMenuRegIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmRegIngresoProducto());
        }

        private void SubMenuDetIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmDetalleIngresoProducto());
        }

        private void SubMenuAbonoIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menucompras, new FrmAbonoIngresoProducto());
        }
        //menu Cliente
        private void menuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuclientes, new FrmCliente());
        }
        //menu proveedor
        private void menuProveedores_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuproveedores, new FrmProveedor());
        }
        //menu Genera Reportes
        private void SubMenuRepVenta_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new FrmReporteVentas());
        }

        private void SubMenuRepIngreso_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menureportes, new FrmReportesIngresoProductos());
        }

        private void menuServicioApartado_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuapartado, new FrmApartado());
        }

        private void devolverVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menudevolución, new FrmDevolucion());
        }

        private void SubMenuDevlProv_Click(object sender, EventArgs e)
        {

        }

        
    }
    
    
}
