REPORT = new Report();

function Report() {
    var self = this;

    this.init = function () {
        $(document).off('click', '#export-excel');

        $(document).on('click', '#export-excel', function () {
            self.exportExcel();
        });
    }

    this.exportExcel = function () {
        var csv = '';

        $('table.dataTable').each(function (index) {
            var table = $(this).DataTable();
            var data = table.buttons.exportData();

            if (data.body.length > 0) {
                var clientName = $('.tableName').eq(index).text().replace(',', '');

                data.body.forEach(function (element) {
                    element.unshift(clientName);
                });

                var text = Papa.unparse(
                    {
                        fields: null,
                        data: data.body
                    }
                );


                if (csv === '') {
                    csv = 'Name,' + data.header.join(',') + '\n';
                }

                csv += text + '\n';
            }
        });

        if (csv.length > 0) {
            var date = new Date();
            var filename = ['export-', date.getMonth() + 1, date.getDate(), date.getHours(), date.getMinutes(), date.getSeconds()].join('') + '.csv';
            var blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });

            if (navigator.msSaveBlob) { // IE 10+
                navigator.msSaveBlob(blob, filename);
            } else {
                var link = document.createElement("a");
                if (link.download !== undefined) { // feature detection
                    // Browsers that support HTML5 download attribute
                    var url = URL.createObjectURL(blob);
                    link.setAttribute("href", url);
                    link.setAttribute("download", filename);
                    link.style.visibility = 'hidden';
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                }
            }
        }
    }
}
