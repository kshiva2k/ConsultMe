var datalist;
$(document).ready(function () {    
    //$.widget("custom.catcomplete", $.ui.autocomplete, {        
    //    _create: function () {
    //        this._super();
    //        this.widget().menu("option", "items", "> :not(.ui-autocomplete-category)");
    //    },
    //    _renderMenu: function (ul, items) {
    //        alert(datalist);
    //        var that = this,
    //            currentCategory = "";
    //        $.each(items, function (index, item) {
    //            var li;
    //            if (item.item1 != currentCategory) {
    //                ul.append("<li class='ui-autocomplete-category'>" + item.item1 + "</li>");
    //                currentCategory = item.item1;
    //            }
    //            li = that._renderItemData(ul, item);
    //            if (item.category) {
    //                li.attr("aria-label", item.item1 + " : " + item.item2);
    //            }
    //        });
    //    }
    //});
    //bindAppointment();
    GetCities();
    $('#search').on('click', function () {
        $.each(datalist, function (category, item) {
            if ($('#specialist').val() == item.item3) {
                if (item.item1 == "Clinics") {
                    debugger;
                    $('#searchtype').val(item.item1);
                    $('#searchid').val(item.item2);
                    $('#formappointment').attr('action', 'DoctorList')
                    $('#formappointment').submit();
                }
                else if (item.item1 == "Doctors") {
                    debugger;
                    $('#searchtype').val(item.item1);
                    $('#searchid').val(item.item2);
                    $('#formappointment').attr('action', 'ClinicList')
                    $('#formappointment').submit();
                }
            }
        });
    });
    GetSearchData();
});

function bindAppointment() {
    $('#divappointment').load("/User/AppointmentForm",
        function (response, status, xhr) {
            if (status == "error") {
                alert("An error occurred while loading the results.");
            }
        });
}

function GetCities() {
    $.ajax({
        type: 'Get',
        url: 'GetCities',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#area").autocomplete({
                minLength: 2,
                source: data
            });
        }
    });
}

function GetDoctors() {
    var jsondata = {
        specialistid: $('select[name="Specialist"]').val(),
        area: $('select[name="Area"]').val()
    };
    $.ajax({
        type: 'Get',
        url: 'GetDoctors',
        data: jsondata,
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

function GetSearchData() {
    $.ajax({
        type: 'Get',
        url: 'GetSearchData',
        data: { 'Area': $('#area').val() },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            datalist = data;
            var searchdata = [];
            $.each(data, function (category, item) {
                searchdata.push(item.item3);
            });
            console.log(searchdata);
            $("#specialist").autocomplete({
                minLength: 2,
                source: searchdata
            });
        }
    });
}

function UpdateAppointment() {
    $.ajax({
        url: '/Customer/UpdateAppointment',
        data: { "appointmentid": $("#apptid").val(), "status": "3" },
        contentType: "json",
        success: function (data) {
            debugger;
            console.log(data);
            if (data == true) {
                window.location = "/Customer/Dashboard";
            }
        }
    });
}