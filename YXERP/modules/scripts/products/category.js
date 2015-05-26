/*
*布局页JS
*/
define(function (require, exports, module) {
    var $ = require("jquery"),
        Global = require("global"),
        doT = require("dot"),
        Easydialog = require("easydialog");
    var Category = {
        CategoryID: "",
        PID: ""
    };
    var CacheAttrs = [];
    var ObjectJS = {};
    //初始化数据
    ObjectJS.init = function () {
        ObjectJS.cache();
        ObjectJS.bindStyle();
        ObjectJS.bindEvent();
    }
    //缓存数据
    ObjectJS.cache = function () {
        //获取所有属性
        Global.post("/Products/GetAllAttrList", "", function (data) {
            CacheAttrs = data.Items;
        });
    }
    //绑定元素定位和样式
    ObjectJS.bindStyle = function () {
        var _height = document.documentElement.clientHeight - 250;
        $(".category-list").css("height", _height);

    }
    //绑定事件
    ObjectJS.bindEvent = function () {
        var _self = this;
        //调整浏览器窗体
        $(window).resize(function () {
            ObjectJS.bindStyle();
        });
        $(".ico-add").click(function () {
            var _this = $(this);
            Category.CategoryID
            Category.PID = _this.data("id");
            _self.showCategory();
        });
    }
    //添加分类弹出层
    ObjectJS.showCategory = function () {
        doT.exec("template/products/category_add.html", function (templateFun) {
            var html = templateFun(CacheAttrs);
            //html = $(html);

            Easydialog.open({
                container: {
                    id: "category-add-div",
                    header: Category.CategoryID == "" ? "添加分类" : "编辑分类",
                    content: html,
                    yesFn: function () {
                        if (!VerifyObject.isPass()) {
                            return false;
                        }

                        _self.saveCategory(editback);
                    },
                    callback: function () {

                    }
                }
            });
        });
    }
    ObjectJS.saveCategory = function () {

    };

    module.exports = ObjectJS;
})