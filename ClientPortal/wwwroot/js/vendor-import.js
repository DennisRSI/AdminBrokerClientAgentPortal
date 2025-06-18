var VENDORIMPORT = new VendorImport();

function VendorImport() {
    var self = this;

    this.init = function () {
        $('#btnUpload').on('click', function () {
            var fileExtension = ['xls', 'xlsx'];
            var filename = $('#fUpload').val();
            if (filename.length === 0) {
                alert("Please select a file.");
                return false;
            }
            else {
                var extension = filename.replace(/^.*\./, '');
                if ($.inArray(extension, fileExtension) === -1) {
                    alert("Please select only excel files.");
                    return false;
                }
            }
            var fdata = new FormData();
            var fileUpload = $("#fUpload").get(0);
            var files = fileUpload.files;
            fdata.append(files[0].name, files[0]);
            $.ajax({
                type: "POST",
                url: "/vendorimport/import",
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                },
                data: fdata,
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.length === 0)
                        alert('Some error occured while uploading');
                    else {
                        $('#dvData').html(response);
                    }
                },
                error: function (e) {
                    $('#dvData').html(e.responseText);
                }
            });
        });
    };
}

/*$(document).ready(function () {
    
});*/
