var MENU = new Menu(); 

function Menu() {
    var self = this;
    
    this.init = function () {
        $('.nav-item').click(function () {

            $('#sidebar-menu .nav-item').removeClass('last-clicked');
            $(this).addClass('last-clicked');

            var id = '';
            var page = $(this).data('page')
            var cmd = "";

            if (page === "admin-change" || page === "user-list") {
                cmd = $(this).data('cmd');
                self.get_page(page, cmd, id);
            }

            if (page === "user-list") {
                id = $(this).data('id');
                self.get_page(page, cmd, id);
            }

            if (page === "my-account") {
                id = $(this).data('id');
                self.loadPage(page, id);
            }
        });

        $('#search').click(function () {
            var query = $('#search-query').val();
            self.loadPage('search', query);
        });
    }

    this.get_page = function (page, cmd, id) {
        var url = "/api/menu/" + page + "/" + cmd;
        $("#loader-container").show();

        $.get(url, function (data, status) {
            $('#main_panel').html(data);
            $("#loader-container").hide();
            self.after_load(page, cmd, id);
        });
    }

    this.after_load = function (page, cmd, id) {
        if (page === "user-list") {
            var brokerId = 0;
            var clientId = 0;
            
            LIST.init(cmd, brokerId, clientId);
        }
    }

    this.loadPage = function(page, param1, param2) {
        var url = "/api/menu/" + page

        if (param1 !== undefined) {
            url += "/" + param1;
        }

        if (param2 !== undefined) {
            url += "/" + param2;
        }

        $("#loader-container").show();

        $.get(url, function (data, status) {
            $('#main_panel').html(data);
            $("#loader-container").hide();
        });
    }
}