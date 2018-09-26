var UTILITY = new Utility();

function Utility() {
    var self = this;

    this.formatCurrency = function (value, displayCents) {
        displayCents = typeof displayCents !== 'undefined' ? displayCents : true;

        var digits = 2;

        if (!displayCents) {
            digits = 0;
        }

        return '$' + value.toLocaleString(undefined, { minimumFractionDigits: digits, maximumFractionDigits: digits });
    }

    this.serializeFormJSON = function (form) {
        var o = {};
        var a = form.serializeArray();
        $.each(a, function () {
            if (o[this.name]) {
                if (!o[this.name].push) {
                    o[this.name] = [o[this.name]];
                }
                o[this.name].push(this.value || '');
            } else {
                o[this.name] = this.value || '';
            }
        });
        return o;
    }

    this.formatDateTime = function (value) {
        if (value === null || value.length < 8) {
            return value;
        }

        var date = new Date(value);
        var month = date.getMonth() + 1;
        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
    }

    this.getTodayString = function () {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var year = now.getFullYear();

        return year + "-" + (month) + "-" + (day);
    }
}

