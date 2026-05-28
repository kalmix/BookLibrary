using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using BookLibrary.BLL;
using BookLibrary.Entities.Models;

namespace BookLibrary.UI.Forms;

public partial class AddBookForm : Form
{
    private bool loaded = false;
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

    private void txtAuthor_KeyPress(object sender, KeyPressEventArgs e)
    {
        // solo permitir letras, espacios, backspace y (,.;)
        if (!char.IsLetter(e.KeyChar) &&
        !char.IsWhiteSpace(e.KeyChar) &&
        !char.IsControl(e.KeyChar) &&
        e.KeyChar != '.' &&
        e.KeyChar != ',' &&
        e.KeyChar != ';')
        {
            e.Handled = true;
        }
    }

    private void txtAuthor_TextChanged(object sender, EventArgs e)
    {

    }
    private void AddBookForm_Shown(object sender, EventArgs e)
    {
        loaded = true;
    }

    private void txtAuthor_Enter(object sender, EventArgs e)
    {
        ToolTip toolTipAuthor = new ToolTip();
        toolTipAuthor.ToolTipTitle = "Nombre Del Autor (e.j. J. Verne)";
        toolTipAuthor.Show("Solo se permiten letras, espacios y estos caracteres especiales (,.;)", txtAuthor);
    }

    private void txtTitle_Enter(object sender, EventArgs e)
    {
        if (loaded)
        {
            ToolTip toolTipTitle = new ToolTip();
            toolTipTitle.ToolTipTitle = "Titulo (e.j. El Quijote)";
            toolTipTitle.Show("Se permite cualquier caracter", txtTitle);
        }
    }

    private void lblTitle_Click(object sender, EventArgs e)
    {

    }
}