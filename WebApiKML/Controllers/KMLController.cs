using Microsoft.AspNetCore.Mvc;
using WebApiKML.DTO;
using WebApiKML.Exceptions;
using WebApiKML.Models;
using WebApiKML.Services;

namespace WebApiKML.Controllers
{
    [ApiController]
    public class KMLController : ControllerBase 
    {
        private readonly KMLService _kmlService;
        public KMLController(KMLService kmlService) 
        {
            _kmlService = kmlService;
        }

        [HttpGet("kmlapi/map-items")]
        public IActionResult GetMapItems()
        {
            try
            {
                List<MapItemDTO> mapItems = _kmlService.GetMapItems();
                return Ok(mapItems);
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("kmlapi/map-items/{id}/size")]
        public IActionResult GetSizeOfMapItemById(int id)
        {
            try
            {
                int size = _kmlService.GetSizeOfMapItemById(id);
                return Ok(new GetMapItemSizeResponseDTO(size));
            }
            catch (NotFoundKMLElement)
            {
                return NotFound();
            }
            catch (Exception) 
            {
                return StatusCode(500);
            }
        }

        [HttpGet("kmlapi/map-items/{id}/area")]
        public IActionResult GetAreaOfMapItemById(int id)
        {
            try
            {
                double area = _kmlService.GetAreaOfMapItemById(id);
                return Ok(new GetMapItemAreaResponseDTO(area));
            }
            catch(NotFoundKMLElement)
            {
                return NotFound();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("kmlapi/map-items/{mapItemId}/distance-to-point")]
        public IActionResult GetDistance([FromRoute] int mapItemId, [FromQuery] double latitude, [FromQuery] double longitude)
        {
            try
            {
                var fromPoint = new WGS84Point
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
                double distance = _kmlService.GetDistance(mapItemId, fromPoint);
                return Ok(new GetDistanceToMapItemResponseDTO(distance));
            }
            catch (NotFoundKMLElement)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("kmlapi/map-items/{mapItemId}/contains")]
        public IActionResult GetContains([FromQuery] double latitude, [FromQuery] double longitude)
        {
            try
            {
                var point = new WGS84Point
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
                if (_kmlService.TryFindMapItemContainPoint(point, out Field? field))
                    return Ok(new CheckContainsPointSuccessResponseDTO(field.Fid, field.Name));
                else
                    return Ok(new CheckContainsPointUnsuccessResponseDTO());
            }
            catch (NotFoundKMLElement)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
