$(document).ready(() => {
    $('#MessageIcon').on('click', () => {
        $.ajax({
            url: '/DashBoard/UpdateMessages',
        });
    })

});