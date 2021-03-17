var timingid;
var appdate;
$(document).ready(function () {
    $("#datepicker").datepicker({ dateFormat: 'dd/mm/yy' });
    var today = new Date();
    var dd = today.getDate();

    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }
    $("#datepicker").val(dd + '/' + mm + '/' + yyyy);
    GetTimings();
    $("#datepicker").change(function () {
        GetTimings();
    });
    $('#bookappointment').click(function () {
        //alert('Timing ' + timingid + ' on ' + appdate + ' for doctor ' + $('#doctorid').val() + ' on ' + $('#clinicid').val());
        var inputjson = {
            'SlotTime': timingid,
            'Appointmentdate': appdate,
            'CustomerId': $('#doctorid').val(),
            'ClinicId': $('#clinicid').val(),
            'PatientId': $('#patientlist').val(),
            'Symptoms': $('#symptoms').val()
        };
        $.ajax({
            url: 'SaveAppointment',
            data: inputjson,
            contentType: "application/json",
            success: function (data) {
                if (data != null && data != 0) {
                    $('#bookingId').val(data);
                    $('#frmbooking').submit();
                }
                else {
                    alert("Error on booking appointment");
                }
            }
        });
    });
});

function GetTimings() {
    $.ajax({
        url: 'GetVisitorTimings',
        data: { "customer": $("#doctorid").val(), "clinic": $("#clinicid").val(), "appointmentdate": $("#datepicker").val() },
        contentType: "application/html",
        success: function (data) {
            if (data != null) {
                $("#divschedule").html(data);
            }
        }
    });
}

function EnableDisableTiming(div, id, dt) {
    timingid = id;
    appdate = dt;
    $('.clearfix li a').removeClass('selected');    
    $(div).addClass('selected');
    $.ajax({
        url: 'GetPatientsList',
        contentType: "application/html",
        success: function (data) {
            if (data != null) {
                $("#divpatient").html(data);
            }
        }
    });
}