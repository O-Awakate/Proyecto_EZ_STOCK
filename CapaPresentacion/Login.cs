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

namespace CapaPresentacion
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            EncryptMD5 cifrado = new EncryptMD5();

            string claveCifrada = cifrado.Encrypt(txtClave.Text);

            Usuario oUsuario = new CN_Usuario().Listar().FirstOrDefault(u => u.oDatosPersona.Nacionalidad + u.oDatosPersona.CI == txtCedula.Text && u.Clave == claveCifrada);

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
    }
}
