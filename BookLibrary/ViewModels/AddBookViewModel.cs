using System;
using System.IO;
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

    public event Action? OnBookAdded;

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

            var newBook = new Book
            {
                Title = Title,
                Author = Author,
                PdfPath = PdfPath
            };

            // Generar thumbnail
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

                            // Hacemos lo mismo que con el PDF para obtener la ruta relativa
                            newBook.CoverImagePath = Path.Combine("Storage", "Covers", coverFileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        // para que no falle todo si no se puede generar el thumbnail
                        Debug.WriteLine($"[DEBUG] Error al generar el thumbnail: {ex.Message}");
                    }
                }
            }

            await _bookService.AddBookAsync(newBook);
            OnBookAdded?.Invoke();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
