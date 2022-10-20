using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeuToDo.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeuToDo.Controllers
{
  [ApiController]
  [Route(template: "v1")]
  public class TodoController : ControllerBase
  {
    [HttpGet]
    [Route(template:"todos")]
    public List<Todo> Get() => new List<Todo>();
    
  }
}