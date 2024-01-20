using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Utilidades;
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
        public FrmProveedor()
        {
            InitializeComponent();
        }

        private void FrmProveedor_Load(object sender, EventArgs e)
        {
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
                    item.oDatosPersona.CI,
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
            txtEstado.Text = "";
            txtCiudad.Text = "";
            txtSector.Text = "";
            txtCalle.Text = "";
            txtNurCasa.Text = "";
            cboEstado.SelectedIndex = 0;

            txtCI.Select();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Proveedor obj = new Proveedor()
            {
                IdProveedor = Convert.ToInt32(txtId.Text),
                oDatosPersona = new Datos_Persona
                {
                    IdDatosPersona = Convert.ToInt32(txtIdDatosPersonas.Text),
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
                        Estado = txtEstado.Text,
                        Ciudad = txtCiudad.Text,
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
                        txtCI.Text,
                        txtNombre.Text,
                        txtApellido.Text,
                        txtIdCasaProveedora.Text,
                        txtRIF.Text,
                        txtRazonSocial.Text,
                        txtSitioWeb.Text,
                        txtIdTelefono.Text,
                        txtTelefono.Text,
                        txtIdDireccion.Text,
                        txtEstado.Text,
                        txtCiudad.Text,
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
                    row.Cells["Cedula"].Value = txtCI.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Apellido"].Value = txtApellido.Text;
                    row.Cells["IdCasaProveedora"].Value = txtIdCasaProveedora.Text;
                    row.Cells["RazonSocial"].Value = txtRazonSocial.Text;
                    row.Cells["RIF"].Value = txtRIF.Text;
                    row.Cells["SitioWeb"].Value = txtSitioWeb.Text;
                    row.Cells["IdTelefono"].Value = txtIdTelefono.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
                    row.Cells["IdDireccion"].Value = txtIdDireccion.Text;
                    row.Cells["Estado"].Value = txtEstado.Text;
                    row.Cells["Ciudad"].Value = txtCiudad.Text;
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
                    txtCI.Text = dgvData.Rows[indice].Cells["Cedula"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtApellido.Text = dgvData.Rows[indice].Cells["Apellido"].Value.ToString();
                    txtIdCasaProveedora.Text = dgvData.Rows[indice].Cells["IdCasaProveedora"].Value.ToString();
                    txtRazonSocial.Text = dgvData.Rows[indice].Cells["RazonSocial"].Value.ToString();
                    txtSitioWeb.Text = dgvData.Rows[indice].Cells["SitioWeb"].Value.ToString();
                    txtRIF.Text = dgvData.Rows[indice].Cells["RIF"].Value.ToString();
                    txtIdTelefono.Text = dgvData.Rows[indice].Cells["IdTelefono"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();
                    txtIdDireccion.Text = dgvData.Rows[indice].Cells["IdDireccion"].Value.ToString();
                    txtEstado.Text = dgvData.Rows[indice].Cells["Estado"].Value.ToString();
                    txtCiudad.Text = dgvData.Rows[indice].Cells["Ciudad"].Value.ToString();
                    txtSector.Text = dgvData.Rows[indice].Cells["Sector"].Value.ToString();
                    txtCalle.Text = dgvData.Rows[indice].Cells["Calle"].Value.ToString();
                    txtNurCasa.Text = dgvData.Rows[indice].Cells["Casa"].Value.ToString();

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
    }
}
