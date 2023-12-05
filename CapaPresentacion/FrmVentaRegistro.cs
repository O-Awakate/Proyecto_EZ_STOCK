using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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
    public partial class FrmVentaRegistro : Form
    {
        private Usuario _Usuario;

        public FrmVentaRegistro(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void FrmVentaRegistro_Load(object sender, EventArgs e)
        {
            cboTipDocumento.Items.Add(new OpcionCombo() { Valor = "Recibo", Texto = "Recibo" });
            cboTipDocumento.DisplayMember = "Texto";
            cboTipDocumento.ValueMember = "Valor";
            cboTipDocumento.SelectedIndex = 0;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdCliente.Text = "0";
            txtIdProducto.Text = "0";

            txtPago.Text = "";
            txtCambio.Text = "";
            txtTotalPagar.Text = "0";
            txtMontoBs.Text = "0";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var modal = new FrmListaClientes())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdCliente.Text = modal._Cliente.IdCliente.ToString();
                    txtDocumento.Text = modal._Cliente.oDatosPersona.CI;
                    txtNombreCompleto.Text = modal._Cliente.oDatosPersona.Nombre;
                    txtApellido.Text = modal._Cliente.oDatosPersona.Apellido;
                }
                else
                {
                    txtDocumento.Select();
                }

            }
        }

        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            using (var modal = new FrmListaProductos())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProducto.Text = modal._Producto.IdProducto.ToString();
                    txtCodigoAvila.Text = modal._Producto.CodigoAvila;
                    txtCodigoFabrica.Text = modal._Producto.CodigoFabrica;
                    txtMarcaProducto.Text = modal._Producto.MarcaProducto;
                    txtMarcaCarro.Text = modal._Producto.MarcaCarro;
                    txtDescripcion.Text = modal._Producto.DescripcionProducto;
                    txtCategoria.Text = modal._Producto.oCategoria.NombreCategoria;
                    txtAplica.Text = modal._Producto.AplicaParaCarro;
                    txtPrecVenta.Text = modal._Producto.PrecioVenta.ToString("0.00");
                    txtStock.Text = modal._Producto.Stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodigoAvila.Select();
                }

            }
        }

        private void txtCodigoAvila_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Producto oProducto = new CN_Producto().listar().Where(p => p.CodigoAvila == txtCodigoAvila.Text && p.Estado == true).FirstOrDefault();

                if (oProducto != null)
                {
                    txtCodigoAvila.BackColor = Color.Honeydew;
                    txtIdProducto.Text = oProducto.IdProducto.ToString();
                    txtCodigoFabrica.Text = oProducto.CodigoFabrica;
                    txtMarcaProducto.Text = oProducto.MarcaProducto;
                    txtMarcaCarro.Text = oProducto.MarcaCarro;
                    txtDescripcion.Text = oProducto.DescripcionProducto;
                    txtCategoria.Text = oProducto.oCategoria.NombreCategoria;
                    txtAplica.Text = oProducto.AplicaParaCarro;
                    txtPrecVenta.Text = oProducto.PrecioVenta.ToString("0.00");
                    txtStock.Text = oProducto.Stock.ToString();
                    txtCantidad.Select();
                }
                else
                {
                    txtCodigoAvila.BackColor = Color.MistyRose;
                    txtIdProducto.Text = "0";
                    txtCodigoFabrica.Text = "";
                    txtMarcaProducto.Text = "";
                    txtMarcaCarro.Text = "";
                    txtDescripcion.Text = "";
                    txtCategoria.Text = "";
                    txtAplica.Text = "";
                    txtPrecVenta.Text = "";
                    txtStock.Text = "";
                    txtCantidad.Value = 1;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal PrecioVenta = 0;
            bool Producto_Existe = false;

            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!decimal.TryParse(txtPrecVenta.Text, out PrecioVenta))
            {
                MessageBox.Show("Precio venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecVenta.Select();
                return;
            }

            if (Convert.ToInt32(txtStock.Text) < Convert.ToInt32(txtCantidad.Value.ToString()))
            {
                MessageBox.Show("No existe stock suficiente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataGridViewRow fila in dgvData.Rows)
            {
                if (fila.Cells["IdProducto"].Value.ToString() == txtIdProducto.Text)
                {
                    Producto_Existe = true;
                    break;
                }
            }

            if (!Producto_Existe)
            {
                string mensaje = string.Empty;
                bool respuesta = new CN_Venta().RestarStock(
                    Convert.ToInt32(txtIdProducto.Text),
                    Convert.ToInt32(txtCantidad.Value.ToString())
                    );

                if (respuesta)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        txtIdProducto.Text,
                        txtDescripcion.Text + " | " +txtMarcaProducto.Text,
                        PrecioVenta.ToString("0.00"),
                        txtCantidad.Value.ToString(),
                        (txtCantidad.Value * PrecioVenta).ToString("0.00")

                    });
                    calcularTotal();
                    calcularTotalBs();
                    LimpiarProducto();

                    txtCodigoAvila.Select();
                }

                
            }
        }

        private void calcularTotal()
        {
            decimal total = 0;
            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                    total += Convert.ToDecimal(row.Cells["SubTotal"].Value.ToString());
            }
            txtTotalPagar.Text = total.ToString("0.00");
        }

        private void calcularTotalBs()
        {
            Otros_Datos Dolar = new CN_OtrosDatos().obtenerOtrosDatos();
            txtMontoBs.Text = (decimal.Parse(txtTotalPagar.Text) * Dolar.ValorDolar).ToString("0.00");
        }

        private void LimpiarProducto()
        {

            txtIdProducto.Text = "0";
            txtCodigoAvila.Text = "";
            txtCodigoAvila.BackColor = Color.White;
            txtCodigoFabrica.Text = "";
            txtMarcaProducto.Text = "";
            txtMarcaCarro.Text = "";
            txtDescripcion.Text = "";
            txtCategoria.Text = "";
            txtAplica.Text = "";
            txtPrecVenta.Text = "";
            txtStock.Text = "";
            txtCantidad.Value = 1;

        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 5)
            {

                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.clear_box_outline__1_.Width;
                var h = Properties.Resources.clear_box_outline__1_.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.clear_box_outline__1_, new Rectangle(x, y, w, h));
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
                    bool respuesta = new CN_Venta().SumarStock(
                        Convert.ToInt32(dgvData.Rows[indice].Cells["IdProducto"].Value.ToString()),
                        Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value.ToString())
                        );
                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(indice);
                        calcularTotal();
                        calcularTotalBs();
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

        private void txtPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPago.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                if (txtPago.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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

        private void calcularCambio()
        {
            if (txtTotalPagar.Text.Trim() == "")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagaCon;
            decimal total = Convert.ToDecimal(txtTotalPagar.Text);

            if (txtPago.Text.Trim() == "")
            {
                txtPago.Text = "0";
            }

            if (decimal.TryParse(txtPago.Text.Trim(), out pagaCon))
            {
                if (pagaCon < total)
                {
                    txtCambio.Text = "0.00";
                }
                else
                {
                    decimal cambio = pagaCon - total;
                    txtCambio.Text = cambio.ToString("0.00");
                }
            }
        }

        private void txtPago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calcularCambio();
            }
        }

        private void txtRegistrar_Click(object sender, EventArgs e)
        {
            Compra TieneDeuda = new Compra();

            TieneDeuda.TieneDeuda = false;

            if (txtDocumento.Text == "")
            {
                MessageBox.Show("Debe ingresar documento del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtNombreCompleto.Text == "")
            {
                MessageBox.Show("Debe ingresar nombre del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtApellido.Text == "")
            {
                MessageBox.Show("Debe ingresar Apellido del cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_venta = new DataTable();

            detalle_venta.Columns.Add("IdProducto", typeof(int));
            detalle_venta.Columns.Add("Precio", typeof(decimal));
            detalle_venta.Columns.Add("Cantidad", typeof(int));
            detalle_venta.Columns.Add("SubTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                detalle_venta.Rows.Add(new object[] {
                    row.Cells["IdProducto"].Value.ToString(),
                    row.Cells["Precio"].Value.ToString(),
                    row.Cells["Cantidad"].Value.ToString(),
                    row.Cells["SubTotal"].Value.ToString()
                });
            }

            int idCorrelativo = new CN_Venta().ObtenerCorrelativo();
            string NumeroDocumento = string.Format("{0:00000}", idCorrelativo);
            calcularCambio();

            Venta oVenta = new Venta()
            {
                oUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                oCliente = new Cliente() { IdCliente = Convert.ToInt32(txtIdCliente.Text)},
                TipoDocumento = ((OpcionCombo)cboTipDocumento.SelectedItem).Texto,
                NumeroDocumento = NumeroDocumento,
                MontoPago = Convert.ToDecimal(txtPago.Text),
                MontoBs = Convert.ToDecimal(txtMontoBs.Text),
                MontoCambio = Convert.ToDecimal(txtCambio.Text),
                MontoTotal = Convert.ToDecimal(txtTotalPagar.Text)
            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Registrar(oVenta, detalle_venta, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Numero de venta generada:\n" + NumeroDocumento + "\n\n¿Desea copiar al portapapeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(NumeroDocumento);

                txtIdCliente.Text = "0";
                txtDocumento.Text = "";
                txtNombreCompleto.Text = "";
                txtApellido.Text = "";
                dgvData.Rows.Clear();
                calcularTotal();
                txtPago.Text = "";
                txtCambio.Text = "";
                calcularTotalBs();
            }

            else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }
    }
}
