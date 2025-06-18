var IMPORTEXCEL = new ImportExcel();

function ImportExcel()
{
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
            var url = '/api/arn/excelimport';
            fdata.append(files[0].name, files[0]);
            $.ajax({
                type: "POST",
                url: url,
                beforeSend: function (xhr) {
                    xhr.setRequestHeader("XSRF-TOKEN",
                        $('input:hidden[name="__RequestVerificationToken"]').val());
                    $("#loader-container").show();
                },
                data: fdata,
                contentType: false,
                processData: false,
                success: function (response) {
                    $("#loader-container").hide();
                    if (response.length === 0)
                        alert('Some error occured while uploading');
                    else {
                        //return response;
                        var message = response.item1 === false ? "Error: " : "";
                        message += response.item2;
                        $('#dvData').html(message);
                    }

                    //console.log(response);
                },
                error: function (e) {
                    $("#loader-container").hide();
                    $('#dvData').html(e.responseText);
                    //console.log(response.item2);
                }
            });
        });
    };
}
