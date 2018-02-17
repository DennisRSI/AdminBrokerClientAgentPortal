var CAMPAIGN = new Campaign();

function Campaign(){
    var self = this;

    this.init = function (clientId) {

        self.updateClientCampaigns(clientId);

        $.get('/api/video/?isPreLogin=true', function (data) {
            self.processAjaxData(data, 'pre');
        });

        $.get('/api/video/?isPreLogin=false', function (data) {
            self.processAjaxData(data, 'post');
        });

        $('#preVidCarousel').on('click', '.video-select', function () {
            self.selectVideo($(this), 'pre');
        });

        $('#postVidCarousel').on('click', '.video-select', function () {
            self.selectVideo($(this), 'post');
        });
    }

    this.selectVideo = function (element, type) {
        var id = element.data('videoid');
        $('input#' + type + 'VideoId').val(id);

        $('#' + type + 'VidCarousel .x_panel').removeClass('selected');
        element.parent().addClass('selected');
    }

    this.processAjaxData = function (data, type) {

        // Convert YouTube link to embedded
        for (var i = 0; i < data.length; i++) {
            data[i].url = data[i].url.replace("/watch?v=", "/embed/");
        }

        var template = $.templates('#template');
        var output = template.render(data);
        $('#' + type + 'VidCarousel .carousel-inner').html(output);

        var divs = $('#' + type + 'VidCarousel .video');
        for (var i = 0; i < divs.length; i += 3) {
            divs.slice(i, i + 3).wrapAll("<div class='item'></div>");
        }

        $('#' + type + 'VidCarousel .item:first').addClass('active');
    }

    this.updateClientCampaigns = function (clientId) {
        var url = "api/campaign/getbyclient/" + clientId;

        var cols = [
            { "data": "campaignId" },
            { "data": "campaignName" },
            { "data": "cardQuantity" }
        ];

        $dt = LIST.generateList("campaign_tbl", url, cols, "GET", false);
    }
}