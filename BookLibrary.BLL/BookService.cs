using BookLibrary.DAL;
using BookLibrary.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.BLL;

public class BookService
{
    private readonly LibraryContext _context;

    public BookService(LibraryContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books.ToListAsync();
    }

    public async Task<List<Book>> SearchBooksAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return await _context.Books.ToListAsync();

        return await _context.Books
            .Where(b => b.Title.Contains(query) || b.Author.Contains(query))
            .ToListAsync();
    }

    public async Task AddBookAsync(Book book)
    {
        if (string.IsNullOrWhiteSpace(book.Title))
            throw new ArgumentException("El titulo es requerido.");

        if (string.IsNullOrWhiteSpace(book.Author))
            throw new ArgumentException("El autor es requerido.");

        if (string.IsNullOrWhiteSpace(book.PdfPath) || !book.PdfPath.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Un archivo PDF valido es requerido.");

        bool exists = await _context.Books.AnyAsync(b => b.Title == book.Title);
        if (exists)
            throw new InvalidOperationException($"Un libro con el titulo '{book.Title}' ya existe en la libreria");
            
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}

