var ADDUSER = new AddUser();

function AddUser(){
    var self = this;
    var defaultCountry = true;

    this.init = function () {

        self.initDefaultCountry();

        $('select.country').unbind('change').change(function () {
            var value = $(this).val();

            if (value === 'USA' || value === 'CAN') {
                self.initDefaultCountry();
            }
            else {
                self.initOtherCountry();
            }
        });

        $('button.add-user-open-modal').unbind('click').click(function (event) {
            var target = $(this).data('target');
            $('.nav-tabs').removeClass('active');
            $(target + ' .tab-pane').addClass('active');
        });

        $('button.add-user').unbind('click').click(function (event) {

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
                        required: true
                    },
                    email: {
                        required: true,
                        email: true
                    },
                    mobile_phone: {
                        required: function (element) {
                            var val = $(element).parent().parent().find('.workPhone').val();
                            var required = val.length === 0;

                            if (!required) {
                                $(element).removeClass('error');
                            }

                            return required;
                        }
                    },
                    work_phone: {
                        required: function (element) {
                            var val = $(element).parent().parent().find('.mobilePhone').val();
                            var required = val.length === 0;

                            if (!required) {
                                $(element).removeClass('error');
                            }

                            return required;
                        }
                    },
                    state: {
                        required: function (element) {
                            return self.defaultCountry;
                        }
                    },
                    state_freeform: {
                        required: function (element) {
                            return !self.defaultCountry;
                        }
                    },
                }
            });

            var valid = form.valid();

            if (!valid) {
                return;
            }

            var data = UTILITY.serializeFormJSON(form);

            if (!self.defaultCountry) {
                data.state = data.state_freeform;
            }

            var role = form.children('.userType').val();
            var url = '/api/user/' + role;

            $.ajax({
                url: url,
                type: "POST",
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    if (result.is_success === true) {
                        var fileInput1 = form.find('.documentW9')[0];
                        var fileInput2; // form.find('.documentOther')[0];

                        if (fileInput1 !== undefined) {
                            var file1 = fileInput1.files[0];
                            var file2 = undefined;

                            if (fileInput2 !== undefined) {
                                file2 = fileInput2.files[0];
                            }

                            self.uploadFile('w9', file1, file2, role, result.broker_id);
                        }

                        $('.modal').removeClass('fade').modal('hide');
                        //alert(role);
                        if (role.toLowerCase() === 'client') {
                            //window.clientTable.ajax.reload();
                            //$('#userHolder').html(result);
                            //self.refreshList(role, result.broker_id, 0);
                            MYCLIENTS.refresh();
                        }
                        else {
                            $('#sidebar-menu .last-clicked').click();
                        }
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

    this.refreshList = function (userType, brokerId, clientId) {
        var url = '/api/user/' + userType + '/' + brokerId + '/' + clientId;
        //alert(url);
        $.ajax({
            url: url,
            type: "GET",
            dataType: 'html',
            //contentType: 'application/json',
            //data: JSON.stringify(data),
            success: function (result) {
                //alert(result);
                $('#userHolder').html(result);
            },
            error: function (xhr, resp, text) {
                console.log(xhr, resp, text);
            }
        })
    }

    this.initDefaultCountry = function () {
        self.defaultCountry = true;
        $('.state').removeClass('hidden');
        $('.stateFreeForm').addClass('hidden');

        //$('input.ein').inputmask({
            //mask: '99-9999999'
        //});

        $('input[type="tel"]').removeAttr('maxlength');

        //$('input[type="tel"]').inputmask({
            //mask: '(999) 999-9999'
        //});
    }

    this.initOtherCountry = function () {
        self.defaultCountry = false;
        $('.state').addClass('hidden');
        $('.stateFreeForm').removeClass('hidden');

        $('input.ein').inputmask('remove');
        $('input[type="tel"]').inputmask('remove');
        $('input[type="tel"]').attr('maxlength', '20');
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
