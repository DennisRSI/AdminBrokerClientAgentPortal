var CLIENTDETAILS = new ClientDetails();

function ClientDetails() {
    var self = this;

    this.init = function (clientId) {
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

