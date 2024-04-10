using MyLibrary.Enums;

namespace MyLibrary.Entities;

public class Book
{
    public Guid Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Author { get; set; } = String.Empty;
    public EGenre Genre { get; set; } = EGenre.None;
    public double Price { get; set; }
    public int QuantityInStock { get; set; }
}
