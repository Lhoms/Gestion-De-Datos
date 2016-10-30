using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaFrba.extras
{
    public class Sesion
    {
        public string username;
        public int tipo_doc_id;
        public int user_id;

        public Sesion(string username, int tipo_doc_id, int user_id)
        {
            this.username = username;
            this.tipo_doc_id = tipo_doc_id;
            this.user_id = user_id;
        }

    }
}
