using CapaEntidad;
using CapaNegocio;
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
    public partial class FrmDevolucionVenta : Form
    {
        public FrmDevolucionVenta()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Text);
            try
            {
                Console.WriteLine("Iniciando método ObtenerVenta");
                if (oVenta.IdVenta != 0)
                {
                    txtIdVenta.Text = oVenta.IdVenta.ToString();

                    txtFecha.Text = oVenta.FechaRegistro;
                    txtTipoDocumento.Text = oVenta.TipoDocumento;
                    txtNumeroDocumento.Text = oVenta.NumeroDocumento;
                    txtIdUsuario.Text = oVenta.oUsuario.IdUsuario.ToString();
                    txtUsuario.Text = oVenta.oUsuario.oDatosPersona.Nombre + " " + oVenta.oUsuario.oDatosPersona.Apellido;
                    txtIdCliente.Text = oVenta.oCliente.IdCliente.ToString();
                    txtDocumento.Text = oVenta.oCliente.oDatosPersona.CI;
                    txtNombreCliente.Text = oVenta.oCliente.oDatosPersona.Nombre;
                    txtApellidoCliente.Text = oVenta.oCliente.oDatosPersona.Apellido;
                    txtMetodo.Text = oVenta.MetodoPago;

                    if (txtMetodo.Text == "Credito")
                    {
                        lblDeuda.Visible = true;
                        lblCambio.Visible = false;
                        txtDeuda.Visible = true;
                        txtCambio.Visible = false;
                    }
                    else
                    {
                        lblCambio.Visible = true;
                        txtCambio.Visible = true;
                        lblDeuda.Visible = false;
                        txtDeuda.Visible = false;
                    }

                    dgvData.Rows.Clear();
                    foreach (Detalle_Venta dv in oVenta.oDetalleVenta)
                    {
                        dgvData.Rows.Add(new object[] { dv.OProducto.IdProducto, dv.OProducto.DescripcionProducto + " | " + dv.OProducto.MarcaProducto, dv.PrecioVenta, dv.Cantidad, dv.SubTotal });
                    }

                    txtMontoTotal.Text = oVenta.MontoTotal.ToString("0.00");
                    txtMontoBs.Text = oVenta.MontoBs.ToString("0.00");
                    txtPaga.Text = oVenta.MontoPago.ToString("0.00");
                    txtDeuda.Text = oVenta.oCredito.Deuda.ToString("0.00");
                    txtCambio.Text = oVenta.MontoCambio.ToString("0.00");

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error en ObtenerVenta: {ex.Message}");
            }

            
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdVenta.Text = "0";
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocumento.Text = "";
            txtNombreCliente.Text = "";
            txtApellidoCliente.Text = "";
            txtMetodo.Text = "";

            dgvData.Rows.Clear();
            txtMontoTotal.Text = "000";
            txtMontoBs.Text = "0.00";
            txtPaga.Text = "0.00";
            txtCambio.Text = "0.00";
            txtDeuda.Text = "0.00";
        }

        private void CancelCompra()
        {
            if (txtIdVenta.Text == "")
            {
                MessageBox.Show("Selecciones una venta para devolver", "Confirmar Devolución", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var confirmResult = MessageBox.Show("¿Estás seguro de que deseas realizar la devolución?", "Confirmar Devolución", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.Yes)
            {
                Venta oventa = new Venta()
                {
                    IdVenta = Convert.ToInt32(txtIdVenta.Text)

                };

                string mensaje = string.Empty;
                bool respuesta = new CN_Venta().DevolucionVenta(oventa, out mensaje);

                if (respuesta)
                {
                    var result = MessageBox.Show("Devolucion Completa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtFecha.Text = "";
                    txtTipoDocumento.Text = "";
                    txtIdVenta.Text = "";
                    txtUsuario.Text = "";
                    txtDocumento.Text = "";
                    txtNombreCliente.Text = "";
                    txtApellidoCliente.Text = "";

                    dgvData.Rows.Clear();
                    txtMontoBs.Text = "";
                    txtMontoTotal.Text = "";
                    txtDeuda.Text = "";

                }

                else
                    MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            CancelCompra();
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
                            bool respuesta = new CN_Venta().SumarStock(idProducto, cantidadADevolver);
                            
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

        private void DevolverProducto()
        {
            Venta TieneDeuda = new Venta();
            TieneDeuda.TieneDeuda = false;

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

            Venta oVenta = new Venta()
            {
                IdVenta = Convert.ToInt32(txtIdVenta.Text),
                oUsuario = new Usuario() { IdUsuario = Convert.ToInt32(txtIdUsuario.Text) },
                oCliente = new Cliente() { IdCliente = Convert.ToInt32(txtIdCliente.Text) },
                TipoDocumento = txtTipoDocumento.Text,
                NumeroDocumento = txtNumeroDocumento.Text,
                MetodoPago = txtMetodo.Text,
                MontoPago = Convert.ToDecimal(txtPaga.Text),
                MontoBs = Convert.ToDecimal(txtMontoBs.Text),
                MontoCambio = Convert.ToDecimal(txtCambio.Text),
                MontoTotal = Convert.ToDecimal(txtMontoTotal.Text),
                oCredito = new Credito(),
                Estado = true
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
            bool respuesta = new CN_Venta().DevolverProducto(oVenta, detalle_venta, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Devolucion Completa", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtFecha.Text = "";
                txtTipoDocumento.Text = "";
                txtIdVenta.Text = "";
                txtUsuario.Text = "";
                txtDocumento.Text = "";
                txtNombreCliente.Text = "";
                txtApellidoCliente.Text = "";

                dgvData.Rows.Clear();
                txtMontoBs.Text = "";
                txtMontoTotal.Text = "";
                txtDeuda.Text = "";

            }

            else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void btnDevolverProducto_Click(object sender, EventArgs e)
        {
            if (txtIdVenta.Text == "")
            {
                MessageBox.Show("Selecciones una Venta para devolver", "Confirmar Devolución", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            btnGuardar.Visible = true;
            btnDevolverProducto.Visible = false;

            if(dgvData.Columns["btnSeleccionar"].Visible == false)
            {
                dgvData.Columns["btnSeleccionar"].Visible = true;
            }
            else
            {
                dgvData.Columns["btnSeleccionar"].Visible = false;
            }
        }

        private void FrmDevolucionVenta_Load(object sender, EventArgs e)
        {
            dgvData.Columns["btnSeleccionar"].Visible = false;
            btnGuardar.Visible = false;
        }

        private void btnGuardar_Click_1(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtMontoTotal.Text) > 0.00M)
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
