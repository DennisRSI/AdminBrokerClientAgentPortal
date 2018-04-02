var PDF = new Pdf();

function Pdf() {
    var self = this;

    this.getPurchasePdf = function (purchaseId) {
        var url = '/api/purchase/viewconfirmation/' + purchaseId;
        self.downloadFromUrl(url, 'PurchaseConfirmation-' + purchaseId + '.pdf');
    }

    this.downloadFromUrl = function (url, filename) {
        $.ajax({
            url: url,
            success: function (result) {
                self.downloadPdf(result, filename);
            },
            error: function (xhr, resp, text) {
                console.log(xhr, resp, text);
            }
        });
    }

    this.downloadPdf = function (source, filename) {
        var document = new jsPDF('p', 'pt', 'letter');

        document.fromHTML(
            source,
            margins.left,
            margins.top, {
                'width': margins.width
            },
            function (dispose) {
                document.save(filename);
            },
            margins
        );
    }

    var margins = {
                     top: 80,
                     bottom: 60,
                     left: 40,
                     width: 522
                  };
}

