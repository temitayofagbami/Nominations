using IndividualNominationCatalogAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividualNominationCatalogAPI.Data
{
    public class IndividualNominationCatalogSeed
    {
        public static async Task SeedAsync(IndividualNominationCatalogContext context)

        {

            context.Database.Migrate();

            if (!context.AwardCategories.Any())

            {

                context.AwardCategories.AddRange

                    (GetPreconfiguredAwardCategories());

                await context.SaveChangesAsync();

            }

            if (!context.Locations.Any())

            {

                context.Locations.AddRange

                    (GetPreconfiguredLocations());

                context.SaveChanges();

            }

            if (!context.SubOrgs.Any())

            {

                context.SubOrgs.AddRange

                    (GetPreconfiguredSubOrgs());

                context.SaveChanges();

            }

            if (!context.Nominations.Any())

            {

                context.Nominations.AddRange

                    (GetPreconfiguredNominations());

                context.SaveChanges();

            }



        }



        static IEnumerable<AwardCategory> GetPreconfiguredAwardCategories()

        {

            return new List<AwardCategory>()

            {
               new AwardCategory() { Name = "Diversity and Inclusion", CreatedBy="v-kinarg"},

               new AwardCategory() { Name = "Climate of Trust", CreatedBy="v-kinarg"},

               new AwardCategory() { Name = "Develop and Learn", CreatedBy="v-kinarg"},

            };

        }



        static IEnumerable<Location> GetPreconfiguredLocations()

        {

            return new List<Location>()

            {
              new Location() { Name = "United States", CreatedBy="v-kinarg"},

              new Location() { Name = "India", CreatedBy="v-kinarg"},

              new Location() { Name = "Ireland", CreatedBy="v-kinarg"},

              new Location() { Name = "Other", CreatedBy="v-kinarg"},

            };

        }


        static IEnumerable<SubOrg> GetPreconfiguredSubOrgs()

        {

            return new List<SubOrg>()

            {
              new SubOrg() { Name = "Supply Chain Engineering", CreatedBy="v-kinarg"},

              new SubOrg() { Name = "Commercial Sales and Marketing Engineering", CreatedBy="v-kinarg"},

              new SubOrg() { Name = "Consumer Sales and Marketing Engineering", CreatedBy="v-kinarg"},

              new SubOrg() { Name = "Other", CreatedBy="v-kinarg"},

            };

        }



        static IEnumerable<Nomination> GetPreconfiguredNominations()

        {

            return new List<Nomination>()

            {
             
            new Nomination() { Alias="v-tefagb", Headline="Tayo Nomination",  AwardCategoryId=1, LocationId=1, SubOrgId = 1, DescriptionComments = "she was great", ImpactComments="she was impactful", ReviewStatus = false, Winner=false, CreatedBy= "v-kinarg"},
            new Nomination() { Alias="v-kinarg", Headline="Kina Nomination",  AwardCategoryId=2, LocationId=2, SubOrgId = 2, DescriptionComments = "she was awesome", ImpactComments="very impactful", ReviewStatus = true, Winner=true, CreatedBy= "v-rusahu"},
            new Nomination() { Alias="v-rusahu", Headline="Ruchi Nomination",  AwardCategoryId=3, LocationId=3, SubOrgId = 3, DescriptionComments = "she is good teacher", ImpactComments="so insightful", ReviewStatus = true, Winner=true, CreatedBy= "v-inmato"},
            new Nomination() { Alias="v-inmato", Headline="Inelda Nomination",  AwardCategoryId=1, LocationId=2, SubOrgId = 3, DescriptionComments = "she was top notch", ImpactComments="very first rate", ReviewStatus = true, Winner=false, CreatedBy= "v-rusahu"},
            new Nomination() { Alias="v-tefagb", Headline="Tayo 2nd Nomination",  AwardCategoryId=2, LocationId=1, SubOrgId = 1, DescriptionComments = "she was great again", ImpactComments="she was impactful again", ReviewStatus = false, Winner=false, CreatedBy= "v-inmato"},
            new Nomination() { Alias="v-kinarg", Headline="Kina 2nd Nomination",  AwardCategoryId=1, LocationId=2, SubOrgId = 2, DescriptionComments = "she was awesome again", ImpactComments="impactful 2nd time around", ReviewStatus = false, Winner=false, CreatedBy= "v-tefagb"},
            new Nomination() { Alias="v-rusahu", Headline="Ruchi 2nd Nomination",  AwardCategoryId=1, LocationId=3, SubOrgId = 3, DescriptionComments = "she is good teacher again", ImpactComments="so insightful once more", ReviewStatus = true, Winner=false, CreatedBy= "v-tefagb"},
            new Nomination() { Alias="v-inmato", Headline="Inelda 2nd Nomination",  AwardCategoryId=3, LocationId=2, SubOrgId = 1, DescriptionComments = "she was top notch again", ImpactComments="very first rate as expected", ReviewStatus = false, Winner=false, CreatedBy= "v-kinarg"},



            };

        }

                                                                                                                                                                                                     


    }
}
