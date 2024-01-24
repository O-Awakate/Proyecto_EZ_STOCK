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
using CapaPresentacion.Utilities;

namespace CapaPresentacion
{
    public partial class FrmUsuario : Form
    {
        public FrmUsuario()
        {
            InitializeComponent();
        }
        
        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdUsuario.Text = "0";
            txtIdDatosPersonas.Text = "0";
            txtIdDireccion.Text = "0";
            txtIdCorreo.Text = "0";
            txtIdTelefono.Text = "0";
            txtCI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtCorreo.Text = "";
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

            txtCI.Select();

        }

        private bool SeguridadClaves(string contraseña)
        {
            if (contraseña.Length < 8 || contraseña.Length > 16)
            {
                MessageBox.Show("La contraseña debe tener entre 8 y 16 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            if (!SeguridadClaves(txtContraseña.Text))
            {
                return;
            }

            if (txtContraseña.Text != txtConfirmarContraseña.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden. Por favor, verifique.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EncryptMD5 cifrado = new EncryptMD5();

            string claveCifrada = cifrado.Encrypt(txtContraseña.Text);

            Usuario objUsuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtIdUsuario.Text),
                oDatosPersona = new Datos_Persona
                {
                    IdDatosPersona= Convert.ToInt32(txtIdDatosPersonas.Text),
                    CI = txtCI.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    oCorreo = new Correo
                    {
                        IdCorreo = Convert.ToInt32(txtIdCorreo.Text),
                        UsuarioCorreo = txtCorreo.Text
                    },
                    oTelefono = new Telefono
                    {
                        IdTelefono = Convert.ToInt32(txtIdTelefono.Text),
                        Numero = txtTelefono.Text
                    },
                    oDireccion = new Direccion
                    {
                        IdDireccion = Convert.ToInt32(txtIdDireccion.Text),
                        Estado = txtEstado.Text,
                        Ciudad = txtCiudad.Text,
                        Sector = txtSector.Text,
                        Calle = txtCalle.Text,
                        Casa = txtNurCasa.Text
                    },
                },
                Clave = claveCifrada,
                oRol = new Rol() { IdRol = Convert.ToInt32(((OpcionCombo)cboRol.SelectedItem).Valor) },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (objUsuario.IdUsuario == 0)
            {

                int IdUsuarioGenrado = new CN_Usuario().Registrar(objUsuario, out mensaje);

                if (IdUsuarioGenrado != 0)
                {

                    dgvData.Rows.Add(new object[] { IdUsuarioGenrado, "", txtIdDatosPersonas.Text, txtCI.Text, txtNombre.Text, txtApellido.Text,
                        txtIdCorreo.Text, txtCorreo.Text, txtIdTelefono.Text, txtTelefono.Text, txtIdDireccion.Text, txtEstado.Text, txtCiudad.Text, txtSector.Text, txtCalle.Text, txtNurCasa.Text,
                        txtContraseña.Text,
                        ((OpcionCombo)cboRol.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboRol.SelectedItem).Texto.ToString(),
                        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString(),
                        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString()
                    });

                    Limpiar();
                }

                else
                {
                    MessageBox.Show(mensaje);
                }

            }

            else
            {
                bool resultado = new CN_Usuario().Editar(objUsuario, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["IdUsuario"].Value = txtIdUsuario.Text;
                    row.Cells["IdDatosPersonas"].Value = txtIdDatosPersonas.Text;
                    row.Cells["Cedula"].Value = txtCI.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Apellido"].Value = txtApellido.Text;
                    row.Cells["IdCorreo"].Value = txtIdCorreo.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text;
                    row.Cells["IdTelefono"].Value = txtIdTelefono.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
                    row.Cells["IdDireccion"].Value = txtIdDireccion.Text;
                    row.Cells["Estado"].Value = txtEstado.Text;
                    row.Cells["Ciudad"].Value = txtCiudad.Text;
                    row.Cells["Sector"].Value = txtSector.Text;
                    row.Cells["Calle"].Value = txtCalle.Text;
                    row.Cells["Casa"].Value = txtNurCasa.Text;
                    row.Cells["Clave"].Value = txtContraseña.Text;
                    row.Cells["idRol"].Value = ((OpcionCombo)cboRol.SelectedItem).Valor.ToString();
                    row.Cells["Rol"].Value = ((OpcionCombo)cboRol.SelectedItem).Texto.ToString();
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["EstadoActual"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    Limpiar();

                }

                else
                {
                    MessageBox.Show(mensaje);
                }

            }

        }

        private void FrmUsuario_Load_1(object sender, EventArgs e)
        {
            

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
                dgvData.Rows.Add(new object[] { item.IdUsuario, "",item.oDatosPersona.IdDatosPersona,
                    item.oDatosPersona.CI, item.oDatosPersona.Nombre, item.oDatosPersona.Apellido, item.oDatosPersona.oCorreo.IdCorreo,
                    item.oDatosPersona.oCorreo.UsuarioCorreo,item.oDatosPersona.oTelefono.IdTelefono, item.oDatosPersona.oTelefono.Numero,
                    item.oDatosPersona.oDireccion.IdDireccion, item.oDatosPersona.oDireccion.Estado, item.oDatosPersona.oDireccion.Ciudad, item.oDatosPersona.oDireccion.Sector, item.oDatosPersona.oDireccion.Calle, item.oDatosPersona.oDireccion.Casa,
                    item.Clave, item.oRol.IdRol,item.oRol.Descripcion,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.Estado == true ? 1 : 0

                });
            }
            cboRol.DisplayMember = "Texto";
            cboRol.ValueMember = "Valor";
            cboRol.SelectedIndex = 0;
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.checkbox_outline__1_.Width;
                var h = Properties.Resources.checkbox_outline__1_.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.checkbox_outline__1_, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtIdUsuario.Text = dgvData.Rows[indice].Cells["IdUsuario"].Value.ToString();
                    txtIdDatosPersonas.Text= dgvData.Rows[indice].Cells["IdDatosPersonas"].Value.ToString();
                    txtCI.Text = dgvData.Rows[indice].Cells["Cedula"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtApellido.Text = dgvData.Rows[indice].Cells["Apellido"].Value.ToString();
                    txtIdCorreo.Text = dgvData.Rows[indice].Cells["IdCorreo"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["Correo"].Value.ToString();
                    txtIdTelefono.Text = dgvData.Rows[indice].Cells["IdTelefono"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();
                    txtIdDireccion.Text = dgvData.Rows[indice].Cells["IdDireccion"].Value.ToString();
                    txtEstado.Text = dgvData.Rows[indice].Cells["Estado"].Value.ToString();
                    txtCiudad.Text = dgvData.Rows[indice].Cells["Ciudad"].Value.ToString();
                    txtSector.Text = dgvData.Rows[indice].Cells["Sector"].Value.ToString();
                    txtCalle.Text = dgvData.Rows[indice].Cells["Calle"].Value.ToString();
                    txtNurCasa.Text = dgvData.Rows[indice].Cells["Casa"].Value.ToString();
                    
                    foreach (OpcionCombo oc in cboRol.Items)
                    {
                        if (Convert.ToInt32(oc.Valor)== Convert.ToInt32(dgvData.Rows[indice].Cells["idRol"].Value))
                        {
                            int indice_combo = cboRol.Items.IndexOf(oc);
                            cboRol.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    foreach (OpcionCombo oc in cboEstado.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["EstadoValor"].Value))
                        {
                            int indice_combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdUsuario.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el usuario?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    Usuario objUsuario = new Usuario()
                    {
                        IdUsuario = Convert.ToInt32(txtIdUsuario.Text)
                    };

                    bool Respuesta = new CN_Usuario().Eliminar(objUsuario, out mensaje);

                    if (Respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtIndice.Text));
                        Limpiar();
                    }

                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        
    }
}
