namespace ClinicaFrba.AbmRol
{
    partial class ModificarRol
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxRoles = new System.Windows.Forms.ComboBox();
            this.groupBoxFuncionalidades = new System.Windows.Forms.GroupBox();
            this.checkedListFunciones = new System.Windows.Forms.CheckedListBox();
            this.groupBoxBaja = new System.Windows.Forms.GroupBox();
            this.buttonBaja = new System.Windows.Forms.Button();
            this.groupBoxModificarNombre = new System.Windows.Forms.GroupBox();
            this.buttonCambiar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNuevoNombre = new System.Windows.Forms.TextBox();
            this.buttonAceptar = new System.Windows.Forms.Button();
            this.buttonCancelar = new System.Windows.Forms.Button();
            this.groupBoxFuncionalidades.SuspendLayout();
            this.groupBoxBaja.SuspendLayout();
            this.groupBoxModificarNombre.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione Rol:  ";
            // 
            // comboBoxRoles
            // 
            this.comboBoxRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRoles.FormattingEnabled = true;
            this.comboBoxRoles.Location = new System.Drawing.Point(106, 10);
            this.comboBoxRoles.Name = "comboBoxRoles";
            this.comboBoxRoles.Size = new System.Drawing.Size(121, 21);
            this.comboBoxRoles.TabIndex = 2;
            this.comboBoxRoles.SelectedIndexChanged += new System.EventHandler(this.comboBoxRoles_SelectedIndexChanged);
            // 
            // groupBoxFuncionalidades
            // 
            this.groupBoxFuncionalidades.Controls.Add(this.checkedListFunciones);
            this.groupBoxFuncionalidades.Location = new System.Drawing.Point(324, 60);
            this.groupBoxFuncionalidades.Name = "groupBoxFuncionalidades";
            this.groupBoxFuncionalidades.Size = new System.Drawing.Size(286, 208);
            this.groupBoxFuncionalidades.TabIndex = 3;
            this.groupBoxFuncionalidades.TabStop = false;
            this.groupBoxFuncionalidades.Text = "Modificacion";
            // 
            // checkedListFunciones
            // 
            this.checkedListFunciones.FormattingEnabled = true;
            this.checkedListFunciones.Location = new System.Drawing.Point(6, 19);
            this.checkedListFunciones.Name = "checkedListFunciones";
            this.checkedListFunciones.Size = new System.Drawing.Size(269, 184);
            this.checkedListFunciones.TabIndex = 0;
            // 
            // groupBoxBaja
            // 
            this.groupBoxBaja.Controls.Add(this.buttonBaja);
            this.groupBoxBaja.Location = new System.Drawing.Point(15, 60);
            this.groupBoxBaja.Name = "groupBoxBaja";
            this.groupBoxBaja.Size = new System.Drawing.Size(275, 85);
            this.groupBoxBaja.TabIndex = 4;
            this.groupBoxBaja.TabStop = false;
            this.groupBoxBaja.Text = "Baja";
            // 
            // buttonBaja
            // 
            this.buttonBaja.Location = new System.Drawing.Point(64, 30);
            this.buttonBaja.Name = "buttonBaja";
            this.buttonBaja.Size = new System.Drawing.Size(138, 34);
            this.buttonBaja.TabIndex = 0;
            this.buttonBaja.UseVisualStyleBackColor = true;
            this.buttonBaja.Click += new System.EventHandler(this.buttonBaja_Click);
            // 
            // groupBoxModificarNombre
            // 
            this.groupBoxModificarNombre.Controls.Add(this.buttonCambiar);
            this.groupBoxModificarNombre.Controls.Add(this.label2);
            this.groupBoxModificarNombre.Controls.Add(this.textBoxNuevoNombre);
            this.groupBoxModificarNombre.Location = new System.Drawing.Point(15, 160);
            this.groupBoxModificarNombre.Name = "groupBoxModificarNombre";
            this.groupBoxModificarNombre.Size = new System.Drawing.Size(275, 108);
            this.groupBoxModificarNombre.TabIndex = 5;
            this.groupBoxModificarNombre.TabStop = false;
            this.groupBoxModificarNombre.Text = "Cambiar nombre";
            // 
            // buttonCambiar
            // 
            this.buttonCambiar.Location = new System.Drawing.Point(173, 79);
            this.buttonCambiar.Name = "buttonCambiar";
            this.buttonCambiar.Size = new System.Drawing.Size(75, 23);
            this.buttonCambiar.TabIndex = 8;
            this.buttonCambiar.Text = "Cambiar";
            this.buttonCambiar.UseVisualStyleBackColor = true;
            this.buttonCambiar.Click += new System.EventHandler(this.buttonCambiar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre nuevo: ";
            // 
            // textBoxNuevoNombre
            // 
            this.textBoxNuevoNombre.Location = new System.Drawing.Point(110, 41);
            this.textBoxNuevoNombre.Name = "textBoxNuevoNombre";
            this.textBoxNuevoNombre.Size = new System.Drawing.Size(138, 20);
            this.textBoxNuevoNombre.TabIndex = 1;
            // 
            // buttonAceptar
            // 
            this.buttonAceptar.Location = new System.Drawing.Point(524, 274);
            this.buttonAceptar.Name = "buttonAceptar";
            this.buttonAceptar.Size = new System.Drawing.Size(75, 23);
            this.buttonAceptar.TabIndex = 6;
            this.buttonAceptar.Text = "Aceptar";
            this.buttonAceptar.UseVisualStyleBackColor = true;
            this.buttonAceptar.Click += new System.EventHandler(this.buttonAceptar_Click);
            // 
            // buttonCancelar
            // 
            this.buttonCancelar.Location = new System.Drawing.Point(535, 312);
            this.buttonCancelar.Name = "buttonCancelar";
            this.buttonCancelar.Size = new System.Drawing.Size(75, 23);
            this.buttonCancelar.TabIndex = 7;
            this.buttonCancelar.Text = "Volver";
            this.buttonCancelar.UseVisualStyleBackColor = true;
            this.buttonCancelar.Click += new System.EventHandler(this.buttonCancelar_Click);
            // 
            // ModificarRol
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 347);
            this.Controls.Add(this.buttonCancelar);
            this.Controls.Add(this.buttonAceptar);
            this.Controls.Add(this.groupBoxModificarNombre);
            this.Controls.Add(this.groupBoxBaja);
            this.Controls.Add(this.groupBoxFuncionalidades);
            this.Controls.Add(this.comboBoxRoles);
            this.Controls.Add(this.label1);
            this.Name = "ModificarRol";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modificacion Rol";
            this.groupBoxFuncionalidades.ResumeLayout(false);
            this.groupBoxBaja.ResumeLayout(false);
            this.groupBoxModificarNombre.ResumeLayout(false);
            this.groupBoxModificarNombre.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxRoles;
        private System.Windows.Forms.GroupBox groupBoxFuncionalidades;
        private System.Windows.Forms.CheckedListBox checkedListFunciones;
        private System.Windows.Forms.GroupBox groupBoxBaja;
        private System.Windows.Forms.Button buttonBaja;
        private System.Windows.Forms.GroupBox groupBoxModificarNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxNuevoNombre;
        private System.Windows.Forms.Button buttonAceptar;
        private System.Windows.Forms.Button buttonCancelar;
        private System.Windows.Forms.Button buttonCambiar;
    }
}