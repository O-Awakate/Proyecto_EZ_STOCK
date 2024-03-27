﻿using System;
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
        private ToolTip toolTip1;
        public FrmNegocio()
        {
            InitializeComponent();

            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btnSubir, "Subir un nuevo logo para la empresa.");
            toolTip1.SetToolTip(button1, "Guarda precio del dolar.");
            toolTip1.SetToolTip(btnGuardar, "Guardar datos de la empresa.");
            toolTip1.SetToolTip(btnGuradarBackup, "Guardar respaldo en direccion seleccionada.");
            toolTip1.SetToolTip(btnBuscarBackup, "Seleccionar direccion para generar un respaldo.");
            toolTip1.SetToolTip(btnGuradarRestore, "Restausar desde la version seleccionada.");
            toolTip1.SetToolTip(btnBuscarRestore, "Buscar punto de restauracion.");
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
            byte[] byteimage = new CN_OtrosDatos().obtenerLogo(out obtenido);

            if (obtenido)
                picLogo.Image = byteToImagege(byteimage);

            Negocio datos = new CN_OtrosDatos().obtenerDatos();

            txtNombreNegocio.Text = datos.Nombre;
            txtRIF.Text = datos.RIF;
            txtDireccion.Text = datos.Direccion;


            Otros_Datos OtrosDatos = new CN_OtrosDatos().obtenerOtrosDatos();

            txtPrecioDolar.Text = OtrosDatos.ValorDolar.ToString();
            txtFecha.Text = OtrosDatos.FechaRegistro.ToString();

        }

        private void btnSubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            OpenFileDialog oOpenFileDialog = new OpenFileDialog();
            oOpenFileDialog.FileName = "Files|*.jpg;*.jpeg;*.png";

            if (oOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                byte[] byteimage = File.ReadAllBytes(oOpenFileDialog.FileName);
                bool respuesta = new CN_OtrosDatos().actualizarLogo(byteimage, out mensaje);

                if (respuesta)
                    picLogo.Image = byteToImagege(byteimage);
                else
                    MessageBox.Show(mensaje, "mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!txtRIF.Text.Contains("J-"))
            {
                MessageBox.Show("El campo RIF debe contener 'J-'", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            string mensaje = string.Empty;

            Negocio obj = new Negocio()
            {
                Nombre = txtNombreNegocio.Text,
                RIF = txtRIF.Text,
                Direccion = txtDireccion.Text
            };

            bool respuesta = new CN_OtrosDatos().GuardarDatos(obj, out mensaje);

            if (respuesta)
                MessageBox.Show("Los cambios a la información del negocio fueron guardados exitosamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudieron guardar los cambios.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;

            Otros_Datos obj = new Otros_Datos()
            {
                ValorDolar = decimal.Parse(txtPrecioDolar.Text)
            };

            bool respuesta = new CN_OtrosDatos().GuardarOtrosDatos(obj, out mensaje);

            if (respuesta)
                MessageBox.Show("Los cambios fueron guardados.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("No se pudieron guardar los cambios.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void btnBuscarBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            if(folder.ShowDialog()== DialogResult.OK)
            {
                txtBackup.Text = folder.SelectedPath;
            }
        }

        private void btnGuradarBackup_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (txtBackup.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la ubicación para guardar el archivo de copia de seguridad");
                return;
            }
            try
            {
                bool respuesta = new CN_OtrosDatos().Respaldo(txtBackup.Text, out mensaje);
                MessageBox.Show("El respaldo fue creado con exito");
                txtBackup.Text = "";

            }
            catch
            {
                MessageBox.Show("No se pudo crear un respaldo");
            }


        }

        private void btnBuscarRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDialog = new OpenFileDialog();

            OpenDialog.Filter = "SQL SERVER database backup files | *.bak";
            OpenDialog.Title = "database restore";
            if(OpenDialog.ShowDialog() == DialogResult.OK)
            {
                txtRestore.Text = OpenDialog.FileName;
            }

        }

        private void btnGuradarRestore_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            if (txtRestore.Text == string.Empty)
            {
                MessageBox.Show("Seleccione la ubicación del archivo de copia de seguridad");
                return;
            }
            try
            {
                bool respuesta = new CN_OtrosDatos().RecuperarInformacion(txtRestore.Text, out mensaje);
                MessageBox.Show("La copia de seguridad se realizo exitosamente");
                txtBackup.Text = "";

            }
            catch
            {
                MessageBox.Show("No se pudo actualizar la informacion");
            }
        }

        private void txtPrecioDolar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (txtPrecioDolar.Text.Trim().Length == 0 && e.KeyChar.ToString() == ".")
                {
                    e.Handled = true;
                }
                if (txtPrecioDolar.Text.Trim().Length == 0 && e.KeyChar.ToString() == ",")
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
