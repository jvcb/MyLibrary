using Microsoft.AspNetCore.Mvc;
using MyLibrary.Communication.Requests;
using MyLibrary.Data;
using MyLibrary.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyLibrary.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly BookDb _bookDb;

    public BookController(BookDb bookDb)
    {
        _bookDb = bookDb;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(_bookDb.Books);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] CreateBookRequest request)
    {
        _bookDb.Books.Add(new Book
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Author = request.Author,
            Genre = request.Genre,
            Price = request.Price,
            QuantityInStock = request.QuantityInStock,
        });

        return Created("", _bookDb.Books[_bookDb.Books.Count - 1]);
    }

    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPut]
    public IActionResult Update([FromBody] UpdateBookRequest request)
    {
        var bookExist = _bookDb.Books.Find(b => b.Id == request.Id);

        if (bookExist is null) return NotFound();

        _bookDb.Books = _bookDb.Books.Where(b => b.Id == request.Id)
            .Select(b =>
            {
                b.Title = request.Title;
                b.Author = request.Author;
                b.Genre = request.Genre;
                b.Price = request.Price;
                b.QuantityInStock = request.QuantityInStock;

                return b;
            }).ToList();

        return NoContent();
    }

    [HttpDelete]
    [Route("{bookId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete([FromRoute] Guid bookId) 
    { 
        var bookIndex = _bookDb.Books.FindIndex(b => b.Id == bookId);

        if (bookIndex == -1) return NotFound();

        _bookDb.Books.RemoveAt(bookIndex);

        return NoContent();
    }
}
