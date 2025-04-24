using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP2
{
    public class Paciente
    {
        public string Nome { get; set; }
        public string CPF { get; set; }

        public Paciente(string nome, string cpf) 
        {
            Nome = nome;
            CPF = cpf;
        }
    }
}
