using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
  [Route("/api/vehicles")]
  public class VehiclesController : Controller
  {
    private readonly IMapper mapper;
    private readonly VegaDbContext context;
    public VehiclesController(VegaDbContext context, IMapper mapper)
    {
      this.mapper = mapper;
      this.context = context;
    }

    [HttpPost("/api/vehicles")]
    public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
    {
      var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
      vehicle.LastUpdate = DateTime.Now;
      context.Vehicles.Add(vehicle);
      await context.SaveChangesAsync();

      var result = mapper.Map<Vehicle, VehicleResource>(vehicle);

      return Ok(result);
    }
  }
}