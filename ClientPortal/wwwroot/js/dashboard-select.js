var DASHBOARDSELECT = new DashboardSelect();

function DashboardSelect() {
    var self = this;

    this.init = function (role) {
        $('#simulate').on('click', function () {
            var id = $('select').val();
            MENU.loadPage('Dashboard', 'view', role, id);
        });
    };
}
