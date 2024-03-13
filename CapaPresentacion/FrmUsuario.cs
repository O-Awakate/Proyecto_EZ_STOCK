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
using System.Text.RegularExpressions;

namespace CapaPresentacion
{
    public partial class FrmUsuario : Form
    {
        private ToolTip toolTip1;
        public FrmUsuario()
        {
            InitializeComponent();
            
            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(txtContraseña, "La contraseña requiere un mínimo de una mayúscula, una minúscula y un número.");

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
            cboEstado.SelectedValue = "AN";
            cboCiudad.SelectedIndex = 1;
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

        private bool ValidarFormatoCI(string ci)
        {
                // Verificar que la cadena tenga exactamente 8 caracteres y que todos sean dígitos
            if (ci.Length > 5 && ci.All(char.IsDigit))
            {
                return true;
            }
            return false;

        }

        private void Guardar()
        {
            string mensaje = string.Empty;

            if (!ValidarFormatoCI(txtCI.Text))
            {
                MessageBox.Show("Ingrese una Cedula válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!SeguridadClaves(txtContraseña.Text))
            {
                return;
            }

            if (txtContraseña.Text != txtConfirmarContraseña.Text)
            {
                MessageBox.Show("La contraseña y confirmación de contraseña no coinciden. Por favor, verifique que ingrese la contraseña y/o confirmación correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EncryptMD5 cifrado = new EncryptMD5();

            string claveCifrada = cifrado.Encrypt(txtContraseña.Text);

            Usuario objUsuario = new Usuario()
            {
                IdUsuario = Convert.ToInt32(txtIdUsuario.Text),
                oDatosPersona = new Datos_Persona
                {
                    IdDatosPersona = Convert.ToInt32(txtIdDatosPersonas.Text),
                    Nacionalidad = Convert.ToString(((OpcionCombo)cboNacionalidad.SelectedItem).Valor),
                    CI =  txtCI.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
                    oCorreo = new Correo
                    {
                        IdCorreo = Convert.ToInt32(txtIdCorreo.Text),
                        UsuarioCorreo = txtCorreo.Text,
                        Dominio = Convert.ToString(((OpcionCombo)cboDominio.SelectedItem).Valor)
                    },
                    oTelefono = new Telefono
                    {
                        IdTelefono = Convert.ToInt32(txtIdTelefono.Text),
                        Numero = txtTelefono.Text
                    },
                    oDireccion = new Direccion
                    {
                        IdDireccion = Convert.ToInt32(txtIdDireccion.Text),
                        Estado = Convert.ToString(((OpcionCombo)cboEstadoVen.SelectedItem).Texto),
                        Ciudad = Convert.ToString(((OpcionCombo)cboCiudad.SelectedItem).Texto),
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

                    dgvData.Rows.Add(new object[] { IdUsuarioGenrado,
                        "",
                        txtIdDatosPersonas.Text,
                        ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString() + txtCI.Text,
                        txtCI.Text,
                        ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString(),
                        txtNombre.Text,
                        txtApellido.Text,
                        txtIdCorreo.Text,
                        txtCorreo.Text + ((OpcionCombo)cboDominio.SelectedItem).Texto.ToString(),
                        txtCorreo.Text,
                        ((OpcionCombo)cboDominio.SelectedItem).Texto.ToString(),
                        txtIdTelefono.Text,
                        txtTelefono.Text,
                        txtIdDireccion.Text,
                        ((OpcionCombo)cboEstadoVen.SelectedItem).Texto.ToString(),
                        ((OpcionCombo)cboCiudad.SelectedItem).Texto.ToString(),
                        txtSector.Text,
                        txtCalle.Text,
                        txtNurCasa.Text,
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
                    row.Cells["Cedula"].Value = ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString() + txtCI.Text;
                    row.Cells["NumeroCI"].Value = txtCI.Text;
                    row.Cells["Nacionalidad"].Value = ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString();
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Apellido"].Value = txtApellido.Text;
                    row.Cells["IdCorreo"].Value = txtIdCorreo.Text;
                    row.Cells["Correo"].Value = txtCorreo.Text + ((OpcionCombo)cboDominio.SelectedItem).Texto.ToString();
                    row.Cells["UsuarioCorreo"].Value = txtCorreo.Text;
                    row.Cells["Dominio"].Value = ((OpcionCombo)cboDominio.SelectedItem).Texto.ToString();
                    row.Cells["IdTelefono"].Value = txtIdTelefono.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
                    row.Cells["IdDireccion"].Value = txtIdDireccion.Text;
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstadoVen.SelectedItem).Texto.ToString();
                    row.Cells["Ciudad"].Value = ((OpcionCombo)cboCiudad.SelectedItem).Texto.ToString();
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

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            Guardar();
        }

        private void Estados()
        {
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "AM", Texto = "Amazonas" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "AN", Texto = "Anzoátegui" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "AP", Texto = "Apure" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "AR", Texto = "Aragua" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "BA", Texto = "Barinas" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "BO", Texto = "Bolívar" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "CA", Texto = "Carabobo" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "CO", Texto = "Cojedes" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "DA", Texto = "Delta Amacuro" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "DI", Texto = "Distrito Capital" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "FA", Texto = "Falcón" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "GU", Texto = "Guárico" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "LA", Texto = "Lara" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "ME", Texto = "Mérida" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "MI", Texto = "Miranda" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "MO", Texto = "Monagas" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "NE", Texto = "Nueva Esparta" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "PO", Texto = "Portuguesa" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "SU", Texto = "Sucre" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "TA", Texto = "Táchira" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "TR", Texto = "Trujillo" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "VA", Texto = "Vargas" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "YA", Texto = "Yaracuy" });
            cboEstadoVen.Items.Add(new OpcionCombo() { Valor = "ZU", Texto = "Zulia" });

            cboEstadoVen.DisplayMember = "Texto";
            cboEstadoVen.ValueMember = "Valor";
            cboEstadoVen.SelectedIndex = 1;
        }
        

        private void FrmUsuario_Load_1(object sender, EventArgs e)
        {
            Estados();

            txtCI.ShortcutsEnabled = false;
            txtTelefono.ShortcutsEnabled = false;
            txtNombre.ShortcutsEnabled = false;
            txtApellido.ShortcutsEnabled = false;
            txtSector.ShortcutsEnabled = false;

            //ComboBoxDominio

            cboDominio.Items.Add(new OpcionCombo() { Valor = "@gmail.com", Texto = "@Gmail.com" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@yahoo.com", Texto = "@Yahoo.com" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@outlook.com", Texto = "@Outlook.com" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@hotmail.com", Texto = "@Hotmail.com" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@icloud.com", Texto = "@iCloud.com" });

            // Versiones .es
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@gmail.es", Texto = "@Gmail.es" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@yahoo.es", Texto = "@Yahoo.es" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@outlook.es", Texto = "@Outlook.es" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@hotmail.es", Texto = "@hotmail.es" });
            cboDominio.Items.Add(new OpcionCombo() { Valor = "@icloud.es", Texto = "@iCloud.es" });

            cboDominio.DisplayMember = "Texto";
            cboDominio.ValueMember = "Valor";
            cboDominio.SelectedIndex = 0;

            //ComboBox Nacionalidad
            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "V", Texto = "V" });
            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "E", Texto = "E" });
            cboNacionalidad.DisplayMember = "Texto";
            cboNacionalidad.ValueMember = "Valor";
            cboNacionalidad.SelectedIndex = 0;


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
                dgvData.Rows.Add(new object[] { item.IdUsuario,
                    "",
                    item.oDatosPersona.IdDatosPersona,
                    item.oDatosPersona.Nacionalidad + item.oDatosPersona.CI,
                    item.oDatosPersona.CI,
                    item.oDatosPersona.Nacionalidad,
                    item.oDatosPersona.Nombre,
                    item.oDatosPersona.Apellido,
                    item.oDatosPersona.oCorreo.IdCorreo,
                    item.oDatosPersona.oCorreo.UsuarioCorreo + item.oDatosPersona.oCorreo.Dominio,
                    item.oDatosPersona.oCorreo.UsuarioCorreo,
                    item.oDatosPersona.oCorreo.Dominio,
                    item.oDatosPersona.oTelefono.IdTelefono,
                    item.oDatosPersona.oTelefono.Numero,
                    item.oDatosPersona.oDireccion.IdDireccion,
                    item.oDatosPersona.oDireccion.Estado,
                    item.oDatosPersona.oDireccion.Ciudad,
                    item.oDatosPersona.oDireccion.Sector,
                    item.oDatosPersona.oDireccion.Calle,
                    item.oDatosPersona.oDireccion.Casa,
                    item.Clave,
                    item.oRol.IdRol,
                    item.oRol.Descripcion,
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
                    txtCI.Text = dgvData.Rows[indice].Cells["NumeroCI"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtApellido.Text = dgvData.Rows[indice].Cells["Apellido"].Value.ToString();
                    txtIdCorreo.Text = dgvData.Rows[indice].Cells["IdCorreo"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["UsuarioCorreo"].Value.ToString();
                    txtIdTelefono.Text = dgvData.Rows[indice].Cells["IdTelefono"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();
                    txtIdDireccion.Text = dgvData.Rows[indice].Cells["IdDireccion"].Value.ToString();
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
                    string estadoBuscado = dgvData.Rows[indice].Cells["Estado"].Value.ToString();

                    foreach (OpcionCombo oc in cboEstadoVen.Items)
                    {
                        if (oc.Texto == estadoBuscado)
                        {
                            int indice_combo = cboEstadoVen.Items.IndexOf(oc);
                            cboEstadoVen.SelectedIndex = indice_combo;
                            break;
                        }
                    }

                    string Ciudad = dgvData.Rows[indice].Cells["Ciudad"].Value.ToString();

                    foreach (object item in cboCiudad.Items)
                    {
                        if (item is OpcionCombo oc && oc.Texto == Ciudad)
                        {
                            int indice_combo = cboCiudad.Items.IndexOf(item);
                            cboCiudad.SelectedIndex = indice_combo;
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

                    string Nacionalidad = dgvData.Rows[indice].Cells["Nacionalidad"].Value.ToString();

                    foreach (object item in cboNacionalidad.Items)
                    {
                        if (item is OpcionCombo oc && oc.Texto == Nacionalidad)
                        {
                            int indice_combo = cboNacionalidad.Items.IndexOf(item);
                            cboNacionalidad.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    string Dominio = dgvData.Rows[indice].Cells["Dominio"].Value.ToString();

                    foreach (object item in cboDominio.Items)
                    {
                        if (item is OpcionCombo oc && oc.Texto == Dominio)
                        {
                            int indice_combo = cboDominio.Items.IndexOf(item);
                            cboDominio.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (txtIdUsuario.Text == "1")
            {
                MessageBox.Show("El administrador no puede ser Eliminado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter || (txtCI.Text.Length >= 8 && e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtCI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNombre.Focus();

                e.Handled = true;
            }
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtApellido.Focus();

                e.Handled = true;
            }
        }

        private void txtApellido_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCorreo.Focus();

                e.Handled = true;
            }
        }

        private void txtCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtTelefono.Focus();

                e.Handled = true;
            }
        }

        private void cboEstadoVen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciudades ciudadesInstance = new Ciudades();

            string estadoSeleccionado = ((OpcionCombo)cboEstadoVen.SelectedItem).Texto;
            string[] ciudades = ciudadesInstance.CiudadesPorEstado[estadoSeleccionado];
            cboCiudad.Items.Clear();
            foreach (var ciudad in ciudades)
            {
                cboCiudad.Items.Add(new OpcionCombo() { Valor = ciudad, Texto = ciudad });
            }
            if (ciudades.Length > 0)
            {
                cboCiudad.SelectedIndex = 0;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter || (txtTelefono.Text.Length >= 11 && e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSector.Focus();

                e.Handled = true;
            }
        }

        private void txtSector_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCalle.Focus();

                e.Handled = true;
            }
        }

        private void txtCalle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtNurCasa.Focus();

                e.Handled = true;
            }
        }

        private void txtNurCasa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtContraseña.Focus();
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Guardar();
                e.SuppressKeyPress = true;
            }
            
        }

        private void btnClave_Click(object sender, EventArgs e)
        {
            if (txtContraseña.PasswordChar == '*')
            {
                txtContraseña.PasswordChar = '\0';
            }
            else
            {
                txtContraseña.PasswordChar = '*';
            }
        }

        private void btnConClave_Click(object sender, EventArgs e)
        {
            if (txtConfirmarContraseña.PasswordChar == '*')
            {
                txtConfirmarContraseña.PasswordChar = '\0';
            }
            else
            {
                txtConfirmarContraseña.PasswordChar = '*';
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' || txtNombre.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' || txtApellido.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtSector_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verificar si el caracter ingresado es una letra o la tecla "Enter"
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' || txtSector.Text.Length >= 15 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtCalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCalle.Text.Length >= 15 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtNurCasa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtNurCasa.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtContraseña.Text.Length >= 16 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtConfirmarContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtConfirmarContraseña.Text.Length >= 16 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCorreo.Text.Length >= 16 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }
    }
}
