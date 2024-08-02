using Microsoft.AspNetCore.Mvc;
using s4.Data;

namespace s4.Models.Entities
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly DBContext context;

        public ItemsController(DBContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllItemScores()
        {
            var ItemScores = context.Items.ToList();
            return Ok(ItemScores);
        }

        [HttpGet("details/Search")]
        public IActionResult Search(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest("Search parameter cannot be empty.");
            }

            var items = (from item in context.Items
                         join area in context.Areas on item.AreaID equals area.ID
                         join itemType in context.ItemTypes on item.ItemTypeID equals itemType.ID
                         join itemAmenity in context.ItemAmenities on item.ID equals itemAmenity.ItemID
                         join amenity in context.Amenities on itemAmenity.AmenityID equals amenity.ID
                         join itemAttraction in context.ItemAttractions on item.ID equals itemAttraction.ItemID
                         join attraction in context.Attractions on itemAttraction.AttractionID equals attraction.ID
                         where
                             item.Title.Contains(search) ||
                             area.Name.Contains(search) ||
                             attraction.Name.Contains(search) ||
                             itemType.Name.Contains(search) ||
                             amenity.Name.Contains(search)
                         select new SearchDto
                         {
                             itemID = item.ID,
                             title = item.Title,
                             area = area.Name,
                             type = itemType.Name,
                             amenity = amenity.Name,
                             attraction = attraction.Name
                         }).Distinct().ToList(); // Using Distinct to avoid duplicate records due to joins

            return Ok(items);
        }

        [HttpGet("{itemID}")]
        public IActionResult GetItem(int itemID)
        {
            try
            {
                var item = (from items in context.Items
                            join area in context.Areas on items.AreaID equals area.ID
                            join itemScores in context.ItemScores on items.ID equals itemScores.ItemID into itemScoreGroup
                            from itemScores in itemScoreGroup.DefaultIfEmpty()
                            join itemprices in context.ItemPrices on items.ID equals itemprices.ItemID into itemPricesGroup
                            from itemprices in itemPricesGroup.DefaultIfEmpty()
                            join booking in context.BookingDetails on itemprices.ID equals booking.ItemPriceID into bookingsGroup
                            from booking in bookingsGroup.DefaultIfEmpty()
                            where items.ID == itemID
                            group new { items, area, itemScores, itemprices, booking } by new { items.ID, area.Name, items.Title } into g
                            select new TableDto
                            {
                                itemID = g.Key.ID,
                                area = g.Key.Name,
                                average = (long?)(g.Any(x => x.itemScores != null) ? g.Average(x => x.itemScores.Value) : (double?)null),
                                title = g.Key.Title,
                                completed = g.SelectMany(x => context.BookingDetails
                                    .Where(b => b.ItemPriceID == x.itemprices.ID)
                                    .GroupBy(b => b.ItemPriceID)
                                    .Select(bGroup => new { ItemPriceID = bGroup.Key, Count = bGroup.Count() }))
                                    .GroupBy(x => x.ItemPriceID)
                                    .Sum(x => x.Sum(y => y.Count)),
                                payable = g.Select(x => x.itemprices.Price).FirstOrDefault()
                            }).ToList();

                if (item == null || item.Count == 0)
                {
                    return NotFound("No data found for the given itemID.");
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        [HttpGet("advanced-search")]
        public IActionResult AdvancedSearch(
            [FromQuery] string area = null,
            [FromQuery] string attraction = null,
            [FromQuery] string title = null,
            [FromQuery] double? minPrice = null,
            [FromQuery] double? maxPrice = null,
            [FromQuery] string type = null,
            [FromQuery] string amenity1 = null,
            [FromQuery] string amenity2 = null,
            [FromQuery] string amenity3 = null)
        {
            try
            {
                var query = from item in context.Items
                            join areaEntity in context.Areas on item.AreaID equals areaEntity.ID into areaGroup
                            from areaEntity in areaGroup.DefaultIfEmpty()
                            join itemType in context.ItemTypes on item.ItemTypeID equals itemType.ID into typeGroup
                            from itemType in typeGroup.DefaultIfEmpty()
                            join itemAmenity in context.ItemAmenities on item.ID equals itemAmenity.ItemID into itemAmenityGroup
                            from itemAmenity in itemAmenityGroup.DefaultIfEmpty()
                            join amenity in context.Amenities on itemAmenity.AmenityID equals amenity.ID into amenityGroup
                            from amenity in amenityGroup.DefaultIfEmpty()
                            join itemAttraction in context.ItemAttractions on item.ID equals itemAttraction.ItemID into attractionGroup
                            from itemAttraction in attractionGroup.DefaultIfEmpty()
                            join attractionEntity in context.Attractions on itemAttraction.AttractionID equals attractionEntity.ID into attractionGroup2
                            from attractionEntity in attractionGroup2.DefaultIfEmpty()
                            join itemPrices in context.ItemPrices on item.ID equals itemPrices.ItemID into priceGroup
                            from itemPrices in priceGroup.DefaultIfEmpty()
                            where
                                (string.IsNullOrEmpty(area) || areaEntity.Name.Contains(area)) ||
                                (string.IsNullOrEmpty(attraction) || attractionEntity.Name.Contains(attraction)) ||
                                (string.IsNullOrEmpty(title) || item.Title.Contains(title)) ||
                                (!minPrice.HasValue || itemPrices.Price >= (decimal)minPrice) ||
                                (!maxPrice.HasValue || itemPrices.Price <= (decimal)maxPrice) ||
                                (string.IsNullOrEmpty(type) || itemType.Name.Contains(type)) ||
                                (string.IsNullOrEmpty(amenity1) || amenity.Name.Contains(amenity1)) &&
                                (string.IsNullOrEmpty(amenity2) || amenity.Name.Contains(amenity2)) &&
                                (string.IsNullOrEmpty(amenity3) || amenity.Name.Contains(amenity3))
                            select new
                            {
                                item.ID,
                                AreaName = areaEntity.Name,
                                item.Title,
                                Price = itemPrices.Price
                            };

                var results = query
                    .AsEnumerable()
                    .GroupBy(x => x.ID)
                    .Select(g =>
                    {
                        var itemID = g.Key;

                        // Calculate completed bookings
                        var completedCount = (from ip in context.ItemPrices
                                              join b in context.BookingDetails on ip.ID equals b.ItemPriceID
                                              where ip.ItemID == itemID
                                              select b).Count();

                        // Calculate average score
                        var averageScore = (from iscore in context.ItemScores
                                            where iscore.ItemID == itemID
                                            select iscore.Value).Average();

                        return new TableDto
                        {
                            itemID = itemID,
                            area = g.First().AreaName,
                            title = g.First().Title,
                            payable = g.First().Price,
                            completed = completedCount,
                            average = (long?)Math.Round(averageScore ?? 0) // Round and convert to long?
                        };
                    })
                    .ToList();

                return Ok(results);
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error occurred: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}