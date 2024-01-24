namespace CapaPresentacion
{
    partial class Verificacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.icon = new System.Windows.Forms.PictureBox();
            this.lblCI = new System.Windows.Forms.Label();
            this.txtCedula = new System.Windows.Forms.TextBox();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.btnSMS = new System.Windows.Forms.Button();
            this.btnCorreo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConfirmarClave = new System.Windows.Forms.TextBox();
            this.lblClave = new System.Windows.Forms.Label();
            this.lblConClave = new System.Windows.Forms.Label();
            this.panelCambioClave = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.panelCorreoSMS = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).BeginInit();
            this.panelCambioClave.SuspendLayout();
            this.panelCorreoSMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Location = new System.Drawing.Point(8, 112);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(232, 123);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 49;
            this.picLogo.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 418);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Versión 0.0";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(30, 347);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(28, 20);
            this.txtID.TabIndex = 21;
            this.txtID.Visible = false;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Location = new System.Drawing.Point(12, 373);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(174, 20);
            this.txtCorreo.TabIndex = 18;
            this.txtCorreo.Visible = false;
            // 
            // btnSalir
            // 
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.Location = new System.Drawing.Point(167, 272);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 15;
            this.btnSalir.Text = "Cancelar";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // icon
            // 
            this.icon.Image = global::CapaPresentacion.Properties.Resources.user__1_;
            this.icon.Location = new System.Drawing.Point(13, 75);
            this.icon.Name = "icon";
            this.icon.Size = new System.Drawing.Size(24, 24);
            this.icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.icon.TabIndex = 5;
            this.icon.TabStop = false;
            // 
            // lblCI
            // 
            this.lblCI.AutoSize = true;
            this.lblCI.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCI.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.lblCI.Location = new System.Drawing.Point(43, 78);
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(77, 17);
            this.lblCI.TabIndex = 4;
            this.lblCI.Text = "Numero C.I.";
            // 
            // txtCedula
            // 
            this.txtCedula.Location = new System.Drawing.Point(37, 102);
            this.txtCedula.Name = "txtCedula";
            this.txtCedula.Size = new System.Drawing.Size(253, 20);
            this.txtCedula.TabIndex = 3;
            // 
            // btnIngresar
            // 
            this.btnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.Location = new System.Drawing.Point(73, 272);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(75, 23);
            this.btnIngresar.TabIndex = 2;
            this.btnIngresar.Text = "Aceptar";
            this.btnIngresar.UseVisualStyleBackColor = true;
            this.btnIngresar.Click += new System.EventHandler(this.btnIngresar_Click);
            // 
            // btnSMS
            // 
            this.btnSMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSMS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSMS.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSMS.Location = new System.Drawing.Point(37, 144);
            this.btnSMS.Name = "btnSMS";
            this.btnSMS.Size = new System.Drawing.Size(253, 23);
            this.btnSMS.TabIndex = 17;
            this.btnSMS.Text = "Verificación por SMS";
            this.btnSMS.UseVisualStyleBackColor = true;
            this.btnSMS.Click += new System.EventHandler(this.btnSMS_Click);
            // 
            // btnCorreo
            // 
            this.btnCorreo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCorreo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCorreo.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCorreo.Location = new System.Drawing.Point(37, 102);
            this.btnCorreo.Name = "btnCorreo";
            this.btnCorreo.Size = new System.Drawing.Size(253, 23);
            this.btnCorreo.TabIndex = 16;
            this.btnCorreo.Text = "Verificar por correo";
            this.btnCorreo.UseVisualStyleBackColor = true;
            this.btnCorreo.Click += new System.EventHandler(this.btnCorreo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.label1.Location = new System.Drawing.Point(27, 247);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Repuestos y Servicios Ávila; C.A.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtConfirmarClave
            // 
            this.txtConfirmarClave.Location = new System.Drawing.Point(48, 160);
            this.txtConfirmarClave.Name = "txtConfirmarClave";
            this.txtConfirmarClave.PasswordChar = '*';
            this.txtConfirmarClave.Size = new System.Drawing.Size(249, 20);
            this.txtConfirmarClave.TabIndex = 20;
            // 
            // lblClave
            // 
            this.lblClave.AutoSize = true;
            this.lblClave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.lblClave.Location = new System.Drawing.Point(54, 94);
            this.lblClave.Name = "lblClave";
            this.lblClave.Size = new System.Drawing.Size(162, 17);
            this.lblClave.TabIndex = 22;
            this.lblClave.Text = "Ingrese Nueva Contraseña";
            // 
            // lblConClave
            // 
            this.lblConClave.AutoSize = true;
            this.lblConClave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConClave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.lblConClave.Location = new System.Drawing.Point(54, 140);
            this.lblConClave.Name = "lblConClave";
            this.lblConClave.Size = new System.Drawing.Size(172, 17);
            this.lblConClave.TabIndex = 23;
            this.lblConClave.Text = "Confirme Nueva Contraseña";
            // 
            // panelCambioClave
            // 
            this.panelCambioClave.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelCambioClave.Controls.Add(this.lblConClave);
            this.panelCambioClave.Controls.Add(this.lblClave);
            this.panelCambioClave.Controls.Add(this.btnCancelar);
            this.panelCambioClave.Controls.Add(this.groupBox2);
            this.panelCambioClave.Controls.Add(this.txtConfirmarClave);
            this.panelCambioClave.Controls.Add(this.txtClave);
            this.panelCambioClave.Controls.Add(this.btnAceptar);
            this.panelCambioClave.Controls.Add(this.label7);
            this.panelCambioClave.Location = new System.Drawing.Point(0, 0);
            this.panelCambioClave.Name = "panelCambioClave";
            this.panelCambioClave.Size = new System.Drawing.Size(337, 403);
            this.panelCambioClave.TabIndex = 48;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(174, 292);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 15;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(80, 321);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 5);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(48, 118);
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(249, 20);
            this.txtClave.TabIndex = 19;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.Location = new System.Drawing.Point(80, 292);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.label7.Location = new System.Drawing.Point(111, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 30);
            this.label7.TabIndex = 0;
            this.label7.Text = "Verificación";
            // 
            // btnBack
            // 
            this.btnBack.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Image = global::CapaPresentacion.Properties.Resources.arrow_back_circle_outline__1_;
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(35, 31);
            this.btnBack.TabIndex = 50;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // panelCorreoSMS
            // 
            this.panelCorreoSMS.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelCorreoSMS.Controls.Add(this.panelCambioClave);
            this.panelCorreoSMS.Controls.Add(this.btnSalir);
            this.panelCorreoSMS.Controls.Add(this.btnBack);
            this.panelCorreoSMS.Controls.Add(this.groupBox4);
            this.panelCorreoSMS.Controls.Add(this.icon);
            this.panelCorreoSMS.Controls.Add(this.btnSMS);
            this.panelCorreoSMS.Controls.Add(this.lblCI);
            this.panelCorreoSMS.Controls.Add(this.label4);
            this.panelCorreoSMS.Controls.Add(this.txtCedula);
            this.panelCorreoSMS.Controls.Add(this.btnIngresar);
            this.panelCorreoSMS.Controls.Add(this.btnCorreo);
            this.panelCorreoSMS.Location = new System.Drawing.Point(248, 28);
            this.panelCorreoSMS.Name = "panelCorreoSMS";
            this.panelCorreoSMS.Size = new System.Drawing.Size(337, 403);
            this.panelCorreoSMS.TabIndex = 51;
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(80, 321);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(168, 5);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(33)))), ((int)(((byte)(61)))));
            this.label4.Location = new System.Drawing.Point(111, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 30);
            this.label4.TabIndex = 0;
            this.label4.Text = "Verificación";
            // 
            // Verificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 456);
            this.Controls.Add(this.panelCorreoSMS);
            this.Controls.Add(this.txtCorreo);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Verificacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmVerificacion";
            this.Load += new System.EventHandler(this.FrmVerificacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.icon)).EndInit();
            this.panelCambioClave.ResumeLayout(false);
            this.panelCambioClave.PerformLayout();
            this.panelCorreoSMS.ResumeLayout(false);
            this.panelCorreoSMS.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox icon;
        private System.Windows.Forms.Label lblCI;
        private System.Windows.Forms.TextBox txtCedula;
        private System.Windows.Forms.Button btnIngresar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSMS;
        private System.Windows.Forms.Button btnCorreo;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label lblConClave;
        private System.Windows.Forms.Label lblClave;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtConfirmarClave;
        private System.Windows.Forms.Panel panelCambioClave;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Panel panelCorreoSMS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
    }
}