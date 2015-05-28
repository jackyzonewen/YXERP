/*
*布局页JS
*/
define(function (require, exports, module) {
    var $ = require("jquery"),
        Global = require("global"),
        doT = require("dot"),
        Verify = require("verify"), VerifyObject,
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
        _self.addBindEvent($(".ico-add"));
        _self.bindElementEvent($(".category-list li"));
        
    }
    //添加分类绑定事件并处理回调
    ObjectJS.addBindEvent = function (ele) {
        var _self = this;
        ele.click(function () {
            var _this = $(this);
            Category.CategoryID
            Category.PID = _this.data("id");
            _self.showCategory(function (model) {
                var ele = $('<li data-id="' + model.CategoryID + '" title="' + model.Description + '">' +
                                '<span class="category-name">' + model.CategoryName + '</span>' +
                                '<span>></span>' +
                            '</li>');
                _self.bindElementEvent(ele);
                _this.parent().next("ul").append(ele);
            });
        });
    }
    //添加分类弹出层
    ObjectJS.showCategory = function (callback) {
        var _self = this;
        doT.exec("template/products/category_add.html", function (templateFun) {
            var html = templateFun(CacheAttrs);

            Easydialog.open({
                container: {
                    id: "category-add-div",
                    header: Category.CategoryID == "" ? "添加分类" : "编辑分类",
                    content: html,
                    yesFn: function () {
                        if (!VerifyObject.isPass()) {
                            return false;
                        }
                        var model = {
                            CategoryID: Category.CategoryID,
                            CategoryCode: "",
                            CategoryName: $("#categoryName").val(),
                            PID: Category.PID,
                            Status: $("#categoryStatus").prop("checked") ? 1 : 0,
                            Description: $("#description").val()
                        };
                        //属性
                        var attrs = "";
                        $(".attr-item").each(function () {
                            if ($(this).prop("checked")) {
                                attrs += $(this).data("id") + ",";
                            }
                        });
                        _self.saveCategory(model, attrs, callback);
                    },
                    callback: function () {

                    }
                }
            });
            //编辑填充数据
            if (Category.CategoryID) {
                $("#categoryName").val(Category.CategoryName);
                $("#categoryStatus").prop("checked", Category.Status == 1);
                $("#description").val(Category.Description);
                $(".attr-item").each(function () {
                    var _this = $(this);
                    _this.prop("checked", Category.AttrList.indexOf(_this.data("id")) >= 0);
                });
            }

            $("#categoryName").focus();

            VerifyObject = Verify.createVerify({
                element: ".verify",
                emptyAttr: "data-empty",
                verifyType: "data-type",
                regText: "data-text"
            });
        });
    }
    //元素绑定事件
    ObjectJS.bindElementEvent = function (element) {
        var _self = this;
        //鼠标悬浮
        element.mouseover(function () {
            var _this = $(this);
            _this.find(".edit").addClass("ico-edit").html("");
        });
        //鼠标悬浮
        element.mouseout(function () {
            var _this = $(this);
            _this.find(".edit").removeClass("ico-edit").html(">");
        });
        //编辑
        element.find(".edit").click(function () {
            var _this = $(this);
            Global.post("/Products/GetCategoryByID", {
                categoryid: _this.parent().data("id")
            }, function (data) {
                Category = data.Model;
                _self.showCategory(function (model) {
                    _this.prev().html(model.CategoryName);
                    if (model.Status == 1) {
                        _this.prev().removeClass("colorccc");
                    } else {
                        _this.prev().addClass("colorccc");
                    }
                    _this.parent().attr("title", model.Description);
                });
            });
            return false;
        })

        //点击
        element.click(function () {
            var _this = $(this);
            _this.siblings().removeClass("hover");
            _this.addClass("hover");
            _this.parents(".category-layer").nextAll().remove();
            Global.post("/Products/GetChildCategorysByID", {
                categoryid: _this.data("id")
            }, function (data) {
                doT.exec("template/products/category_list.html", function (templateFun) {
                    var html = templateFun(data.Items);
                    html = $(html);
                    //绑定添加事件
                    html.find(".category-header span").html(_this.find(".category-name").html());
                    _self.addBindEvent(html.find(".ico-add").data("id", _this.data("id")));
                    
                    _self.bindElementEvent(html.find("li"));

                    _this.parents(".category-layer").after(html);
                    _self.bindStyle();
                });
            });
        });
    }

    //保存分类
    ObjectJS.saveCategory = function (category, attrs, callback) {
        Global.post("/Products/SavaCategory", {
            category: JSON.stringify(category),
            attrlist: attrs
        }, function (data) {
            if (data.ID) {
                category.CategoryID = data.ID;
                !!callback && callback(category);
            }
        });
    };

    module.exports = ObjectJS;
})