using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndividualNominationCatalogAPI.Domain
{
    public class Nomination
    {

       
        public int Id { get; set; }
        public string Alias {get; set; }
        public string Headline { get; set; }
        public string DescriptionComments { get; set; }
        public string ImpactComments { get; set; }
        public bool Winner { get; set; }
        public bool ReviewStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        //foreignKeys
        public int AwardCategoryId { get; set; }
        public int LocationId { get; set; }
        public int SubOrgId { get; set; }

        public virtual AwardCategory AwardsCategory { get; set; }
        public virtual Location Location { get; set; }
        public virtual SubOrg SubOrg { get; set; }


    }
}
