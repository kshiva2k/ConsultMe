$(document).ready(function () {
    bindData();
    $('#savepatientbutton').click(function () {
        var jsondata = {
            Id: $('#Id').val(),
            Name: $('#Name').val(),
            Age: $('#Age').val(),
            Gender: $('#Gender').val()
        };
        $.ajax({
            type: 'Get',
            url: 'SavePatient',
            data: jsondata,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                if (data == true) {
                    clearData();
                    bindData();
                }
            }
        });
    });
});

function bindData() {
    $('#patientlist').load("/User/GetPatients",
        function (response, status, xhr) {
            if (status == "error") {
                alert("An error occurred while loading the results.");
            }
        });
}

function clearData() {
    Name: $('#Name').val('');
    Age: $('#Age').val('');
    Gender: $('#Gender').val('');
}
