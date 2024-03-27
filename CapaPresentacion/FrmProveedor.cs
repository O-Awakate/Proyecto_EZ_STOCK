using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
using CapaPresentacion.Utilities;
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
    public partial class FrmProveedor : Form
    {
        private ToolTip toolTip1;
        public FrmProveedor()
        {
            InitializeComponent();

            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btnBuscar, "Buscar proveedores filtrados.");
            toolTip1.SetToolTip(btnLimpiarBuscador, "Limpiar filtro.");
            toolTip1.SetToolTip(btnGuardar, "Guardar proveedores.");
            toolTip1.SetToolTip(btnLimpiar, "Limpiar cuadros de texto.");
            toolTip1.SetToolTip(btnEliminar, "Eliminar proveedores.");
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

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
            Estados();

            txtCI.ShortcutsEnabled = false;
            txtTelefono.ShortcutsEnabled = false;
            txtNombre.ShortcutsEnabled = false;
            txtApellido.ShortcutsEnabled = false;
            txtSector.ShortcutsEnabled = false;

            //ComboBox Nacionalidad
            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "V", Texto = "V" });
            cboNacionalidad.Items.Add(new OpcionCombo() { Valor = "E", Texto = "E" });
            cboNacionalidad.DisplayMember = "Texto";
            cboNacionalidad.ValueMember = "Valor";
            cboNacionalidad.SelectedIndex = 0;

            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Activo" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "No Activo" });

            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor";
            cboEstado.SelectedIndex = 0;

            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBuscar.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBuscar.DisplayMember = "Texto";
            cboBuscar.ValueMember = "Valor";
            cboBuscar.SelectedIndex = 0;

            //Mostrando Usuarios (todos)

            List<Proveedor> Lista = new CN_Proveedor().listar();

            foreach (Proveedor item in Lista)
            {
                dgvData.Rows.Add(new object[] { item.IdProveedor,
                    "",
                    item.oDatosPersona.IdDatosPersona,
                    item.oDatosPersona.Nacionalidad + item.oDatosPersona.CI,
                    item.oDatosPersona.CI,
                    item.oDatosPersona.Nacionalidad,
                    item.oDatosPersona.Nombre,
                    item.oDatosPersona.Apellido,
                    item.oCasaProveedora.IdCasaProveedora,
                    item.oCasaProveedora.RIF,
                    item.oCasaProveedora.RazonSocial,
                    item.oCasaProveedora.SitioWeb,
                    item.oDatosPersona.oTelefono.IdTelefono,
                    item.oDatosPersona.oTelefono.Numero,
                    item.oDatosPersona.oDireccion.IdDireccion,
                    item.oDatosPersona.oDireccion.Estado,
                    item.oDatosPersona.oDireccion.Ciudad,
                    item.oDatosPersona.oDireccion.Sector,
                    item.oDatosPersona.oDireccion.Calle,
                    item.oDatosPersona.oDireccion.Casa,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.Estado == true ? 1 : 0

                });
            }
            
        }



        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            txtIdCasaProveedora.Text = "0";
            txtRIF.Text = "";
            txtRazonSocial.Text = "";
            txtSitioWeb.Text = "";
            txtIdDatosPersonas.Text = "0";
            txtIdDireccion.Text = "0";
            txtIdTelefono.Text = "0";
            txtCI.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtTelefono.Text = "";
            cboEstadoVen.SelectedIndex = 0;
            cboCiudad.SelectedIndex = 0;
            txtSector.Text = "";
            txtCalle.Text = "";
            txtNurCasa.Text = "";
            cboEstado.SelectedIndex = 0;

            txtCI.Select();

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

            if (!txtRIF.Text.Contains("J-"))
            {
                MessageBox.Show("El campo RIF debe contener 'J-'", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Proveedor obj = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(txtId.Text),
                oDatosPersona = new Datos_Persona
                {
                    IdDatosPersona = Convert.ToInt32(txtIdDatosPersonas.Text),
                    Nacionalidad = Convert.ToString(((OpcionCombo)cboNacionalidad.SelectedItem).Valor),
                    CI = txtCI.Text,
                    Nombre = txtNombre.Text,
                    Apellido = txtApellido.Text,
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
                oCasaProveedora = new Casa_Proveedora
                {
                    IdCasaProveedora = Convert.ToInt32(txtIdCasaProveedora.Text),
                    RIF = txtRIF.Text,
                    RazonSocial = txtRazonSocial.Text,
                    SitioWeb = txtSitioWeb.Text,
                },
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProveedor == 0)
            {

                int IdGenerado = new CN_Proveedor().Registrar(obj, out mensaje);

                if (IdGenerado != 0)
                {

                    dgvData.Rows.Add(new object[] { IdGenerado,
                        "",
                        txtIdDatosPersonas.Text,
                        ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString() + txtCI.Text,
                        txtCI.Text,
                        ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString(),
                        txtNombre.Text,
                        txtApellido.Text,
                        txtIdCasaProveedora.Text,
                        txtRIF.Text,
                        txtRazonSocial.Text,
                        txtSitioWeb.Text,
                        txtIdTelefono.Text,
                        txtTelefono.Text,
                        txtIdDireccion.Text,
                        ((OpcionCombo)cboEstadoVen.SelectedItem).Texto.ToString(),
                        ((OpcionCombo)cboCiudad.SelectedItem).Texto.ToString(),
                        txtSector.Text,
                        txtCalle.Text,
                        txtNurCasa.Text,
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
                bool resultado = new CN_Proveedor().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["IdDatosPersonas"].Value = txtIdDatosPersonas.Text;
                    row.Cells["Cedula"].Value = ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString() + txtCI.Text;
                    row.Cells["Nacionalidad"].Value = ((OpcionCombo)cboNacionalidad.SelectedItem).Texto.ToString();
                    row.Cells["NumeroCI"].Value = txtCI.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Apellido"].Value = txtApellido.Text;
                    row.Cells["IdCasaProveedora"].Value = txtIdCasaProveedora.Text;
                    row.Cells["RazonSocial"].Value = txtRazonSocial.Text;
                    row.Cells["RIF"].Value = txtRIF.Text;
                    row.Cells["SitioWeb"].Value = txtSitioWeb.Text;
                    row.Cells["IdTelefono"].Value = txtIdTelefono.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
                    row.Cells["IdDireccion"].Value = txtIdDireccion.Text;
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstadoVen.SelectedItem).Texto.ToString();
                    row.Cells["Ciudad"].Value = ((OpcionCombo)cboCiudad.SelectedItem).Texto.ToString();
                    row.Cells["Sector"].Value = txtSector.Text;
                    row.Cells["Calle"].Value = txtCalle.Text;
                    row.Cells["Casa"].Value = txtNurCasa.Text;
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
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();

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

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtIdDatosPersonas.Text = dgvData.Rows[indice].Cells["IdDatosPersonas"].Value.ToString();
                    txtCI.Text = dgvData.Rows[indice].Cells["NumeroCI"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtApellido.Text = dgvData.Rows[indice].Cells["Apellido"].Value.ToString();
                    txtIdCasaProveedora.Text = dgvData.Rows[indice].Cells["IdCasaProveedora"].Value.ToString();
                    txtRazonSocial.Text = dgvData.Rows[indice].Cells["RazonSocial"].Value.ToString();
                    txtSitioWeb.Text = dgvData.Rows[indice].Cells["SitioWeb"].Value.ToString();
                    txtRIF.Text = dgvData.Rows[indice].Cells["RIF"].Value.ToString();
                    txtIdTelefono.Text = dgvData.Rows[indice].Cells["IdTelefono"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();
                    txtIdDireccion.Text = dgvData.Rows[indice].Cells["IdDireccion"].Value.ToString();
                    txtSector.Text = dgvData.Rows[indice].Cells["Sector"].Value.ToString();
                    txtCalle.Text = dgvData.Rows[indice].Cells["Calle"].Value.ToString();
                    txtNurCasa.Text = dgvData.Rows[indice].Cells["Casa"].Value.ToString();

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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {

            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {

                row.Visible = true;
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFiltro = ((OpcionCombo)cboBuscar.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFiltro].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;

                    else
                        row.Visible = false;

                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Proveedor?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    Proveedor obj = new Proveedor()
                    {
                        IdProveedor = Convert.ToInt32(txtId.Text)
                    };

                    bool Respuesta = new CN_Proveedor().Eliminar(obj, out mensaje);

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

        private void txtCI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter || (txtCI.Text.Length >= 8 && e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter || (txtTelefono.Text.Length >= 11 && e.KeyChar != (char)Keys.Back))
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
                txtTelefono.Focus();

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
                txtRazonSocial.Focus();

                e.Handled = true;
            }
        }

        private void txtRazonSocial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRIF.Focus();

                e.Handled = true;
            }
        }

        private void txtRIF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSitioWeb.Focus();

                e.Handled = true;
            }
        }

        private void txtSitioWeb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Guardar();
                e.SuppressKeyPress = true;
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' || txtNombre.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' || txtApellido.Text.Length >= 25 && e.KeyChar != (char)Keys.Back)
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

        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtRazonSocial.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtRIF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtRIF.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtSitioWeb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtSitioWeb.Text.Length >= 50 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }
    }
}
