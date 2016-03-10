using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaDataModel.CustomValidation;

namespace DiplomaDataModel
{
    public class Choice {
        [Key]
        public int ChoiceId { get; set; }

        [ForeignKey("YearTerm")]
        public int? YearTermId { get; set; }
        [ForeignKey("YearTermId")]
        public virtual YearTerm YearTerm { get; set; }

        [MaxLength(9)]
        [DisplayName("Student ID")]
        public string StudentId { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "First name is required")]
        [DisplayName("First Name")]
        public string StudentFirstName { get; set; }

        [MaxLength(40)]
        [Required(ErrorMessage = "Last name is required")]
        [DisplayName("Last Name")]
        public string StudentLastName { get; set; }

        [UIHint("OptionDropDown")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }
        [ForeignKey("FirstChoiceOptionId")]
        [DisplayName("First Choice")]
        public virtual Option FirstOption { get; set; }


        [UIHint("OptionDropDown")]
        [ForeignKey("SecondOption")]
        [CheckDuplicateChoice("FirstChoiceOptionId")]
        public int? SecondChoiceOptionId { get; set; }
        [ForeignKey("SecondChoiceOptionId")]
        [DisplayName("Second Choice")]
        public virtual Option SecondOption { get; set; }


        [UIHint("OptionDropDown")]
        [ForeignKey("ThirdOption")]
        [CheckDuplicateChoice("FirstChoiceOptionId")]
        [CheckDuplicateChoice2("SecondChoiceOptionId")]
        public int? ThirdChoiceOptionId { get; set; }
        [ForeignKey("ThirdChoiceOptionId")]
        [DisplayName("Third Choice")]
        public virtual Option ThirdOption { get; set; }

        [UIHint("OptionDropDown")]
        [ForeignKey("FourthOption")]
        [CheckDuplicateChoice("FirstChoiceOptionId")]
        [CheckDuplicateChoice2("SecondChoiceOptionId")]
        [CheckDuplicateChoice3("ThirdChoiceOptionId")]
        public int? FourthChoiceOptionId { get; set; }
        [ForeignKey("FourthChoiceOptionId")]
        [DisplayName("Fourth Choice")]
        public virtual Option FourthOption { get; set; }


        private DateTime _SelectionDate = DateTime.MinValue;

        [ScaffoldColumn(false)]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime SelectionDate
        {
            get
            {
                return (_SelectionDate == DateTime.MinValue) ? DateTime.Now : _SelectionDate;
            }
            set { _SelectionDate = value; }
        }
    }
}