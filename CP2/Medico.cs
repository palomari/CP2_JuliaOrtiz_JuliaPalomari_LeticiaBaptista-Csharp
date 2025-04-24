using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP2
{
    public class Medico
    {
        public string Nome {  get; set; }
        public string CRM { get; set; }
        private List<Consulta> consultas = new();
        public Medico(string nome, string crm) 
        {
            Nome = nome;
            CRM = crm;
        }

        public void AdicionarConsulta(Consulta consulta) => consultas.Add(consulta);
        public void RemoverConsulta(Consulta consulta) => consultas.Remove(consulta);
        public List<Consulta> ObterConsultas() => consultas;
    }
}
