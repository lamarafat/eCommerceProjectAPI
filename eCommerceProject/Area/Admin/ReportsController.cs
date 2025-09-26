using System.Threading.Tasks;
using eCommerceProject.BLL.Service.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace eCommerceProject.PL.Area.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("SalesReport")]
        public async Task<IResult> GetProductsReports()
        {
            var document = await _reportService.CreateDocumentAsync();

            var pdf = document.GeneratePdf();
            return Results.File(pdf, "application/pdf", "productsReport.pdf");
        }
    }
}
