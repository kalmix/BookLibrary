namespace BookLibrary.Entities.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string PdfPath { get; set; } = string.Empty;
    public string CoverImagePath { get; set; } = string.Empty;
}
