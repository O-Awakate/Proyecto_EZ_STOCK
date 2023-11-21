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
            
            Usuario oUsuario = new CN_Usuario().Listar().Where(u => u.oDatosPersona.CI == txtCedula.Text && u.Clave == txtClave.Text).FirstOrDefault();
            
            if (oUsuario!= null)
            {
                Inicio form = new Inicio(oUsuario);

                form.Show();
                this.Hide();

                form.FormClosing += frm_clossign;
            }

            else
            {
                MessageBox.Show("No se encontro el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            

        }

        private void frm_clossign(object sende, FormClosingEventArgs e)
        {
            txtCedula.Text = "";
            txtClave.Text = "";

            this.Show();
        }

        public Image byteToImagege(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageByte, 0, imageByte.Length);
            Image image = new Bitmap(ms);

            return image;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = new CN_OtrosDatos().obtenerLogo(out obtenido);

            if (obtenido)
                picLogo.Image = byteToImagege(byteimage);
        }
    }
}
