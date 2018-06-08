ACTIVATION = new Activation();

function Activation() {
    var self = this;

    this.init = function () {

        $('#select-report').click(function () {
            $('#client, #agent').hide();
            var value = $('#select-report').val();
            $('#' + value).show();
        });

        $('#run-report').click(function () {
            self.getHtml();
        });
    }

    this.getHtml = function () {
        var type = $('#select-report').val();
        var id = $('#select-client').val();

        var url = ['/api/reportactivation/gethtml', type, id].join('/');

        $("#loader-container").show();

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $('#result').html(data);
                $("#loader-container").hide();
            },
            error: function (xhr, resp, text) {
                console.log(xhr, resp, text);
            }
        })
    }
}
