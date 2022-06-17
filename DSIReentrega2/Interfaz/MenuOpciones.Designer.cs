
namespace DSIReentrega.Clases
{
    partial class MenuOpciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuOpciones));
            this.btnRegReservaVisita = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRegReservaVisita
            // 
            this.btnRegReservaVisita.BackColor = System.Drawing.Color.White;
            this.btnRegReservaVisita.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegReservaVisita.Location = new System.Drawing.Point(266, 160);
            this.btnRegReservaVisita.Name = "btnRegReservaVisita";
            this.btnRegReservaVisita.Size = new System.Drawing.Size(196, 86);
            this.btnRegReservaVisita.TabIndex = 0;
            this.btnRegReservaVisita.Text = "Registrar Reserva Visita";
            this.btnRegReservaVisita.UseVisualStyleBackColor = false;
            this.btnRegReservaVisita.Click += new System.EventHandler(this.btnRegReservaVisita_Click);
            // 
            // MenuOpciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(716, 450);
            this.Controls.Add(this.btnRegReservaVisita);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MenuOpciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu registrar reserva";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRegReservaVisita;
    }
}