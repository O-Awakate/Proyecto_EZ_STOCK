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
using CapaPresentacion.Utilidades;

namespace CapaPresentacion
{
    public partial class Verificacion : Form
    {
        private ToolTip toolTip1;
        public Verificacion()
        {
            InitializeComponent();
            // Burbujas con informacion relevante
            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(txtClave, "La contraseña requiere un mínimo de una mayúscula, una minúscula y un número.");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close(); //Cierra ventana de verificacion
        }

        private void FrmVerificacion_Load(object sender, EventArgs e)
        {
            //Ingresa valores al Combo box

            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "V", Texto = "V" });
            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "E", Texto = "E" });
            cboNacionalidad.DisplayMember = "Texto";
            cboNacionalidad.ValueMember = "Valor";
            cboNacionalidad.SelectedIndex = 0;

            //Oculta votones hasta que sean necesarios
            btnBack.Visible = false; 
            btnCorreo.Visible = false;
            panelCambioClave.Visible = false;
        }

        public void verificacion()
        {
            //Opciones para verificar usuario y oculta lo que no es necesario
            btnCorreo.Visible = true;

            lblCI.Visible = false;
            icon.Visible = false;
            txtCedula.Visible = false;
            btnIngresar.Visible = false;
            btnSalir.Visible = false;

        }

        private void btnCorreo_Click(object sender, EventArgs e)
        {
            //Veridicacion por correo
            btnCorreo.Visible = false;

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
                        resultado = Convert.ToInt32(Interaction.InputBox("Por favor, ingrese el código de verificación enviado a su correo electrónico.", "Verificación"));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("El código de verificación que ha ingresado es inválido.");
                        result = MessageBox.Show("¿Desea enviar un nuevo código de verificación?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information); 
                    }
                    if(numero == resultado)
                    {
                        panelCambioClave.Visible = true;
                    }
                }
                else
                {
                    result = MessageBox.Show("¿Desea enviar un nuevo código de verificación?", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            } while (result == DialogResult.Yes);



        }

        private void btnSMS_Click(object sender, EventArgs e)
        {
            btnCorreo.Visible = false;

            verificacion();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCedula.Text))
            {
                MessageBox.Show("Debe ingresar el número de cédula del usuario para la recuperación de contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {

                Usuario oUsuario = new CN_Usuario().Listar().FirstOrDefault(u => u.oDatosPersona.Nacionalidad  == cboNacionalidad.Text && u.oDatosPersona.CI == txtCedula.Text);
                if (oUsuario != null)
                {
                    verificacion();
                    txtCorreo.Text = oUsuario.oDatosPersona.oCorreo.UsuarioCorreo + oUsuario.oDatosPersona.oCorreo.Dominio;
                    txtID.Text = oUsuario.IdUsuario.ToString();

                    btnBack.Visible = true;
                }
                else
                {
                    MessageBox.Show("El usuario ingresado no se ha encontrado en el sistema. Ingrese el número de cédula de un usuario válido. ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

            }

            panelCorreoSMS.Visible = true;
        }

        private bool SeguridadClaves(string contraseña)
        {
            if (contraseña.Length < 8 || contraseña.Length > 16)
            {
                MessageBox.Show("La nueva contraseña debe tener entre 8 y 16 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!contraseña.Any(char.IsLower))
            {
                MessageBox.Show("La nueva contraseña debe contener al menos una letra minúscula.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!contraseña.Any(char.IsUpper))
            {
                MessageBox.Show("La nueva contraseña debe contener al menos una letra mayúscula.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!contraseña.Any(char.IsDigit))
            {
                MessageBox.Show("La nueva contraseña debe contener al menos un número.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("La confirmación de contraseñas no coinciden. Por favor, verifique que la confirmación de clave sea correcta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("La contraseña ha sido actualizada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error al actualizar la contraseña: " + mensajeResultado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btnBack.Visible = false;


            lblCI.Visible = true;
            icon.Visible = true;
            txtCedula.Visible = true;
            btnIngresar.Visible = true;
            btnSalir.Visible = true;
            btnBack.Visible = false;
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
                MessageBox.Show("Se ha recuperado su contraseña exitosamente.", "Mensaje",MessageBoxButtons.OK,MessageBoxIcon.Information);

                Login form = new Login();

                form.Show();
                this.Close();
                
                form.FormClosing += frm_clossign;
                

            }
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

        private void btnConClave_Click(object sender, EventArgs e)
        {
            if (txtConfirmarClave.PasswordChar == '*')
            {
                txtConfirmarClave.PasswordChar = '\0';
            }
            else
            {
                txtConfirmarClave.PasswordChar = '*';
            }
        }
    }
}
