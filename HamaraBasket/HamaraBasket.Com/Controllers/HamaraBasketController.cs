using HamaraBasket.Com.Interfaces;
using HamaraBasket.Com.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HamaraBasket.Com.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HamaraBasketController : Controller
    {
        public IRuleEngine<List<Items>> ruleEngine;

        private readonly ILogger<HamaraBasketController> _logger;
        public HamaraBasketController(ILogger<HamaraBasketController> pLogger, IRuleEngine<List<Items>> pRuleEngine)
        {
            ruleEngine = pRuleEngine;
            _logger = pLogger;
        }
        // GET: api/<HamaraBasket>
        [HttpGet]
        public IEnumerable<Items> Get()
        {
            return ruleEngine.RuleEngine().ToList();
        }

       
    }
}
