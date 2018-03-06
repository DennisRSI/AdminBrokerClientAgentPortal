var ADDUSER = new AddUser();

function AddUser(){
    var self = this;

    this.init = function () {
        var url = '/api/user/' + $('#userType').val();
        
        $('#addUserBTN').click(function () {

            jQuery.validator.setDefaults({
                errorPlacement: function (error, element) {
                },
            });

            var valid = $('#addUserForm').valid();

            if (!valid) {
                return;
            }

            var data = self.serializeFormJSON($('#addUserForm'));

            $.ajax({
                url: url, // url where to submit the request
                type: "POST", // type of action POST || GET
                dataType: 'json', // data type
                contentType: 'application/json',
                data: JSON.stringify(data), // post data || get data
                success: function (result) {
                    if (result.is_success == true) {
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

    this.serializeFormJSON = function (form) {
        var o = {};
        var a = form.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    }
}