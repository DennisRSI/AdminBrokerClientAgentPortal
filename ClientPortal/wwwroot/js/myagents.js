var MYAGENTS = new MyAgents();

function MyAgents() {
    var self = this;
    
    this.init = function () {

        $('table.jambo_table').attr('style', 'width: 100%');

        var url = '/api/viewdata/agents';

        var cols = [
            { "data": "name" },
            { "data": "email" },
            { "data": "phoneNumber" }
        ];

        var selector = '#myagents_tbl';
        var tableSettings = LIST.getDataTableDefaults(url, cols, 'GET', 'applicationReference');
        tableSettings.ajax.dataSrc = '';

        if (!$.fn.DataTable.isDataTable(selector)) {
            var table = $(selector).DataTable(tableSettings);

            table.on('click', 'tr', function () {
                var id = $(this).attr('id');
                MENU.loadPage('menu', 'my-account', id);
            });
        }
    };
}
