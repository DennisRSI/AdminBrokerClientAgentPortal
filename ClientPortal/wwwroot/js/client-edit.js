var CLIENTEDIT = new ClientEdit();

function ClientEdit() {
    var self = this;
    var defaultCountry = true;

    this.init = function (clientId) {

        $('#deactivate-message').hide();

        self.initCountry();

        $('select#country').unbind('change').change(function () {
            self.initCountry();
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

            if (!self.defaultCountry) {
                data.state = data.state_freeform;
            }

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

    this.initCountry = function () {
        var value = $('select#country').val();

        if (value === 'USA' || value === 'CAN') {
            self.initDefaultCountry();
        }
        else {
            self.initOtherCountry();
        }
    }

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
    }

    this.initOtherCountry = function () {
        self.defaultCountry = false;
        $('.state').addClass('hidden');
        $('.stateFreeForm').removeClass('hidden');

        $('input.ein').inputmask('remove');
        $('input#zip').inputmask('remove');
        $('input[type="tel"]').inputmask('remove');
        $('input[type="tel"]').attr('maxlength', '20');
    }
}
