namespace PetTrackingApp
{
    partial class TransfarePanel
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
            this.txtPrevious = new System.Windows.Forms.TextBox();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.txtNewOwner = new System.Windows.Forms.TextBox();
            this.txtPetID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.petTransfareBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPetDOB = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(29, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Current Owner ID";
            // 
            // txtPrevious
            // 
            this.txtPrevious.Location = new System.Drawing.Point(336, 137);
            this.txtPrevious.Name = "txtPrevious";
            this.txtPrevious.ReadOnly = true;
            this.txtPrevious.Size = new System.Drawing.Size(355, 20);
            this.txtPrevious.TabIndex = 3;
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(336, 266);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(355, 53);
            this.txtReason.TabIndex = 4;
            // 
            // txtNewOwner
            // 
            this.txtNewOwner.Location = new System.Drawing.Point(336, 181);
            this.txtNewOwner.Name = "txtNewOwner";
            this.txtNewOwner.Size = new System.Drawing.Size(355, 20);
            this.txtNewOwner.TabIndex = 8;
            // 
            // txtPetID
            // 
            this.txtPetID.Location = new System.Drawing.Point(336, 221);
            this.txtPetID.Name = "txtPetID";
            this.txtPetID.Size = new System.Drawing.Size(355, 20);
            this.txtPetID.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(29, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 23);
            this.label2.TabIndex = 10;
            this.label2.Text = "New Owner ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(29, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 23);
            this.label3.TabIndex = 11;
            this.label3.Text = "Pet Identity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(29, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 23);
            this.label4.TabIndex = 12;
            this.label4.Text = "Reason For Transfare";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.Location = new System.Drawing.Point(29, 310);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 23);
            this.label5.TabIndex = 13;
            this.label5.Text = "Date Of Transfare";
            // 
            // petTransfareBtn
            // 
            this.petTransfareBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.petTransfareBtn.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.petTransfareBtn.ForeColor = System.Drawing.Color.White;
            this.petTransfareBtn.Location = new System.Drawing.Point(389, 369);
            this.petTransfareBtn.Name = "petTransfareBtn";
            this.petTransfareBtn.Size = new System.Drawing.Size(224, 31);
            this.petTransfareBtn.TabIndex = 14;
            this.petTransfareBtn.Text = "Transfre";
            this.petTransfareBtn.UseVisualStyleBackColor = true;
            this.petTransfareBtn.Click += new System.EventHandler(this.petTransfareBtn_Click);
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(723, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Back";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPetDOB
            // 
            this.txtPetDOB.Location = new System.Drawing.Point(336, 326);
            this.txtPetDOB.Name = "txtPetDOB";
            this.txtPetDOB.Size = new System.Drawing.Size(355, 20);
            this.txtPetDOB.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 26.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label6.Location = new System.Drawing.Point(271, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(319, 60);
            this.label6.TabIndex = 17;
            this.label6.Text = "Transfer Pet";
            // 
            // TransfarePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPetDOB);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.petTransfareBtn);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPetID);
            this.Controls.Add(this.txtNewOwner);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.txtPrevious);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransfarePanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TransfarePanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrevious;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.TextBox txtNewOwner;
        private System.Windows.Forms.TextBox txtPetID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button petTransfareBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker txtPetDOB;
        private System.Windows.Forms.Label label6;
    }
}