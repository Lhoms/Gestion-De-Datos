using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaFrba.extras
{

    public class Profesional
    {
        public int id;
        public string nombre;
        public string apellido;
        public string nombre_apellido;

        public Profesional(int id, string nombre, string apellido)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.nombre_apellido = apellido + ", " +  nombre;
        }
    }
}
