namespace BookLibrary.UI.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            dataGridViewBooks = new DataGridView();
            btnAddBook = new FontAwesome.Sharp.IconButton();
            btnRemoveBook = new FontAwesome.Sharp.IconButton();
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewBooks
            // 
            dataGridViewBooks.AllowUserToAddRows = false;
            dataGridViewBooks.AllowUserToDeleteRows = false;
            dataGridViewBooks.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewBooks.BackgroundColor = Color.FloralWhite;
            dataGridViewBooks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewBooks.Location = new Point(12, 53);
            dataGridViewBooks.Name = "dataGridViewBooks";
            dataGridViewBooks.ReadOnly = true;
            dataGridViewBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewBooks.Size = new Size(776, 385);
            dataGridViewBooks.TabIndex = 0;
            // 
            // btnAddBook
            // 
            btnAddBook.Font = new Font("Roboto SemiBold", 10F, FontStyle.Bold);
            btnAddBook.IconChar = FontAwesome.Sharp.IconChar.CirclePlus;
            btnAddBook.IconColor = Color.Tan;
            btnAddBook.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnAddBook.IconSize = 24;
            btnAddBook.ImageAlign = ContentAlignment.MiddleLeft;
            btnAddBook.Location = new Point(12, 12);
            btnAddBook.Name = "btnAddBook";
            btnAddBook.Size = new Size(183, 35);
            btnAddBook.TabIndex = 1;
            btnAddBook.Text = "Agregar Libro";
            btnAddBook.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddBook.UseVisualStyleBackColor = true;
            btnAddBook.Click += btnAddBook_Click;
            // 
            // btnRemoveBook
            // 
            btnRemoveBook.Font = new Font("Roboto SemiBold", 10F, FontStyle.Bold);
            btnRemoveBook.IconChar = FontAwesome.Sharp.IconChar.TrashAlt;
            btnRemoveBook.IconColor = Color.Tan;
            btnRemoveBook.IconFont = FontAwesome.Sharp.IconFont.Auto;
            btnRemoveBook.IconSize = 24;
            btnRemoveBook.ImageAlign = ContentAlignment.MiddleLeft;
            btnRemoveBook.Location = new Point(201, 12);
            btnRemoveBook.Name = "btnRemoveBook";
            btnRemoveBook.Size = new Size(183, 35);
            btnRemoveBook.TabIndex = 2;
            btnRemoveBook.Text = "Eliminar Libro";
            btnRemoveBook.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRemoveBook.UseVisualStyleBackColor = true;
            btnRemoveBook.Click += btnRemoveBook_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(800, 450);
            Controls.Add(btnRemoveBook);
            Controls.Add(btnAddBook);
            Controls.Add(dataGridViewBooks);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Libreria";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewBooks).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private FontAwesome.Sharp.IconButton btnAddBook;
        private FontAwesome.Sharp.IconButton btnRemoveBook;
    }
}