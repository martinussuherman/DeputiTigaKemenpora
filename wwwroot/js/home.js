function hideTooltip() {
   $("#maps_mapsTooltip").remove();
}

function AjaxSummaryKegiatan(tahunSummary) {
   $.getJSON('/Ajax/FilterSummary',
      { tahun: tahunSummary }, function (data) {
         $('#kegiatan-summary').empty();
         var items = '';
         var summaryList = data.summary;

         $.each(summaryList, function (i, data) {
            items += DisplaySummary(data);
         });

         items += DisplayTotal(data);
         $('#kegiatan-summary').html(items);
      });
}

function DisplaySummary(data) {
   return '<tr>' +
      '<td>' + data.namaPenanggungJawab + '</td>' +
      '<td class="text-right pr-4">' + data.jumlahKegiatanView + '</td>' +
      '<td class="text-right pr-4">' + data.totalPesertaView + '</td>' +
      '</tr>';
}

function DisplayTotal(data) {
   return '<tr class="table-warning">' +
      '<td>Total</td>' +
      '<td class="text-right pr-4">' + data.grandTotalKegiatanView + '</td>' +
      '<td class="text-right pr-4">' + data.grandTotalPesertaView + '</td>' +
      '</tr>';
}

$(document).ready(function () {
   AjaxSummaryKegiatan($('#Tahun').val());

   $('#Tahun').change(function () {
      AjaxSummaryKegiatan($('#Tahun').val());
   });
});
