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
        private List<Producto> productosEnVenta = new List<Producto>();

        private Usuario _Usuario;

        public FrmVentaRegistro(Usuario oUsuario = null)
        {
            //Objeto para obtener datos del Usuario Actual
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void FrmVentaRegistro_Load(object sender, EventArgs e)
        {
            txtCodigoAvila.ShortcutsEnabled = false;
            txtPrecVenta.ShortcutsEnabled = false;

            //Llenando el ComboBox de Tipo Documento
            cboTipDocumento.Items.Add(new OpcionCombo() { Valor = "Recibo", Texto = "Recibo" });
            cboTipDocumento.DisplayMember = "Texto";
            cboTipDocumento.ValueMember = "Valor";
            cboTipDocumento.SelectedIndex = 0;

            //Llenando el ComboBox de Metodo de pago
            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Efectivo", Texto = "Efectivo" });
            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Divisa", Texto = "Divisa" });
            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Pago Movil", Texto = "Pago Movil" });
            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Credito", Texto = "Credito" });
            cboMetodo.DisplayMember = "Texto";
            cboMetodo.ValueMember = "Valor";
            cboMetodo.SelectedIndex = 0;
            cboMetodo.SelectedIndexChanged += cboMetodo_SelectedIndexChanged;

            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtIdCliente.Text = "0";
            txtIdProducto.Text = "0";

            txtPago.Text = "";
            txtCambio.Text = "";
            txtTotalPagar.Text = "0";
            txtMontoBs.Text = "0";

            DateTime fechaActual = DateTime.Now;

            Otros_Datos datos = new CN_OtrosDatos().obtenerOtrosDatos();

            if (DateTime.TryParse(datos.FechaRegistro, out DateTime fechaValorDolar))
            {
                if (fechaActual.Date > fechaValorDolar.Date || (fechaActual.Date == fechaValorDolar.Date && fechaActual.Hour >= 9 && fechaValorDolar.Hour < 9))
                {
                    // Si la fecha actual es después de la fecha de FechaRegistro o es el mismo día y la hora actual es después de las 9 AM y la hora de FechaRegistro es antes de las 9 AM
                    txtMontoBs.BackColor = Color.MistyRose;
                }
                else if (fechaActual.Date == fechaValorDolar.Date && fechaActual.Hour >= 13 && fechaValorDolar.Hour < 13)
                {
                    // Si la fecha actual es igual a la fecha de FechaRegistro y la hora actual es después de la 1 PM
                    txtMontoBs.BackColor = Color.MistyRose;
                }
            }


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
                    if (modal._Producto.Stock == 0)
                    {
                        MessageBox.Show("El stock no es suficiente para este producto.", "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        txtIdProducto.Text = modal._Producto.IdProducto.ToString();
                        txtCodigoAvila.Text = modal._Producto.CodigoAvila;
                        txtCodigoFabrica.Text = modal._Producto.CodigoFabrica;
                        txtMarcaProducto.Text = modal._Producto.MarcaProducto;
                        txtMarcaCarro.Text = modal._Producto.MarcaCarro;
                        txtDescripcion.Text = modal._Producto.DescripcionProducto;
                        txtAplica.Text = modal._Producto.AplicaParaCarro;
                        txtPrecVenta.Text = modal._Producto.PrecioVenta.ToString("0.00");
                        txtStock.Text = modal._Producto.Stock.ToString();
                        txtPrecioCompra.Text = modal._Producto.PrecioCompra.ToString("0.00");
                        txtCantidad.Select();
                    }
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

            if (decimal.Parse(txtPrecVenta.Text) < decimal.Parse(txtPrecioCompra.Text))
            {
                MessageBox.Show("El precio de venta no puede ser menor al precio de compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                

                // Restar el stock visualmente en el DataGridView
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
            txtCambio.Text = "0.00";
            if (txtTotalPagar.Text.Trim() == "0")
            {
                MessageBox.Show("No existen productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal pagaCon;
            decimal total = Convert.ToDecimal(txtTotalPagar.Text);
            decimal Bs = Convert.ToDecimal(txtMontoBs.Text);

            if (txtPago.Text.Trim() == "")
            {
                txtPago.Text = "0";

            }

            string metodoSeleccionado = Convert.ToString(((OpcionCombo)cboMetodo.SelectedItem).Valor);

            if (metodoSeleccionado == "Pago Movil" || metodoSeleccionado == "Efectivo")
            {
                if (decimal.TryParse(txtPago.Text.Trim(), out pagaCon))
                {
                    if (pagaCon == Bs)
                    {
                        txtCambio.Text = "0.00";
                    }
                    else
                    {
                        decimal cambio = pagaCon - Bs;
                        txtCambio.Text = cambio.ToString("0.00");
                    }
                }
            }
            else
            {
                if (txtCambio.Visible == true)
                {
                    if (decimal.TryParse(txtPago.Text.Trim(), out pagaCon))
                    {
                        if (pagaCon == total)
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
                else
                {
                    if (decimal.TryParse(txtPago.Text.Trim(), out pagaCon))
                    {
                        decimal Deuda = total - pagaCon;
                        txtDeuda.Text = Deuda.ToString("0.00");
                    }
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
            calcularCambio();

            var confirmResult = MessageBox.Show("¿Estás seguro de que deseas realizar la venta?", "Confirmar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                Venta TieneDeuda = new Venta();
                double cambio = 0.00;
                TieneDeuda.TieneDeuda = false;

                if (double.TryParse(txtCambio.Text, out cambio))
                {
                    string metodoSeleccionado = Convert.ToString(((OpcionCombo)cboMetodo.SelectedItem).Valor);

                    if (metodoSeleccionado != "Credito" && cambio < 0)
                    {
                        MessageBox.Show("Actualice método de pago", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                if (dgvData.Rows.Count < 1)
                {
                    MessageBox.Show("Debe ingresar productos en la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtPago.Text == "")
                {
                    MessageBox.Show("No inserto pago cliente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

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

                foreach (DataGridViewRow fila in dgvData.Rows)
                {
                    int idProducto = Convert.ToInt32(fila.Cells["IdProducto"].Value);
                    int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);

                    bool resta = new CN_Venta().RestarStock(idProducto, cantidad);
                    // Aquí puedes manejar la respuesta si es necesario
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
                    oCliente = new Cliente() { IdCliente = Convert.ToInt32(txtIdCliente.Text) },
                    TipoDocumento = ((OpcionCombo)cboTipDocumento.SelectedItem).Texto,
                    NumeroDocumento = NumeroDocumento,
                    MetodoPago = ((OpcionCombo)cboMetodo.SelectedItem).Texto,
                    MontoPago = Convert.ToDecimal(txtPago.Text),
                    MontoBs = Convert.ToDecimal(txtMontoBs.Text),
                    MontoCambio = Convert.ToDecimal(txtCambio.Text),
                    MontoTotal = Convert.ToDecimal(txtTotalPagar.Text),
                    oCredito = new Credito()
                };

                if (oVenta.MetodoPago == "Credito")
                {
                    oVenta.TieneDeuda = true;

                    if (oVenta.oCredito != null)
                    {
                        oVenta.oCredito.Deuda = Convert.ToDecimal(txtDeuda.Text);
                    }
                }
                else
                {
                    oVenta.TieneDeuda = false;
                }

                
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
                    cboMetodo.SelectedIndex = 0;
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

        private void cboMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metodoSeleccionado = Convert.ToString(((OpcionCombo)cboMetodo.SelectedItem).Valor);

            if (metodoSeleccionado == "Credito")
            {
                lblCambio.Visible = false;
                lblDeuda.Visible = true;
                txtDeuda.Visible = true;
                txtCambio.Visible = false;
            }
            else
            {
                lblCambio.Visible = true;
                lblDeuda.Visible = false;
                txtDeuda.Visible = false;
                txtCambio.Visible = true;
            }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != '0')
            {
                e.Handled = true; 
            }
        }

        private void txtCodigoAvila_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter || txtCodigoAvila.Text.Length >= 10 && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Enter)
            {
                e.Handled = true; 
            }
        }
        
    }
}
