using Microsoft.AspNetCore.Mvc;
using TareaCRUD.Models; // Importante: Conecta con tu archivo Cliente.cs

namespace TareaCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        // --- BASE DE DATOS TEMPORAL (EN MEMORIA) ---
        // Usamos "static" para que los datos no se borren entre peticiones
        private static List<Cliente> listaClientes = new List<Cliente>
        {
            new Cliente {
                Id = 1,
                Cedula = "1720000001",
                Nombres = "Ejemplo",
                Apellidos = "Inicial",
                FechaNacimiento = DateTime.Now,
                Genero = "Masculino",
                Direccion = "Av. Uniandes",
                Telefono = "0999999999",
                Correo = "admin@uniandes.edu.ec",
                Estado = true
            }
        };

        // 1. GET: Obtener todos los clientes
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetClientes()
        {
            return Ok(listaClientes);
        }

        // 2. GET: Obtener un cliente por ID
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = listaClientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound("Cliente no encontrado");
            return Ok(cliente);
        }

        // 3. POST: Guardar nuevo cliente
        [HttpPost]
        public ActionResult<Cliente> PostCliente(Cliente nuevoCliente)
        {
            // Generamos ID automático (El último + 1)
            int nuevoId = listaClientes.Count > 0 ? listaClientes.Max(c => c.Id) + 1 : 1;
            nuevoCliente.Id = nuevoId;

            listaClientes.Add(nuevoCliente);
            return Ok(nuevoCliente);
        }

        // 4. PUT: Editar cliente existente
        [HttpPut("{id}")]
        public IActionResult PutCliente(int id, Cliente clienteEditado)
        {
            var clienteExistente = listaClientes.FirstOrDefault(c => c.Id == id);
            if (clienteExistente == null) return NotFound("No existe ese ID");

            // Actualizamos los campos
            clienteExistente.Cedula = clienteEditado.Cedula;
            clienteExistente.Nombres = clienteEditado.Nombres;
            clienteExistente.Apellidos = clienteEditado.Apellidos;
            clienteExistente.FechaNacimiento = clienteEditado.FechaNacimiento;
            clienteExistente.Genero = clienteEditado.Genero;
            clienteExistente.Direccion = clienteEditado.Direccion;
            clienteExistente.Telefono = clienteEditado.Telefono;
            clienteExistente.Correo = clienteEditado.Correo;
            clienteExistente.Estado = clienteEditado.Estado;

            return Ok("Cliente actualizado correctamente");
        }

        // 5. DELETE: Eliminar cliente
        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = listaClientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null) return NotFound("Cliente no encontrado");

            listaClientes.Remove(cliente);
            return Ok("Cliente eliminado");
        }
    }
}