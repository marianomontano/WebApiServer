using Microsoft.AspNetCore.Mvc;
using Servidor.Models;
using Servidor.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Servidor.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private IContactMapper _db;

		public ContactController(IContactMapper contacts)
		{
			_db = contacts;
		}

		// GET: api/<ContactController>
		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_db.ListarTodos());
		}

		// GET api/<ContactController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var contacto = _db.BuscarContactoPorId(id);

			if (contacto == null)
				return NotFound();

			return Ok(contacto);
		}

		// POST api/<ContactController>
		[HttpPost]
		public IActionResult Post([FromBody] ContactModel contacto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			bool saved = _db.GuardarContacto(contacto);
			int id = _db.ListarTodos().Max(c => c.Id);

			if (!saved)
				return StatusCode(500);

			return Created($"api/contact/{id}", contacto);
		}

		// PUT api/<ContactController>/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] ContactModel contacto)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			var contact = _db.BuscarContactoPorId(id);

			if (contact == null)
				return NotFound();

			bool saved = _db.EditarContacto(contacto);

			if (!saved)
				return StatusCode(500);

			return Ok(contacto);
		}

		// DELETE api/<ContactController>/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var contacto = _db.BuscarContactoPorId(id);
			if (contacto == null)
				return NotFound();

			bool deleted = _db.EliminarContacto(id);
			if (!deleted)
				return StatusCode(500);

			return Ok(contacto);
		}
	}
}
