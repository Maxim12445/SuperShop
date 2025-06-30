using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperShopApi.Services;

namespace SuperShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

    }
}
