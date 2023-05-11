using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonasPrueba.Models
{
	public class People
    {
		public int idPersona { get; set; }
        public int idTipoPersona { get; set; }
        public string tipoPersona { get; set; }
        public string numeroDocumento { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string fechaNacimiento { get; set; }
        public int idEmpresa { get; set; }
        public string empresa { get; set; }
        public int idUsuarioCreacion { get; set; }
        public string fechaCreacion { get; set; }
        public int idusuarioModificacion { get; set; }
        public string fechaModificacion { get; set; }
        public int idEstado { get; set; }
        public string estado { get; set; }
        public string urlFoto { get; set; }
        public string urlFotoOld { get; set; }
    }
}