using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meetup.Models
{
    public enum Profession
    {
        [Display(Name = "Employed")] Employed=1,
        [Display(Name = "Student")] Student
    }
    [Table("Participants")]
    public class Participant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParticipantId { get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
        [Column("DateOfBirth")]
        [DataType(DataType.Date)]
        public DateTime DoB { get; set; }
        public Profession Profession { get; set; }
        [Required]
        public string Locality { get; set; }
        [Range(0,2)]
        public int NumberOfGuests { get; set; }        
        [StringLength(50)]
        [Required]
        public string Address { get; set; }

        internal void UpdateProperties(Participant participant)
        {
            this.Name = participant.Name;
            this.Age = participant.Age;
            this.DoB = participant.DoB;
            this.Profession = participant.Profession;
            this.Locality = participant.Locality;
            this.NumberOfGuests = participant.NumberOfGuests;
            this.Address = participant.Address;
        }
    }
}
