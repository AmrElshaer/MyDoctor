$('#delete').submit(function (e) {
    console.log('Delete');
    var r = confirm("Are You Sure You Want To Delete");
    if (r === false) e.preventDefault();

});