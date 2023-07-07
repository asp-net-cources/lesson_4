using Lecture_4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lecture_4.Controllers;


/// <summary>
/// Пример контроллера не в архитектуре REST
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LibraryController : ControllerBase
{
    private static readonly List<Book> _library = new List<Book>();
    private static int _index = 0;

    /// <summary>
    /// Эндпоинт GetNext опирается на состояние переменной _index.
    /// Такой запрос определяется не только на Http запрос, что нарушает ограничения архитектуры REST.
    /// </summary>
    /// <returns></returns>
    [HttpGet("next")]
    public Book? GetNext() {
        if (_index < _library.Count) {
            return _library[_index++];
        }

        _index = 0;
        return null;
    }

    [HttpGet("getBook")]
    public Book? GetBook([FromQuery] int id) {
        foreach(var book in _library) {
            if (book.Id == id) {
                return book;
            }
        }
        return null;
    }

    /// <summary>
    /// Эндпоинт добавления новой книги.
    /// Эндпоинт маппится с Post запросом и ожидает в теле запроса информацию для создания объекта класса Book.
    /// </summary>
    /// <param name="book">книга для добавления</param>
    /// <returns>добавленную книгу</returns>
    [HttpPost("new")]
    public Book PostBook([FromBody] Book book) {
        _library.Add(book);
        return book;
    }

    /// <summary>
    /// Эндпоинт замены книги.
    /// Эндпоинт маппится с Put запросом и ожидает в теле запроса информацию для создания объекта класса Book,
    ///     а в URL запроса информацию для определения id заменяемой книги.
    /// </summary>
    /// <param name="id">id заменяемой книги</param>
    /// <param name="book">книга для замены</param>
    /// <returns>книга</returns>
    [HttpPut("replace_book")]
    public Book PutBook([FromQuery] int id, [FromBody] Book book) {
        return book;
    }

    [HttpDelete("destroy")]
    public void DeleteBook([FromQuery] int id) {
    }
}


