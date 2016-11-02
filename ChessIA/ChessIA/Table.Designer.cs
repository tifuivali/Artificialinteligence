namespace ChessIA
{
    partial class Table
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
            this.ChessTablePicture = new System.Windows.Forms.PictureBox();
            this.LabelStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PlayerName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PlayerColor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ChessTablePicture)).BeginInit();
            this.SuspendLayout();
            // 
            // ChessTablePicture
            // 
            this.ChessTablePicture.Location = new System.Drawing.Point(13, 13);
            this.ChessTablePicture.Name = "ChessTablePicture";
            this.ChessTablePicture.Size = new System.Drawing.Size(737, 534);
            this.ChessTablePicture.TabIndex = 1;
            this.ChessTablePicture.TabStop = false;
            // 
            // LabelStatus
            // 
            this.LabelStatus.AutoSize = true;
            this.LabelStatus.Location = new System.Drawing.Point(30, 550);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(35, 13);
            this.LabelStatus.TabIndex = 2;
            this.LabelStatus.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(774, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Player:";
            // 
            // PlayerName
            // 
            this.PlayerName.AutoSize = true;
            this.PlayerName.Location = new System.Drawing.Point(820, 38);
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.Size = new System.Drawing.Size(10, 13);
            this.PlayerName.TabIndex = 4;
            this.PlayerName.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(777, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Color:";
            // 
            // PlayerColor
            // 
            this.PlayerColor.AutoSize = true;
            this.PlayerColor.Location = new System.Drawing.Point(818, 55);
            this.PlayerColor.Name = "PlayerColor";
            this.PlayerColor.Size = new System.Drawing.Size(10, 13);
            this.PlayerColor.TabIndex = 6;
            this.PlayerColor.Text = "-";
            // 
            // Table
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 580);
            this.Controls.Add(this.PlayerColor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PlayerName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.ChessTablePicture);
            this.Name = "Table";
            this.Text = "Table";
            this.Load += new System.EventHandler(this.Table_OnLoad);
            ((System.ComponentModel.ISupportInitialize)(this.ChessTablePicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox ChessTablePicture;
        private System.Windows.Forms.Label LabelStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label PlayerName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PlayerColor;
    }
}

