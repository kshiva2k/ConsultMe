$(document).ready(function () {
    bindClinicData();
    $('#adduserbutton').on('click', function () {
        $('#divcontent').load("/Admin/AddUser");        
    });
    $('#usertable').dataTable();
});

function bindClinicData() {
    $('#cliniclist').load("/Admin/GetClinics",
        function (response, status, xhr) {
            if (status == "error") {
                alert("An error occurred while loading the results.");
            }
        });
}
