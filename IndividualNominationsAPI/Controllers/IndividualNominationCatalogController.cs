using System.Linq;
using System.Threading.Tasks;
using IndividualNominationCatalogAPI.Domain;
using IndividualNominationCatalogAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IndividualNominationCatalogAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/IndividualNominationsCatalog")]
    [ApiController]
    public class IndividualNominationCatalogController : ControllerBase
    {
        //dbcontect property
        private readonly IndividualNominationCatalogContext _individualNominationCatalogContext;


        //controller ctor - injects the dbcontext, puts it in readonly property
        public IndividualNominationCatalogController(IndividualNominationCatalogContext individualNominationCatalogContext)
        {
            _individualNominationCatalogContext = individualNominationCatalogContext;
        }

        [HttpGet]
        [Route("[action]")] //[]-passing in method name,  the action should match the method name, {}-passing parameters
        // api/IndividualNominationsCatalog/AwardCategories
        public async Task<IActionResult> AwardCategories()
        {
            var items =  await _individualNominationCatalogContext.AwardCategories.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")] //[]-passing in method name,  the action should match the method name, {}-passing parameters
        // api/IndividualNominationsCatalog/Locations
        public async Task<IActionResult> Locations()
        {
            var items = await _individualNominationCatalogContext.Locations.ToListAsync();
            return Ok(items);
        }

        [HttpGet]
        [Route("[action]")] //[]-passing in method name,  the action should match the method name, {}-passing parameters
        // api/IndividualNominationsCatalog/SubOrg
        public async Task<IActionResult> SubOrgs()
        {
            var items = await _individualNominationCatalogContext.AwardCategories.ToListAsync();
            return Ok(items);
        }


        [HttpGet]

        [Route("[action]")]
        // api/IndividualNominationsCatalog/Nominations
        public async Task<IActionResult> Nominations(
           [FromQuery] int pageSize = 6,
           [FromQuery] int pageIndex = 0)

        {

            var totalItems = await _individualNominationCatalogContext.Nominations
                              .LongCountAsync();

            var itemsOnPage = await _individualNominationCatalogContext.Nominations
                              .OrderBy(c => c.Alias)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();
                                 
            var model = new PaginatedNominationsViewModel<Nomination>
                (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        [HttpGet]

        [Route("nominations/{alias}")]
        //api/IndividualNominationsCatalog/nominations/v-tefagb
        public async Task<IActionResult> GetNominationByAlias(string alias,
            [FromQuery] int pageSize = 6,
           [FromQuery] int pageIndex = 0)
        {

            if (alias == null)

            {
                return BadRequest();
            }

            var totalItems = await _individualNominationCatalogContext.Nominations
                .Where(c => c.Alias.StartsWith(alias)).LongCountAsync();

            var itemsOnPage = await _individualNominationCatalogContext.Nominations
                              .Where(c => c.Alias.StartsWith(alias))
                              .OrderBy(c => c.Alias)
                              .Skip(pageSize * pageIndex)
                              .Take(pageSize)
                              .ToListAsync();
                                 
            var model = new PaginatedNominationsViewModel<Nomination>
                (pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);

        }

        [HttpGet]

        [Route("nominations/{id:int}")]
        //api/IndividualNominationsCatalog/nomination/1
        public async Task<IActionResult> GetNominationById(int id)
        {

            if (id <=0 )

            {
                return BadRequest();
            }

            var item = await _individualNominationCatalogContext.Nominations
                .SingleOrDefaultAsync(c => c.Id == id);

            //item exist
            if (item != null)
            {
             return Ok(item);

            }

            return NotFound();

        }

        // GET api/IndividualNominationsCatalog/Nominations/alias/null/awardcategory/1/location/null/suborg/2[?pageSize=4&pageIndex=0]

        [HttpGet]

        [Route("[action]/alias/{Alias}/awardcategory/{AwardCategoryId}/location/{LocationId}/suborg/{SubOrgId}")]

        public async Task<IActionResult> Nominations( string alias, int? awardcategoryId,

            int? locationId, int? suborgId,

            [FromQuery] int pageSize = 6,

            [FromQuery] int pageIndex = 0)

        {

            var root = (IQueryable<Nomination>)_individualNominationCatalogContext.Nominations;

            //filter for alias

            if (alias != null)

            {

                root = root.Where(c => c.Alias == alias);

            }

            //then for award category
            if (awardcategoryId.HasValue)

            {

                root = root.Where(c => c.AwardCategoryId == awardcategoryId);

            }

            //then location
            if (locationId.HasValue)

            {

                root = root.Where(c => c.LocationId == locationId);

            }

            //then for sub org
            if (suborgId.HasValue)

            {

                root = root.Where(c => c.SubOrgId == suborgId);

            }

            //put in totalItems variable

            var totalItems = await root

                              .LongCountAsync();

            //sort and paginate
            var itemsOnPage = await root



                              .OrderBy(c => c.Alias)

                              .Skip(pageSize * pageIndex)

                              .Take(pageSize)

                              .ToListAsync();
            //pass in paginated view model

            //return model in json format
         
            var model = new PaginatedNominationsViewModel<Nomination>(pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);



        }



        [HttpPost]

        [Route("nominations")]

        public async Task<IActionResult> CreateProduct(

            [FromBody] Nomination nomination)

        {

            //create new nomination
            var item = new Nomination

            {
                Alias = nomination.Alias,

                Headline = nomination.Headline,

                DescriptionComments = nomination.DescriptionComments,

                ImpactComments = nomination.ImpactComments,

                ReviewStatus = nomination.ReviewStatus,

                Winner = nomination.Winner,

                CreatedBy = nomination.CreatedBy,

                AwardCategoryId = nomination.AwardCategoryId,

                LocationId = nomination.LocationId,

                SubOrgId = nomination.SubOrgId

            };

            //add to nomiation table
           _individualNominationCatalogContext.Nominations.Add(item);

            await _individualNominationCatalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNominationById), new { id = item.Id });

        }





        [HttpPut]

        [Route("items")]

        public async Task<IActionResult> UpdateNomination(

            [FromBody] Nomination nominationToUpdate)

        {
            //find nomination  to be updated by id
            var nomination = await _individualNominationCatalogContext.Nominations
                .Where(i => i.Id == nominationToUpdate.Id)
                .SingleOrDefaultAsync();


            //if it does not exist
            if (nomination == null)

            {

                return NotFound(new { Message = $"Nomination with id {nominationToUpdate.Id} not found." });

            }

            //if found, add update to nomination in table
            //save changes
            nomination = nominationToUpdate;
            //nomination.UpdatedDate.Add(DateTime);
            _individualNominationCatalogContext.Nominations.Update(nomination);

            await _individualNominationCatalogContext.SaveChangesAsync();



            return CreatedAtAction(nameof(GetNominationById), new { id = nominationToUpdate.Id });

        }





        [HttpDelete]

        [Route("{id}")]

        public async Task<IActionResult> DeleteNomination(int id)

        {
            //look for nomination to remove by its id
            var nomination = await _individualNominationCatalogContext.Nominations

                .SingleOrDefaultAsync(p => p.Id == id);

            //if is does not exist, report that it was not found
            if (nomination == null)
            {
                return NotFound();

            }

            //if found, remove and save the changes

            _individualNominationCatalogContext.Nominations.Remove(nomination);

            await _individualNominationCatalogContext.SaveChangesAsync();

            return NoContent();

        }


    }
}