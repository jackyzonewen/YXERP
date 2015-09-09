
/* 
作者：Allen
日期：2014-11-15
示例:
    $(...).searchKeys(callback);
*/

define(function (require, exports, module) {
    require("plug/seach_keys/style.css");
    (function ($) {
        $.fn.searchKeys = function (callback) {
            return this.each(function () {
                var _this = $(this);
                $.fn.drawSearch(_this,callback);
            })
        }

        $.fn.drawSearch = function (obj, callback) {
            if (!obj.hasClass("searth-module")) {
                obj.addClass("searth-module");
            }
            var _input = $('<input class="search-ipt" type="text" placeholder="' + (obj.data("text") || '智能搜索...') + '">');
            _input.css("width", (obj.data("width") || 180) + "px");
            var _ico = $('<span class="search-ico hand"></span>');
            obj.append(_input).append(_ico);

            _input.focus();

            //处理事件
            _ico.click(function () {
                var _this = $(this);
                !!callback && callback(_this.prev().val());
            })
            _input.keydown(function (e) {
                var _this = $(this);
                if (e.keyCode == 13) {
                    !!callback && callback(_this.val())
                }
            });
        }
    })(jQuery)
    module.exports = jQuery;
});