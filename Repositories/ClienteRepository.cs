using System;
using System.IO;
using RoleTopMVC.Models;

namespace RoleTopMVC.Repositories
{
    public class ClienteRepository : RepositoryBase
    {
        private const string PATH = "Database/Cliente.csv";

        public ClienteRepository()
        {
            if(!File.Exists(PATH))
            {
                File.Create(PATH).Close();
            }
        }

        public bool Inserir(Cliente cliente)
        {
            var linha = new string[] { PrepararRegistroCSV(cliente) };
            File.AppendAllLines(PATH, linha);

            return true;            
        }

        public Cliente ObterPor (string email)
        {
            var linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                if(ExtrairValorDoCampo("cliente_email", item).Equals(email))
                {
                    Cliente c = new Cliente();
                    c.TipoUsuario = uint.Parse(ExtrairValorDoCampo("tipo_usuario", item));
                    c.Nome = ExtrairValorDoCampo("cliente_nome", item);
                    c.Email = ExtrairValorDoCampo("cliente_email", item);
                    c.Cpf = ExtrairValorDoCampo ("cliente_cpf", item);
                    c.Senha = ExtrairValorDoCampo("cliente_senha", item);
                    c.Telefone = ExtrairValorDoCampo("cliente_telefone", item);
                    

                    return c;
                }
            }
            return null;
        }

        private string PrepararRegistroCSV(Cliente cliente)
        {
            return $"tipo_usuario={cliente.TipoUsuario};cliente_nome={cliente.Nome};cliente_email={cliente.Email};cliente_cpf={cliente.Cpf};cliente_senha={cliente.Senha};cliente_telefone={cliente.Telefone};";
        }
    }
}