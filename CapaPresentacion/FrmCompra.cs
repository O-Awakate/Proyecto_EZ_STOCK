using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
using CapaPresentacion.Utilidades ;
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
    public partial class FrmCompra : Form
    {
        private Usuario _Usuario;

        public FrmCompra(Usuario oUsuario = null)
        {
            _Usuario = oUsuario;
            InitializeComponent();
        }

        private void FrmRegIngresoProducto_Load(object sender, EventArgs e)
        {
            cboTipDocumento.Items.Add(new OpcionCombo() { Valor = "Recibo", Texto = "Recibo" });
            cboTipDocumento.DisplayMember = "Texto";
            cboTipDocumento.ValueMember = "Valor";
            cboTipDocumento.SelectedIndex = 0;

            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Efectivo", Texto = "Efectivo" });
            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Divisa", Texto = "Divisa" });
            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Pago Movil", Texto = "Pago Movil" });
            cboMetodo.Items.Add(new OpcionCombo() { Valor = "Credito", Texto = "Credito" });
            cboMetodo.DisplayMember = "Texto";
            cboMetodo.ValueMember = "Valor";
            cboMetodo.SelectedIndex = 0;
            cboMetodo.SelectedIndexChanged += cboMetodo_SelectedIndexChanged;


            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");

            txtIdProv.Text = "0";
            txtIdProducto.Text = "0";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (var modal = new FrmListaProveedores())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtIdProv.Text = modal._Provedor.IdProveedor.ToString();
                    txtDocumento.Text = modal._Provedor.oDatosPersona.CI;
                    txtRazonSocial.Text = modal._Provedor.oCasaProveedora.RazonSocial;
                    txtNombreProveedor.Text = modal._Provedor.oDatosPersona.Nombre;
                    txtApellidoProveedor.Text = modal._Provedor.oDatosPersona.Apellido;
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
                    txtPrecCompra.Select();
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
                Producto oProducto = new CN_Producto().listar().Where(p => p.CodigoAvila == txtCodigoAvila .Text && p.Estado == true).FirstOrDefault();

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
                    txtPrecCompra.Select();
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
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            decimal precioCompra = 0;
            decimal PrecioVenta = 0;
            bool Producto_Existe = false;

            if (txtPrecCompra.Text == "")
            {
                MessageBox.Show("Asegurese de establecer un precio para la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtPrecVenta.Text == "")
            {
                MessageBox.Show("Asegurese de establecer un precio para la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (decimal.Parse(txtPrecCompra.Text) > decimal.Parse(txtPrecVenta.Text))
            {
                MessageBox.Show("El precio de venta no puede ser menor al de la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (int.Parse(txtIdProducto.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un producto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!decimal.TryParse(txtPrecCompra.Text, out precioCompra))
            {
                MessageBox.Show("Precio compra - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecCompra.Select();
                return;
            }
            if (!decimal.TryParse(txtPrecVenta.Text, out PrecioVenta))
            {
                MessageBox.Show("Precio venta - Formato moneda incorrecto", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrecVenta.Select();
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
                dgvData.Rows.Add(new object[]
                {
                    txtIdProducto.Text,
                    txtDescripcion.Text + " | " +txtMarcaProducto.Text,
                    precioCompra.ToString("0.00"),
                    PrecioVenta.ToString("0.00"),
                    txtCantidad.Value.ToString(),
                    (txtCantidad.Value * precioCompra).ToString("0.00")

                });
                calcularTotal();
                calcularTotalBs();
                LimpiarProducto();
                
                txtCodigoAvila.Select();
            }
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
            txtPrecCompra.Text = "";
            txtPrecVenta.Text = "";
            txtCantidad.Value = 1;

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
            
            txtMontoBs.Text = ( decimal.Parse(txtTotalPagar.Text) * Dolar.ValorDolar).ToString("0.00");
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            if (e.ColumnIndex == 6)
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
                    dgvData.Rows.RemoveAt(indice);
                    calcularTotal();
                    calcularTotalBs();
                }

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

        private void txtRegistrar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtIdProv.Text) == 0)
            {
                MessageBox.Show("Debe seleccionar un proveedor", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dgvData.Rows.Count < 1)
            {
                MessageBox.Show("Debe ingresar productos en la compra", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable detalle_Compra = new DataTable();
            detalle_Compra.Columns.Add("IdProducto", typeof(int));
            detalle_Compra.Columns.Add("PrecioCompra", typeof(decimal));
            detalle_Compra.Columns.Add("PrecioVenta", typeof(decimal));
            detalle_Compra.Columns.Add("Cantidad", typeof(int));
            detalle_Compra.Columns.Add("MontoTotal", typeof(decimal));

            foreach (DataGridViewRow row in dgvData.Rows)
            {
                detalle_Compra.Rows.Add(
                    new object[]{
                        Convert.ToInt32(row.Cells ["IdProducto"].Value.ToString()),
                        row.Cells ["PrecioCompra"].Value.ToString(),
                        row.Cells ["PrecioVenta"].Value.ToString(),
                        row.Cells ["Cantidad"].Value.ToString(),
                        row.Cells ["SubTotal"].Value.ToString()

                });
            }

            int IdCorrelativo = new CN_Compra().ObtenerCorrelativo();
            string NumeroDocumento = string.Format("{0:00000}", IdCorrelativo);

            Compra oCompra = new Compra()
            {
                OUsuario = new Usuario() { IdUsuario = _Usuario.IdUsuario },
                OProvedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtIdProv.Text) },
                TipoDocumento = ((OpcionCombo)cboTipDocumento.SelectedItem).Texto,
                NumeroDocumento = NumeroDocumento,
                MetodoPago = ((OpcionCombo)cboMetodo.SelectedItem).Texto,
                MontoTotal = Convert.ToDecimal(txtTotalPagar.Text),
                MontoBs = Convert.ToDecimal(txtMontoBs.Text),
                oCredito = new Credito()

            };

            Console.WriteLine("Valor de oCompra.MetodoPago: '" + oCompra.MetodoPago + "'");

            if (oCompra.MetodoPago == "Credito")
            {
                oCompra.TieneDeuda = true;

                if (oCompra.oCredito != null)
                {
                    oCompra.oCredito.Deuda = Convert.ToDecimal(txtDeuda.Text);
                }
            }
            else
            {
                oCompra.TieneDeuda = false;
            }

            string mensaje = string.Empty;
            bool Respuesta = new CN_Compra().Registrar(oCompra, detalle_Compra, out mensaje);

            if (Respuesta)
            {
                var result = MessageBox.Show("Numero de compra generada:\n" + NumeroDocumento + "\n\n¿Desea copiar al porta papeles?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                    Clipboard.SetText(NumeroDocumento);

                txtIdProv.Text = "0";
                txtDocumento.Text = "";
                txtNombreProveedor.Text = "";
                txtApellidoProveedor.Text = "";
                txtRazonSocial.Text = "";
                cboMetodo.SelectedIndex = 0;
                dgvData.Rows.Clear();
                calcularTotal();
                calcularTotalBs();
            }

            else
            {
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void cboMetodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string metodoSeleccionado = Convert.ToString ( ((OpcionCombo)cboMetodo.SelectedItem).Valor);

            if (metodoSeleccionado == "Credito")
            {
                lblDeuda.Visible = true;
                txtDeuda.Visible = true;
                txtIdCredito.Visible = true;
            }
            else
            {
                lblDeuda.Visible = false;
                txtDeuda.Visible = false;
                txtIdCredito.Visible = false;
            }

        }
    }
}
