using Microsoft.AspNetCore.Mvc;
using s4.Data;
using System.Linq;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DBContext _context;

        public ItemsController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllItemScores()
        {
            var items = _context.Items.ToList();
            return Ok(items);
        }

        [HttpGet("details/Search")]
        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
                return BadRequest("Search parameter cannot be empty.");

            var items = (from item in _context.Items
                         join area in _context.Areas on item.AreaID equals area.ID
                         join itemType in _context.ItemTypes on item.ItemTypeID equals itemType.ID
                         join itemAmenity in _context.ItemAmenities on item.ID equals itemAmenity.ItemID
                         join amenity in _context.Amenities on itemAmenity.AmenityID equals amenity.ID
                         join itemAttraction in _context.ItemAttractions on item.ID equals itemAttraction.ItemID
                         join attraction in _context.Attractions on itemAttraction.AttractionID equals attraction.ID
                         where item.Title.Contains(search) ||
                               area.Name.Contains(search) ||
                               attraction.Name.Contains(search) ||
                               itemType.Name.Contains(search) ||
                               amenity.Name.Contains(search)
                         select new
                         {
                             item.ID,
                             item.Title,
                             Area = area.Name,
                             Type = itemType.Name,
                             Amenity = amenity.Name,
                             Attraction = attraction.Name
                         }).Distinct().ToList();

            return Ok(items);
        }

        [HttpGet("{itemID}")]
        public IActionResult GetItem(int itemID)
        {
            var item = (from items in _context.Items
                        join area in _context.Areas on items.AreaID equals area.ID
                        join itemScores in _context.ItemScores on items.ID equals itemScores.ItemID into itemScoreGroup
                        from itemScores in itemScoreGroup.DefaultIfEmpty()
                        join itemPrices in _context.ItemPrices on items.ID equals itemPrices.ItemID into itemPricesGroup
                        from itemPrices in itemPricesGroup.DefaultIfEmpty()
                        where items.ID == itemID
                        select new
                        {
                            items.ID,
                            Area = area.Name,
                            Title = items.Title,
                            AverageScore = itemScores != null ? (double?)itemScores.Value : null,
                            Price = itemPrices.Price,
                            Completed = _context.BookingDetails.Count(b => b.ItemPriceID == itemPrices.ID)
                        }).FirstOrDefault();

            if (item == null)
                return NotFound("No data found for the given itemID.");

            return Ok(item);
        }

        [HttpGet("advanced-search")]
        public IActionResult AdvancedSearch([FromQuery] string area = null,
                                            [FromQuery] string attraction = null,
                                            [FromQuery] string title = null,
                                            [FromQuery] double? minPrice = null,
                                            [FromQuery] double? maxPrice = null,
                                            [FromQuery] string type = null,
                                            [FromQuery] string amenity1 = null,
                                            [FromQuery] string amenity2 = null,
                                            [FromQuery] string amenity3 = null)
        {
            var query = from item in _context.Items
                        join areaEntity in _context.Areas on item.AreaID equals areaEntity.ID into areaGroup
                        from areaEntity in areaGroup.DefaultIfEmpty()
                        join itemType in _context.ItemTypes on item.ItemTypeID equals itemType.ID into typeGroup
                        from itemType in typeGroup.DefaultIfEmpty()
                        join itemPrices in _context.ItemPrices on item.ID equals itemPrices.ItemID into priceGroup
                        from itemPrices in priceGroup.DefaultIfEmpty()
                        where (string.IsNullOrEmpty(area) || areaEntity.Name.Contains(area)) &&
                              (string.IsNullOrEmpty(attraction) || _context.Attractions.Any(a => a.Name.Contains(attraction) && a.ID == item.ID)) &&
                              (string.IsNullOrEmpty(title) || item.Title.Contains(title)) &&
                              (!minPrice.HasValue || itemPrices.Price >= (decimal)minPrice) &&
                              (!maxPrice.HasValue || itemPrices.Price <= (decimal)maxPrice) &&
                              (string.IsNullOrEmpty(type) || itemType.Name.Contains(type)) &&
                              (string.IsNullOrEmpty(amenity1) || _context.Amenities.Any(a => a.Name.Contains(amenity1) && _context.ItemAmenities.Any(ia => ia.AmenityID == a.ID && ia.ItemID == item.ID))) &&
                              (string.IsNullOrEmpty(amenity2) || _context.Amenities.Any(a => a.Name.Contains(amenity2) && _context.ItemAmenities.Any(ia => ia.AmenityID == a.ID && ia.ItemID == item.ID))) &&
                              (string.IsNullOrEmpty(amenity3) || _context.Amenities.Any(a => a.Name.Contains(amenity3) && _context.ItemAmenities.Any(ia => ia.AmenityID == a.ID && ia.ItemID == item.ID)))
                        select new
                        {
                            item.ID,
                            Area = areaEntity.Name,
                            Title = item.Title,
                            Price = itemPrices.Price,
                            Completed = _context.BookingDetails.Count(b => b.ItemPriceID == itemPrices.ID),
                            AverageScore = _context.ItemScores.Where(iscore => iscore.ItemID == item.ID).Average(iscore => (double?)iscore.Value)
                        };

            var results = query.ToList();
            return Ok(results);
        }
    }
}
