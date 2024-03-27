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
    public partial class FrmAcercaDe : Form
    {
        private ToolTip toolTip1;

        public FrmAcercaDe()
        {
            InitializeComponent();
            toolTip1 = new ToolTip();
            toolTip1.SetToolTip(btnManual, "Descargar manual de usuario");
            toolTip1.SetToolTip(PicMaria, "Click para ver roles");
            toolTip1.SetToolTip(PicCesa, "Click para ver roles");
            toolTip1.SetToolTip(PicPedro, "Click para ver roles");
            toolTip1.SetToolTip(PicCarlos, "Click para ver roles");
            toolTip1.SetToolTip(PicFran, "Click para ver roles");



        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Archivos PDF|*.pdf|Todos los archivos|*.*"; // Filtro para el tipo de archivo a guardar
            saveFileDialog.Title = "Guardar archivo"; // Título del diálogo
            saveFileDialog.FileName = "Manual de usuario.pdf"; // Nombre predeterminado del archivo
            saveFileDialog.ShowDialog();

            // Si el usuario presiona el botón Guardar en el diálogo
            if (saveFileDialog.FileName != "")
            {
                try
                {
                    // Obtener el contenido del Manual de usuario
                    byte[] resourceBytes = Properties.Resources.Manual_de_usuario;

                    // Guardar el contenido del recurso en el archivo seleccionado
                    System.IO.File.WriteAllBytes(saveFileDialog.FileName, resourceBytes);

                    MessageBox.Show("Archivo guardado exitosamente.", "Guardar archivo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el archivo: " + ex.Message, "Guardar archivo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PicCesa_Click(object sender, EventArgs e)
        {
            visible(); // Ocultar todos los paneles
            panelCesa.Visible = true;
        }

        private void PicMaria_Click(object sender, EventArgs e)
        {
            visible(); // Ocultar todos los paneles
            PanelMaria.Visible = true;
        }

        private void FrmAcercaDe_Load(object sender, EventArgs e)
        {
            visible();
        }

        private void PicPedro_Click(object sender, EventArgs e)
        {
            visible(); // Ocultar todos los paneles
            panelPedro.Visible = true;
        }

        private void PicCarlos_Click(object sender, EventArgs e)
        {
            visible(); // Ocultar todos los paneles
            panelCalo.Visible = true;
        }

        private void PicFran_Click(object sender, EventArgs e)
        {
            visible(); // Ocultar todos los paneles
            panelFran.Visible = true;
        }

        private void visible()
        {
            PanelMaria.Visible = false;
            panelCesa.Visible = false;
            panelPedro.Visible = false;
            panelCalo.Visible = false;
            panelFran.Visible = false;

        }
    }
}
