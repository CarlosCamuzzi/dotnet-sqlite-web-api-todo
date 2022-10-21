using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuToDo.Data;
using MeuToDo.Models;
using MeuToDo.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeuToDo.Controllers
{
  [ApiController]
  [Route(template: "v1")]
  public class TodoController : ControllerBase
  {
    [HttpGet]
    [Route(template: "todos")]
    public async Task<IActionResult> Get([FromServices] AppDbContext context)
    {
      var todos = await context.Todos
      .AsNoTracking()
      .ToListAsync();
      return Ok(todos);
    }

    [HttpGet]
    [Route(template: "todos/{id}")] // Parâmetro de Rota
    public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {
      var todo = await context.Todos
      .AsNoTracking()
      .FirstOrDefaultAsync(x => x.Id == id);
      return todo == null ? NotFound() : Ok(todo);
    }

    // Rota também pode ser passada dessa forma
    [HttpPost(template: "todos")]
    public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model)
    {
      if (!ModelState.IsValid)
      {    // Aplica validaçãos no create todo
        return BadRequest();
      }

      var todo = new Todo
      {
        Date = DateTime.Now,
        Done = false,
        Title = model.Title
      };

      try
      {
        await context.Todos.AddAsync(todo);   // Salva na memória
        await context.SaveChangesAsync(); // Salva no banco
        return Created($"v1/todos/{todo.Id}" //url
        , todo);
      }
      catch (System.Exception)
      {
        return BadRequest(); // ver retorno correto
      }
    }

    // Rota também pode ser passada dessa forma
    [HttpPut(template: "todos/{id}")]
    public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model, [FromRoute] int id)    // id vem da rota
    {
      if (!ModelState.IsValid)
      {    // ModelState Aplica validação no create 'todo'
        return BadRequest();
      }

      // Recuperando do banco
      var todo = await context.Todos
      .FirstOrDefaultAsync(x => x.Id == id);

      if (todo == null)
      {
        return NotFound();
      }

      try
      {
        todo.Title = model.Title;

        context.Todos.Update(todo);
        await context.SaveChangesAsync();
        return Ok(todo);
      }
      catch (System.Exception)
      {
        return BadRequest(); // ver retorno correto
      }
    }

    [HttpDelete(template: "todos/{id}")]
    public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
    {

      var todo = await context.Todos
      .FirstOrDefaultAsync(x => x.Id == id);

      try
      {
        context.Todos.Remove(todo);
        await context.SaveChangesAsync();

        return Ok();
      }
      catch (System.Exception)
      {
        return BadRequest();
      }
    }
  }
}