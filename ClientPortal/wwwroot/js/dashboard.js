var DASHBOARD = new Dashboard();

function Dashboard() {
    var self = this;

    this.init = function () {
        self.initDistributionTable();

        $('#search').click(function () {
            var query = $('#search-query').val();
            MENU.loadPage('menu', 'search', query);
        });
    }

    this.initDistributionTable = function () {
        var columns = [
            { "data": "name" },
            { "data": "physicalTotal" },
            { "data": "physicalActivated" },
            { "data": "physicalRate" },
            { "data": "virtualTotal" },
            { "data": "virtualActivated" },
            { "data": "virtualRate" },
        ];

        var selector = '#card-distribution';
        var table = $(selector);
        var role = table.data('role');
        var accountId = table.data('id');

        if (role.indexOf('Admin') >= 0) {
            role = 'admin';
        }

        var url = ['/api/dashboarddistribution', role, accountId].join('/');

        var tableSettings = LIST.getDataTableDefaults(url, columns, 'GET');

        if (!$.fn.DataTable.isDataTable(selector)) {
            $(selector).DataTable(tableSettings);
            $(selector).css('width', '100%');
        }
    }
}
