var CAMPAIGN = new Campaign();

function Campaign(){
    var self = this;
    var dataTable;

    this.init = function (clientId) {
        self.initCampaignTable(clientId);

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

        $('#campaign_tbl').on('click', 'i.clone', function () {
            var id = $(this).data('clone');
            self.cloneCampaign(id);
        });

        $('#addCampaignButton').click(function () {

            jQuery.validator.setDefaults({
                errorPlacement: function (error, element) {
                },
            });

            var form = $('#addCampaignForm');
            var formValid = form.valid();

            var preVideoValid = self.validateVideo('pre');
            var postVideoValid = self.validateVideo('post');

            if (!formValid || !preVideoValid || !postVideoValid) {
                return;
            }

            var data = UTILITY.serializeFormJSON($('#addCampaignForm'));

            $.ajax({
                url: '/api/campaign/create/' + clientId,
                type: 'POST',
                dataType: 'text',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    $('.new-campaign-modal-lg').modal('hide');
                    dataTable.ajax.reload();
                },
                error: function (xhr, resp, text) {
                    console.log(xhr, resp, text);
                }
            })
        });
    }

    this.selectTemplate = function (element, type) {
        var id = element.data('videoid');
        $('input#' + type + 'LoginVideoId').val(id);

        $('#' + type + 'VidCarousel .x_panel').removeClass('selected');
        element.parent().addClass('selected');
    }

    this.selectVideo = function (element, type) {
        var id = element.data('videoid');
        $('input#' + type + 'LoginVideoId').val(id);

        $('#' + type + 'VidCarousel .x_panel').removeClass('selected');
        element.parent().addClass('selected');
    }

    this.validateVideo = function (type) {
        var videoId = $('input#' + type + 'LoginVideoId').val();
        var videoUrl = $('input#' + type + 'LoginVideoUrl').val();
        var selector = '#' + type + 'ActivationVideo';

        if (videoId || videoUrl) {
            $(selector).removeClass('error');
            return true;
        }

        $(selector).addClass('error');
        return false;
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

    this.initCampaignTable = function (clientId) {
        var url = "api/campaign/getbyclient/" + clientId;

        var cols = [
            { "data": "campaignId" },
            { "data": "campaignName" },
            { "data": "cardQuantity" },
            { "data": "campaignType" },
            { "data": "benefitText" },
            { "data": "statusText" },
            {
                "data": "campaignId",
                "className": "text-center",
                "render": function (data, type, row) {
                    return '<i data-clone="' + data + '" class="clone fa fa-clone fa-sm"></i>';
                }
            },
        ];

        if (!$.fn.DataTable.isDataTable("#campaign_tbl")) {
            dataTable = LIST.generateUpdatableList("#campaign_tbl", url, cols, "GET");
        }
    }

    this.cloneCampaign = function (campaignId) {
        $.post('/api/campaign/clone/' + campaignId,
            function (data) {
                dataTable.ajax.reload();
            }
        );
    }
}
