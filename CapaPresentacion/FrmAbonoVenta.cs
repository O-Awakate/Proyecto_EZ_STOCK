using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.Modales;
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
    public partial class FrmAbonoVenta : Form
    {
        private ToolTip toolTip;
        public FrmAbonoVenta()
        {
            InitializeComponent();

            toolTip = new ToolTip();
            toolTip.SetToolTip(btnBuscar, "Buscar venta.");
            toolTip.SetToolTip(btnLimpiar, "Limpiar cuadros de texto.");
            toolTip.SetToolTip(btnBuscarVentas, "Buscar lista de ventas que poseen deudas.");
            toolTip.SetToolTip(txtRegistrar, "Registrar abono");
        }

        private void ObtenerVenta(string buscar)
        {
            Venta oVenta = new CN_Venta().obtenerVenta(buscar);

            if (oVenta.IdVenta > 0 && oVenta.oCredito.Deuda <= 0)
            {
                MessageBox.Show("Esta venta no posee una deuda", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (oVenta.IdVenta != 0)
            {
                txtNumDocumento.Text = oVenta.NumeroDocumento;

                txtFecha.Text = oVenta.FechaRegistro;
                txtTipoDocumento.Text = oVenta.TipoDocumento;
                txtUsuario.Text = oVenta.oUsuario.oDatosPersona.Nombre + " " + oVenta.oUsuario.oDatosPersona.Apellido;
                txtDocumento.Text = oVenta.oCliente.oDatosPersona.CI;
                txtNombreCliente.Text = oVenta.oCliente.oDatosPersona.Nombre;
                txtApellidoCliente.Text = oVenta.oCliente.oDatosPersona.Apellido;
                txtIdCredito.Text = oVenta.oCredito.IdCredito.ToString();

                dgvData.Rows.Clear();
                foreach (Detalle_Venta dv in oVenta.oDetalleVenta)
                {
                    dgvData.Rows.Add(new object[] { dv.OProducto.DescripcionProducto + " | " + dv.OProducto.MarcaProducto, dv.PrecioVenta, dv.Cantidad, dv.SubTotal });
                }

                txtMontoTotal.Text = oVenta.MontoTotal.ToString("0.00");
                txtMontoBs.Text = oVenta.MontoBs.ToString("0.00");
                txtDeuda.Text = oVenta.oCredito.Deuda.ToString("0.00");

            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ObtenerVenta(txtBusqueda.Text);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFecha.Text = "";
            txtIdCredito.Text = "0";
            txtTipoDocumento.Text = "";
            txtNumDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocumento.Text = "";
            txtNombreCliente.Text = "";
            txtApellidoCliente.Text = "";
            txtDeudafinal.Text = "";

            dgvData.Rows.Clear();
            txtMontoTotal.Text = "000";
            txtMontoBs.Text = "0.00";
            txtAbono.Text = "0.00";
            txtDeuda.Text = "0.00";
        }

        private void txtRegistrar_Click(object sender, EventArgs e)
        {
            if (txtIdCredito.Text == "")
            {
                MessageBox.Show("Seleccione primero una venta de credito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (txtAbono.Text == "")
            {
                MessageBox.Show("No esta ingresando un monto a abonar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Abono_Credito oAbono = new Abono_Credito()
            {
                oCredito = new Credito() { IdCredito = Convert.ToInt32(txtIdCredito.Text) },
                Monto = Convert.ToDecimal(txtAbono.Text)

            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Venta().Abono(oAbono, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Se actualizo la deuda", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                txtFecha.Text = "";
                txtTipoDocumento.Text = "";
                txtNumDocumento.Text = "";
                txtUsuario.Text = "";
                txtDocumento.Text = "";
                txtNombreCliente.Text = "";
                txtApellidoCliente.Text = "";

                dgvData.Rows.Clear();
                txtMontoBs.Text = "";
                txtMontoTotal.Text = "";
                txtDeuda.Text = "";
                txtAbono.Text = "";
                txtDeudafinal.Text = "";

            }

            else
                MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }

        private void calculaDeudaFinal()
        {
            if (Convert.ToDecimal(txtAbono.Text) > Convert.ToDecimal(txtMontoTotal.Text))
            {
                MessageBox.Show("No puede abonar un monto mayor a la venta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtIdCredito.Text == "")
            {
                MessageBox.Show("Seleccione primero una venta de credito", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtAbono.Text == "")
            {
                MessageBox.Show("No esta ingresando un monto a abonar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            decimal abono;
            decimal deuda = Convert.ToDecimal(txtDeuda.Text);

            if (txtAbono.Text.Trim() == "")
            {
                txtAbono.Text = "0";
            }

            if (decimal.TryParse(txtAbono.Text.Trim(), out abono))
            {
                if (abono >= deuda)
                {
                    txtDeudafinal.Text = "0.00";
                }
                else
                {
                    decimal cambio = deuda - abono;
                    txtDeudafinal.Text = cambio.ToString("0.00");
                }
            }
        }

        private void txtAbono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                calculaDeudaFinal();
            }
        }

        private void FrmAbonoVenta_Load(object sender, EventArgs e)
        {
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

        private void btnBuscarVentas_Click(object sender, EventArgs e)
        {
            using (var modal = new FrmListaCreditoVentas())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtBusqueda.Text = modal._Venta.NumeroDocumento.ToString();

                    ObtenerVenta(txtBusqueda.Text);
                }
                else
                {
                    txtDocumento.Select();
                }

            }
        }

        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                // Si no es un número ni la tecla de retroceso, no permitir la entrada
                e.Handled = true;
            }
        }

        private void txtAbono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtAbono.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                if (txtAbono.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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
    }
}
