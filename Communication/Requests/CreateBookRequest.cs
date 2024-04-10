using MyLibrary.Enums;

namespace MyLibrary.Communication.Requests;

public class CreateBookRequest
{
    public string Title { get; set; } = String.Empty;
    public string Author { get; set; } = String.Empty;
    public EGenre Genre { get; set; }
    public double Price { get; set; }
    public int QuantityInStock { get; set; }
}
