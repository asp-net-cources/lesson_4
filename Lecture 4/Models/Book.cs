using System.Text.Json.Serialization;

namespace Lecture_4.Models;

/// <summary>
/// Модель книги
/// </summary>
public class Book
{
    /// <summary>
    /// Генератор id
    /// Приватные поля класса не попадают тело ответа и не сопоставляются из тела запроса.
    /// </summary>
    private static int _idGenerator = 1;

    public int Id { get; } = _idGenerator++;
    public string Name { get; set; }
    public string AuthorName { get; set; }
    public int PagesCount { get; set; }
    public DateTime PublicationDate { get; set; }
}
