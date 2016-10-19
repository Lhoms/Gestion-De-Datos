namespace ClinicaFrba
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ID_Usuario = new System.Windows.Forms.Label();
            this.buttonCerrarSesion = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxTurno = new System.Windows.Forms.GroupBox();
            this.buttonCancelarTurno = new System.Windows.Forms.Button();
            this.buttonCompraBono = new System.Windows.Forms.Button();
            this.buttonPedirTurno = new System.Windows.Forms.Button();
            this.buttonListadoEstadistico = new System.Windows.Forms.Button();
            this.groupBoxAfiliado = new System.Windows.Forms.GroupBox();
            this.buttonModificarAfiliado = new System.Windows.Forms.Button();
            this.buttonBajaAfiliado = new System.Windows.Forms.Button();
            this.buttonAltaAfiliado = new System.Windows.Forms.Button();
            this.buttonRegistrarResultado = new System.Windows.Forms.Button();
            this.groupBoxRol = new System.Windows.Forms.GroupBox();
            this.buttonModificarRol = new System.Windows.Forms.Button();
            this.buttonBajaRol = new System.Windows.Forms.Button();
            this.buttonAltaRol = new System.Windows.Forms.Button();
            this.buttonRegistrarLlegada = new System.Windows.Forms.Button();
            this.comboBoxRol = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxAtencionMedica = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBoxTurno.SuspendLayout();
            this.groupBoxAfiliado.SuspendLayout();
            this.groupBoxRol.SuspendLayout();
            this.groupBoxAtencionMedica.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario: ";
            // 
            // ID_Usuario
            // 
            this.ID_Usuario.AutoSize = true;
            this.ID_Usuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ID_Usuario.Location = new System.Drawing.Point(67, 400);
            this.ID_Usuario.Name = "ID_Usuario";
            this.ID_Usuario.Size = new System.Drawing.Size(70, 13);
            this.ID_Usuario.TabIndex = 1;
            this.ID_Usuario.Text = "ID_Usuario";
            // 
            // buttonCerrarSesion
            // 
            this.buttonCerrarSesion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCerrarSesion.Location = new System.Drawing.Point(322, 395);
            this.buttonCerrarSesion.Name = "buttonCerrarSesion";
            this.buttonCerrarSesion.Size = new System.Drawing.Size(111, 23);
            this.buttonCerrarSesion.TabIndex = 2;
            this.buttonCerrarSesion.Text = "Cerrar Sesion";
            this.buttonCerrarSesion.UseVisualStyleBackColor = true;
            this.buttonCerrarSesion.Click += new System.EventHandler(this.buttonCerrarSesion_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxAtencionMedica);
            this.groupBox1.Controls.Add(this.groupBoxTurno);
            this.groupBox1.Controls.Add(this.buttonListadoEstadistico);
            this.groupBox1.Controls.Add(this.groupBoxAfiliado);
            this.groupBox1.Controls.Add(this.groupBoxRol);
            this.groupBox1.Location = new System.Drawing.Point(15, 51);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 338);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Funcionalidades";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // groupBoxTurno
            // 
            this.groupBoxTurno.Controls.Add(this.buttonCancelarTurno);
            this.groupBoxTurno.Controls.Add(this.buttonCompraBono);
            this.groupBoxTurno.Controls.Add(this.buttonPedirTurno);
            this.groupBoxTurno.Location = new System.Drawing.Point(11, 171);
            this.groupBoxTurno.Name = "groupBoxTurno";
            this.groupBoxTurno.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxTurno.Size = new System.Drawing.Size(381, 68);
            this.groupBoxTurno.TabIndex = 4;
            this.groupBoxTurno.TabStop = false;
            this.groupBoxTurno.Text = "Turno";
            // 
            // buttonCancelarTurno
            // 
            this.buttonCancelarTurno.Location = new System.Drawing.Point(262, 19);
            this.buttonCancelarTurno.Name = "buttonCancelarTurno";
            this.buttonCancelarTurno.Size = new System.Drawing.Size(103, 33);
            this.buttonCancelarTurno.TabIndex = 8;
            this.buttonCancelarTurno.Text = "Cancelar turno";
            this.buttonCancelarTurno.UseVisualStyleBackColor = true;
            // 
            // buttonCompraBono
            // 
            this.buttonCompraBono.Location = new System.Drawing.Point(17, 19);
            this.buttonCompraBono.Name = "buttonCompraBono";
            this.buttonCompraBono.Size = new System.Drawing.Size(103, 33);
            this.buttonCompraBono.TabIndex = 6;
            this.buttonCompraBono.Text = "Comprar bono";
            this.buttonCompraBono.UseVisualStyleBackColor = true;
            // 
            // buttonPedirTurno
            // 
            this.buttonPedirTurno.Location = new System.Drawing.Point(137, 19);
            this.buttonPedirTurno.Name = "buttonPedirTurno";
            this.buttonPedirTurno.Size = new System.Drawing.Size(103, 33);
            this.buttonPedirTurno.TabIndex = 3;
            this.buttonPedirTurno.Text = "Pedir turno";
            this.buttonPedirTurno.UseVisualStyleBackColor = true;
            // 
            // buttonListadoEstadistico
            // 
            this.buttonListadoEstadistico.Location = new System.Drawing.Point(273, 266);
            this.buttonListadoEstadistico.Name = "buttonListadoEstadistico";
            this.buttonListadoEstadistico.Size = new System.Drawing.Size(103, 33);
            this.buttonListadoEstadistico.TabIndex = 7;
            this.buttonListadoEstadistico.Text = "Listado Estadistico";
            this.buttonListadoEstadistico.UseVisualStyleBackColor = true;
            // 
            // groupBoxAfiliado
            // 
            this.groupBoxAfiliado.Controls.Add(this.buttonModificarAfiliado);
            this.groupBoxAfiliado.Controls.Add(this.buttonBajaAfiliado);
            this.groupBoxAfiliado.Controls.Add(this.buttonAltaAfiliado);
            this.groupBoxAfiliado.Location = new System.Drawing.Point(11, 95);
            this.groupBoxAfiliado.Name = "groupBoxAfiliado";
            this.groupBoxAfiliado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxAfiliado.Size = new System.Drawing.Size(381, 69);
            this.groupBoxAfiliado.TabIndex = 3;
            this.groupBoxAfiliado.TabStop = false;
            this.groupBoxAfiliado.Text = "Afiliado";
            // 
            // buttonModificarAfiliado
            // 
            this.buttonModificarAfiliado.Location = new System.Drawing.Point(262, 19);
            this.buttonModificarAfiliado.Name = "buttonModificarAfiliado";
            this.buttonModificarAfiliado.Size = new System.Drawing.Size(103, 33);
            this.buttonModificarAfiliado.TabIndex = 2;
            this.buttonModificarAfiliado.Text = "Modificar  Afiliado";
            this.buttonModificarAfiliado.UseVisualStyleBackColor = true;
            this.buttonModificarAfiliado.Click += new System.EventHandler(this.buttonModificarAfiliado_Click);
            // 
            // buttonBajaAfiliado
            // 
            this.buttonBajaAfiliado.Location = new System.Drawing.Point(137, 19);
            this.buttonBajaAfiliado.Name = "buttonBajaAfiliado";
            this.buttonBajaAfiliado.Size = new System.Drawing.Size(103, 33);
            this.buttonBajaAfiliado.TabIndex = 1;
            this.buttonBajaAfiliado.Text = "Baja Afiliado";
            this.buttonBajaAfiliado.UseVisualStyleBackColor = true;
            // 
            // buttonAltaAfiliado
            // 
            this.buttonAltaAfiliado.Location = new System.Drawing.Point(17, 19);
            this.buttonAltaAfiliado.Name = "buttonAltaAfiliado";
            this.buttonAltaAfiliado.Size = new System.Drawing.Size(103, 33);
            this.buttonAltaAfiliado.TabIndex = 0;
            this.buttonAltaAfiliado.Text = "Alta Afiliado";
            this.buttonAltaAfiliado.UseVisualStyleBackColor = true;
            // 
            // buttonRegistrarResultado
            // 
            this.buttonRegistrarResultado.Location = new System.Drawing.Point(137, 19);
            this.buttonRegistrarResultado.Name = "buttonRegistrarResultado";
            this.buttonRegistrarResultado.Size = new System.Drawing.Size(103, 33);
            this.buttonRegistrarResultado.TabIndex = 4;
            this.buttonRegistrarResultado.Text = "Registrar resultado";
            this.buttonRegistrarResultado.UseVisualStyleBackColor = true;
            // 
            // groupBoxRol
            // 
            this.groupBoxRol.Controls.Add(this.buttonModificarRol);
            this.groupBoxRol.Controls.Add(this.buttonBajaRol);
            this.groupBoxRol.Controls.Add(this.buttonAltaRol);
            this.groupBoxRol.Location = new System.Drawing.Point(11, 20);
            this.groupBoxRol.Name = "groupBoxRol";
            this.groupBoxRol.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBoxRol.Size = new System.Drawing.Size(381, 69);
            this.groupBoxRol.TabIndex = 0;
            this.groupBoxRol.TabStop = false;
            this.groupBoxRol.Text = "Rol";
            // 
            // buttonModificarRol
            // 
            this.buttonModificarRol.Location = new System.Drawing.Point(262, 19);
            this.buttonModificarRol.Name = "buttonModificarRol";
            this.buttonModificarRol.Size = new System.Drawing.Size(103, 33);
            this.buttonModificarRol.TabIndex = 2;
            this.buttonModificarRol.Text = "Modificar Rol";
            this.buttonModificarRol.UseVisualStyleBackColor = true;
            this.buttonModificarRol.Click += new System.EventHandler(this.buttonModificarRol_Click);
            // 
            // buttonBajaRol
            // 
            this.buttonBajaRol.Location = new System.Drawing.Point(137, 19);
            this.buttonBajaRol.Name = "buttonBajaRol";
            this.buttonBajaRol.Size = new System.Drawing.Size(103, 33);
            this.buttonBajaRol.TabIndex = 1;
            this.buttonBajaRol.Text = "Baja Rol";
            this.buttonBajaRol.UseVisualStyleBackColor = true;
            // 
            // buttonAltaRol
            // 
            this.buttonAltaRol.Location = new System.Drawing.Point(17, 19);
            this.buttonAltaRol.Name = "buttonAltaRol";
            this.buttonAltaRol.Size = new System.Drawing.Size(103, 33);
            this.buttonAltaRol.TabIndex = 0;
            this.buttonAltaRol.Text = "Alta Rol";
            this.buttonAltaRol.UseVisualStyleBackColor = true;
            // 
            // buttonRegistrarLlegada
            // 
            this.buttonRegistrarLlegada.Location = new System.Drawing.Point(17, 20);
            this.buttonRegistrarLlegada.Name = "buttonRegistrarLlegada";
            this.buttonRegistrarLlegada.Size = new System.Drawing.Size(103, 33);
            this.buttonRegistrarLlegada.TabIndex = 5;
            this.buttonRegistrarLlegada.Text = "Registrar llegada";
            this.buttonRegistrarLlegada.UseVisualStyleBackColor = true;
            // 
            // comboBoxRol
            // 
            this.comboBoxRol.FormattingEnabled = true;
            this.comboBoxRol.Location = new System.Drawing.Point(110, 14);
            this.comboBoxRol.Name = "comboBoxRol";
            this.comboBoxRol.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRol.TabIndex = 4;
            this.comboBoxRol.SelectedIndexChanged += new System.EventHandler(this.comboBoxRol_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rol seleccionado:";
            // 
            // groupBoxAtencionMedica
            // 
            this.groupBoxAtencionMedica.Controls.Add(this.buttonRegistrarLlegada);
            this.groupBoxAtencionMedica.Controls.Add(this.buttonRegistrarResultado);
            this.groupBoxAtencionMedica.Location = new System.Drawing.Point(11, 246);
            this.groupBoxAtencionMedica.Name = "groupBoxAtencionMedica";
            this.groupBoxAtencionMedica.Size = new System.Drawing.Size(256, 72);
            this.groupBoxAtencionMedica.TabIndex = 8;
            this.groupBoxAtencionMedica.TabStop = false;
            this.groupBoxAtencionMedica.Text = "Atencion Medica";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 422);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxRol);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCerrarSesion);
            this.Controls.Add(this.ID_Usuario);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Clinica Frba - Home";
            this.groupBox1.ResumeLayout(false);
            this.groupBoxTurno.ResumeLayout(false);
            this.groupBoxAfiliado.ResumeLayout(false);
            this.groupBoxRol.ResumeLayout(false);
            this.groupBoxAtencionMedica.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ID_Usuario;
        private System.Windows.Forms.Button buttonCerrarSesion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBoxRol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxRol;
        private System.Windows.Forms.Button buttonModificarRol;
        private System.Windows.Forms.Button buttonBajaRol;
        private System.Windows.Forms.Button buttonAltaRol;
        private System.Windows.Forms.GroupBox groupBoxTurno;
        private System.Windows.Forms.Button buttonCancelarTurno;
        private System.Windows.Forms.Button buttonListadoEstadistico;
        private System.Windows.Forms.Button buttonCompraBono;
        private System.Windows.Forms.Button buttonPedirTurno;
        private System.Windows.Forms.Button buttonRegistrarResultado;
        private System.Windows.Forms.Button buttonRegistrarLlegada;
        private System.Windows.Forms.GroupBox groupBoxAfiliado;
        private System.Windows.Forms.Button buttonModificarAfiliado;
        private System.Windows.Forms.Button buttonBajaAfiliado;
        private System.Windows.Forms.Button buttonAltaAfiliado;
        private System.Windows.Forms.GroupBox groupBoxAtencionMedica;
    }
}

