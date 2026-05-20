using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using BookLibrary.BLL;
using BookLibrary.Entities.Models;

namespace BookLibrary.UI.Forms;

public partial class AddBookForm : Form
{
    private readonly BookService _bookService;

    public AddBookForm(BookService bookService)
    {
        InitializeComponent();
        var addIco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "add.ico");
        var defaultIco = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "buklib.ico");
        if (File.Exists(addIco))
        {
            this.Icon = new Icon(addIco);
        }
        else if (File.Exists(defaultIco))
        {
            this.Icon = new Icon(defaultIco);
        }
        _bookService = bookService;
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var book = new Book
            {
                Title = txtTitle.Text,
                Author = txtAuthor.Text,
                PdfPath = txtPdfPath.Text
            };

            await _bookService.AddBookAsync(book);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error al guardar el libro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.Filter = "PDF (*.pdf)|*.pdf";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            txtPdfPath.Text = ofd.FileName;
        }
    }

    private void AddBookForm_Load(object sender, EventArgs e)
    {

    }

    private void txtTitle_TextChanged(object sender, EventArgs e)
    {

    }
}