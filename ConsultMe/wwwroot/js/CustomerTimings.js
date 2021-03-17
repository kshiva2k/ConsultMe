$(document).ready(function () {
    var id = $('#CustomerId').val();
    bindCustomerTiming(id);
    bindCustomerTimingForm();
});

function bindCustomerTiming(id) {
    $('#divcustomertiming').load("/Admin/GetTimings", { "_customerId": id },
        function (response, status, xhr) {
            if (status == "error") {
                alert("An error occurred while loading the results.");
            }
        });
}
function bindCustomerTimingForm() {
    $('#clinictimingsform').load("/Admin/GetDoctorTimingForm",
        function (response, status, xhr) {
            if (status == "error") {
                alert("An error occurred while loading the results.");
            }
        });
}