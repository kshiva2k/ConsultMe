$(document).ready(function () {
    bindData();
    $('#addbutton').on('click', function () {
        $('#divcontent').load("/Admin/AddVendor");        
    });
    $('#customertable').dataTable();
});

function bindData() {
    $('#vendorlist').load("/Admin/GetVendors",
        function (response, status, xhr) {
            if (status == "error") {
                alert("An error occurred while loading the results.");
            }
        });
}
