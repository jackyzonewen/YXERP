
define(function (require, exports, module) {
    var Global = require("global"),
        doT = require("dot");
    require("pager");
    var Params = {
        keyWords: "",
        status: -1,
        pageIndex: 1,
        totalCount: 0
    };
    var ObjectJS = {};
    //初始化
    ObjectJS.init = function () {
        var _self = this;
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
    }
    //获取品牌列表
    ObjectJS.getList = function () {
        var _self = this;
        $(".tr-header").nextAll().remove();
        Global.post("/Purchase/GetMyPurchase", Params, function (data) {
            doT.exec("template/purchase/mypurchase.html", function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $(".tr-header").after(innerText);

                //删除事件
                $(".ico-del").click(function () {
                    if (confirm("采购单删除后不可恢复,确认删除吗？")) {
                        Global.post("/Purchase/DeleteDoc", { docid: $(this).data("id") }, function (data) {
                            if (data.Status) {
                                _self.getList();
                            } else {
                                alert("删除失败！");
                            }
                        });
                    }
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