
define(function (require, exports, module) {
    var Global = require("global"),
        doT = require("dot"),
        Easydialog = require("easydialog"), editor;

    var ObjectJS = {};
    //添加页初始化
    ObjectJS.init = function (model) {
        var _self = this;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        
        _self.bindDetail(model);
        _self.bindEvent(model);
        
    }
    //绑定事件
    ObjectJS.bindEvent = function (model) {
        var _self = this;

    }
    //更改状态
    ObjectJS.bindDetail = function (model) {
        var _self = this;
        _self.ProductID = model.ProductID;

        $("#description").html(decodeURI(model.Description));
    }
    module.exports = ObjectJS;
})