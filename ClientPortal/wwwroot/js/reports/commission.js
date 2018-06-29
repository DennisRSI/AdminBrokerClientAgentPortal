COMMISSION = new Commission();

function Commission() {
    var self = this;

    this.init = function () {

        self.updateControls();

        $('#select-report').change(function () {
            self.updateControls();
        });

        $('button.runreport').click(function () {
            self.getHtml();
        });
    }

    this.updateControls = function () {
        $('.filter').addClass('hidden');
        var value = $('#select-report option:selected').val();
        $('#' + value).removeClass('hidden');
    }

    this.getHtml = function () {
        var type = $('#select-report').val();
        var id = $('#select-' + type + ' option:selected').val();
        var name = $('#select-' + type + ' option:selected').text();
        var checkOutStart = $('#checkOutStart').val();
        var checkOutEnd = $('#checkOutEnd').val();

        // TODO: Fill this in
        var paymentStatus = "1";

        var url = '/api/reportcommission/gethtml';

        var url = [url, type, id, name, paymentStatus, checkOutStart, checkOutEnd].join('/');
        url = encodeURI(url);

        $("#loader-container").show();

        $.ajax({
            url: url,
            type: 'GET',
            success: function (data) {
                $('#result').html(data);
                $('#start-report').hide();
                $('#update-report').removeClass('hidden');
                $("#loader-container").hide();
                self.initDataTables();
            },
            error: function (xhr, resp, text) {
                console.log(xhr, resp, text);
            }
        })
    }

    this.initDataTables = function () {
        $('.jambo_table').DataTable();
    }
}
