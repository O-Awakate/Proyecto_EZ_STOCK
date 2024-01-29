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

                txtFecha.Text = oCompra.FechaRegistro;
                txtTipoDocumento.Text = oCompra.TipoDocumento;
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
                    dgvData.Rows.Add(new object[] { dc.OProducto.IdProducto, dc.OProducto.DescripcionProducto + " | " + dc.OProducto.MarcaProducto, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal });
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

        private void FrmDevolucionCompra_Load(object sender, EventArgs e)
        {
            dgvData.Columns["btnSeleccionar"].Visible = false;
        }

        private void btnDevolverProducto_Click(object sender, EventArgs e)
        {
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

                                // Verificar si la cantidad final es 0 y eliminar el producto si es necesario
                                if ((int)dgvData.Rows[indice].Cells["Cantidad"].Value == 0)
                                {
                                    Console.WriteLine("Eliminando el producto...");
                                    dgvData.Rows.RemoveAt(indice);
                                }
                                //DevolverProducto();

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

        private void btnCancelarCompra_Click(object sender, EventArgs e)
        {
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
    }
}
