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

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;


namespace CapaPresentacion
{
    public partial class FrmDetalleCompra : Form
    {
        private ToolTip toolTip1;
        public FrmDetalleCompra()
        {
            InitializeComponent();

            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btnBuscar, "Buscar compra.");
            toolTip1.SetToolTip(btnLimpiar, "Limpiar datos de la compra.");
            toolTip1.SetToolTip(btnpdf, "Generar factura en PDF.");
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
                Compra oCompra = new CN_Compra().ObtenerCompra(txtBusqueda.Text);
            
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

        private void btnpdf_Click(object sender, EventArgs e)
        {
            if(txtTipoDocumento.Text == "")
            {
                MessageBox.Show("No se encontraron resultados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string texto_HTML;

            if (txtMetodo.Text == "Credito")
            {
                texto_HTML = Properties.Resources.Plantilla_Credito_Compra.ToString();
            }
            else
            {
                texto_HTML = Properties.Resources.Plantilla_compra.ToString();
            }

            Negocio oDatos = new CN_OtrosDatos().obtenerDatos();

            texto_HTML = texto_HTML.Replace("@nombrenegocio", oDatos.Nombre.ToUpper());
            texto_HTML = texto_HTML.Replace("@docnegocio", oDatos.RIF);
            texto_HTML = texto_HTML.Replace("@direcnegocio", oDatos.Direccion);
            
            texto_HTML = texto_HTML.Replace("@tipodocumento", txtUsuario.Text);
            texto_HTML = texto_HTML.Replace("@numerodocumento", txtNumDocumento.Text);
            texto_HTML = texto_HTML.Replace("@fecharegistro", txtFecha.Text);
            texto_HTML = texto_HTML.Replace("@usuarioregistro", txtUsuario.Text);


            texto_HTML = texto_HTML.Replace("@docproveedor", txtDocumento.Text);
            texto_HTML = texto_HTML.Replace("@nombreproveedor", txtNombreProveedor.Text);
            texto_HTML = texto_HTML.Replace("@apellidoproveedor", txtApellidoProveedor.Text);
            texto_HTML = texto_HTML.Replace("@razonsocial", txtRazonSocial.Text);
            texto_HTML = texto_HTML.Replace("@RIF", txtRIF.Text);

            string filas = string.Empty;
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                filas += "<tr>";
                filas += "<td>" + row.Cells["Producto"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["PrecioCompra"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["Cantidad"].Value.ToString() + "</td>";
                filas += "<td>" + row.Cells["SubTotal"].Value.ToString() + "</td>";
                filas += "</tr>";

            }
            texto_HTML = texto_HTML.Replace("@filas", filas);
            texto_HTML = texto_HTML.Replace("@montototal", txtMontoTotal.Text);
            texto_HTML = texto_HTML.Replace("@montobs", txtMontoBs.Text);
            texto_HTML = texto_HTML.Replace("@metodopago", txtMetodo.Text);
            texto_HTML = texto_HTML.Replace("@deuda", txtDeuda.Text);

            SaveFileDialog savefile = new SaveFileDialog();
            savefile.FileName = string.Format("Compra_{0}.pdf", txtNumDocumento.Text);
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
 