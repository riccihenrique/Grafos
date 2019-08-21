namespace Grafos
{
    partial class fGrafos
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
            this.ckbDigrafo = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbxLista = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxMI = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbxMA = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lbEx = new System.Windows.Forms.Label();
            this.lbSel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTot = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ckbDigrafo
            // 
            this.ckbDigrafo.AutoSize = true;
            this.ckbDigrafo.Location = new System.Drawing.Point(6, 20);
            this.ckbDigrafo.Name = "ckbDigrafo";
            this.ckbDigrafo.Size = new System.Drawing.Size(73, 19);
            this.ckbDigrafo.TabIndex = 0;
            this.ckbDigrafo.Text = "Dígrafo?";
            this.ckbDigrafo.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbxLista);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbxMI);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lbxMA);
            this.groupBox1.Controls.Add(this.ckbDigrafo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(942, 209);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Informações";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(710, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Lista";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxLista
            // 
            this.lbxLista.BackColor = System.Drawing.SystemColors.Control;
            this.lbxLista.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxLista.FormattingEnabled = true;
            this.lbxLista.Location = new System.Drawing.Point(713, 43);
            this.lbxLista.Name = "lbxLista";
            this.lbxLista.Size = new System.Drawing.Size(220, 160);
            this.lbxLista.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(343, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Matriz de Adjacência";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxMI
            // 
            this.lbxMI.BackColor = System.Drawing.SystemColors.Control;
            this.lbxMI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxMI.FormattingEnabled = true;
            this.lbxMI.Location = new System.Drawing.Point(346, 42);
            this.lbxMI.Name = "lbxMI";
            this.lbxMI.Size = new System.Drawing.Size(353, 160);
            this.lbxMI.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(180, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Matriz de Adjacência";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxMA
            // 
            this.lbxMA.BackColor = System.Drawing.SystemColors.Control;
            this.lbxMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxMA.FormattingEnabled = true;
            this.lbxMA.Location = new System.Drawing.Point(183, 43);
            this.lbxMA.Name = "lbxMA";
            this.lbxMA.Size = new System.Drawing.Size(146, 160);
            this.lbxMA.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbTot);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lbSel);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbEx);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(7, 45);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 164);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Informações";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "Selecionado P/ Exclusão";
            // 
            // lbEx
            // 
            this.lbEx.AutoSize = true;
            this.lbEx.Location = new System.Drawing.Point(10, 40);
            this.lbEx.Name = "lbEx";
            this.lbEx.Size = new System.Drawing.Size(55, 15);
            this.lbEx.TabIndex = 1;
            this.lbEx.Text = "Nenhum";
            // 
            // lbSel
            // 
            this.lbSel.AutoSize = true;
            this.lbSel.Location = new System.Drawing.Point(10, 83);
            this.lbSel.Name = "lbSel";
            this.lbSel.Size = new System.Drawing.Size(55, 15);
            this.lbSel.TabIndex = 3;
            this.lbSel.Text = "Nenhum";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "Selecionado P/ Seleção";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 15);
            this.label7.TabIndex = 4;
            this.label7.Text = "Total de Vértices:";
            // 
            // lbTot
            // 
            this.lbTot.AutoSize = true;
            this.lbTot.Location = new System.Drawing.Point(10, 123);
            this.lbTot.Name = "lbTot";
            this.lbTot.Size = new System.Drawing.Size(14, 15);
            this.lbTot.TabIndex = 5;
            this.lbTot.Text = "0";
            // 
            // fGrafos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(966, 543);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "fGrafos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grafos";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ckbDigrafo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lbxMA;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxMI;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbxLista;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbEx;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbTot;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbSel;
        private System.Windows.Forms.Label label6;
    }
}

