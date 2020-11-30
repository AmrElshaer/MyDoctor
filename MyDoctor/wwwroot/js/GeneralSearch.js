$(document).ready(() => {
    $('#GeneralSearch').keyup(() => {
        var searchvalue = $('#GeneralSearch').val();

        $.ajax({
            url: `/DashBoard/GeneralSearch?searchVal=${searchvalue}`,
            success: function (data) {
                $('#SearchContent').html(data);
            },
            error: function () {
                console.log("error");
            }


        });



    });
});