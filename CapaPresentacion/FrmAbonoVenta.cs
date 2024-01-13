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
        public FrmAbonoVenta()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Text);

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

        
    }
}
