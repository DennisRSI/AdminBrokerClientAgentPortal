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

    this.loadChartMonthlyUsage = function () {
        new Chart(document.getElementById("activationsMonthly"), {
            type: 'bar',
            data: {
                labels: ["Jun", "Jul", "Aug", "Sept", "Oct", "Nov"],
                datasets: [
                    {
                        label: "Hotel $",
                        backgroundColor: ["#f2a707", "#f2a707", "#f2a707", "#f2a707", "#f2a707", "#f2a707"],
                        data: [0, 18.43, 32.74, 121.98, 36.24, 0]
                    }, {
                        label: "Condo $",
                        backgroundColor:
                        ["#00A4DE", "#00A4DE", "#00A4DE", "#00A4DE", "#00A4DE", "#00A4DE"],
                        data: [0, 0, 0, 0, 0, 0]
                    }, {
                        label: "Shopping $",
                        backgroundColor:
                        ["#3c763d", "#3c763d", "#3c763d", "#3c763d", "#3c763d", "#3c763d"],
                        data: [0, 0, 0, 0, 0, 0]
                    }
                ]
            },
            options: {
                legend: { display: false },
                title: {
                    display: true,
                    text: 'Monthly Card Usage by Type ($)'
                }
            }
        });
    }
}
