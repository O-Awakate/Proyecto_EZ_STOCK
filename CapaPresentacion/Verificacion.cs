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
    public partial class Verificacion : Form
    {
        public Verificacion()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Login form = new Login();

            form.Show();
            this.Close();

            form.FormClosing += frm_clossign;
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
            panelCambioClave.Visible = false;
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
            btnCorreo.Visible = false;
            btnSMS.Visible = false;

            EncryptMD5 cifrado = new EncryptMD5();

            DialogResult result = DialogResult.OK;

            Datos_Sistema oDatosSistema = new CN_Datos_Sistema().ObtenerDatos(2);

            string emisor = cifrado.Decrypt(oDatosSistema.Identificador);
            string clave = cifrado.Decrypt(oDatosSistema.Autentificador);
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
                        resultado = Convert.ToInt32(Interaction.InputBox("Ingrese el codigo enviado a su correo", "Verificación"));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Codigo invalido");
                        result = MessageBox.Show("¿Desea reenviar el codigo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information); 
                    }
                    if(numero == resultado)
                    {
                        panelCambioClave.Visible = true;
                    }
                }
                else
                {
                    result = MessageBox.Show("¿Desea reenviar el correo?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            } while (result == DialogResult.Yes);



        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            btnCorreo.Visible = false;
            btnSMS.Visible = false;

            verificacion();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                MessageBox.Show("Necesario inglesar numero de cedula", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                Usuario oUsuario = new CN_Usuario().Listar().FirstOrDefault(u => u.oDatosPersona.CI == txtCedula.Text);
                if (oUsuario != null)
                {
                    verificacion();
                    txtCorreo.Text = oUsuario.oDatosPersona.oCorreo.UsuarioCorreo;
                    txtID.Text = oUsuario.IdUsuario.ToString();
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }

            panelCorreoSMS.Visible = true;
        }

        private bool SeguridadClaves(string contraseña)
        {
            if (contraseña.Length < 8 || contraseña.Length > 16)
            {
                MessageBox.Show("La contraseña debe tener entre 8 y 12 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!contraseña.Any(char.IsLower))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra minúscula.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!contraseña.Any(char.IsUpper))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra mayúscula.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!contraseña.Any(char.IsDigit))
            {
                MessageBox.Show("La contraseña debe contener al menos un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private bool ActualizarClave()
        {
            lblClave.Visible = true;
            lblConClave.Visible = true;
            txtConfirmarClave.Visible = true;
            txtClave.Visible = true;

            string mensaje = string.Empty;

            EncryptMD5 cifrado = new EncryptMD5();

            string claveCifrada = cifrado.Encrypt(txtClave.Text);

            if (!SeguridadClaves(txtClave.Text))
            {
                return false;
            }

            if (txtClave.Text != txtConfirmarClave.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, verifique.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Usuario objUsuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtID.Text),
                Clave = claveCifrada,
            };

            string mensajeResultado;
            bool resultado = new CN_Usuario().ActualizarContraseña(objUsuario, out mensajeResultado);

            if (resultado)
            {
                MessageBox.Show("Contraseña actualizada con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña: " + mensajeResultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resultado;
        }

        private void frm_clossign(object sende, FormClosingEventArgs e)
        {
            txtCedula.Text = "";
            txtClave.Text = "";
            txtConfirmarClave.Text = "";
            txtCorreo.Text = "";
            txtID.Text = "";
            this.Show();
        }

        private void brnVolverlogin_Click(object sender, EventArgs e)
        {
            Login form = new Login();

            form.Show();
            this.Hide();

            form.FormClosing += frm_clossign;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            btnCorreo.Visible = false;
            btnSMS.Visible = false;
            btnBack.Visible = false;


            lblCI.Visible = true;
            icon.Visible = true;
            txtCedula.Visible = true;
            btnIngresar.Visible = true;
            btnSalir.Visible = true;
        }
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            panelCambioClave.Visible = false;
             verificacion();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ActualizarClave())
            {
                MessageBox.Show("Se recupero su contraseña exitosamente","Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);

                Login form = new Login();

                form.Show();
                this.Close();
                
                form.FormClosing += frm_clossign;
                

            }
        }
    }
}
