function onFocus(element) {
   var datepickerObject = element.ej2_instances[0];
   datepickerObject.show();
}

function onSwitchChange(args) {
   var element = document.getElementById('kendala');

   if (args.checked) {
      element.disabled = false;
      element.parentElement.classList.remove('e-disabled');
      return;
   }

   element.disabled = true;
   element.parentElement.classList.add('e-disabled');
}

$(document).ready(function () {
   $('.foto-kegiatan .btn-upload').click(function () {
      process(this, 'foto-kegiatan');
   });

   $('.file-pendukung .btn-upload').click(function () {
      process(this, 'file-pendukung');
   });

   $('.ada-kendala').change(function () {
      if (this.checked) {
         $('.kendala').removeAttr('disabled');
         return;
      }

      $('.kendala').attr('disabled', 'disabled');
   });

   $('#tanggal-mulai, #tanggal-selesai').focus(function () {
      onFocus(this);
   });
});
