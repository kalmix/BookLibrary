namespace BookLibrary.UI.Forms
{
    partial class AddBookForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddBookForm));
            lblTitle = new Label();
            txtTitle = new TextBox();
            lblAuthor = new Label();
            txtAuthor = new TextBox();
            lblPdfPath = new Label();
            txtPdfPath = new TextBox();
            btnBrowse = new FontAwesome.Sharp.IconButton();
            btnSave = new FontAwesome.Sharp.IconButton();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Roboto", 9F);
            lblTitle.Location = new Point(12, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(41, 14);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Titulo:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(82, 12);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(315, 23);
            txtTitle.TabIndex = 1;
            txtTitle.TextChanged += txtTitle_TextChanged;
            // 
            // lblAuthor
            // 
            lblAuthor.AutoSize = true;
            lblAuthor.Font = new Font("Roboto", 9F);
            lblAuthor.Location = new Point(12, 44);
            lblAuthor.Name = "lblAuthor";
            lblAuthor.Size = new Size(40, 14);
            lblAuthor.TabIndex = 2;
            lblAuthor.Text = "Autor:";
            // 
            // txtAuthor
            // 
            txtAuthor.Location = new Point(82, 41);
            txtAuthor.Name = "txtAuthor";
            txtAuthor.Size = new Size(315, 23);
            txtAuthor.TabIndex = 3;
            // 
            // lblPdfPath
            // 
            lblPdfPath.AutoSize = true;
            lblPdfPath.Font = new Font("Roboto", 9F);
            lblPdfPath.Location = new Point(12, 73);
            lblPdfPath.Name = "lblPdfPath";
            lblPdfPath.Size = new Size(33, 14);
            lblPdfPath.TabIndex = 4;
            lblPdfPath.Text = "PDF:";
            // 
            // txtPdfPath
            // 
            txtPdfPath.Location = new Point(82, 70);
            txtPdfPath.Name = "txtPdfPath";
            txtPdfPath.Size = new Size(234, 23);
            txtPdfPath.TabIndex = 5;
            // 
            // btnBrowse
            // 
            btnBrowse.Font = new Font("Roboto SemiCondensed SemiBold", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBrowse.IconChar = FontAwesome.Sharp.IconChar.FolderOpen;
            btnBrowse.IconColor = Color.Tan;
            btnBrowse.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnBrowse.IconSize = 16;
            btnBrowse.Location = new Point(322, 69);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(75, 25);
            btnBrowse.TabIndex = 6;
            btnBrowse.Text = "Explorar";
            btnBrowse.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Roboto SemiBold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.IconChar = FontAwesome.Sharp.IconChar.Save;
            btnSave.IconColor = Color.Tan;
            btnSave.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnSave.IconSize = 24;
            btnSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnSave.Location = new Point(82, 110);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(234, 35);
            btnSave.TabIndex = 7;
            btnSave.Text = "Guardar Libro";
            btnSave.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // AddBookForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(409, 161);
            Controls.Add(btnSave);
            Controls.Add(btnBrowse);
            Controls.Add(txtPdfPath);
            Controls.Add(lblPdfPath);
            Controls.Add(txtAuthor);
            Controls.Add(lblAuthor);
            Controls.Add(txtTitle);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddBookForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Agregar Libro";
            Load += AddBookForm_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblPdfPath;
        private System.Windows.Forms.TextBox txtPdfPath;
        private FontAwesome.Sharp.IconButton btnBrowse;
        private FontAwesome.Sharp.IconButton btnSave;
    }
}