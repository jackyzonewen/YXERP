define(function (require, exports, module) {
    var Global = {},
        jQuery = require("jquery");
    Global.post = function (url, params, callback, anync) {
        jQuery.ajax({
            type: "POST",
            url: url,
            data: params,
            dataType: "json",
            async: !anync,
            cache: false,
            success: function (data) {
                if (data.error) {
                    return;
                } else {
                    !!callback && callback(data);
                }
            }
        });
    }
    //格式化日期
    Date.prototype.toString = function (format) {
        var o = {
            "M+": this.getMonth() + 1,
            "d+": this.getDate(),
            "h+": this.getHours(),
            "m+": this.getMinutes(),
            "s+": this.getSeconds(),
            "q+": Math.floor((this.getMonth() + 3) / 3),
            "S": this.getMilliseconds()
        }

        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        }

        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    };
    //日期字符串转换日期格式
    String.prototype.toDate = function (format) {
        var d = new Date();
        d.setTime(this.match(/\d+/)[0]);
        return (!!format) ? d.toString(format) : d;
    }
    //截取字符串
    String.prototype.subString = function (len) {
        if (this.length > len) {
            return this.substr(0, len-1) + "...";
        }
        return this;
    }
    //判断字符串是否整数
    String.prototype.isInt = function () {
        return this.match(/^(0|([1-9]\d*))$/);
    }

    /*重写alert*/
    window.alert = function (msg) {

        $("#window_alert").remove();
        var left = 0, top = 250, alertwidth = 0,
            alert = $("<div />").attr("id", "window_alert").addClass("alert"),
            wrap = $("<div/>").addClass("alertwrap"),
            close = $("<div/>").text("×").addClass("close");
        alert.appendTo($("body"));
        wrap.append(close);
        wrap.append(msg);
        alert.append(wrap);

        left = $(window).width() / 2 - (alert.width() / 2);
        top = $(window).scrollTop() + top;
        alert.show();
        alert.offset({ left: left });

        close.click(function () { alert.remove() });
        setTimeout(function () { alert.remove(); }, 5000);
    }

    module.exports = Global;
});