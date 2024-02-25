// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

showInPopup = (url, title) => {
    $.ajax({
        type: 'GET',
        url: url,
        success: (res) => {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    });
}

jQueryAJAXPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    $('#view-table').html(res.html);
                    $('#form-modal .modal-body').html('');
                    $('#form-modal .modal-title').html('');
                    $('#form-modal').modal('hide');
                }
                else {
                    $('#form-modal .modal-body').html(res.html);
                }
            },
            error: function (err) {
                console.log(err)
            }
        })
    } catch (e) {
        console.log(e);
    }

    // to prevent default form submit event
    return false;
}


jQueryAJAXDelete = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            success: res => {
                $('#view-table').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
    }
    catch (e) {
        console.log(e);
    }


    // to prevent default form submit event
    return false;
}