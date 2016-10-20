$(document).ready(function () {
    var errocode = GetParameterValue('e');
    if(errocode != null)
    {
        if (errocode == "1")
            $('#message').html("Enter Email ID..");
        else if (errocode == "2")
            $('#message').html("Enter Password..");
        else if (errocode == "11") {
            var mesg = GetParameterValue('mesg');
            $('#message').html(mesg);
        }

        $('#errormessage').show();
    }
});




function GetParameterValue(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}