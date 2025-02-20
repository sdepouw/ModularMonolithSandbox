using Microsoft.AspNetCore.Mvc;

namespace RiverBooks.Reporting.ReportEndpoints;

internal record TopSalesByMonthRequest([property: FromQuery] int Year, [property: FromQuery] int Month);
