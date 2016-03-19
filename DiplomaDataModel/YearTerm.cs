using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DiplomaDataModel
{
    public class YearTerm
    {

        [Key]
        public int YearTermId { get; set; }

        public int Year { get; set; }

        [RegularExpression("^10|20|30$", ErrorMessage = "Term only can be 10, 20 or 30")]
        public int Term { get; set; }

        [DisplayName("Is Default")]
        public Boolean IsDefault { get; set; }

        public List<Choice> Choices { get; set; }

    }
}
