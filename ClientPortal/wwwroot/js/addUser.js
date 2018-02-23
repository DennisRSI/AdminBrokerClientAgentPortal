var ADDUSER = new AddUser();

function AddUser(){
    var self = this;

    this.init = function () {
        var url = '/api/user/' + $('#userType').val();
        
        $('#addUserBTN').click(function () {
            var data = self.serializeFormJSON($('#addUserForm'));

            $.ajax({
                url: url, // url where to submit the request
                type: "POST", // type of action POST || GET
                dataType: 'json', // data type
                contentType: 'application/json',
                data: JSON.stringify(data), // post data || get data
                success: function (result) {
                    if (result.is_success == true) {
                        self.redirectToPage(result.account_id);
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

    this.redirectToPage = function (accountId) {
        var url = '/api/menu/my-account/';
        url += accountId;
        $("#loader-container").show();

        $.get(url, function (data, status) {
            $('#main_panel').html(data);
            $("#loader-container").hide();
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