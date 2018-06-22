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
        if (value === null || value.length < 8) {
            return value;
        }

        var date = new Date(value);
        var month = date.getMonth() + 1;
        return (month.toString().length > 1 ? month : "0" + month) + "/" + date.getDate() + "/" + date.getFullYear();
    }
}

