var LIST = new List();

function List() {
    var self = this;
    
    this.init = function (role, brokerId, clientId) {
        switch (role) {
            case "Super Administrator":
            case "Administrator":
                self.adminList(role);
                break;
            case "Client":
                self.clientList(brokerId, clientId);
                break;
            case "Agent":
                self.agentList(brokerId, clientId);
                break;
            case "Broker":
                self.brokerList();
                break;
            case "Purchases":
                self.purchasesList(brokerId);
                break;
        }
    };

    this.purchasesList = function (brokerId) {
        var url = "api/purchase/list/" + brokerId;
        var cols = [
            { "data": "purchaseDateString" },
            { "data": "orderId" },
            { "data": "physicalValueString" },
            { "data": "physicalQuantity" },
            { "data": "virtualValueString" },
            { "data": "virtualQuantity" },
            { "data": "sequenceStart" },
            { "data": "sequenceEnd" },
            { "data": "totalValue" },
            { "data": null, defaultContent: '<a href="#" class="pdf"><i class="fa fa-file-pdf-o pdf-icon-red"></i> Download</a>' }
        ];

        var selector = '#purchase_tbl';
        var tableSettings = this.getDataTableDefaults(url, cols, 'GET', 'orderId');
        tableSettings.ajax.dataSrc = '';

        if (!$.fn.DataTable.isDataTable(selector)) {
            $(selector).DataTable(tableSettings);
            $(selector).css('width', '100%');

            $(selector).on('click', 'a.pdf', function () {
                var id = $(this).closest('tr').attr('id');
                PDF.getPurchasePdf(id);
            });
        }
    };

    this.adminList = function (role) {
        var $dt;
        url = "/api/list/";
        if (role === "Administrator")
            url += "admin";
        else
            url += "sa";

        var cols = [
            { "data": "full_name" },
            { "data": "company" },
            { "data": "email" },
            { "data": "phone" },
            {
                "data": "activation_date", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "data": "deactivation_date"
            }
        ];

        $dt = self.generateUpdatableList('#admin_tbl', url, cols, 'POST');

        $dt.on('click', 'tr', function () {
            var data = $dt.row(this).id();
            self.redirectToPage('/api/menu/my-account/' + data);
        });
    };

    this.brokerList = function () {
        var url = "api/list/broker";
        var cols = [
            {
                "data": "full_name",
                "createdCell": function (td, cellData, rowData, row, col) {
                    $(td).attr('title', 'Click to view details');
                    $(td).attr('data-toggle', 'tooltip');
                    $(td).attr('data-container', 'body');
                }
            },
            { "data": "company" },
            { "data": "email" },
            { "data": "phone" },
            { "data": "commission_rate" },
            {
                "data": "activation_date", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "data": "deactivation_date"
            }
        ];

        var selector = '#broker_tbl';
        var tableSettings = this.getDataTableDefaults(url, cols, 'POST');

        if (!$.fn.DataTable.isDataTable(selector)) {
            var table = $(selector).DataTable(tableSettings);

            $(selector).on('click', 'tr', function () {
                var data = $(this).attr('id');
                self.redirectToPage('/api/menu/my-account/' + data);
            });
        }
    };

    this.agentList = function (brokerId, clientId) {
        var url = "api/list/agent/" + brokerId.toString() + "/" + clientId.toString();
        var cols = [
            { "data": "full_name" },
            { "data": "company" },
            { "data": "email" },
            { "data": "phone" },
            { "data": "commission_rate" },
            { "data": "number_of_clients" },
            { "data": "primaryAgent" },
            {
                "data": "activation_date", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "data": "deactivation_date"
            }
        ];

        $dt = self.generateList("agent_tbl", url, cols);

        $dt.on('click', 'tr', function () {
            var data = $dt.row(this).id();
            self.redirectToPage('/api/menu/my-account/' + data);
        });
    };

    this.clientList = function (brokerId, clientId) {
        var url = "api/list/client/" + brokerId.toString() + "/" + clientId.toString();

        var cols = [
            { "data": "full_name" },
            { "data": "company" },
            { "data": "email" },
            { "data": "phone" },
            { "data": "commission_rate" },
            { "data": "card_quantity" },
            {
                "data": "activation_date", "render": function (data) {
                    var date = new Date(data);
                    var month = date.getMonth() + 1;
                    return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
                }
            },
            {
                "data": "deactivation_date", "render": function (data) {
                    return UTILITY.formatDateTime(data);
                }
            }
        ];

        if ($.fn.DataTable.isDataTable('#client_tbl')) {
            window.clientTable.destroy();
        }

        $dt = self.generateUpdatableList('#client_tbl', url, cols, 'POST');
        window.clientTable = $dt;

        $dt.on('click', 'tr', function () {
            var data = $dt.row(this).id();
            self.redirectToPage('/api/menu/client-details/' + data);
        });
    };

    this.generateList = function (tableName, url, columns, method, serverSide) {

        if (typeof method === 'undefined') {
            method = 'POST';
        }

        if (typeof serverSide === 'undefined') {
            serverSide = true;
        }

        var tblParsed = '#' + tableName;
        var $dt;
        if (!$.fn.DataTable.isDataTable(tblParsed)) {
            $dt = $(tblParsed).DataTable({
                "processing": true, // for show progress bar  
                "language": {
                    "loadingRecords": "&nbsp;",
                    "processing": "<i class='fa fa-spinner fa-pulse fa-3x fa-fw'></i><span> Loading...</span>"
                },
                "serverSide": serverSide, // for process server side  
                "filter": true, // this is for disable filter (search box)  
                "orderMulti": false, // for disable multiple column at once  
                "ajax": $.fn.dataTable.pipeline({
                    url: url,
                    method: method,
                    pages: 5
                }),
                "columnDefs":
                    [{
                        "targets": [0],
                        "visible": true,
                        "searchable": true
                    }],
                "columns": columns,
                "initComplete": function (settings, json) {
                    $('[data-toggle="tooltip"]').tooltip();
                }
            });
        } else {
            $dt = $(tblParsed);
        }

        $('table.dataTable').css('width', '100%');

        return $dt;
    };

    // generateList() doesn't work with ajax.reload(), this method can be used instead
    this.generateUpdatableList = function (tableSelector, url, columns, method) {
        var table = $(tableSelector).DataTable({
            "processing": true,
            "language": {
                "loadingRecords": "&nbsp;",
                "processing": "<i class='fa fa-spinner fa-pulse fa-3x fa-fw'></i><span> Loading...</span>"
            },
            "serverSide": false,
            "filter": true,
            "orderMulti": false,
            "ajax": {
                url: url,
                method: method
            },
            "columnDefs":
                [{
                    "targets": [0],
                    "visible": true,
                    "searchable": true
                }],
            "columns": columns
        });

        $(tableSelector).css('width', '100%');

        return table;
    };

    this.redirectToPage = function (url) {
        $("#loader-container").show();
        $.get(url, function (data, status) {
            $('#main_panel').html(data);
            $("#loader-container").hide();
            ACCOUNT.init();
        });
    };

    this.getDataTableDefaults = function (url, columns, method, identifier) {
        return {
            "processing": true,
            "language": {
                "loadingRecords": "&nbsp;",
                "processing": "<i class='fa fa-spinner fa-pulse fa-3x fa-fw'></i><span> Loading...</span>"
            },
            "serverSide": false,
            "filter": true,
            "orderMulti": false,
            "ajax": {
                url: url,
                method: method
            },
            "columnDefs":
                [{
                    "targets": [0],
                    "visible": true,
                    "searchable": true
                }],
            "columns": columns,
            "createdRow": function (row, data, index) {
                if (data.hasOwnProperty(identifier)) {
                    row.id = data[identifier];
                }
            }
        };
    };
}
