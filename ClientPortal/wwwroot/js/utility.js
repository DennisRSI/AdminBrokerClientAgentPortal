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
            console.log('name: ' + this.name);
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
}

