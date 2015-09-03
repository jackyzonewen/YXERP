/*
*布局页JS
*/
define(function (require, exports, module) {
    var $ = require("jquery"),
        Global = require("global");

    var Height = document.documentElement.clientHeight - 84,
        Width = document.documentElement.clientWidth;

    var LayoutObject = {};
    //初始化数据
    LayoutObject.init = function () {
        LayoutObject.bindStyle();
        LayoutObject.bindEvent();
    }
    //绑定元素定位和样式
    LayoutObject.bindStyle = function () {
        var _height = Height, _width = Width - 190;
        $(".main-content").css({ "height": _height, "width": _width });
    }
    //绑定事件
    LayoutObject.bindEvent = function () {

        //展开三级菜单
        $(".controller").bind("click", function () {
            var _height = Height - 65 - $(".controller").length * 29;
            $(".controller").removeClass("select").css("height", "28px");
            $(this).addClass("select").css("height", _height + 29);
            $(this).find("ul").css("height", _height);
        });

        //隐藏、展开左侧菜单
        $(".ico-left-pull").click(function () {
            var _this = $(this);
            if (_this.attr("data-status") == "open") {
                _this.attr("data-status", "close");
                $("nav").animate({ left: "-186px" }, "fast");
                _this.animate({ left: "2px" }, "fast");
                _this.removeClass("ico-left-pull").addClass("ico-left-open");
                $(".main-content").css("width", Width - 4);
            } else {
                _this.attr("data-status", "open");
                $("nav").animate({ left: "0px" }, "fast");
                _this.animate({ left: "188px" }, "fast");
                _this.removeClass("ico-left-open").addClass("ico-left-pull");
                $(".main-content").css("width", Width - 190);
            }
        });
        //调整浏览器窗体
        $(window).resize(function () {
            Height = document.documentElement.clientHeight - 84, Width = document.documentElement.clientWidth;
            LayoutObject.bindStyle();
            $(".controller.select").click();
        });
       
        $(".controller.select").click();
    }
    module.exports = LayoutObject;
})