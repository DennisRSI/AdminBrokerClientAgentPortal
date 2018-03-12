var PURCHASE = new Purchase();

function Purchase() {
    var self = this;

    this.init = function () {

        $('.price-change').on('input', function () {
            var value = $('#physical-value').val().replace('$', '');
            var quantity = parseInt($('#physical-quantity').val());
            var rate = self.getRate(quantity);

            var orderCard = value * rate * quantity;
            var orderTotal = orderCard + 24.95;

            var cardStr = self.formatCurrency(orderCard);
            var cardTotal = self.formatCurrency(orderTotal);

            $('#order-card').text(cardStr);
            $('#order-total').text(cardTotal);
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

    this.formatCurrency = function (value) {
        return value.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
    }
}

//# sourceURL=purchase.js
