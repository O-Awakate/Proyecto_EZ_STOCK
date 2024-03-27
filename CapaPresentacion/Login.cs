using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using CapaEntidad;
using System.IO;
using CapaPresentacion.Utilities;
using CapaPresentacion.Utilidades;

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        private ToolTip toolTip1;
        public Login()
        {
            InitializeComponent();
            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btnIngresar, "Ingresar a EZ-Stock");
            toolTip1.SetToolTip(btnSalir, "Salir de EZ-Stock");
            toolTip1.SetToolTip(btnClave, "Hacer visible la contraseña");
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ingresar()
        {
            EncryptMD5 cifrado = new EncryptMD5();

            string claveCifrada = cifrado.Encrypt(txtClave.Text);

            Usuario oUsuario = new CN_Usuario().Listar().FirstOrDefault(u => u.oDatosPersona.Nacionalidad == cboNacionalidad.Text && u.oDatosPersona.CI == txtCedula.Text && u.Clave == claveCifrada);

            if (oUsuario != null)
            {
                Inicio form = new Inicio(oUsuario);

                form.Show();
                this.Hide();

                form.FormClosing += frm_clossign;
            }
            else
            {
                MessageBox.Show("El usuario y/o contraseña ingresados son incorrectos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {

            ingresar();

        }

        private void frm_clossign(object sende, FormClosingEventArgs e)
        {
            txtCedula.Text = "";
            txtClave.Text = "";

            this.Show();
        }
        

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Verificacion form = new Verificacion();

            form.Show();
            this.Hide();

            form.FormClosing += frm_clossign;
        }

        private void btnClave_Click(object sender, EventArgs e)
        {
            if (txtClave.PasswordChar == '*')
            {
                txtClave.PasswordChar = '\0';
            }
            else
            {
                txtClave.PasswordChar = '*';
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            //Ingresa valores al Combo box
            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "V", Texto = "V" });
            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "E", Texto = "E" });
            cboNacionalidad.DisplayMember = "Texto";
            cboNacionalidad.ValueMember = "Valor";
            cboNacionalidad.SelectedIndex = 0;
        }

        private void txtCedula_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter || (txtCedula.Text.Length >= 8 && e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ingresar();

                e.Handled = true;
            }
        }

    }
}
