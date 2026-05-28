using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using BookLibrary.BLL;
using BookLibrary.Entities.Models;

namespace BookLibrary.UI.Forms;

public partial class AddBookForm : Form
{
    private readonly BookService _bookService;
    private readonly ErrorProvider _errorProvider;

    public AddBookForm(BookService bookService)
    {
        InitializeComponent();
        _errorProvider = new ErrorProvider();
        _errorProvider.ContainerControl = this;
        _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

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
        _errorProvider.Clear();

        bool hasError = false;

        if (string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            _errorProvider.SetError(txtTitle, "El titulo es requerido.");
            hasError = true;
        }

        if (string.IsNullOrWhiteSpace(txtAuthor.Text))
        {
            _errorProvider.SetError(txtAuthor, "El autor es requerido.");
            hasError = true;
        }
        else if (txtAuthor.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '.' && c != ',' && c != ';'))
        {
            _errorProvider.SetError(txtAuthor, "El nombre del autor contiene caracteres no validos.");
            hasError = true;
        }

        if (string.IsNullOrWhiteSpace(txtPdfPath.Text) || !txtPdfPath.Text.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            _errorProvider.SetError(txtPdfPath, "Un archivo PDF valido es requerido.");
            hasError = true;
        }

        if (hasError)
        {
            return;
        }

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
        catch (InvalidOperationException ex)
        {
            _errorProvider.SetError(txtTitle, ex.Message);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error al guardar el libro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog();
        ofd.Filter = "PDF (*.pdf)|*.pdf";
        if (ofd.ShowDialog() == DialogResult.OK)
        {
            txtPdfPath.Text = ofd.FileName;
            _errorProvider.SetError(txtPdfPath, string.Empty);
        }
    }

    private void AddBookForm_Load(object sender, EventArgs e)
    {
    }

    private void txtTitle_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtTitle.Text))
        {
            _errorProvider.SetError(txtTitle, string.Empty);
        }
    }

    private void txtAuthor_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(txtAuthor.Text) && !txtAuthor.Text.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '.' && c != ',' && c != ';'))
        {
            _errorProvider.SetError(txtAuthor, string.Empty);
        }
    }

    private void AddBookForm_Shown(object sender, EventArgs e)
    {
    }

    private void lblTitle_Click(object sender, EventArgs e)
    {
    }
}