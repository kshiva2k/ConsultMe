$(document).ready(function () {
    $.ajax({
        url: '/Customer/GetClinicList',
        contentType: "json",
        success: function (data) {
            debugger;
            //console.log(data);
            $.each(data, function (key, value) {
                $("#Clinic").append($("<option></option>").val(value.id).html(value.name));
            });
            BindTiming();
        }
    });
    $('#searchbutton').click(function () {
        SearchData();
    });
});
function BindTiming() {
    $.ajax({
        url: '/Customer/GetClinicTiming',
        data: { "clinicid": $("#Clinic").val() },
        contentType: "json",
        success: function (data) {
            debugger;
            console.log(data);
            $.each(data, function (key, value) {
                $("#Timing").append($("<option></option>").val(value.id).html(value.startTime + ' : ' + value.endTime));
            });
        }
    });
}
function SearchData() {
    $('#patientlist').load("/Customer/SearchData?clinicid=" + $("#Clinic").val() + "&timingid=" + $("#Timing").val());
}