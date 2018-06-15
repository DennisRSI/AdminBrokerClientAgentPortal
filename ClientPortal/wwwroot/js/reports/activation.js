ACTIVATION = new Activation();

function Activation() {
    var self = this;

    this.init = function () {

        UTILITY.setQueryStartDate('input.startDate');
        UTILITY.setQueryEndDate('input.endDate');

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
        var id = $('#select-' + type).val();
        var start = $('#startDate').val();
        var end = $('#endDate').val();
        var name = $('#select-' + type + ' option:selected').text();

        var valid = true;

        if (start.length < 8) {
            $('#startDate').addClass('error');
            valid = false;
        }

        if (end.length < 8) {
            $('#endDate').addClass('error');
            valid = false;
        }

        if (!valid) {
            return;
        }

        $('#startDate').removeClass('error');
        $('#endDate').removeClass('error');

        var url = ['/api/reportactivation/gethtml', type, id, name, start, end].join('/');
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
        var status = $('#select-status').val();
        var used = $('#select-used').val();
        var start = $('#startDate').val();
        var end = $('#endDate').val();

        var columns = [
            { "data": "cardNumber" },
            {
                "data": "activationDate",
                "render":
                    function (data) {
                        return UTILITY.formatDateTime(data);
                    }
            },

            { "data": "memberName" },
            { "data": "denomination" },
            { "data": "cardType" },
            { "data": "isCardUsed" },
            { "data": "campaignName" },
            { "data": "cardStatus" }
        ];

        $('table').each(function (index) {
            var id = $(this).attr('id');
            var type = $(this).data('type');
            var accountId = $(this).data('id');
            var selector = '#' + id;
            var url = ['/api/reportactivation/getjson', type, accountId, status, used, start, end].join('/');

            var tableSettings = LIST.getDataTableDefaults(url, columns, 'GET');

            if (!$.fn.DataTable.isDataTable(selector)) {
                $(selector).DataTable(tableSettings);
                $(selector).css('width', '100%');
            }
        });
    }
}
