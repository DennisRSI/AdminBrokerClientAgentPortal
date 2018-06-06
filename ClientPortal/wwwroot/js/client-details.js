var CLIENTDETAILS = new ClientDetails();

function ClientDetails() {
    var self = this;

    this.init = function (clientId) {
        self.initCampaignTable(clientId);
    }

    // Is this still used?
    this.initCampaignTable = function (clientId) {
        var url = "api/campaign/getbyclient/" + clientId;

        var cols = [
            { "data": "campaignId" },
            { "data": "campaignName" },
            { "data": "startNumber" },
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

    this.loadChart = function (labelList, dataList) {
        new Chart(document.getElementById("chart-campaign"), {
            type: 'doughnut',
            data: {
                labels: labelList,
                datasets: [
                    {
                        label: "Card Usage",
                        backgroundColor: [
                            '#e6194b',
                            '#3cb44b',
                            '#ffe119',
                            '#0082c8',
                            '#f58231',
                            '#911eb4',
                            '#46f0f0',
                            '#f032e6',
                            '#d2f53c',
                            '#fabebe',
                            '#008080',
                            '#e6beff',
                            '#aa6e28',
                            '#fffac8',
                            '#800000',
                            '#aaffc3',
                            '#808000',
                            '#ffd8b1',
                            '#000080',
                            '#808080',
                            '#FFFFFF',
                            '#000000'
                        ],
                        data: dataList
                    }
                ]
            }
        });
    }
}

