using System;
using CapaNegocio;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaEntidad;

namespace CapaPresentacion
{
    public partial class FrmNegocio : Form
    {
        public FrmNegocio()
        {
            InitializeComponent();
        }

        public Image byteToImagege(byte[] imageByte)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(imageByte, 0, imageByte.Length);
            Image image = new Bitmap(ms);

            return image;
        }

        private void FrmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = new CN_Negocio().obtenerLogo(out obtenido);

            if (obtenido)
                picLogo.Image = byteToImagege(byteimage);

            Negocio datos = new CN_Negocio().obtenerDatos();

            txtNombreNegocio.Text = datos.Nombre;
            txtRIF.Text = datos.RIF;
            txtDireccion.Text = datos.Direccion;
        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.FileName = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteimage = File.ReadAllBytes(oOpenFileDialog.FileName);
                bool respuesta = new CN_Negocio().actualizarLogo(byteimage, out mensaje);

                if (respuesta)
                    picLogo.Image = byteToImagege(byteimage);
                else
                    MessageBox.Show(mensaje, "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Negocio obj = new Negocio()
            {
                Nombre = txtNombreNegocio.Text,
                RIF = txtRIF.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CN_Negocio().GuardarDatos(obj, out mensaje);

            if (respuesta)
                MessageBox.Show("Los cambios fueron guardados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudo guaedar los cambios", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
    
}
