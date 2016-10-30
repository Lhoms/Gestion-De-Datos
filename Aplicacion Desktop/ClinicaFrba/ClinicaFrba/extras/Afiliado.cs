using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaFrba.extras
{
    public class Afiliado
    {
        public int id;

        //Atributos como usuario
        public string username; //documento en string
        public string tipo_doc;

        //Atributos como persona
        public string nombre;
        public string apellido;
        public string direccion;
        public string mail;
        public long documento;
        public string documento_s;
        public long telefono;
        public string telefono_s;
        public string sexo;
        public DateTime nacimiento;

        //Atributos como afiliado
        public long numeroAfiliado;
        public string numeroAfiliado_s;
        public string estadoCivil;
        public string planMedico;
        public int planMedico_id;
        public int cantFamiliares;
        public int nroConsulta;

    }
}
