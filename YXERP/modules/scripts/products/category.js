/*
*布局页JS
*/
define(function (require, exports, module) {
    var $ = require("jquery"),
        Global = require("global");

    var ObjectJS = {};
    //初始化数据
    ObjectJS.init = function () {
        ObjectJS.bindStyle();
        ObjectJS.bindEvent();
    }
    //绑定元素定位和样式
    ObjectJS.bindStyle = function () {
        var _height = document.documentElement.clientHeight - 250;
        $(".category-list").css("height", _height);

    }
    //绑定事件
    ObjectJS.bindEvent = function () {
        //调整浏览器窗体
        $(window).resize(function () {
            ObjectJS.bindStyle();
        });
    }
    module.exports = ObjectJS;
})