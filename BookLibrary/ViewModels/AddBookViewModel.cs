using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookLibrary.BLL;
using BookLibrary.Entities.Models;
using System.Diagnostics;

namespace BookLibrary.ViewModels;

public partial class AddBookViewModel : ObservableObject
{
    private readonly BookService _bookService;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _author = string.Empty;

    [ObservableProperty]
    private string _pdfPath = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private string _titleError = string.Empty;

    [ObservableProperty]
    private string _authorError = string.Empty;

    [ObservableProperty]
    private string _pdfError = string.Empty;

    public event Action? OnBookAdded;

    partial void OnTitleChanged(string value)
    {
        TitleError = string.Empty;
        if (string.IsNullOrWhiteSpace(value))
        {
            TitleError = "El titulo es requerido.";
        }
    }

    partial void OnAuthorChanged(string value)
    {
        AuthorError = string.Empty;
        if (string.IsNullOrWhiteSpace(value))
        {
            AuthorError = "El autor es requerido.";
        }
        else if (value.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '.' && c != ',' && c != ';'))
        {
            AuthorError = "El nombre del autor contiene caracteres no validos.";
        }
    }

    partial void OnPdfPathChanged(string value)
    {
        PdfError = string.Empty;
        if (string.IsNullOrWhiteSpace(value) || !value.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        {
            PdfError = "Un archivo PDF valido es requerido.";
        }
    }

    public AddBookViewModel(BookService bookService)
    {
        _bookService = bookService;
    }

    [RelayCommand]
    private async Task SaveBookAsync()
    {
        try
        {
            ErrorMessage = string.Empty;
            TitleError = string.Empty;
            AuthorError = string.Empty;
            PdfError = string.Empty;

            bool hasError = false;

            if (string.IsNullOrWhiteSpace(Title))
            {
                TitleError = "El titulo es requerido.";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(Author))
            {
                AuthorError = "El autor es requerido.";
                hasError = true;
            }
            else if (Author.Any(c => !char.IsLetter(c) && !char.IsWhiteSpace(c) && c != '.' && c != ',' && c != ';'))
            {
                AuthorError = "El nombre del autor contiene caracteres no validos.";
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(PdfPath) || !PdfPath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                PdfError = "Un archivo PDF valido es requerido.";
                hasError = true;
            }

            if (hasError)
            {
                return;
            }

            var newBook = new Book
            {
                Title = Title,
                Author = Author,
                PdfPath = PdfPath
            };

            if (!string.IsNullOrWhiteSpace(PdfPath))
            {
                string fullPdfPath = Path.Combine(AppContext.BaseDirectory, PdfPath);
                if (File.Exists(fullPdfPath))
                {
                    try
                    {
                        using var pdfFileStream = File.OpenRead(fullPdfPath);
                        var winrtStream = pdfFileStream.AsRandomAccessStream();
                        var pdfDoc = await Windows.Data.Pdf.PdfDocument.LoadFromStreamAsync(winrtStream);
                        if (pdfDoc.PageCount > 0)
                        {
                            using var firstPage = pdfDoc.GetPage(0);
                            
                            var storagePath = Path.Combine(AppContext.BaseDirectory, "Storage", "Covers");
                            Directory.CreateDirectory(storagePath);

                            var coverFileName = $"{Guid.NewGuid()}_cover.png";
                            var coverFilePath = Path.Combine(storagePath, coverFileName);

                            using var memStream = new Windows.Storage.Streams.InMemoryRandomAccessStream();
                            var options = new Windows.Data.Pdf.PdfPageRenderOptions
                            {
                                DestinationHeight = 300
                            };
                            await firstPage.RenderToStreamAsync(memStream, options);

                            using var fileStream = File.Create(coverFilePath);
                            memStream.Seek(0);
                            await memStream.AsStreamForRead().CopyToAsync(fileStream);

                            newBook.CoverImagePath = Path.Combine("Storage", "Covers", coverFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"[DEBUG] Error al generar el thumbnail: {ex.Message}");
                    }
                }
            }

            await _bookService.AddBookAsync(newBook);
            OnBookAdded?.Invoke();
        }
        catch (InvalidOperationException ex)
        {
            TitleError = ex.Message;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
