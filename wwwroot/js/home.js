function hideTooltip() {
   $("#maps_mapsTooltip").remove();
}

function AjaxSummaryKegiatan(tahunSummary) {
   $.getJSON('/Ajax/FilterSummary',
      { tahun: tahunSummary }, function (data) {
         var items = '';
         var summaryList = data.summary;

         $.each(summaryList, function (i, data) {
            items += DisplaySummary(data);
         });

         items += DisplayTotal(data);
         $('#kegiatan-summary').empty();
         $('#kegiatan-summary').html(items);
      });
}

function DisplaySummary(data) {
   if ($('#kegiatan-summary b.tablesaw-cell-label').length) {
      return `
         <tr>
            <td>
               <b class="tablesaw-cell-label">Nama</b>
               <span class="tablesaw-cell-content">${data.nama}</span>
            </td>
            <td>
               <b class="tablesaw-cell-label">Sumber Dana</b>
               <span class="tablesaw-cell-content">${data.namaSumberDana}</span>
            </td>
            <td>
               <b class="tablesaw-cell-label">Jumlah Kegiatan</b>
               <span class="tablesaw-cell-content">${data.jumlahKegiatanView}</span>
            </td>
            <td>
               <b class="tablesaw-cell-label">Total Peserta</b>
               <span class="tablesaw-cell-content">${data.totalPesertaView}</span>
            </td>
         </tr>`;
   }

   return `
      <tr>
         <td>${data.nama}</td>
         <td>${data.namaSumberDana}</td>
         <td class="text-right pr-4">${data.jumlahKegiatanView}</td>
         <td class="text-right pr-4">${data.totalPesertaView}</td>
      </tr>`;
}

function DisplayTotal(data) {
   if ($('#kegiatan-summary b.tablesaw-cell-label').length) {
      return `
         <tr class="table-warning">
            <td>Total</td>
            <td></td>
            <td>
               <b class="tablesaw-cell-label">Kegiatan</b>
               <span class="tablesaw-cell-content">${data.grandTotalKegiatanView}</span>
            </td>
            <td>
               <b class="tablesaw-cell-label">Peserta</b>
               <span class="tablesaw-cell-content">${data.grandTotalPesertaView}</span>
            </td>
         </tr>`;
   }

   return `
      <tr class="table-warning">
         <td>Total</td>
         <td></td>
         <td class="text-right pr-4">${data.grandTotalKegiatanView}</td>
         <td class="text-right pr-4">${data.grandTotalPesertaView}</td>
      </tr>`;
}

$(document).ready(function () {
   AjaxSummaryKegiatan($('#Tahun').val());

   $('#Tahun').change(function () {
      AjaxSummaryKegiatan($('#Tahun').val());
   });
});
