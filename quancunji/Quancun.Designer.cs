namespace quancunji
{
    partial class Quancun
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
            this.quancuninfo = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // quancuninfo
            // 
            this.quancuninfo.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.quancuninfo.Location = new System.Drawing.Point(136, 119);
            this.quancuninfo.Name = "quancuninfo";
            this.quancuninfo.Size = new System.Drawing.Size(302, 168);
            this.quancuninfo.TabIndex = 0;
            this.quancuninfo.Text = "请放卡...\r\n";
            this.quancuninfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Image = global::quancunji.Properties.Resources.btn_return;
            this.button1.Location = new System.Drawing.Point(198, 354);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(181, 57);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Quancun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 475);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.quancuninfo);
            this.DoubleBuffered = true;
            this.Name = "Quancun";
            this.Text = "Quancun";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label quancuninfo;
        private System.Windows.Forms.Button button1;
    }
}