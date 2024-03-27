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
using ClosedXML.Excel;


namespace CapaPresentacion
{
    public partial class FrmProducto : Form
    {
        private ToolTip toolTip1;
        public FrmProducto()
        {
            InitializeComponent();
            txtCantidad.Minimum = 0;
            txtCantidad.Value = 0;

            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btnBuscar, "Buscar productos filtrados.");
            toolTip1.SetToolTip(btnLimpiarBuscador, "Limpiar filtro.");
            toolTip1.SetToolTip(btnExcel, "Generar un excel con los productos visibles en la lista.");
            toolTip1.SetToolTip(btnGuardar, "Guardar producto.");
            toolTip1.SetToolTip(btnLimpiar, "Limpiar cuadros de texto.");
            toolTip1.SetToolTip(btnEliminar, "Eliminar producto.");
        }

        private void FrmProducto_Load(object sender, EventArgs e)
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

            //Mostrando Producto (todos)

            List<Producto> Lista = new CN_Producto().listar();

            foreach (Producto item in Lista)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.IdProducto,
                    item.CodigoFabrica,
                    item.CodigoAvila,
                    item.DescripcionProducto,
                    item.MarcaProducto,
                    item.MarcaCarro,
                    item.AplicaParaCarro,
                    item.Stock,
                    item.PrecioCompra,
                    item.PrecioVenta,
                    item.Estado == true ? 1 : 0,
                    item.Estado == true ? "Activo": "No Activo"
                });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            string mensaje = string.Empty;
            decimal PrecioCompra = 0;
            decimal precioVenta = 0;

            if (txtPrecCompra.Text != "")
            {
                PrecioCompra = Convert.ToDecimal(txtPrecCompra.Text);
            }

            if (txtPrecVenta.Text != "")
            {
                precioVenta = Convert.ToDecimal(txtPrecVenta.Text);
            }

            Producto obj = new Producto()
            {
                IdProducto = Convert.ToInt32(txtID.Text),
                CodigoFabrica = txtCodigoFabrica.Text,
                CodigoAvila = txtCodigoAvila.Text,
                DescripcionProducto = txtDescripcion.Text,
                MarcaProducto = txtMarcaProducto.Text,
                MarcaCarro = txtMarcaCarro.Text,
                AplicaParaCarro = txtAplicaParaCarro.Text,
                Stock = Convert.ToInt32(txtCantidad.Value),
                PrecioCompra = PrecioCompra,
                PrecioVenta = precioVenta,
                Estado = Convert.ToInt32(((OpcionCombo)cboEstado.SelectedItem).Valor) == 1 ? true : false
            };

            if (obj.IdProducto == 0)
            {

                int IdProductoGenrado = new CN_Producto().Registrar(obj, out mensaje);

                if (IdProductoGenrado != 0)
                {

                    dgvData.Rows.Add(new object[] {
                        "",
                        IdProductoGenrado,
                        txtCodigoFabrica.Text,
                        txtCodigoAvila.Text,
                        txtDescripcion.Text,
                        txtMarcaProducto.Text,
                        txtMarcaCarro.Text,
                        txtAplicaParaCarro.Text,
                        txtCantidad.Value,
                        PrecioCompra,
                        precioVenta,
                        ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString()
                    });

                    Limpiar();

                    MessageBox.Show("Se registro el producto correctamente","Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    MessageBox.Show(mensaje);
                }

            }

            else
            {
                bool resultado = new CN_Producto().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtID.Text;
                    row.Cells["CodigoFabrica"].Value = txtCodigoFabrica.Text;
                    row.Cells["CodigoAvila"].Value = txtCodigoAvila.Text;
                    row.Cells["DescripcionProducto"].Value = txtDescripcion.Text;
                    row.Cells["MarcaProducto"].Value = txtMarcaProducto.Text;
                    row.Cells["MarcaCarro"].Value = txtMarcaCarro.Text;
                    row.Cells["AplicaParaCarro"].Value = txtAplicaParaCarro.Text;
                    row.Cells["EstadoValor"].Value = ((OpcionCombo)cboEstado.SelectedItem).Valor.ToString();
                    row.Cells["Estado"].Value = ((OpcionCombo)cboEstado.SelectedItem).Texto.ToString();

                    Limpiar();

                    MessageBox.Show("El producto ha sido editado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            txtID.Text = "0";
            txtCodigoFabrica.Text = "";
            txtCodigoAvila.Text = "";
            txtDescripcion.Text = "";
            txtMarcaProducto.Text = "";
            txtMarcaCarro.Text = "";
            txtAplicaParaCarro.Text = "";
            cboEstado.SelectedIndex = 0;
            txtPrecCompra.Text = "";
            txtPrecVenta.Text = "";
            txtCantidad.Value = 0;

            txtCodigoFabrica.Select();


        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 0)
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

                int Indice = e.RowIndex;

                if (Indice >= 0)
                {
                    txtIndice.Text = Indice.ToString();
                    txtID.Text = dgvData.Rows[Indice].Cells["Id"].Value.ToString();
                    txtCodigoFabrica.Text = dgvData.Rows[Indice].Cells["CodigoFabrica"].Value.ToString();
                    txtCodigoAvila.Text = dgvData.Rows[Indice].Cells["CodigoAvila"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[Indice].Cells["DescripcionProducto"].Value.ToString();
                    txtMarcaProducto.Text = dgvData.Rows[Indice].Cells["MarcaProducto"].Value.ToString();
                    txtMarcaCarro.Text = dgvData.Rows[Indice].Cells["MarcaCarro"].Value.ToString();
                    txtAplicaParaCarro.Text = dgvData.Rows[Indice].Cells["AplicaParaCarro"].Value.ToString();

                    
                    foreach (OpcionCombo oc in cboEstado.Items)
                    {

                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[Indice].Cells["EstadoValor"].Value))
                        {

                            int indice_Combo = cboEstado.Items.IndexOf(oc);
                            cboEstado.SelectedIndex = indice_Combo;
                            break;

                        }

                    }
                }

            }
        
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtID.Text) != 0)
            {
                if (MessageBox.Show("¿Desea eliminar el Producto?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;

                    Producto obj = new Producto()
                    {
                        IdProducto = Convert.ToInt32(txtID.Text)
                    };

                    bool Respuesta = new CN_Producto().Eliminar(obj, out mensaje);

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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("No hay datos para exportar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();

                foreach (DataGridViewColumn column in dgvData.Columns)
                {
                    if (column.HeaderText != "" && column.Visible)
                        dt.Columns.Add(column.HeaderText, typeof(string));
                }
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Visible)
                        dt.Rows.Add(new object[]
                        {
                            row.Cells[2].Value.ToString(),
                            row.Cells[3].Value.ToString(),
                            row.Cells[4].Value.ToString(),
                            row.Cells[5].Value.ToString(),
                            row.Cells[6].Value.ToString(),
                            row.Cells[7].Value.ToString(),
                            row.Cells[8].Value.ToString(),
                            row.Cells[9].Value.ToString(),
                            row.Cells[10].Value.ToString(),
                            row.Cells[12].Value.ToString(),
                        });
                }

                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = string.Format("ReporteProducto_{0}.xlsx", DateTime.Now.ToString("ddMMyyyyHHmmss"));
                savefile.Filter = "Excel Files | *.xlsx*";

                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XLWorkbook wb = new XLWorkbook();
                        var hoja = wb.Worksheets.Add(dt, "Informe");
                        hoja.ColumnsUsed().AdjustToContents();
                        wb.SaveAs(savefile.FileName);
                        MessageBox.Show("Reporte generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    catch
                    {
                        MessageBox.Show("Error al generar reporte", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void txtCodigoFabrica_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtCodigoFabrica.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtCodigoAvila_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back || txtCodigoFabrica.Text.Length >= 10 && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ignora el carácter
            }
        }

        private void txtPrecCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                if (txtPrecCompra.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ",")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
            }
        }

        private void txtPrecVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                if (txtPrecVenta.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
                {
                    e.Handled = true;
                }
                else
                {
                    if (Char.IsControl(e.KeyChar) || e.KeyChar.ToString() == ",")
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }

                }
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '0')
            {
                e.Handled = true; // Ignorar el carácter
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtDescripcion.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtMarcaProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtMarcaProducto.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtAplicaParaCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtAplicaParaCarro.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }

        private void txtMarcaCarro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtMarcaCarro.Text.Length >= 20 && e.KeyChar != (char)Keys.Back)
            {
                // Si no es una letra ni la tecla "Enter", ignorar el caracter
                e.Handled = true;
            }
        }
    }
}
