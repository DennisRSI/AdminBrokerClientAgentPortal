var DASHBOARD = new Dashboard();

function Dashboard() {
    var self = this;

    this.init = function () {
        $('#card-distribution').DataTable();

        $('#search').click(function () {
            var query = $('#search-query').val();
            MENU.loadPage('menu', 'search', query);
        });
    }
}
