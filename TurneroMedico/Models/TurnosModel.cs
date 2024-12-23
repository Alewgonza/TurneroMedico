﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace TurneroMedico.Models
{
    public class TurnosModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Número de turno")]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Por favor complete su nombre y apellido"), MaxLength(60)]
        [Display(Name = "Nombre y Apellido")]
        public string NombreApellido { get; set; }

        [Required(ErrorMessage = "Por favor complete su DNI")]
        [Display(Name = "DNI:")]
        [Range(0, 99999999)]
        public int Dni { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Por favor ingrese su email")]
        [Display(Name = "EMAIL:")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Por favor ingrese su nro de teléfono")]
        [Display(Name = "Número de telefono")]
        [Range(0, 9999999999)]
        public int Telefono { get; set; }

        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Por favor seleccione un turno")]
        [Display(Name = "Fecha del turno:")]
        public DateTime FechaTurno { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaFinTurno { get; set; }

        [EnumDataType(typeof(Especialidades))]
        [Required(ErrorMessage = "Por favor seleccione una especialidad")]
        [Display(Name = "Especialidad:")]
        public Especialidades EspecialidadElegida { get; set; }

        [EnumDataType(typeof(Doctores))]
        [Required(ErrorMessage = "Por favor seleccione un doctor")]
        [Display(Name = "Doctor:")]
        public Doctores DoctorElegido { get; set; }

        public readonly int DuracionTurno = 30;
    }
}
