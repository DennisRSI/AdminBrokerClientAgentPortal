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
        var paymentStatus = $('#paymentStatus option:selected').val();

        var url = '/api/reportcommission/gethtml';

        url = [url, type, id, name, paymentStatus, checkOutStart, checkOutEnd].join('/');
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
        });
    };

    this.initDataTables = function () {
        $('.jambo_table').DataTable();

        $('.jambo_table tr').on('click', function () {
            var tr = $(this);
            var childrenData = tr.data('children');
            var row = tr.parents('table').DataTable().row(tr);

            if (row.child.isShown()) {
                row.child.hide();
                tr.removeClass('shown');
            }
            else {
                var template = '<tr><td colspan="4"></td><td colspan="2"><strong>[NAME]</strong></td><td>[EARNED]</td><td></td></tr>';
                var split = childrenData.split(';');
                var total = '';

                split.forEach(function (childData) {
                    if (childData.length > 2) {
                        var child = childData.split('|');
                        var result = template.replace('[NAME]', child[0]).replace('[EARNED]', child[1])
                        total += result;
                    }
                });

                row.child($(total)).show();
                tr.addClass('shown');
            }
        });
    };
}
