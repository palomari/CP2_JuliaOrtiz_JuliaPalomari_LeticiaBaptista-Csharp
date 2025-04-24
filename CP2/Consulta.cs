using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP2
{
    public class Consulta
    {
        public Paciente Paciente {  get; set; }
        public DateTime DataHora { get; set; }
        public DateTime DataCriacao { get; set; }

        public Consulta(Paciente paciente, DateTime dataHora) 
        {
            Paciente = paciente;
            DataHora = dataHora;    
            DataCriacao = DateTime.Now;
        }
    }
}
