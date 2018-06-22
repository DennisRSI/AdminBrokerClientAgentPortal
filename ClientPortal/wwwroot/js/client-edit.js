var CLIENTEDIT = new ClientEdit();

function ClientEdit() {
    var self = this;

    this.init = function (clientId) {

        $('#deactivate-message').hide();

        $('input[type="tel"]').inputmask({
            mask: '(999) 999-9999'
        });

        $('#ein').inputmask({
            mask: '99-9999999'
        });

        $('#zip').inputmask({
            mask: '99999'
        });

        $('#clientdeactivate').on("click", function (event) {
            self.deactivateClient();
        });

        $("#save").on("click", function (event) {

            var form = $('#edit');
            var valid = form.valid();

            if (!valid) {
                return;
            }

            var data = UTILITY.serializeFormJSON(form);
            console.log('data:');
            console.log(data);

            $.ajax({
                url: '/api/user/clientupdateprofile/' + clientId,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    if (result.isSuccess == true) {
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
    }

    this.deactivateClient = function (clientId) {
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
    }
}

