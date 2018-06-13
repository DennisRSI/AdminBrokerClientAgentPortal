REPORT = new Report();

function Report() {
    var self = this;

    this.init = function () {
        $(document).on('click', '#export-excel', function () {
            self.exportExcel();
        });
    }

    this.exportExcel = function () {
        var showHeader = true;
        var csv = '';

        $('table.dataTable').each(function (index) {
            var table = $(this).DataTable();
            var data = table.buttons.exportData();
            var header = null;

            if (data.body.length > 0) {
                if (showHeader) {
                    header = data.header;
                    showHeader = false;
                }

                var text = Papa.unparse(
                    {
                        fields: header,
                        data: data.body
                    }
                );

                var clientName = $('.tableName').eq(index).text();
                csv += clientName + '\n';
                csv += text + '\n\n';
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
