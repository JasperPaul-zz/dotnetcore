$(document).ready(function () {
    var menuid = sessionStorage.getItem('menuid');
    SetNavigation(menuid);

    $('#nav li').click(function () {
        sessionStorage.setItem("menuid", $(this).attr('id'));
        var menuid = sessionStorage.getItem('menuid');
    });

 
});

function SetNavigation(menuid) {
    if (menuid == null)
        menuid = "mnulistmonitor";

    $('#' + menuid).addClass('active-trail');
    $('#' + menuid).addClass('active');

    $('#' + menuid + ' a').addClass('active-trail');
    $('#' + menuid + ' a').addClass('active');
}


function ShowOverlay() {
        var $divOverlay = $('#divOverlay');
        var bottomTop = 0;
        var bottomLeft = 0;
        var bottomWidth = $(document).width();
        var bottomHeight = $(document).height();
        $divOverlay.css('top', bottomTop);
        $divOverlay.css('left', bottomLeft);
        $divOverlay.css('width', bottomWidth);
        $divOverlay.css('height', bottomHeight);
        $divOverlay.show();
}

function HideOverlay() {
    var $divOverlay = $('#divOverlay');
    $divOverlay.hide();
}