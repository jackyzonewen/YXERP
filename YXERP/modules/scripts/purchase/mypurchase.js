
define(function (require, exports, module) {
    var Global = require("global"),
        doT = require("dot");
    require("pager");
    var Params = {
        keyWords: "",
        status: -1,
        pageIndex: 1,
        totalCount: 0,
        isAll: false
    };
    var ObjectJS = {};
    //初始化
    ObjectJS.init = function (isAudit) {
        var _self = this;
        Params.isAll = isAudit;
        _self.bindEvent();
        _self.getList();
    }
    //绑定事件
    ObjectJS.bindEvent = function () {
        var _self = this;
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                Params.keyWords = keyWords;
                _self.getList();
            });
        });

        $(document).click(function (e) {
            //隐藏下拉
            if (!$(e.target).parents().hasClass("dropdown-ul") && !$(e.target).parents().hasClass("dropdown") && !$(e.target).hasClass("dropdown")) {
                $(".dropdown-ul").hide();
            }
        });

    }
    //获取单据列表
    ObjectJS.getList = function () {
        var _self = this;
        $(".tr-header").nextAll().remove();
        var url = "/Purchase/GetMyPurchase",
            template = "template/purchase/mypurchase.html";
        
        if (Params.isAll) {
            template = "template/purchase/purchaseaudit.html";
        }

        Global.post(url, Params, function (data) {
            doT.exec(template, function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $(".tr-header").after(innerText);

                //删除事件
                $(".ico-del").click(function () {
                    if (confirm("采购单删除后不可恢复,确认删除吗？")) {
                        Global.post("/Purchase/DeletePurchase", { docid: $(this).data("id") }, function (data) {
                            if (data.Status) {
                                Params.pageIndex = 1;
                                _self.getList();
                            } else {
                                alert("删除失败！");
                            }
                        });
                    }
                });
                //下拉事件
                $(".dropdown").click(function () {
                    var _this = $(this);
                    var position = _this.find(".ico-dropdown").position();
                    $(".dropdown-ul").data("id", _this.data("id")).css({ "top": position.top + 15, "left": position.left }).show().mouseleave(function () {
                        $(this).hide();
                    });
                });
            });
            $("#pager").paginate({
                total_count: data.TotalCount,
                count: data.PageCount,
                start: Params.pageIndex,
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
                onChange: function (page) {
                    Params.pageIndex = page;
                    _self.getList();
                }
            });
        });
    }

    module.exports = ObjectJS;
})