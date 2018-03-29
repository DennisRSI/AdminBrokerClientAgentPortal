var PURCHASE = new Purchase();

function Purchase() {
    var self = this;

    this.init = function () {

        $('.price-change').on('input', function () {
            var value = $('#physicalValue').val();
            var quantity = parseInt($('#physicalQuantity').val()) || 0;
            var rate = self.getRate(quantity);

            var orderCard = value * rate * quantity;
            var orderTotal = orderCard + 24.95;

            var cardStr = UTILITY.formatCurrency(orderCard);
            var cardTotal = UTILITY.formatCurrency(orderTotal);

            $('#order-card').text(cardStr);
            $('#order-total').text(cardTotal);
        });

        $('#order').click(function () {
            var data = UTILITY.serializeFormJSON($('#payment-form'));

            $.ajax({
                url: '/api/purchase/purchase/',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(data),
                success: function (result) {
                    MENU.loadPage('purchase', 'viewconfirmation', result.orderId);
                },
                error: function (xhr, resp, text) {
                    console.log(xhr, resp, text);
                }
            })
        });
    }

    this.getRate = function (quantity) {
        var rate = 0.0055;

        $('#price .quantity').each(function (index) {
            var tier = $(this).text().match(/\d/g).join("");
            tier = parseInt(tier);

            if (quantity >= tier) {
                rate = $(this).next().text().replace('%', '') / 100;
            }
        });

        return rate;
    }
}

