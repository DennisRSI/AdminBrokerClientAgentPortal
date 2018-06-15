PRODUCTION = new Production();

function Production() {
    var self = this;

    this.init = function () {

        self.updateControls();

        $('#select-report').change(function () {
            self.updateControls();
        });

        $('button.runreport').click(function () {
            self.getHtml();
        });
    }

    this.updateControls = function () {
        $('.filter').addClass('hidden');
        var value = $('#select-report option:selected').val();
        $('#select-' + value).removeClass('hidden');
    }

    this.getHtml = function () {
    }
}
