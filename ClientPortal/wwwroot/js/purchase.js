var PURCHASE = new Purchase();

function Purchase() {
    var self = this;

    this.init = function () {

        $('#creditCardExpiration').inputmask({
            mask: '[9]9/99'
        });

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

            var target = $(event.target);
            var form = target.parents('form');

            form.validate({
                rules: {
                    physicalQuantity: {
                        required: true,
                        range: [1000, 1000000]
                    },
                    virtualQuantity: {
                        required: true,
                        range: [0, 1000000]
                    },
                    fullName: {
                        required: true,
                        minlength: 2
                    },
                    billingZip: {
                        required: true,
                        range: [10000, 99999]
                    },
                    address: {
                        required: true,
                        minlength: 2
                    },
                    city: {
                        required: true,
                        minlength: 2
                    },
                    shippingZip: {
                        required: true,
                        range: [10000, 99999]
                    },
                    creditCardNumber: {
                        required: true,
                        minlength: 16,
                        maxlength: 16
                    },
                    creditCardExpiration: {
                        required: true,
                    },
                    creditCardCVC: {
                        required: true,
                        minlength: 3,
                        maxlength: 4
                    },
                }
            });

            var valid = form.valid();

            if (!valid) {
                return;
            }

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

