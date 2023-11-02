using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MemberDataEntryForm.Models
{
    public class MemberData
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is must!!!")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Member Number is must!!!")]
        public string MemNo { get; set; } = null!;

        [Required(ErrorMessage = "D.O.B is Necessary!!!")]
        public DateTime Dob { get; set; }
        [Required(ErrorMessage = "Please provide the Addess!!!")]
        public string ResAddress { get; set; } = null!;
        [Required(ErrorMessage = "Enter the Valid Phone number!!!")]
        public string ResPhone { get; set; } = null!;
        [Required]
        public string OfficeNo { get; set; } = null!;
        [Required]
        public string Profession { get; set; } = null!;
        [Required]
        public string OfficeAddress { get; set; } = null!;
        [Required(ErrorMessage = "Enter the Valid Cell Number!!!")]
        public string MobileNo { get; set; } = null!;

        public string? AlternateMobileNo { get; set; }
        [Required(ErrorMessage = "Email is Mandatory!!!")]
        [RegularExpression("^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$")]
        public string Email { get; set; } = null!;
        [Required]
        public DateTime DateofMarriage { get; set; }
        [Required]
        public string NameofSpouse { get; set; } = null!;
        [Required]
        public DateTime SpouseDob { get; set; }

        public string? ChildName { get; set; }
        //[Required]
        //public string UserPassword { get; set; } = null!;
    }
}
