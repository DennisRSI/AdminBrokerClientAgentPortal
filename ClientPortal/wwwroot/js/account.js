ACCOUNT = new Account();

function Account() {
    this.init = function () {
        $('#agents-tab').click(function () {
            id = $(this).data('id');
            LIST.init("Agent", id, 0);
        });
        $('#clients-tab').click(function () {
            id = $(this).data('id');
            LIST.init("Client", id, 0);
        });
        $('#purchase-tab').click(function () {
            id = $(this).data('id');
            LIST.init("Purchases", id, 0);
        });
    }
}