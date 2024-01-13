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
                    dgvData.Rows.Add(new object[] { dc.OProducto.DescripcionProducto + " | " + dc.OProducto.MarcaProducto, dc.PrecioCompra, dc.Cantidad, dc.MontoTotal });
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
