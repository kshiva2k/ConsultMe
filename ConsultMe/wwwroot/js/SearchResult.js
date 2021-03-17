function showpopup(customerid) {
    $('#appt_details').modal('show');
    $.ajax({
        type: 'Get',
        url: 'GetDoctorTimings',
        data: customerid,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var mySelect = $('#Patient');
            $.each(data, function (id, name) {
                mySelect.append(
                    $('<option></option>').val(id).html(name)
                );
            });
        }
    });
}
