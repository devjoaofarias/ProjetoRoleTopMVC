using System.Collections.Generic;
using System.IO;

namespace RoleTopMVC.Repositories
{
    public class TipoEventoRepository
    {
        public const string PATH = "Database/tipoEvento.csv";

        public TipoEventoRepository (){
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