using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    public class FeaturesController
        {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public FeaturesController(VegaDbContext context, IMapper mapper)
        {
        this.mapper = mapper;
        this.context = context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<FeatureResource>> GetMakes()
        {
        var features = await context.Makes.Include(m => m.Models).ToListAsync();

        return mapper.Map<List<Feature>, List<FeatureResource>>(features);
        }  
    }
}