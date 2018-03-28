var ADDUSER = new AddUser();

function AddUser(){
    var self = this;

    this.init = function () {
        
        $('input[type="tel"]').inputmask({
            mask: '(999) 999-9999'
        });

        $('button.add-user-open-modal').click(function (event) {
            var target = $(this).data('target');
            $('.nav-tabs').removeClass('active');
            $(target + ' .tab-pane').addClass('active');
        });

        $('button.add-user').click(function (event) {

            jQuery.validator.setDefaults({
                errorPlacement: function (error, element) {
                },
            });

            var target = $(event.target);
            var form = target.parents('form');

            form.validate({
                rules: {
                    first_name: {
                        required: true,
                        minlength: 1,
                        maxlength: 255
                    },
                    last_name: {
                        required: true,
                        minlength: 1,
                        maxlength: 255
                    },
                    postal_code: {
                        required: false,
                        minlength: 5,
                        maxlength: 10
                    },
                    email: {
                        required: true,
                        email: true
                    }
                }
            });

            var valid = form.valid();

            if (!valid) {
                return;
            }

            var data = UTILITY.serializeFormJSON(form);
            var role = $('#userType').val();
            var url = '/api/user/' + role;

            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    if (result.is_success == true) {
                        var file1 = form.children('.documentW9')[0].files[0];
                        var file2 = form.children('.documentOther')[0].files[0];

                        if (file1 !== undefined) {
                            self.uploadFile('w9', file1, file2, role, result.broker_id);
                        }

                        $('.modal').removeClass('fade').modal('hide');
                        $('#sidebar-menu .nav-item.last-clicked').click();
                    }
                    else {
                        alert('Error: ' + result.message);
                    }
                },
                error: function (xhr, resp, text) {
                    console.log(xhr, resp, text);
                }
            })
        });
    }

    this.uploadFile = function (fileType, file1, file2, role, id) {

        var url = ['/api/user/uploadfile', fileType, role, id].join('/');
        var formData = new FormData();

        if (fileType === 'w9') {
            formData.append('file', file1);
        }
        else {
            formData.append('file', file2);
        }

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (result) {
                if (fileType === 'w9' && file2 !== undefined) {
                    self.uploadFile('other', file1, file2, role, id);
                }
            },
            error: function (xhr, resp, text) {
                console.log(xhr, resp, text);
            }
        });
    }
}
