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
        var start = $('#startDate').val();
        var end = $('#endDate').val();
        var selectReport = $('#select-report').val();
        var name;

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

        console.log('selected: ' + selectReport);

        if (selectReport == 'client') {
            name = $('#select-client option:selected').text();
        }
        else {
            name = $('#select-agent option:selected').text();
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
                $('#result').show();
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
            { "data": "activationDate" },
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
