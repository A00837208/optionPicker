namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using DiplomaDataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\DiplomaMigrations";
        }

        protected override void Seed(DiplomaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

           List<YearTerm> yearTerms = new List<YearTerm>()
            {
                new YearTerm {Year=2015, Term=20, IsDefault=false },
                new YearTerm {Year=2015, Term=30, IsDefault=false },
                new YearTerm {Year=2016, Term=10, IsDefault=false },
                new YearTerm {Year=2016, Term=30, IsDefault=true }
            };
            context.YearTerms.AddOrUpdate(y => new { y.Year, y.Term }, yearTerms.ToArray());
            
            List<Option> options = new List<Option>()
            {
                new Option {Title="Data Communications", IsActive=true },
                new Option {Title="Client Server", IsActive=true },
                new Option {Title="Digital Processing", IsActive=true },
                new Option {Title="Information Systems",IsActive=true },
                new Option {Title="Database", IsActive=false  },
                new Option {Title="Web & Mobile", IsActive=true},
                new Option {Title="Tech Pro", IsActive=false}
            };
            context.Options.AddOrUpdate(o => o.Title, options.ToArray());

            List<Choice> choices = new List<Choice>()
            {

                new Choice {YearTermId=2 , StudentId="A00333333" , StudentFirstName="aa" , StudentLastName="bb" , FirstChoiceOptionId=1 , SecondChoiceOptionId=2 , ThirdChoiceOptionId=3 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=2 , StudentId="A00333334" , StudentFirstName="cc" , StudentLastName="dd" , FirstChoiceOptionId=2 , SecondChoiceOptionId=1 , ThirdChoiceOptionId=3 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=2 , StudentId="A00333335" , StudentFirstName="ee" , StudentLastName="qq" , FirstChoiceOptionId=3 , SecondChoiceOptionId=1 , ThirdChoiceOptionId=2 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=2 , StudentId="A00333336" , StudentFirstName="ww" , StudentLastName="rr" , FirstChoiceOptionId=4 , SecondChoiceOptionId=1 , ThirdChoiceOptionId=2 , FourthChoiceOptionId=3 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=2 , StudentId="A00333337" , StudentFirstName="yy" , StudentLastName="tt" , FirstChoiceOptionId=1 , SecondChoiceOptionId=2 , ThirdChoiceOptionId=3 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now},

                new Choice {YearTermId=3 , StudentId="A00333338" , StudentFirstName="uu" , StudentLastName="ii" , FirstChoiceOptionId=2 , SecondChoiceOptionId=1 , ThirdChoiceOptionId=3 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=3 , StudentId="A00333339" , StudentFirstName="pp" , StudentLastName="oo" , FirstChoiceOptionId=3 , SecondChoiceOptionId=1 , ThirdChoiceOptionId=2 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=3 , StudentId="A00333330" , StudentFirstName="ss" , StudentLastName="ff" , FirstChoiceOptionId=4 , SecondChoiceOptionId=1 , ThirdChoiceOptionId=2 , FourthChoiceOptionId=3 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=3 , StudentId="A00333332" , StudentFirstName="hh" , StudentLastName="gg" , FirstChoiceOptionId=1 , SecondChoiceOptionId=2 , ThirdChoiceOptionId=3 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now},
                new Choice {YearTermId=3 , StudentId="A00333331" , StudentFirstName="jj" , StudentLastName="kk" , FirstChoiceOptionId=2 , SecondChoiceOptionId=1 , ThirdChoiceOptionId=3 , FourthChoiceOptionId=4 , SelectionDate=DateTime.Now}

            };
            context.Choices.AddOrUpdate(c => c.StudentId, choices.ToArray());
        }
    }
}
