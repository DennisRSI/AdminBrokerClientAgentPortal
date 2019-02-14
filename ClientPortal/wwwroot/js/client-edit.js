var CLIENTEDIT = new ClientEdit();

function ClientEdit() {
    var self = this;
    var defaultCountry = true;

    this.init = function (applicationId, clientId) {

        $('#deactivate-message').hide();

        self.initCountry();

        $('select#country').unbind('change').change(function () {
            self.initCountry();
        });

        $('#clientdeactivate').on("click", function (event) {
            self.deactivateClient();
        });

        $("#password-save").on("click", function () {
            self.changePassword(applicationId);
        });

        $('#assignedAgent').on('change', function (e) {
            var agentId = this.value;

            if (agentId > 0) {
                var agentName = $(this).children('option').filter(':selected').text();
                self.assignAgent(clientId, agentId, agentName);
            }
        });

        $('button.agent').on('click', function () {
            var clientId = $(this).data('clientid');
            var agentId = $(this).data('agentid');
        });

        $("#save").on("click", function (event) {

            var form = $('#edit');
            var valid = form.valid();

            if (!valid) {
                return;
            }

            var data = UTILITY.serializeFormJSON(form);

            if (!self.defaultCountry) {
                data.state = data.state_freeform;
            }

            $.ajax({
                url: '/api/user/clientupdateprofile/' + applicationId,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    if (result.isSuccess === true) {
                        alert('Profile successfully updated.');
                    }
                    else {
                        alert(result.message);
                    }
                },
                error: function (xhr, resp, text) {
                    console.log(xhr, resp, text);
                }
            });
        });
    };

    this.deactivateClient = function () {
        var clientId = $('#clientdeactivate').data('id');
        var reason = $('#reason').val();
        reason = encodeURIComponent(reason);

        var url = ['/api/user/deactivateclient', clientId, reason].join('/');

        $.post(url,
            function (data) {
                $('.modal').modal('hide');
                $('#deactivate-openmodal').addClass('hidden');
                $('#deactivate-message').show();
            }
        );
    };

    this.initCountry = function () {
        var value = $('select#country').val();

        if (value === 'USA' || value === 'CAN') {
            self.initDefaultCountry();
        }
        else {
            self.initOtherCountry();
        }
    };

    this.initDefaultCountry = function () {
        self.defaultCountry = true;
        $('.state').removeClass('hidden');
        $('.stateFreeForm').addClass('hidden');

        //$('input.ein').inputmask({
        //mask: '99-9999999'
        //});

        //$('input#zip').inputmask({
        // mask: '99999'
        //});

        $('input[type="tel"]').removeAttr('maxlength');

        //$('input[type="tel"]').inputmask({
        //mask: '(999) 999-9999'
        //});
    };

    this.initOtherCountry = function () {
        self.defaultCountry = false;
        $('.state').addClass('hidden');
        $('.stateFreeForm').removeClass('hidden');

        $('input.ein').inputmask('remove');
        $('input#zip').inputmask('remove');
        $('input[type="tel"]').inputmask('remove');
        $('input[type="tel"]').attr('maxlength', '20');
    };

    this.changePassword = function (clientId) {
        var password1 = $('#password1').val();
        var password2 = $('#password2').val();

        if (password1 !== password2) {
            alert('Passwords do not match');
            return;
        }

        var url = '/api/user/changepasswordclient/' + clientId + '/' + password1;

        $.ajax({
            url: url,
            type: "POST",
            dataType: 'json',
            contentType: 'application/json',
            success: function (result) {
                if (result.isSuccess === true) {
                    $('.modal').modal('hide');
                    alert('Password successfully changed');
                }
                else {
                    var alertMessage = "";

                    for (var i = 0; i < result.messages.length; i++) {
                        alertMessage += result.messages[i] + "\n";
                    }

                    alert(alertMessage);
                }
            },
            error: function (xhr, resp, text) {
                console.log(xhr, resp, text);
            }
        });
    };

    this.assignAgent = function (clientId, agentId, agentName) {
        var url = ['/api/client/addagent', clientId, agentId].join('/');

        var html = '<button type="button" class="btn btn-primary btn-sm agent" data-clientid="#CLIENTID#" data-agentid="#AGENTID#">';
        html += '<i class="fa fa-times icon-white"></i> <span>#AGENTNAME#</span></button>';
        html = html.replace('#CLIENTID#', clientId);
        html = html.replace('#AGENTID#', agentId);
        html = html.replace('#AGENTNAME#', agentName);

        $.post(url,
            function (data) {
                $('#agents').append(html);
            }
        );
    };
}
