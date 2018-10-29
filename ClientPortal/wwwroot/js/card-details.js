var CARDDETAILS = new CardDetails();

function CardDetails() {
    var self = this;

    this.init = function () {
        var settings = {
            searching: false,
            paging: false,
            info: false,
            language: {
                emptyTable: "No search results found"
            }
        };

        $('#table-benefits').DataTable(settings);

        // Copied from site.js
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip({
                container: 'body'
            });
        });
    }

    this.loadChartCardValue = function (total, available, redeemed) {
        new Chart(document.getElementById("availableBalance"), {
            type: 'doughnut',
            data: {
                labels: ["Available", "Redeemed"],
                datasets: [
                    {
                        label: "Card Usage",
                        backgroundColor: ["#6B869B", "#00A4DE"],
                        data: [available, redeemed]
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Total Card Value: ' + total
                }
            }
        });
    }

    this.loadChartBenefitUsage = function (hotelPercent, condoPercent, shoppingPercent) {
        new Chart(document.getElementById("benefitsByType"), {
            type: 'doughnut',
            data: {
                labels: ["Hotel", "Condos", "Shopping"],
                datasets: [
                    {
                        label: "Card Usage",
                        backgroundColor: ["#f2a707", "#00A4DE", "#3c763d"],
                        data: [hotelPercent, condoPercent, shoppingPercent]
                    }
                ]
            },
            options: {
                title: {
                    display: true,
                    text: 'Benefits Usage Breakdown (%)'
                }
            }
        });
    }

    this.loadChartMonthlyUsage = function (labelParam, hotelParam, condoParam, shoppingParam, diningParam) {
        var label = labelParam.split('|');
        var hotel = hotelParam.split('|');
        var condo = condoParam.split('|');
        var shopping = shoppingParam.split('|');
        var dining = diningParam.split('|');

        new Chart(document.getElementById("chartMonthlyUsage"), {
            type: 'bar',
            data: {
                labels: label,
                datasets: [
                    {
                        label: "Hotel",
                        backgroundColor: self.getColorArray(label.length, "#f2a707"),
                        data: hotel
                    }, {
                        label: "Condo",
                        backgroundColor: self.getColorArray(label.length, "#00a4de"),
                        data: condo
                    }, {
                        label: "Shopping",
                        backgroundColor: self.getColorArray(label.length, "#3c763d"),
                        data: shopping
                    }
                ]
            },
            options: {
                legend: { display: true },
                title: {
                    display: true,
                    text: 'Monthly Card Usage by Type ($)'
                }
            }
        });
    }

    this.getColorArray = function (length, color) {
        var data = [];

        for (var i = 0; i < length; i++) {
            data.push(color);
        }

        return data;
    }
}
