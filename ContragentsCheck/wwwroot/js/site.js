// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    var modalContainer = $('#modalContainer');
    $('button[data-toggle="ajax-modal"]').click(function () {
        var url = $(this).attr('formaction');
        $.get(url).done(function (data) {
            modalContainer.html(data);
            modalContainer.find('.modal').modal('show');
        });
    });
    modalContainer.on('click', '[data-save="modal"]', function () {
        var data = [];
        var forms = $(this).parents('.modal').find('form');
        var actionUrl = forms.first().attr("action");
        var allFormValid = true;
        forms.each(function (i) {
            $.validator.unobtrusive.parse($(this));
            if (!$(this).valid()) {
                allFormValid = false;
            }
        });
        if (allFormValid) {
            forms.each(function () {
                data.push({
                    Inn: $(this).find("#Inn").val(),
                    StatusId: parseInt($(this).find("#StatusId").val()),
                    ReportId: parseInt($(this).find("#ReportId").val()),
                });
            })
            $.ajax({
                contentType: 'application/json; charset=utf-8',
                type: "POST",
                url: actionUrl,
                data: JSON.stringify(data),
                async: true
            }).done(function (result) {
                modalContainer.find('.modal').modal("hide");
                $("#partial").html(result)
            }); 
        }
        else return false;
    });
    $('#sendBtn').click(function () {
        $.ajax({
            type: "POST",
            url: $(this).attr('formaction'),
            async: true
        }).done(function (res) {
            $("#partial").html(res)
        });
    });

    var counter = 1
    modalContainer.on('click', '.add-more', function () {
        let firstTr = $(this).parents('.modal').find('#formsTable tbody tr:first');
        let newTr = firstTr.clone()
        newTr.find('form').attr('Id', counter).find('#Inn').val(null);
        newTr.find('.delete-position').prop("disabled", false)
        newTr.appendTo('#formsTable tbody');
        counter += 1;
    })
    modalContainer.on('click', '.delete-position', function () {
        $(this).closest('tr').remove();
    })
})