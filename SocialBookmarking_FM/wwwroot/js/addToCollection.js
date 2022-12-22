$(document).on("click", "#frmAddToCollection", function (event) {
    event.preventDefault();

    var source = event.target || event.srcElement;
    if ($(source).attr('id') == 'catSel')
        return;

    $.ajax('/CategoryCollection/Add', {
        type: 'POST',
        data: {
            bookmarkId: $("#addBkm").val(),
            categoryId: $("#catSel").val()
        },
        success: function (data, status, xhr) {
            $("#collectionModal").modal("hide");
        },
        error: function (jqXhr, textStatus, errorMessage) {
            $("#badResponse").text("An error has occured");

        }
    });
});

$(document).on("click", "#catSel", function (event) {
    event.preventDefault();
});