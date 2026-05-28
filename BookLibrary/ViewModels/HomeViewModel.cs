using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookLibrary.BLL;
using System.Diagnostics;
using System.IO;
using BookLibrary.Entities.Models;

namespace BookLibrary.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly BookService _bookService;

    [ObservableProperty]
    private ObservableCollection<Book> _books = new();

    [ObservableProperty]
    private string _searchQuery = string.Empty;

    public HomeViewModel(BookService bookService)
    {
        _bookService = bookService;
    }

    public async Task LoadBooksAsync()
    {
        var result = await _bookService.GetAllBooksAsync();
        Books.Clear();
        foreach (var book in result)
        {
            Books.Add(book);
        }
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        var result = await _bookService.SearchBooksAsync(SearchQuery);
        Books.Clear();
        foreach (var book in result)
        {
            Books.Add(book);
        }
    }

    [RelayCommand]
    private async Task DeleteBookAsync(Book? book)
    {
        if (book == null) return;
        
        await _bookService.DeleteBookAsync(book);
        
        try 
        {
            var pdfPath = Path.IsPathRooted(book.PdfPath)
                ? book.PdfPath
                : Path.Combine(AppContext.BaseDirectory, book.PdfPath);

            if (!Path.IsPathRooted(book.PdfPath) && !File.Exists(pdfPath))
            {
                var appxPath = Path.Combine(AppContext.BaseDirectory, "AppX", book.PdfPath);
                if (File.Exists(appxPath))
                {
                    pdfPath = appxPath;
                }
            }

            if (File.Exists(pdfPath))
            {
                File.Delete(pdfPath);
            }
        }
        catch {}

        Books.Remove(book);
    }

    [RelayCommand]
    private void OpenPdf(Book? book)
    {
        if (book == null || string.IsNullOrWhiteSpace(book.PdfPath)) return;

        var fullPath = Path.IsPathRooted(book.PdfPath)
            ? book.PdfPath
            : Path.Combine(AppContext.BaseDirectory, book.PdfPath);

        if (!Path.IsPathRooted(book.PdfPath) && !File.Exists(fullPath))
        {
            var appxPath = Path.Combine(AppContext.BaseDirectory, "AppX", book.PdfPath);
            if (File.Exists(appxPath))
            {
                fullPath = appxPath;
            }
        }

        if (File.Exists(fullPath))
        {
            try
            {
                Process.Start(new ProcessStartInfo()
                {
                    FileName = fullPath,
                    UseShellExecute = true,
                    Verb = "open"
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to open PDF: {ex.Message}");
            }
        }
        else
        {
            Debug.WriteLine($"[WARN] PDF no encontrado en: {fullPath}");
        }
    }
}
