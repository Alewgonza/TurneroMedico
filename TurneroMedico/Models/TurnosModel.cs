using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TurneroMedico.Models
{
    public class TurnosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NombreApellido { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }

        public string Telefono { get; set; }

        [Display(Name = "Fecha del turno")]
        public DateTime FechaTurno { get; set; }
        [EnumDataType(typeof(Especialidades))]
        public Especialidades EspecialidadElegida { get; set; }

        [EnumDataType(typeof(Doctores))]
        public Doctores DoctorElegido { get; set; }
    }
}
