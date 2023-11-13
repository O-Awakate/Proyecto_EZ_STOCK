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
    public partial class FrmCliente : Form
    {
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
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

            List<Cliente> Lista = new CN_Cliente().listar();

            foreach (Cliente item in Lista)
            {
                dgvData.Rows.Add(new object[] { item.IdCliente,
                    "",
                    item.oDatosPersona.IdDatosPersona,
                    item.oDatosPersona.CI,
                    item.oDatosPersona.Nombre,
                    item.oDatosPersona.Apellido,
                    item.oDatosPersona.oTelefono.IdTelefono,
                    item.oDatosPersona.oTelefono.Numero,
                    item.oDatosPersona.oDireccion.IdDireccion,
                    item.oDatosPersona.oDireccion.Estado,
                    item.oDatosPersona.oDireccion.Ciudad,
                    item.oDatosPersona.oDireccion.Sector,
                    item.oDatosPersona.oDireccion.Calle,
                    item.oDatosPersona.oDireccion.Casa,
                    item.Deuda,
                    item.Estado == true ? "Activo" : "No Activo",
                    item.Estado == true ? 1 : 0

                });
            }

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Cliente obj = new Cliente()
            {
                IdCliente = Convert.ToInt32(txtIdCliente.Text),
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
                Deuda = Convert.ToDecimal(txtDeuda.Text),
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdCliente == 0)
            {

                int IdClienteGenrado = new CN_Cliente().Registrar(obj, out mensaje);

                if (IdClienteGenrado != 0)
                {

                    dgvData.Rows.Add(new object[] { IdClienteGenrado, "", txtIdDatosPersonas.Text, txtCI.Text, txtNombre.Text, txtApellido.Text,
                        txtIdTelefono.Text, txtTelefono.Text, txtIdDireccion.Text, txtEstado.Text, txtCiudad.Text, txtSector.Text, txtCalle.Text, txtNurCasa.Text,
                        txtDeuda.Text,
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
                bool resultado = new CN_Cliente().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtIdCliente.Text;
                    row.Cells["IdDatosPersonas"].Value = txtIdDatosPersonas.Text;
                    row.Cells["Cedula"].Value = txtCI.Text;
                    row.Cells["Nombre"].Value = txtNombre.Text;
                    row.Cells["Apellido"].Value = txtApellido.Text;
                    row.Cells["IdTelefono"].Value = txtIdTelefono.Text;
                    row.Cells["Telefono"].Value = txtTelefono.Text;
                    row.Cells["IdDireccion"].Value = txtIdDireccion.Text;
                    row.Cells["Estado"].Value = txtEstado.Text;
                    row.Cells["Ciudad"].Value = txtCiudad.Text;
                    row.Cells["Sector"].Value = txtSector.Text;
                    row.Cells["Calle"].Value = txtCalle.Text;
                    row.Cells["Casa"].Value = txtNurCasa.Text;
                    row.Cells["Deuda"].Value = txtDeuda.Text;
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

        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtIdCliente.Text = "0";
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
            txtDeuda.Text = "0";
            cboEstado.SelectedIndex = 0;

            txtCI.Select();

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
                    txtIdCliente.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtIdDatosPersonas.Text = dgvData.Rows[indice].Cells["IdDatosPersonas"].Value.ToString();
                    txtCI.Text = dgvData.Rows[indice].Cells["Cedula"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["Nombre"].Value.ToString();
                    txtApellido.Text = dgvData.Rows[indice].Cells["Apellido"].Value.ToString();
                    txtIdTelefono.Text = dgvData.Rows[indice].Cells["IdTelefono"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["Telefono"].Value.ToString();
                    txtIdDireccion.Text = dgvData.Rows[indice].Cells["IdDireccion"].Value.ToString();
                    txtEstado.Text = dgvData.Rows[indice].Cells["Estado"].Value.ToString();
                    txtCiudad.Text = dgvData.Rows[indice].Cells["Ciudad"].Value.ToString();
                    txtSector.Text = dgvData.Rows[indice].Cells["Sector"].Value.ToString();
                    txtCalle.Text = dgvData.Rows[indice].Cells["Calle"].Value.ToString();
                    txtNurCasa.Text = dgvData.Rows[indice].Cells["Casa"].Value.ToString();
                    txtDeuda.Text = dgvData.Rows[indice].Cells["Deuda"].Value.ToString();

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

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {

                row.Visible = true;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdCliente.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    Cliente obj = new Cliente()
                    {
                        IdCliente = Convert.ToInt32(txtIdCliente.Text)
                    };

                    bool Respuesta = new CN_Cliente().Eliminar(obj, out mensaje);

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
