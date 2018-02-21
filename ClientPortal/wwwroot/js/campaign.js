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

        $('#addCampaignButton').click(function () {
            var data = self.serializeFormJSON($('#addCampaignForm'));

            $.ajax({
                url: '/api/campaign/create',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    if (result.is_success == true) {
                        self.redirectToPage(result.account_id);
                    }
                    else {
                        alert('Error: ' + result.message);
                    }
                },
                error: function (xhr, resp, text) {
                    console.log(xhr, resp, text);
                }
            })
        });
    }

    this.selectVideo = function (element, type) {
        var id = element.data('videoid');
        $('input#' + type + 'LoginVideoId').val(id);

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
            { "data": "cardQuantity" },
            { "data": "campaignType" },
            { "data": "benefitText" },
            { "data": "statusText" }
        ];

        $dt = LIST.generateList("campaign_tbl", url, cols, "GET", false);
    }

    // TODO: Refactor this
    this.serializeFormJSON = function (form) {
        var o = {};
        var a = form.serializeArray();
        $.each(a, function () {
            console.log('name: ' + this.name);
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    }
}