using Microsoft.AspNetCore.Mvc;

namespace Lecture_4.Models;

/// <summary>
/// Модель входных параметров
/// </summary>
public class RequestParam
{
    /// <summary>
    /// Аттрибут BindProperty указывает, с каким именем входного аргумента связать свойство MoyTest.
    /// Например, если в пути будет использован аргумент {id}, то ему в соответствие сопоставится свойство MoyTest.
    /// </summary>
    [BindProperty(Name = "id")]
    public int MoyTest { get; set; }
}

