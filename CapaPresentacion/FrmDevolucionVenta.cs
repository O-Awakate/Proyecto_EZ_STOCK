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


            if (oVenta.IdVenta != 0)
            {
                txtIdVenta.Text = oVenta.IdVenta.ToString();

                txtFecha.Text = oVenta.FechaRegistro;
                txtTipoDocumento.Text = oVenta.TipoDocumento;
                txtUsuario.Text = oVenta.oUsuario.oDatosPersona.Nombre + " " + oVenta.oUsuario.oDatosPersona.Apellido;
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
                    dgvData.Rows.Add(new object[] { dv.OProducto.DescripcionProducto + " | " + dv.OProducto.MarcaProducto, dv.PrecioVenta, dv.Cantidad, dv.SubTotal });
                }

                txtMontoTotal.Text = oVenta.MontoTotal.ToString("0.00");
                txtMontoBs.Text = oVenta.MontoBs.ToString("0.00");
                txtPaga.Text = oVenta.MontoPago.ToString("0.00");
                txtDeuda.Text = oVenta.oCredito.Deuda.ToString("0.00");
                txtCambio.Text = oVenta.MontoCambio.ToString("0.00");

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

        private void btnGuardar_Click(object sender, EventArgs e)
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
}
