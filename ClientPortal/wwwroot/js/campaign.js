var CAMPAIGN = new Campaign();

function Campaign(){
    var self = this;

    this.init = function () {

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
        var template = $.templates('#template');
        var output = template.render(data);
        $('#' + type + 'VidCarousel .carousel-inner').html(output);

        var divs = $('#' + type + 'VidCarousel .video');
        for (var i = 0; i < divs.length; i += 3) {
            divs.slice(i, i + 3).wrapAll("<div class='item'></div>");
        }

        $('#' + type + 'VidCarousel .item:first').addClass('active');
    }
}