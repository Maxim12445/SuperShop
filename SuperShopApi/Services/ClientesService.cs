using Microsoft.EntityFrameworkCore;
using SuperShopApi.Context;
using SuperShopApi.Models;

namespace SuperShopApi.Services
{
    public class ClientesService : IClienteService
    {
        private readonly AppDbContext _context;

        public ClientesService(AppDbContext context)
        {
            _context = context;
        }





        //Serviços
        public async Task<IEnumerable<Cliente>> GetClientes()
        {
            try
            {
                return await _context.Clientes.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving clients.", ex);
            }
        }

        public async Task<IEnumerable<Cliente>> GetClienteByNome(string nome)
        {
            try
            {
                IEnumerable<Cliente> clientes;
                if (!string.IsNullOrWhiteSpace(nome))
                {
                    clientes = await _context.Clientes.Where(n => n.Nome.Contains(nome)).ToListAsync();
                }
                else
                {
                    clientes = await GetClientes();
                }
                return clientes;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while retrieving clients by name.", ex);
            }
        }

        public async Task<Cliente> GetCliente(int id)
        {
            try
            {
                var cliente = await _context.Clientes.FindAsync(id);
                if (cliente == null)
                {
                    throw new KeyNotFoundException($"Client with ID {id} not found.");
                }
                else
                {
                    return cliente;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception($"An error occurred while retrieving the client with ID {id}.", ex);
            }
        }

        public async Task CreateCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while creating the client.", ex);
            }
        }

        public async Task UpdateCliente(Cliente cliente)
        {
            try
            {
                _context.Entry(cliente).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while updating the client.", ex);
            }
        }

        public async Task DeleteCliente(Cliente cliente)
        {
            try
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the client.", ex);
            }
        }
    }
}
