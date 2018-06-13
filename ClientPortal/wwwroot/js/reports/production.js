PRODUCTION = new Production();

function Production() {
    var self = this;

    this.init = function () {

        self.updateControls();

        $('#select-report').change(function () {
            self.updateControls();
        });

        $('button.runreport').click(function () {
        });
    }

    this.updateControls = function () {

        UTILITY.setQueryStartDate('#checkOutStart');
        UTILITY.setQueryStartDate('#bookingStart');
        UTILITY.setQueryEndDate('#checkOutEnd');
        UTILITY.setQueryEndDate('#bookingEnd');

        $('.filter').addClass('hidden');
        var value = $('#select-report option:selected').val();
        $('#select-' + value).removeClass('hidden');
    }
}
