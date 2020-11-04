$('#exampleModal').on('hidden.bs.modal',
    function (e) {
        $(this).find('#CreateEdit')[0].reset();
    });