using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmDevolucionCompra : Form
    {
        public FrmDevolucionCompra()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Compra oCompra = new CN_Compra().ObtenerCompra(txtBusqueda.Text);

            if (oCompra.IdCompra != 0)
            {
                txtIdCompra.Text = oCompra.IdCompra.ToString();
                txtIdUsuario.Text = oCompra.OUsuario.IdUsuario.ToString();
                txtIdProveedor.Text = oCompra.OProvedor.IdProveedor.ToString();
                txtFecha.Text = oCompra.FechaRegistro;
                txtTipoDocumento.Text = oCompra.TipoDocumento;
                txtNumeroDocumento.Text = oCompra.NumeroDocumento;
                txtUsuario.Text = oCompra.OUsuario.oDatosPersona.Nombre + " " + oCompra.OUsuario.oDatosPersona.Apellido;
                txtDocumento.Text = oCompra.OProvedor.oDatosPersona.CI;
                txtRazonSocial.Text = oCompra.OProvedor.oCasaProveedora.RazonSocial;
                txtRIF.Text = oCompra.OProvedor.oCasaProveedora.RIF;
                txtNombreProveedor.Text = oCompra.OProvedor.oDatosPersona.Nombre;
                txtApellidoProveedor.Text = oCompra.OUsuario.oDatosPersona.Apellido;
                txtMetodo.Text = oCompra.MetodoPago;

                if (txtMetodo.Text == "Credito")
                {
                    lblDeuda.Visible = true;
                    txtDeuda.Visible = true;
                }
                else
                {
                    lblDeuda.Visible = false;
                    txtDeuda.Visible = false;
                }

                dgvData.Rows.Clear();
                foreach (Detalle_Compra dc in oCompra.ODetalleCompra)
                {
                    dgvData.Rows.Add(new object[] { dc.OProducto.IdProducto, dc.OProducto.DescripcionProducto + " | " + dc.OProducto.MarcaProducto, dc.PrecioCompra,dc.PrecioVenta, dc.Cantidad, dc.MontoTotal });
                }

                txtMontoTotal.Text = oCompra.MontoTotal.ToString("0.00");
                txtMontoBs.Text = oCompra.MontoBs.ToString("0.00");
                txtDeuda.Text = oCompra.oCredito.Deuda.ToString("0.00");

            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocumento.Text = "";
            txtRazonSocial.Text = "";
            txtNombreProveedor.Text = "";
            txtApellidoProveedor.Text = "";
            txtMetodo.Text = "";

            dgvData.Rows.Clear();
            txtMontoTotal.Text = "0.00";
            txtMontoBs.Text = "0.00";
            txtDeuda.Text = "0.00";
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
        }

        private void DevolverProducto()
        {
            Venta TieneDeuda = new Venta();
            TieneDeuda.TieneDeuda = false;

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
                        row.Cells ["IdProducto"].Value.ToString(),
                        row.Cells ["Precio"].Value.ToString(),
                        row.Cells ["PrecioVenta"].Value.ToString(),
                        row.Cells ["Cantidad"].Value.ToString(),
                        row.Cells ["SubTotal"].Value.ToString()

                });
            }
            Compra oCompra = new Compra()
            {
                IdCompra = Convert.ToInt32(txtIdCompra.Text),
                OUsuario = new Usuario() { IdUsuario = Convert.ToInt32(txtIdUsuario.Text) },
                OProvedor = new Proveedor() { IdProveedor = Convert.ToInt32(txtIdProveedor.Text) },
                TipoDocumento = txtTipoDocumento.Text,
                NumeroDocumento = txtNumeroDocumento.Text,
                MetodoPago = txtMetodo.Text,
                MontoTotal = Convert.ToDecimal(txtMontoTotal.Text),
                MontoBs = Convert.ToDecimal(txtMontoBs.Text),
                oCredito = new Credito()

            };


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
            bool respuesta = new CN_Compra().DevolverProducto(oCompra, detalle_Compra, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Devolucion Completa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFecha.Text = "";
                txtTipoDocumento.Text = "";
                txtIdCompra.Text = "";
                txtUsuario.Text = "";
                txtDocumento.Text = "";
                txtRazonSocial.Text = "";
                txtNombreProveedor.Text = "";
                txtApellidoProveedor.Text = "";
                txtMetodo.Text = "";

                dgvData.Rows.Clear();
                txtMontoBs.Text = "";
                txtMontoTotal.Text = "";
                txtDeuda.Text = "";

            }

            else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void FrmDevolucionCompra_Load(object sender, EventArgs e)
        {
            dgvData.Columns["btnSeleccionar"].Visible = false;
            btnGuardar.Visible = false;
        }

        private void btnDevolverProducto_Click(object sender, EventArgs e)
        {
            if (txtIdCompra.Text == "")
            {
                MessageBox.Show("Selecciones una compra para devolver", "Confirmar Devolución", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btnGuardar.Visible = true;
            btnDevolverProducto.Visible = false;

            if (dgvData.Columns["btnSeleccionar"].Visible == false)
            {
                dgvData.Columns["btnSeleccionar"].Visible = true;
            }
            else
            {
                dgvData.Columns["btnSeleccionar"].Visible = false;
            }
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
                    // Obtener la cantidad actual y el ID del producto
                    int idProducto = Convert.ToInt32(dgvData.Rows[indice].Cells["IdProducto"].Value);
                    int cantidadActual = Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value);

                    // Solicitar al usuario ingresar la cantidad a devolver
                    string cantidadInput = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la cantidad a devolver:", "Cantidad a devolver", "0");

                    // Verificar si la entrada es un número válido
                    if (int.TryParse(cantidadInput, out int cantidadADevolver))
                    {
                        Console.WriteLine($"Cantidad a devolver (entrada): {cantidadADevolver}");

                        // Verificar si la cantidad a devolver es menor o igual a la cantidad actual
                        if (cantidadADevolver <= cantidadActual)
                        {
                            Console.WriteLine("Realizando la devolución...");

                            // Realizar la devolución
                            bool respuesta = new CN_Compra().RestarStock(idProducto, cantidadADevolver);

                            if (respuesta)
                            {
                                Console.WriteLine("Devolución exitosa.");

                                // Actualizar la cantidad en la celda
                                dgvData.Rows[indice].Cells["Cantidad"].Value = cantidadActual - cantidadADevolver;

                                decimal precio = Convert.ToDecimal(dgvData.Rows[indice].Cells["Precio"].Value);
                                int cantidad = Convert.ToInt32(dgvData.Rows[indice].Cells["Cantidad"].Value);
                                dgvData.Rows[indice].Cells["SubTotal"].Value = precio * cantidad;

                                calcularTotal();
                                calcularTotalBs();

                                // Verificar si la cantidad final es 0 y eliminar el producto si es necesario
                                if ((int)dgvData.Rows[indice].Cells["Cantidad"].Value == 0)
                                {
                                    Console.WriteLine("Eliminando el producto...");
                                    dgvData.Rows.RemoveAt(indice);

                                    // Verificar si ya no queda ninguna fila en el DataGridView
                                    if (dgvData.Rows.Count == 0)
                                    {
                                        // Si no quedan filas, borra la compra entera
                                        Compra ocompra = new Compra()
                                        {
                                            IdCompra = Convert.ToInt32(txtIdCompra.Text)
                                        };

                                        string mensaje = string.Empty;
                                        bool Resultado = new CN_Compra().DevolucionCompra(ocompra, out mensaje);
                                        if (Resultado)
                                        {
                                            var result = MessageBox.Show("Devolucion Completada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            txtFecha.Text = "";
                                            txtTipoDocumento.Text = "";
                                            txtIdCompra.Text = "";
                                            txtUsuario.Text = "";
                                            txtDocumento.Text = "";
                                            txtRazonSocial.Text = "";
                                            txtNombreProveedor.Text = "";
                                            txtApellidoProveedor.Text = "";
                                            txtMetodo.Text = "";

                                            dgvData.Rows.Clear();
                                            txtMontoBs.Text = "";
                                            txtMontoTotal.Text = "";
                                            txtDeuda.Text = "";

                                        }
                                    }
                                }


                            }
                            else
                            {
                                Console.WriteLine("Error al realizar la devolución.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error: No puedes devolver una cantidad mayor a la cantidad actual.");
                            MessageBox.Show("No puedes devolver una cantidad mayor a la cantidad actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: Por favor, ingrese una cantidad válida.");
                        MessageBox.Show("Por favor, ingrese una cantidad válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
            txtMontoTotal.Text = total.ToString("0.00");
        }
        private void calcularTotalBs()
        {
            Otros_Datos Dolar = new CN_OtrosDatos().obtenerOtrosDatos();
            txtMontoBs.Text = (decimal.Parse(txtMontoTotal.Text) * Dolar.ValorDolar).ToString("0.00");
        }

        void CancelCompra()
        {
            if (txtIdCompra.Text == "")
            {
                MessageBox.Show("Selecciones una compra para devolver", "Confirmar Devolución", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var confirmResult = MessageBox.Show("¿Estás seguro de que deseas realizar la devolución?", "Confirmar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                Compra ocompra = new Compra()
                {
                    IdCompra = Convert.ToInt32(txtIdCompra.Text)

                };

                string mensaje = string.Empty;
                bool respuesta = new CN_Compra().DevolucionCompra(ocompra, out mensaje);

                if (respuesta)
                {
                    var result = MessageBox.Show("Devolucion Completa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtFecha.Text = "";
                    txtTipoDocumento.Text = "";
                    txtIdCompra.Text = "";
                    txtUsuario.Text = "";
                    txtDocumento.Text = "";
                    txtRazonSocial.Text = "";
                    txtNombreProveedor.Text = "";
                    txtApellidoProveedor.Text = "";
                    txtMetodo.Text = "";

                    dgvData.Rows.Clear();
                    txtMontoBs.Text = "";
                    txtMontoTotal.Text = "";
                    txtDeuda.Text = "";

                }

                else
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void btnCancelarCompra_Click(object sender, EventArgs e)
        {
            CancelCompra();
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtMontoTotal.Text) >= 0.00M)
            {
                DevolverProducto();
            }
            else
            {
                CancelCompra();
            }

            btnGuardar.Visible = false;
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Si no es un número ni la tecla de retroceso, no permitir la entrada
                e.Handled = true;
            }
        }
    }
}
