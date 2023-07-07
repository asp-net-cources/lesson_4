using Lecture_4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture_4.Controllers;

/// <summary>
/// Пример контроллера не в архитектуре REST
/// Путь до контроллера содержит в себе название сущности, с которой контроллер предоставляет возможность работать.
/// Работа с сущностью Book со стороны клиента полностью осуществляется через данный контроллер.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase {
    private static readonly List<Book> _books = new();

    /// <summary>
    /// Эндпоинт маппится с запросом на чтение всех сущностей.
    /// </summary>
    /// <returns>книги</returns>
    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public List<Book> GetAll() {
        return _books;
    }

    /// <summary>
    /// Эндпоинт маппится с запросом на чтение сущности с указанным id.
    /// Здесь входные параметры будут привязываться к полям модели RequestParam.
    /// Общепринятый подход - всегда возвращать успешный статус 200 в случае, если сущность найдена, и 204 в случае, если сущность не найдена.
    /// </summary>
    /// <param name="rp">Модель входных параметров</param>
    /// <returns>найденную книгу</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult<Book?> Get([FromRoute] RequestParam rp) {
        foreach (var book in _books) {
            if (book.Id == rp.MoyTest) {
                return Ok(book);
            }
        }

        return NoContent();
    }

    /// <summary>
    /// Эндпоинт маппится с запросом на добавление сущности.
    /// Эндпоинт ожидает в теле запроса информацию для создания объекта класса Book.
    /// Общепринятый подход - всегда возвращать успешный статус 201, если не возникло никаких ошибок в процессе создания сущности.
    /// </summary>
    /// <param name="book">Модель книги для создания</param>
    /// <returns>Созданная книга</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public ActionResult<Book?> Post([FromBody] Book book) {
        _books.Add(book);
        return new ObjectResult(book) { StatusCode = StatusCodes.Status201Created };
    }

    /// <summary>
    /// Эндпоинт маппится с запросом на обновление (замену) сущности.
    /// Эндпоинт ожидает в теле запроса информацию для создания объекта класса Book,
    ///     а в URL запроса информацию для определения id заменяемой книги.
    /// Общепринятый подход - всегда возвращать успешный статус 200, если не возникло никаких ошибок в процессе создания сущности.
    /// Если сущность с указанным id была найдена, то она заменяется на новую из модели book.
    /// Если сущность с указанным id не была найдена, то происходит вставка новой.
    /// </summary>
    /// <param name="id">id заменяемой книги</param>
    /// <param name="book">книга для замены</param>
    /// <returns>Обновленная книга</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Book?> Put([FromRoute] int id, [FromBody] Book book) {
        var isFounded = false;

        for (int i = 0; i < _books.Count; i++) {
            if (_books[i].Id == id) {
                _books[i] = book;
                isFounded = true;
            }
        }

        if(!isFounded) {
            _books.Add(book);
        }

        return Ok(book);
    }

    /// <summary>
    /// Эндпоинт маппится с запросом на удалением сущности.
    /// Эндпоинт ожидает id книги в Route параметрах запроса.
    /// Общепринятый подход - всегда возвращать успешный статус 204, если не возникло никаких ошибок в процессе создания сущности.
    /// Вне зависимости от того, была ли действительно удалена сущность,
    ///     запрос считается успешным, так как после выполнения запроса такой сущности на сервере не существует.
    /// </summary>
    /// <param name="id">id книги для удаления</param>
    /// <returns>Статус код 204</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete([FromRoute] int id) {
        for(int i = 0; i < _books.Count; i++) {
            if (_books[i].Id == id) {
                _books.RemoveAt(i);
                break;
            }
        }

        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }
}
