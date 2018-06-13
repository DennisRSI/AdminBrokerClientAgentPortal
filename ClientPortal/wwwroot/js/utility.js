var UTILITY = new Utility();

function Utility() {
    var self = this;

    this.formatCurrency = function (value) {
        return value.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 });
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
        var date = new Date(value);
        var month = date.getMonth() + 1;
        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
    }

    this.setQueryStartDate = function (selector) {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() - 10 + "-" + (month) + "-" + (day);

        $(selector).val(today);
    }

    this.setQueryEndDate = function (selector) {
        var now = new Date();
        var day = ("0" + now.getDate()).slice(-2);
        var month = ("0" + (now.getMonth() + 1)).slice(-2);
        var today = now.getFullYear() + "-" + (month) + "-" + (day);

        $(selector).val(today);
    }
}

