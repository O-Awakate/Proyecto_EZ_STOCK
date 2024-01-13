using CapaEntidad;
using CapaNegocio;
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
using CapaPresentacion.Utilities;
using Microsoft.VisualBasic;

namespace CapaPresentacion
{
    public partial class FrmVerificacion : Form
    {
        public FrmVerificacion()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Image byteToImagege(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageByte, 0, imageByte.Length);
            Image image = new Bitmap(ms);

            return image;
        }

        private void FrmVerificacion_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = new CN_OtrosDatos().obtenerLogo(out obtenido);

            if (obtenido)
                picLogo.Image = byteToImagege(byteimage);

            btnCorreo.Visible = false;
            btnSMS.Visible = false;

        }

        public void verificacion()
        {

            btnCorreo.Visible = true;
            btnSMS.Visible = true;

            lblCI.Visible = false;
            icon.Visible = false;
            txtCedula.Visible = false;
            btnIngresar.Visible = false;
            btnSalir.Visible = false;
            

        }

        private void btnCorreo_Click(object sender, EventArgs e)
        {
            
            DialogResult result = DialogResult.OK;

            string emisor = "RESERACA01@gmail.com";
            string clave = "kkak wawz ezml tham";
            string receptor = txtCorreo.Text;
            
            do
            {
                VerificacionCorreo email = new VerificacionCorreo();
                int numero = email.Enviar(emisor, clave, receptor);

                int resultado = 0;

                if (numero != 0)
                {
                    try
                    {
                        resultado = Convert.ToInt32(Interaction.InputBox("Ingresa el digito", "Verificación"));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Caracteres no validos");
                        result = MessageBox.Show("¿Desea reenviar el correo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information); 
                    }
                    if(numero == resultado)
                    {
                        MessageBox.Show("Los numeros coiniden"); 
                    }
                }
                else
                {
                    result = MessageBox.Show("¿Desea reenviar el correo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            } while (result == DialogResult.Yes);
            MessageBox.Show("Proceso Terminado");
            
        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            verificacion();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            verificacion();

            Usuario oUsuario = new CN_Usuario().ObtenerCorreo(txtCedula.Text);
            txtCorreo.Text = oUsuario.oDatosPersona.oCorreo.UsuarioCorreo;
        }
    }
}
