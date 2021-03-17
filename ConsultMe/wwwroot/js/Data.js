function AjaxCall(callType, Url, Params) {
    $.ajax({
        type: callType,
        url: Url,
        data: Params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            debugger;
            return data;
        }
    });
}
function AjaxCallNoParams(callType, Url) {
    $.ajax({
        type: callType,
        url: Url,
        success: function (data) {
            return data;
        },
        failure: {

        }
    });
}