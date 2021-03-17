$(document).ready(function () {
    bindClinicData();
    $('#addclinicbutton').on('click', function () {
        $('#divcontent').load("/Admin/AddClinic");        
    });
    $('#clinictable').dataTable();
});

function bindClinicData() {
    $('#cliniclist').load("/Admin/GetClinics",
        function (response, status, xhr) {
            if (status == "error") {
                alert("An error occurred while loading the results.");
            }
        });
}
