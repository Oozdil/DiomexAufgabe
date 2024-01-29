namespace DiomexAufgabe
{
    partial class MainForm
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
            this.textBoxFormelInput = new System.Windows.Forms.TextBox();
            this.buttonLosung = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textBoxFormelInput
            // 
            this.textBoxFormelInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxFormelInput.Location = new System.Drawing.Point(28, 25);
            this.textBoxFormelInput.Name = "textBoxFormelInput";
            this.textBoxFormelInput.Size = new System.Drawing.Size(408, 29);
            this.textBoxFormelInput.TabIndex = 0;
            // 
            // buttonLosung
            // 
            this.buttonLosung.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonLosung.Location = new System.Drawing.Point(442, 19);
            this.buttonLosung.Name = "buttonLosung";
            this.buttonLosung.Size = new System.Drawing.Size(264, 40);
            this.buttonLosung.TabIndex = 1;
            this.buttonLosung.Text = "Löse die Formel";
            this.buttonLosung.UseVisualStyleBackColor = true;
            this.buttonLosung.Click += new System.EventHandler(this.buttonLosung_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBox1.Location = new System.Drawing.Point(28, 81);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(678, 158);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 264);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.buttonLosung);
            this.Controls.Add(this.textBoxFormelInput);
            this.Name = "Form3";
            this.Text = "Diomex Aufgabe Screen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxFormelInput;
        private System.Windows.Forms.Button buttonLosung;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}