using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DeputiTigaKemenpora.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Syncfusion.EJ2.Maps;

namespace DeputiTigaKemenpora.Pages
{
   public class IndexModel : PageModel
   {
      public IndexModel(
         IHttpClientFactory httpClientFactory,
         IConfiguration configuration,
         ApplicationDbContext context)
      {
         _selectListUtilities = new SelectListUtilities(context);
         _httpClientFactory = httpClientFactory;
         _configuration = configuration;
      }

      public int Tahun { get; set; }

      public string BingKey => _configuration.GetValue<string>("BingKey");

      public object MapsMarkerData;

      public MapsBorder Border => new MapsBorder
      {
         Color = "#374557",
         Width = 14
      };

      public MapsCenterPosition CenterPosition => new MapsCenterPosition
      {
         Latitude = -2.8,
         Longitude = 118
      };

      public MapsMarkerClusterSettings ClusterSettings => new MapsMarkerClusterSettings
      {
         AllowClusterExpand = true,
         AllowClustering = true,
         Shape = MarkerType.Image,
         Height = 40,
         Width = 40,
         ImageUrl = Url.Content("~/cluster.svg"),
         LabelStyle = new MapsFont
         {
            Color = "White"
         }
      };

      public List<MapsMarker> MarkerSettings => new List<MapsMarker>
      {
         new MapsMarker
         {
            AnimationDuration=0,
            DataSource=MapsMarkerData,
            Fill = "#ff0000",
            Height=10,
            Shape = MarkerType.Circle,
            TooltipSettings=_tooltipSettings,
            Visible=true,
            Width=10
         }
      };

      public MapsZoomSettings ZoomSettings => new MapsZoomSettings
      {
         Enable = true,
         HorizontalAlignment = Alignment.Near,
         MouseWheelZoom = false,
         PinchZooming = true,
         ShouldZoomInitially = false,
         Toolbars = _toolbars,
         ToolBarOrientation = Orientation.Vertical,
         ZoomFactor = 5
      };

      public async Task<IActionResult> OnGetAsync()
      {
         MapsMarkerData = await ReadMarkerData();
         Tahun = DateTime.Today.Year;
         ViewData["Tahun"] = await _selectListUtilities.TahunSummaryKegiatan();
         return Page();
      }

      private async Task<object> ReadMarkerData()
      {
         HttpClient httpClient = _httpClientFactory.CreateClient();
         HttpResponseMessage response = await httpClient.SendAsync(new HttpRequestMessage(
            HttpMethod.Get,
            "https://tataruang.atrbpn.go.id/protaru/api/Progress/DaerahMap"));
         return JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
      }

      private readonly string[] _toolbars = new string[]
      {
         "Zoom",
         "ZoomIn",
         "ZoomOut",
         "Pan",
         "Reset"
      };
      private readonly MapsTooltipSettings _tooltipSettings = new MapsTooltipSettings
      {
         Template = "#tooltip-template",
         Visible = true,
         ValuePath = "nama"
      };
      private readonly SelectListUtilities _selectListUtilities;
      private readonly IHttpClientFactory _httpClientFactory;
      private readonly IConfiguration _configuration;
   }
}
