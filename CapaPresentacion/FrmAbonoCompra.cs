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
        
        private void ObtenerCompra(string buscar)
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ObtenerCompra(txtBusqueda.Text);
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

        private void FrmAbonoCompra_Load(object sender, EventArgs e)
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

        private void btnBuscarCompra_Click(object sender, EventArgs e)
        {
            using (var modal = new FrmListaCreditoCompras())
            {
                var result = modal.ShowDialog();

                if (result == DialogResult.OK)
                {
                    txtBusqueda.Text = modal._Compra.NumeroDocumento.ToString();

                    ObtenerCompra(txtBusqueda.Text);
                }
                else
                {
                    txtDocumento.Select();
                }

            }
        }
    }
}
