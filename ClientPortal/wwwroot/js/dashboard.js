var DASHBOARD = new Dashboard();

function Dashboard() {
    var self = this;

    this.init = function () {
        $('#card-distribution').DataTable();
    }
}