function onFocusById(elementId) {
    var datepickerObject = document.getElementById(elementId).ej2_instances[0];
    datepickerObject.show();
}

function onFocus(element) {
    var datepickerObject = element.ej2_instances[0];
    datepickerObject.show();
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
        }
        else {
            $('.kendala').attr('disabled', 'disabled');
        }
    });
    $('#tanggal-mulai, #tanggal-selesai').focus(function() {
        onFocus( this );
    });
});
