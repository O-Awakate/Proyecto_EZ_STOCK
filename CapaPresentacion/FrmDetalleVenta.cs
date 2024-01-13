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

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;

namespace CapaPresentacion
{
    public partial class FrmDetalleVenta : Form
    {
        public FrmDetalleVenta()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Venta oVenta = new CN_Venta().obtenerVenta(txtBusqueda.Text);


            if (oVenta.IdVenta != 0)
            {
                txtNumDocumento.Text = oVenta.NumeroDocumento;

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

        private void btnpdf_Click(object sender, EventArgs e)
        {
            if (txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string texto_HTML = Properties.Resources.Plantilla_Venta.ToString();
            Negocio oDatos = new CN_OtrosDatos().obtenerDatos();

            texto_HTML = texto_HTML.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            texto_HTML = texto_HTML.Replace("@docnegocio", oDatos.RIF);
            texto_HTML = texto_HTML.Replace("@direcnegocio", oDatos.Direccion);

            texto_HTML = texto_HTML.Replace("@tipodocumento", txtTipoDocumento.Text.ToUpper());
            texto_HTML = texto_HTML.Replace("@numerodocumento", txtNumDocumento.Text);


            texto_HTML = texto_HTML.Replace("@doccliente", txtDocumento.Text);
            texto_HTML = texto_HTML.Replace("@nombrecliente", txtNombreCliente.Text);
            texto_HTML = texto_HTML.Replace("@apellidocliente", txtApellidoCliente.Text);
            texto_HTML = texto_HTML.Replace("@fecharegistro", txtFecha.Text);
            texto_HTML = texto_HTML.Replace("@usuarioregistro", txtUsuario.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioVenta"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";

            }
            texto_HTML = texto_HTML.Replace("@filas", filas);
            texto_HTML = texto_HTML.Replace("@montototal", txtMontoTotal.Text);
            texto_HTML = texto_HTML.Replace("@montobs", txtMontoTotal.Text);
            texto_HTML = texto_HTML.Replace("@pagocon", txtPaga.Text);
            texto_HTML = texto_HTML.Replace("@cambio", txtCambio.Text);
            texto_HTML = texto_HTML.Replace("@metodopago", txtMontoTotal.Text);
            texto_HTML = texto_HTML.Replace("@deuda", txtMontoTotal.Text);


            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Venta_{0}.pdf", txtNumDocumento.Text);
            savefile.Filter = "Pdf Files|*.pdf";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 25, 25, 25, 25);

                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    bool obtenido = true;
                    byte[] byteImage = new CN_OtrosDatos().obtenerLogo(out obtenido);

                    if (obtenido)
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(byteImage);
                        img.ScaleToFit(100, 100);
                        img.Alignment = iTextSharp.text.Image.UNDERLYING;
                        img.SetAbsolutePosition(pdfDoc.Left, pdfDoc.GetTop(50));
                        pdfDoc.Add(img);

                    }

                    using (StringReader sr = new StringReader(texto_HTML))
                    {
                        XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    }

                    pdfDoc.Close();
                    stream.Close();
                    MessageBox.Show("Documento Generado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    
    }
}
