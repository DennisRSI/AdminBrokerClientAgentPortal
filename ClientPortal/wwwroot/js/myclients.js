var MYCLIENTS = new MyClients();

function MyClients() {
    var self = this;
    
    this.init = function () {

        $('table.jambo_table').attr('style', 'width: 100%');

        var url = '/api/viewdata/clients';

        var cols = [
            { "data": "companyName" },
            { "data": "contactName" },
            { "data": "email" },
            { "data": "phoneNumber" },
            { "data": "cardQuantity" },
            { "data": "salesAgent" },
        ];

        var selector = '#myclients_tbl';
        var tableSettings = LIST.getDataTableDefaults(url, cols, 'GET', 'clientId');
        tableSettings.ajax.dataSrc = '';

        if (!$.fn.DataTable.isDataTable(selector)) {
            $(selector).DataTable(tableSettings);
        }
    }
}
