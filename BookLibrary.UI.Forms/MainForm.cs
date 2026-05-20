using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BookLibrary.BLL;
using BookLibrary.DAL;
using BookLibrary.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.UI.Forms;

public partial class MainForm : Form
{
    private readonly BookService _bookService;

    public MainForm()
    {
        InitializeComponent();
        var icoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "buklib.ico");
        if (File.Exists(icoPath))
        {
            this.Icon = new Icon(icoPath);
        }

        var context = new LibraryContext();
        _bookService = new BookService(context);
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        try
        {
            using (var context = new LibraryContext())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error de conexion o inicialización de base de datos: {ex.Message}", "Error de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        await LoadBooksAsync();
    }

    private async Task LoadBooksAsync()
    {
        try
        {
            var books = await _bookService.GetAllBooksAsync();
            dataGridViewBooks.DataSource = books;

            if (dataGridViewBooks.Columns.Count > 0)
            {
                dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
                if (dataGridViewBooks.Columns["Id"] != null)
                    dataGridViewBooks.Columns["Id"].FillWeight = 30;
                
                if (dataGridViewBooks.Columns["Title"] != null)
                    dataGridViewBooks.Columns["Title"].FillWeight = 200; // ei titulo mas ancho
                    
                if (dataGridViewBooks.Columns["PdfPath"] != null)
                    dataGridViewBooks.Columns["PdfPath"].FillWeight = 85;

                if (dataGridViewBooks.Columns["CoverImagePath"] != null)
                    dataGridViewBooks.Columns["CoverImagePath"].FillWeight = 85;
            }
        }
        catch (Exception ex)
        {
            // los errores vienen de la BLL
            MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAddBook_Click(object sender, EventArgs e)
    {
        using var addForm = new AddBookForm(_bookService);
        if (addForm.ShowDialog() == DialogResult.OK)
        {
            _ = LoadBooksAsync();
        }
    }

    private async void btnRemoveBook_Click(object sender, EventArgs e)
    {
        if (dataGridViewBooks.SelectedRows.Count > 0)
        {
            var selectedRow = dataGridViewBooks.SelectedRows[0];
            if (selectedRow.DataBoundItem is Book selectedBook)
            {
                var result = MessageBox.Show($"Seguro de que desea eliminar '{selectedBook.Title}'?", "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _bookService.DeleteBookAsync(selectedBook);
                        await LoadBooksAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al eliminar libro: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        else
        {
            MessageBox.Show("Por favor seleccione un libro para eliminar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {

    }
}