using System.Collections.Generic;
using System.IO;

namespace RoleTopMVC.Repositories
{
    public class PublicoPrivadoRepository
    {
        public const string PATH = "Database/publicoPrivado.csv";

        public PublicoPrivadoRepository (){
            if (!File.Exists (PATH)) {
                File.Create (PATH).Close();
            }
        }

        public List<string> ObterTodos () {

            List<string> tipoEvento = new List<string> ();

            var dados = File.ReadAllLines (PATH);

            foreach (var item in dados) {
                tipoEvento.Add (item);
            }
            return tipoEvento;
        }
    }
}