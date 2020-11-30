$(document).ready(() => {
    $('#Notification').on('click', () => {
        $.ajax({
            url: '/DashBoard/UpdateUserTrack',
        });
    })

});