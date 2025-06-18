var LIST = new List();

function List() {
    var self = this;
    var rows_selected = [];
    var $dt = null;
    
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

    this.bulk_update = function () {
        var txt = $('#update_text');
        var $table = $dt.table().node();
        var $chkbox_checked = $('tbody input[type="checkbox"]:checked', $table);
        //var data = $dt.rows({ selected: true }).data();
        //onsole.log($chkbox_checked);
        console.log(rows_selected);
        console.log(txt.val());
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
        //alert(brokerId + ' ' + clientId);

        var url = "api/list/agent/" + brokerId.toString() + "/" + clientId.toString();
        var cols = [
            { "data": null, defaultContent: '<input type="checkbox" class="agent-check">' },
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
            },
            { "data": null, defaultContent: '<button type="button" class="agent-details btn btn-primary">Details</button>' },
        ];

        $dt = self.generateList("agent_tbl", url, cols);

        self.saveCheckBoxChecked("agent_tbl", 'check-all', 'agent-check', '/api/menu/my-account/');
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
                "sDom": '<"dataTables_header"lfr>t<"dataTables_footer"ip>',
                "ajax": $.fn.dataTable.pipeline({
                    url: url,
                    method: method,
                    pages: 5
                }),
                "columnDefs":
                    [{
                        'targets': 0,
                        'orderable': false,
                        'selectable': false//,
                        //'selector': 'td:first-child'
                    }],
                "columns": columns,
                
                "initComplete": function (settings, json) {
                    $('[data-toggle="tooltip"]').tooltip();
                },
                'select': {
                    'style': 'multi',
                    'selector': 'td:first-child'
                },
                'order': [[1, 'asc']],
                'rowCallback': function (row, data, dataIndex) {
                    // Get row ID
                    var rowId = row.id;

                    // If row ID is in the list of selected row IDs
                    if ($.inArray(rowId, rows_selected) !== -1) {
                        $(row).find('input[type="checkbox"]').prop('checked', true);
                        $(row).addClass('selected');
                    }

                    //alert(rowId);
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
                    'targets': 0,
                    'orderable': false,
                    'selectable': false//,
                    //'selector': 'td:first-child'
                }],
            "columns": columns,
            'select': {
                'style': 'multi',
                'selector': 'td:first-child'
            },
            'order': [[1, 'asc']]
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
                    'targets': 0,
                    'orderable': false,
                    'selectable': false

                    //,
                    //'selector': 'td:first-child'//,
                    /*'checkboxes': {
                        'selectRow': true
                    }*/
                }],
            "columns": columns,
            
            "deferRender": true,
            "createdRow": function (row, data, index) {
                if (data.hasOwnProperty(identifier)) {
                    row.id = data[identifier];
                }
            },
            'select': {
                style: 'multi',
                selector: 'td:first-child'
            },
            'order': [[1, 'asc']],
            'rowCallback': function (row, data, dataIndex) {
                // Get row ID
                var rowId = data[0];

                // If row ID is in the list of selected row IDs
                if ($.inArray(rowId, rows_selected) !== -1) {
                    $(row).find('input[type="checkbox"]').prop('checked', true);
                    $(row).addClass('selected');
                }
            }

        };
    };
    this.updateDataTableSelections = function (table) {
        //console.log(table);
        var $table = table.table().node();
        var $chkbox_all = $('tbody input[type="checkbox"]', $table);
        var $chkbox_checked = $('tbody input[type="checkbox"]:checked', $table);
        var chkbox_select_all = $('thead input[id="check-all"]', $table).get(0);
        //console.log();
        // If none of the checkboxes are checked
        if ($chkbox_checked.length === 0) {
            chkbox_select_all.checked = false;
            if ('indeterminate' in chkbox_select_all) {
                chkbox_select_all.indeterminate = false;
            }
            //alert('fired');
            $('#update_text').remove();
            $('#update_text_btn').remove();

            // If all of the checkboxes are checked
        } else if ($chkbox_checked.length === $chkbox_all.length) {
            chkbox_select_all.checked = true;
            if ('indeterminate' in chkbox_select_all) {
                chkbox_select_all.indeterminate = false;
            }
            if (!$('#update_text').length) {
                $(".dataTables_length").append(self.toolBarAdd());
            }
            // If some of the checkboxes are checked
        } else {
            chkbox_select_all.checked = true;
            if ('indeterminate' in chkbox_select_all) {
                chkbox_select_all.indeterminate = true;
            }
            if (!$('#update_text').length) {
                $(".dataTables_length").append(self.toolBarAdd());
            }
        }
    };
    this.saveCheckBoxChecked = function (table, all_button, list_button, url) {
        table = '#' + table;

        $dt.on('click', 'button', function () {
            var data = $(this).closest("tr").attr('id');//$dt.row(this).id();
            self.redirectToPage('/api/menu/my-account/' + data);
        });

        $('thead input[id="' + all_button + '"]', $dt.table().container()).on('click', function (e) {
            if (this.checked) {
                $(table + ' tbody input[type="checkbox"]:not(:checked)').trigger('click');
                //$("div.toolbar").html(this.toolBarAdd());
            } else {
                $(table + ' tbody input[type="checkbox"]:checked').trigger('click');
                //$("div.toolbar").html('');
            }

            // Prevent click event from propagating to parent
            e.stopPropagation();
        });

        $(table).on('click', 'tbody td, thead th:first-child', function (e) {
            $(this).parent().find('input[type="checkbox"]').trigger('click');
        });

        $dt.on('click', 'input:checkbox.' + list_button, function (e) {
            var $row = $(this).closest('tr');
            var data = $dt.row($row).data();

            var rowId = $(this).closest("tr").attr('id');
            var index = $.inArray(rowId, rows_selected);

            if (this.checked && index === -1) {
                rows_selected.push(rowId);
            } else if (!this.checked && index !== -1) {
                rows_selected.splice(index, 1);
            }

            if (this.checked) {
                $row.addClass('selected');
            } else {
                $row.removeClass('selected');
            }

            self.updateDataTableSelections($dt);

            e.stopPropagation();

        });

        // Handle table draw event
        $dt.on('draw', function () {
            // Update state of "Select all" control
            self.updateDataTableSelections($dt);
        });
    };
    this.toolBarAdd = function () {
        //console.log(table);
        return '<input placeholder="Update Commission" class="form-control" type="text" id="update_text" value="" />&nbsp;<button onclick="LIST.bulk_update()" class="btn btn-primary" type="button" id="update_text_btn">Update All</button>';
    };
}
