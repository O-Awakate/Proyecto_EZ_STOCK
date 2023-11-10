using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaPresentacion.Utilidades;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }

        private void FrmUsuario_Load(object sender, EventArgs e)
        {
            //ComboBox CI
            cboTipoCI.Items.Add(new OpcionCombo() { Valor = 1, Texto = "V" });
            cboTipoCI.Items.Add(new OpcionCombo() { Valor = 2, Texto = "E" });
            cboTipoCI.Items.Add(new OpcionCombo() { Valor = 3, Texto = "J" });
            cboTipoCI.DisplayMember = "Texto";
            cboTipoCI.ValueMember = "Valor";
            cboTipoCI.SelectedIndex = 0;

            //ComboBox Correo
            cboDominio.Items.Add(new OpcionCombo() { Valor = 1, Texto = "@gmail" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = 2, Texto = "@hotmail" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = 3, Texto = "@outlook" });
            cboDominio.DisplayMember = "Texto";
            cboDominio.ValueMember = "Valor";
            cboDominio.SelectedIndex = 0;
            
            cboExtencion.Items.Add(new OpcionCombo() { Valor = 1, Texto = ".com" });
            cboExtencion.Items.Add(new OpcionCombo() { Valor = 2, Texto = ".ve" });
            cboExtencion.DisplayMember = "Texto";
            cboExtencion.ValueMember = "Valor";
            cboExtencion.SelectedIndex = 0;

            //ComboBox Telefono
            cboTipoTelefono.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Propio" });
            cboTipoTelefono.Items.Add(new OpcionCombo() { Valor = 2, Texto = "Casa" });
            cboTipoTelefono.Items.Add(new OpcionCombo() { Valor = 3, Texto = "Trabajo" });
            cboTipoTelefono.DisplayMember = "Texto";
            cboTipoTelefono.ValueMember = "Valor";
            cboTipoTelefono.SelectedIndex = 0;

            //ComboBox Estado
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 2, Texto = "No Activo" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            //ComboBox Rol
            List<Rol> listaRol = new CN_Rol().Listar();
            foreach (Rol item in listaRol)
            {
                cboRol.Items.Add(new OpcionCombo() { Valor = item.IdRol, Texto = item.Descripcion });
            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

            //Mostrar todos los usuarios
            List<Usuario> listaUsuario = new CN_Usuario().Listar();

            foreach (Usuario item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] { item.IdUsuario, "",
                    item.oDatosPersona.TipoCI + " " + item.oDatosPersona.CI, item.oDatosPersona.Nombre + " " + item.oDatosPersona.Apellido,
                    item.oDatosPersona.oCorreo.UsuarioCorreo + item.oDatosPersona.oCorreo.Dominio + item.oDatosPersona.oCorreo.ExtensionDominio,
                    item.oDatosPersona.oTelefono.TipoTelefono + " " + item.oDatosPersona.oTelefono.Numero,
                    item.oDatosPersona.oDireccion.Estado + " " + item.oDatosPersona.oDireccion.Ciudad + " " + item.oDatosPersona.oDireccion.Sector + " " + item.oDatosPersona.oDireccion.Calle + " " + item.oDatosPersona.oDireccion.Casa,
                    item.oRol.IdRol,item.oRol.Descripcion,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.Estado == true ? 1 : 0
                    
                });
            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Add(new object[] { txtIdUsuario.Text, "",
                ((OpcionCombo)cboTipoCI.SelectedItem).Texto.ToString() + " " + txtCI.Text, txtNombre.Text + " " + txtApellido.Text,
                txtUsuarioCorreo.Text + ((OpcionCombo)cboDominio.SelectedItem).Texto.ToString() + ((OpcionCombo)cboExtencion.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboTipoTelefono.SelectedItem).Texto.ToString() + " " + txtTelefono.Text,
                txtEstado.Text + " " + txtCiudad.Text + " " + txtSector.Text + " " + txtCalle.Text + " " + txtNurCasa.Text,
                ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString(),
                ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString()
            });

            Limpiar();
        }

        private void Limpiar()
        {
            txtIdUsuario.Text = "0";
            cboTipoCI.SelectedIndex = 0;
            txtCI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtUsuarioCorreo.Text = "";
            cboDominio.SelectedIndex = 0;
            cboExtencion.SelectedIndex = 0;
            cboTipoTelefono.SelectedIndex = 0;
            txtTelefono.Text = "";
            txtEstado.Text = "";
            txtCiudad.Text = "";
            txtSector.Text = "";
            txtCalle.Text = "";
            txtNurCasa.Text = "";
            txtContraseña.Text = "";
            txtConfirmarContraseña.Text="";
            cboRol.SelectedIndex = 0;
            cboEstado.SelectedIndex = 0;

        }
    }
}
