using LiveCoding.Services;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CashflowController : ControllerBase
    {
        private readonly CashflowService cashflowService;

        public CashflowController(CashflowService cashflowService)
        {
            this.cashflowService = cashflowService;
        }

        [HttpGet]
        public int Get(DateTime date)
        {
            return cashflowService.ComputeCashflow(date);
        }
    }
}