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
    public partial class FrmAbonoCompra : Form
    {
        public FrmAbonoCompra()
        {
            InitializeComponent();
        }
        
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Compra oCompra = new CN_Compra().ObtenerCompra(txtBusqueda.Text);

            if (oCompra.IdCompra != 0 && oCompra.oCredito.Deuda <= 0)
            {
                MessageBox.Show("Esta venta no posee una deuda", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (oCompra.IdCompra != 0)
            {

                txtNumDocumento.Text = oCompra.NumeroDocumento;

                txtFecha.Text = oCompra.FechaRegistro;
                txtTipoDocumento.Text = oCompra.TipoDocumento;
                txtUsuario.Text = oCompra.OUsuario.oDatosPersona.Nombre + " " + oCompra.OUsuario.oDatosPersona.Apellido;
                txtDocumento.Text = oCompra.OProvedor.oDatosPersona.CI;
                txtRazonSocial.Text = oCompra.OProvedor.oCasaProveedora.RazonSocial;
                txtRIF.Text = oCompra.OProvedor.oCasaProveedora.RIF;
                txtNombreProveedor.Text = oCompra.OProvedor.oDatosPersona.Nombre;
                txtApellidoProveedor.Text = oCompra.OUsuario.oDatosPersona.Apellido;
                txtIdCredito.Text = oCompra.oCredito.IdCredito.ToString();

                dgvData.Rows.Clear();
                foreach (Detalle_Compra dv in oCompra.ODetalleCompra)
                {
                    dgvData.Rows.Add(new object[] { dv.OProducto.DescripcionProducto + " | " + dv.OProducto.MarcaProducto, dv.PrecioVenta, dv.Cantidad, dv.MontoTotal });
                }

                txtMontoTotal.Text = oCompra.MontoTotal.ToString("0.00");
                txtMontoBs.Text = oCompra.MontoBs.ToString("0.00");
                txtDeuda.Text = oCompra.oCredito.Deuda.ToString("0.00");

            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtIdCredito.Text = "0";
            txtFecha.Text = "";
            txtTipoDocumento.Text = "";
            txtUsuario.Text = "";
            txtDocumento.Text = "";
            txtRazonSocial.Text = "";
            txtNombreProveedor.Text = "";
            txtApellidoProveedor.Text = "";
            txtRIF.Text = "";

            dgvData.Rows.Clear();
            txtMontoBs.Text = "";
            txtMontoTotal.Text = "";
            txtDeuda.Text = "";
            txtAbono.Text = "";
            txtDeudafinal.Text = "";

        }

        private void txtRegistrar_Click(object sender, EventArgs e)
        {
            Abono_Credito oAbono = new Abono_Credito()
            {
                oCredito = new Credito() { IdCredito = Convert.ToInt32(txtIdCredito.Text) },
                Monto = Convert.ToDecimal(txtAbono.Text)

            };

            string mensaje = string.Empty;
            bool respuesta = new CN_Compra().Abono(oAbono, out mensaje);

            if (respuesta)
            {
                var result = MessageBox.Show("Se actualizo la deuda", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtIdCredito.Text = "0";
                txtFecha.Text = "";
                txtTipoDocumento.Text = "";
                txtUsuario.Text = "";
                txtDocumento.Text = "";
                txtRazonSocial.Text = "";
                txtNombreProveedor.Text = "";
                txtApellidoProveedor.Text = "";
                txtRIF.Text = "";

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
