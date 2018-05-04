var CLIENTEDIT = new ClientEdit();

function ClientEdit() {
    var self = this;

    this.init = function (clientId) {

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
                url: '/api/user/updateprofile/' + clientId,
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

}

