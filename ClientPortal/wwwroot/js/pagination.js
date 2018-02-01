var PAGINATION = new Pagination();

function Pagination() {
    var self = this;

    this.init = function () {
        totalPages = $('#total_pages').val();
        numberOfRows = $('#number_of_rows').val();
        brokerId = $('#broker_id').val();
        role = $('#role').val();
        sortColumn = $('#sort_column').val();
        sortDirection = $('#sort_direction').val();
        totalCount = $('#total_count').val();

        $('#td-foot').bootpag({
            total: 10,
            maxVisible: 10
        }).on('page', function (event, num) {
            $("#loader-container").show();
            var url = "/api/list/" + num + "/" + numberOfRows + "/" + brokerId + "/" + sortColumn + "/" + sortDirection + "/" + role + ' #tb-body';
            $("#td-body").load(url, function () {
                $("#page-number").html("Page " + num + " of " + $('#total_pages').val()) + " (" + totalCount + ")"; // or some ajax content loading...
                $("#loader-container").hide();
            });
            
        });
    }
}