
/* 
作者：Allen
日期：2014-10-28
示例:
    $(...).switch({
        width: 60,                          //宽度
        height: 20,                         //高度
        open_color: "#44bf16",              //开启背景色
        open_title: "启用",                 //开启提示
        close_color: "#a3aaa2",             //关闭背景色
        close_title: "禁用",                //关闭提示
        bar_color: "#fff",                  //按钮背景色
        value_key: "value",                 //值key
        speed: 200,                         //速度
        change: function(this,callback){    //更改状态回调函数
            this                            //对象
            callback(bool);                 //处理回调后触发按钮视图状态
        }
    });
*/

define(function (require, exports, module) {
    require("plug/switch/style.css");
    (function ($) {
        $.fn.switch = function (options) {
            var opt = $.extend([], $.fn.switch.defaults, options);
            return this.each(function () {
                $this = $(this);
                var o = $.meta ? $.extend({}, opt, $this.data()) : opt;
                $.fn.drawSwitch(o, $this, $this.data(o.value_key));
            })
        }
        //默认参数
        $.fn.switch.defaults = {
            width: 60,
            height: 20,
            open_color: "#44bf16",
            open_title: "启用",
            close_color: "#a3aaa2",
            close_title: "禁用",
            bar_color: "#fff",
            value_key: "value",
            speed: 200,
            change: null
        };
        $.fn.drawSwitch = function (o, obj, value) {
            obj.addClass("switch");
            obj.css({ width: o.width, height: o.height });
            var _bar = $("<div class='bar'></div>").css({ width: o.width / 2, height: o.height, backgroundColor: o.bar_color });
            if (value) {
                obj.css({ backgroundColor: o.open_color, border: "solid 1px " + o.open_color });
                obj.attr("title", o.close_title);
                _bar.css("left", o.width / 2 + "px");
            } else {
                obj.css({ backgroundColor: o.close_color, border: "solid 1px " + o.close_color });
                obj.attr("title", o.open_title);
                _bar.css("left", "0px");
            }

            obj.append(_bar);

            //处理事件
            obj.click(function () {
                var _this = $(this), _thisBar = _this.find(".bar");

                !!o.change && o.change(_this, function (status) {
                    if (status) {
                        if (_this.data(o.value_key)) {
                            _this.css({ backgroundColor: o.close_color, border: "solid 1px " + o.close_color });
                            _this.attr("title", o.open_title);
                            _thisBar.animate({
                                left: "0px"
                            }, o.speed);
                        } else {
                            _this.css({ backgroundColor: o.open_color, border: "solid 1px " + o.open_color });
                            _this.attr("title", o.close_title);
                            _thisBar.animate({
                                left: o.width / 2 + "px"
                            }, o.speed);
                        }
                        _this.data(o.value_key, !obj.data(o.value_key));
                    }
                });
            })
        }
    })(jQuery)
    module.exports = jQuery;
});