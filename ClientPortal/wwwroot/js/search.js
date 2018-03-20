var SEARCH = new Search();

function Search() {
    var self = this;

    this.init = function () {
        var settings = {
            searching: false,
            paging: false,
            info: false,
            language: {
                emptyTable: "No search results found"
            }
        };

        $('#table-brokers').DataTable(settings);
        $('#table-agents').DataTable(settings);
        $('#table-clients').DataTable(settings);
        $('#table-campaigns').DataTable(settings);
        $('#table-cards').DataTable(settings);

        $('#search').click(function () {
            var query = $('#search-query').val();
            MENU.loadPage('menu', 'search', query);
        });
    }
}
