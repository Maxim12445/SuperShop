using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShopApi.Models;
using SuperShopApi.Services;

namespace SuperShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientesController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                var clientes = await _clienteService.GetClientes();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter clientes: {ex.Message}");
            }
        }

        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Cliente>>>
            GetClientesByName([FromQuery] string nome)
        {
            try
            {
                var clientes = await _clienteService.GetClienteByNome(nome);

                if (clientes == null)
                    return NotFound($"Não existem alunos com o critério {nome}");
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter clientes: {ex.Message}");
            }
        }
        [HttpGet("{id:int}", Name = "GetCliente")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            try
            {
                var cliente = await _clienteService.GetCliente(id);

                if (cliente == null)
                    return NotFound($"Não existem alunos com id = {id}");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter clientes: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Cliente cliente)
        {
            try
            {
                await _clienteService.CreateCliente(cliente);
                return CreatedAtRoute(nameof(GetCliente), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter clientes: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Edit(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if(cliente.Id == id)
                {
                    await _clienteService.UpdateCliente(cliente);
                    //return NoContent();
                    return Ok($"Aluno com id={id} foi atualizado com sucesso");
                }
                else
                {
                    return BadRequest("Dados inconsistentes");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter clientes: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var cliente = await _clienteService.GetCliente(id);
                if(cliente != null)
                {
                    await _clienteService.DeleteCliente(cliente);
                    return Ok($"Aluno de id={id} foi axcluido com seccesso");
                }
                else
                {
                    return NotFound($"Aluno com id={id} não encontrado");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter clientes: {ex.Message}");
            }
        }
    }
}