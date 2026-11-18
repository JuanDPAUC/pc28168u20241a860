using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PC28168.U20241a860.Openpay.Platform.Settlements.Application.Services;

namespace PC28168.U20241a860.Openpay.Platform.Settlements.Interfaces;


[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("DisbursementOrder")]
public class DisbursementOrderController(
    IDisbursementOrderCommandService campaignCommandService,
    IDisbursementOrderQueryService campaignQueryService,
    ILogger<DisbursementOrderController> logger)
    : ControllerBase
{
  
}