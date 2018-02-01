var PROFILE = new Profile();
function Profile() {
    var self = this;

    this.init = function () {
        $(".toggle-accordion").on("click", function () {

            var accordionId = $(this).attr("accordion-id"),
                numPanelOpen = $(accordionId + ' .collapse.in').length;

            $(this).toggleClass("active");

            if (numPanelOpen == 0) {
                self.openAllPanels(accordionId);
            } else {
                self.closeAllPanels(accordionId);
            }
        })

        
    }

    this.openAllPanels = function (aId) {
        console.log("setAllPanelOpen");
        $(aId + ' .panel-collapse:not(".in")').collapse('show');
    }
    this.closeAllPanels = function (aId) {
        console.log("setAllPanelclose");
        $(aId + ' .panel-collapse.in').collapse('hide');
    }
}