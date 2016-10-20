var deletecount = 0;
var deletedcount = 0;
$(document).ready(function () {

    LoadMonitors();

    $('#select_all').on('click', function () {
        if (this.checked) {
            $('.appcheckbox').each(function () {
                $(this).parent().parent().addClass('selected');
                this.checked = true;
            });
        } else {
            $('.appcheckbox').each(function () {
                $(this).parent().parent().removeClass('selected');
                this.checked = false;
            });
        }
    });

    $('#monitors').on('click', '.appcheckbox', function () {
        if (this.checked)
            $(this).parent().parent().addClass('selected');
        else
            $(this).parent().parent().removeClass('selected');

        if ($('.appcheckbox:checked').length == $('.appcheckbox').length) {
            $('#select_all').prop('checked', true);
        } else {
            $('#select_all').prop('checked', false);
        }
    });


    $('#monitoraction').change(function () {
        ShowOverlay($('html'));
        var actionid = $(this).val();
        switch (actionid)
        {
            case '1':
                ManageMonitor('PUT', 'Suspend', true);
                LoadMonitors();
                break;
            case '2':
                ManageMonitor('PUT', 'Activate', true);
                LoadMonitors();
                break;
            case '3':
                ManageMonitor('DELETE', 'Delete', true);
                //LoadMonitors();
                break;
        }
        $(this).val('0');
    });
});

function ManageMonitor(methodtype, action, isasync)
{
    if (action == 'Delete') {
        $('.appcheckbox').each(function () {
            if (this.checked) {
                deletecount++;
            }
        });
    }


    $('.appcheckbox').each(function () {
        if (this.checked) {
            $.ajax({
                url: '../Monitor/' + action + '?monitorId=' + $(this).attr('id'),
                dataType: 'json',
                contentType: 'application/json; charset=UTF-8',
                type: methodtype,
                async: isasync,
                complete: function (respdata) {
                    if (action == 'Delete') {
                        deletedcount++;
                        if (deletedcount == deletecount) {
                            LoadMonitors();
                            deletedcount = 0;
                            deletecount = 0;
                        }
                    }
                }
            });
        }
    });
}


function LoadMonitors()
{
    $('#select_all').removeAttr('checked');
    ShowOverlay($('html'));
    $.ajax({
        url: '../Monitor/GetMonitors',
        dataType: 'json',                   
        contentType: 'application/json; charset=UTF-8',                   
        type: 'GET',                   
        async: true,
        cache: false,
        complete: callback
    });
}


function LoadCurrentStatus() {
    $.ajax({
        url: '../Monitor/GetCurrentStatus',
        dataType: 'json',
        contentType: 'application/json; charset=UTF-8',
        type: 'GET',
        async: true,
        complete: callbackCurrentStatus
    });
}


function callback(respdata) {
    $('#monitors').html('');
    $('#monitorlist').hide();
    $(respdata.responseJSON).each(function (dat) {
        var monitorId = respdata.responseJSON[dat].monitorIdString;
        var trow = '<tr class="trclass"><td align="center"><input class="appcheckbox checkbox" id="' + monitorId + '" type="checkbox"></td><td class="imgtd"><div id="i' + monitorId + '" class="status-suspend-xl imgstatus" ><span class="icon-suspend"> </span></div></td><td class="tdappname"><span class="appname">' +
        respdata.responseJSON[dat].monitorName + '</span></td><td class="tdmonitortype"><span>' +
        respdata.responseJSON[dat].monitorType + '</span></td><td class="tdlastpolledtime"><span id="l' + monitorId + '" class="lastpolltime"></span></td></tr>'
        $('#monitors').append(trow);

    });

    LoadCurrentStatus();
   
}


function callbackCurrentStatus(data) {
    $(data.responseJSON).each(function (dat) {
        var monitorId = data.responseJSON[dat].monitorIdString;
        var imgclass = "status-suspend-xl";
        var downreason = null;
       // alert(data.responseJSON[dat].status);
        var imgcontent = '<span class="icon-suspend"> </span>';
        if (data.responseJSON[dat].status == '0') {
            imgclass = "status-danger-xl";
            downreason = data.responseJSON[dat].downReason;
            imgcontent = '<span class="icon-status-down"> </span>';
        }
        else if (data.responseJSON[dat].status == '1') {
            imgclass = "status-success-xl";
            imgcontent = '<span class="icon-status-up"> </span>';
        }
        else if (data.responseJSON[dat].status == '2') {
            imgclass = "status-warning-xl";
            imgcontent = '<span class="icon-trouble"> </span>';
        }
        else if (data.responseJSON[dat].status == '9') {
            imgclass = "ico-discovery-xl";
            imgcontent = '<span class="icon-discovery"> </span>';
        }
        else if (data.responseJSON[dat].status == '10') {
            imgclass = "ico-discovery-error-xl";
            imgcontent = '<span class="icon-config-error"> </span>';
            downreason = data.responseJSON[dat].downReason;
        }

        $("#monitors .imgstatus").each(function () {
            if ($(this).attr("id") == 'i' + monitorId) {
                $(this).attr("class", imgclass);
                $(this).html(imgcontent);
                if (downreason != null)
                    $(this).attr("title", downreason);
            }
        });

        $("#monitors .lastpolltime").each(function () {
            if ($(this).attr("id") == 'l' + monitorId) {
                var mydate = data.responseJSON[dat].lastPolledTime.replace('T', ' ');
                $(this).html(my_date_format(mydate));
            }
        });
    });

    $('#monitorlist').show();
    HideOverlay();
}

var my_date_format = function (input) {
    var d = new Date(Date.parse(input.replace(/-/g, "/")));
    var month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
    var date = d.getDate() + " " + month[d.getMonth()] + ", " + d.getFullYear();
    var time = d.toLocaleTimeString().toLowerCase().replace(/([\d]+:[\d]+):[\d]+(\s\w+)/g, "$1$2");
    return (date + " " + time.toUpperCase());
};