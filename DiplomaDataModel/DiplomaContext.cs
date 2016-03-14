using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DiplomaDataModel
{
    public class DiplomaContext : DbContext
    {
        public DiplomaContext() : base("DefaultConnection") { }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<YearTerm> YearTerms { get; set; }

    }
}
