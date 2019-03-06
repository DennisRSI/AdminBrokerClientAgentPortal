PRODUCTION = new Production();

function Production() {
    var self = this;
    var _role;

    this.init = function (role) {
        self._role = role;
        self.updateControls();

        $('#select-report').change(function () {
            self.updateControls();
        });

        $('button.runreport').click(function () {
            self.getHtml();
        });
    };

    this.updateControls = function () {
        $('.filter').addClass('hidden');
        var value = $('#select-report option:selected').val();
        $('#' + value).removeClass('hidden');
    };

    this.getHtml = function () {
        var type = $('#select-report').val();
        var id = $('#select-' + type + ' option:selected').val();
        var name = $('#select-' + type + ' option:selected').text();
        var checkOutStart = $('#checkOutStart').val();
        var checkOutEnd = $('#checkOutEnd').val();
        var bookingStart = $('#bookingStart').val();
        var bookingEnd = $('#bookingEnd').val();
        var paymentStatus = $('#paymentStatus option:selected').val();

        var url = '/api/reportproduction/gethtmldetail';

        if (id === '0') {
            url = '/api/reportproduction/gethtmlsummary';
        }

        url = [url, type, id, name, paymentStatus, checkOutStart, checkOutEnd, bookingStart, bookingEnd].join('/');
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
                self.initDataTables(id);
            },
            error: function (xhr, resp, text) {
                console.log(xhr, resp, text);
            }
        });
    };

    this.initDataTables = function (id) {
        var sortColumn = $('.jambo_table').data('sortcolumn');
        var sortOrder = $('.jambo_table').data('sortorder');

        if (sortColumn === undefined) {
            $('.jambo_table').DataTable();
        }
        else {
            $('.jambo_table').DataTable({
                "order": [[sortColumn, sortOrder]]
            });
        }

        if (id === '0' && self._role === 'Agent') {
            var table = $('.jambo_table').DataTable();
            var column = table.column(4);
            column.visible(false);
        }
    };
}
