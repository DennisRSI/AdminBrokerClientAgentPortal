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

            if (page === "admin-change" || page === "my-account" || page === "user-list") {
                cmd = $(this).data('cmd');
                //alert(cmd);
            }

            if (page === "user-list") {
                id = $(this).data('id');
            }

            //alert($(this).data('id'));

            self.get_page(page, cmd, id);
        });
        
    }

    this.get_page = function (page, cmd, id) {
        var url = "/api/menu/" + page + "/" + cmd;
        $("#loader-container").show();
        //alert(url);
        $.get(url, function (data, status) {
            $('#main_panel').html(data);
            $("#loader-container").hide();
            self.after_load(page, cmd, id);
            //alert("Data: " + data + "\nStatus: " + status);
        });
    }

    this.after_load = function (page, cmd, id) {
        if (page === "user-list") {
            var brokerId = 0;
            var clientId = 0;
            
            LIST.init(cmd, brokerId, clientId);
        }
    }
}