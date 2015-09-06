define(function (require, exports, module) {
    var Global = require("global"),
    doT = require("dot");

    require("pager");

    var CacheCategorys = [];
    var CacheChildCategorys = [];

    var Params = {
        CategoryID: "",
        BeginPrice: "",
        EndPrice: "",
        PageIndex: 1,
        keyWords: ""
    }

    var ObjectJS = {};
    //初始化
    ObjectJS.init = function (type) {
        var _self = this;
        _self.type = type;
        _self.getChildCategory("");
        _self.bindEvent();
    }

    //获取分类信息和下级分类
    ObjectJS.getChildCategory = function (pid) {
        var _self = this;
        $("#category-child").empty();
        
        if (!CacheChildCategorys[pid]) {
            Global.post("/Products/GetChildCategorysByID", {
                categoryid: pid
            }, function (data) {
                CacheChildCategorys[pid] = data.Items;
                _self.bindChildCagegory(pid);
            });
        } else {
            _self.bindChildCagegory(pid);
        }
        if (!!pid) {
            if (!CacheCategorys[pid]) {
                Global.post("/Products/GetCategoryDetailsByID", {
                    categoryid: pid
                }, function (data) {
                    CacheCategorys[pid] = data.Model;
                    _self.bindCagegoryAttr(pid);
                });
            } else {
                _self.bindCagegoryAttr(pid);
            }
        }

        Params.CategoryID = pid;
        _self.getProducts();
    }
    //绑定分类属性
    ObjectJS.bindCagegoryAttr = function (pid) {
        var _self = this;
        $("#attr-price").nextAll(".attr-item").remove();
        //属性
        doT.exec("template/products/filter_attr_list.html", function (templateFun) {
            var html = templateFun(CacheCategorys[pid].AttrLists);
            html = $(html);

            $("#attr-price").after(html);
        });
        //规格
        doT.exec("template/products/filter_attr_list.html", function (templateFun) {
            var html = templateFun(CacheCategorys[pid].SaleAttrs);
            html = $(html);

            $("#attr-price").after(html);
        });
    }
    //绑定下级分类
    ObjectJS.bindChildCagegory = function (pid) {
        var _self = this;
        var length = CacheChildCategorys[pid].length;
        if (length > 0) {
            $(".category-child").show();
            for (var i = 0; i < length; i++) {
                var _ele = $(" <li data-id='" + CacheChildCategorys[pid][i].CategoryID + "'>" + CacheChildCategorys[pid][i].CategoryName + "</li>");
                _ele.click(function () {
                    //处理分类MAP
                    var _map = $(" <li data-id='" + $(this).data("id") + "'>" + $(this).html() + "<span>></span></li>");
                    _map.click(function () {
                        $(this).nextAll().remove();
                        _self.getChildCategory($(this).data("id"));
                    })
                    $(".category-map").append(_map);
                    _self.getChildCategory($(this).data("id"));
                });
                $("#category-child").append(_ele);
            }
        } else {
            $(".category-child").hide();
        }
    }

    //绑定事件
    ObjectJS.bindEvent = function () {
        var _self = this;

        $(".category-map li").click(function () {
            $(this).nextAll().remove();
            _self.getChildCategory($(this).data("id"));
        });

        //搜索
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                Params.keyWords = keyWords;
                _self.getProducts();
            });
        });

    }

    //绑定产品列表
    ObjectJS.getProducts = function (params) {
        var opt = $.extend({
            CategoryID: Params.CategoryID,
            PageIndex: Params.PageIndex,
            Keywords: Params.keyWords
        }, params);
        Global.post("/Products/GetProductListForShopping", { filter: JSON.stringify(opt) }, function (data) {
            $("#productlist").empty();
            doT.exec("template/products/filter_product_list.html", function (templateFun) {
                var html = templateFun(data.Items);
                html = $(html);

                $("#productlist").append(html);
            });

            $("#pager").paginate({
                total_count: data.TotalCount,
                count: data.PageCount,
                start: Params.PageIndex,
                display: 5,
                border: true,
                border_color: '#fff',
                text_color: '#333',
                background_color: '#fff',
                border_hover_color: '#ccc',
                text_hover_color: '#000',
                background_hover_color: '#efefef',
                rotate: true,
                images: false,
                mouse: 'slide',
                float: "normal",
                onChange: function (page) {
                    Params.PageIndex = page;
                    ObjectJS.getProducts();
                }
            });
        });
    }

    module.exports = ObjectJS;
});