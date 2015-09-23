
define(function (require, exports, module) {
    var Global = require("global"),
        doT = require("dot");
    require("pager");

    //缓存货位
    var CacheDepot = [];

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
        //审核
        $("#audit").click(function () {
            location.href = "/Purchase/AuditDetail/" + _self.docid;
        });
        //作废
        $("#invalid").click(function () {
            if (confirm("采购单作废后不可恢复,确认作废吗？")) {
                Global.post("/Purchase/InvalidPurchase", { docid: _self.docid }, function (data) {
                    if (data.Status) {
                        Params.pageIndex = 1;
                        _self.getList();
                    } else {
                        alert("删除失败！");
                    }
                });
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
                    $(".dropdown-ul").css({ "top": position.top + 15, "left": position.left }).show().mouseleave(function () {
                        $(this).hide();
                    });
                    _self.docid = _this.data("id");
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

    //审核页初始化
    ObjectJS.initDetail = function (wares) {
        var _self = this;
        wares = JSON.parse(wares.replace(/&quot;/g, '"'));
        //绑定仓库
        $(".item").each(function () {
            var _this = $(this), ware = $("<select data-id='" + _this.data("id") + "'></select>"), warebox = _this.find(".ware-li"), depotbox = _this.find(".depot-li");
            for (var i = 0, j = wares.length; i < j; i++) {
                ware.append($("<option value='" + wares[i].WareID + "'>" + wares[i].Name + "</option>"))
            }
            ware.val(warebox.data("id"));

            //选择仓库
            ware.change(function () {
                var _ware = $(this);
                if (CacheDepot[_ware.val()]) {
                    _self.bindDepot(_ware.parent().next(), CacheDepot[_ware.val()], _ware.val(), _ware.data("id"), true);
                } else {
                    Global.post("/Warehouse/GetDepotSeatsByWareID", { wareid: _ware.val() }, function (data) {
                        CacheDepot[_ware.val()] = data.Items;
                        _self.bindDepot(_ware.parent().next(), CacheDepot[_ware.val()], _ware.val(), _ware.data("id"), true);
                    });
                }
            });

            warebox.append(ware);

            if (CacheDepot[warebox.data("id")]) {
                _self.bindDepot(depotbox, CacheDepot[warebox.data("id")], warebox.data("id"), _this.data("id"),false);
            } else {
                Global.post("/Warehouse/GetDepotSeatsByWareID", { wareid: warebox.data("id") }, function (data) {
                    CacheDepot[warebox.data("id")] = data.Items;
                    _self.bindDepot(depotbox, data.Items, warebox.data("id"), _this.data("id"), false);
                });
            }
        })

        //全部选中
        $("#checkall").click(function () {
            $(".item").find(".check").prop("checked", $(this).prop("checked"));
        });

        $("#btnconfirm").click(function () {
            if ($(".item").find("input:checked").length <= 0) {
                alert("请选择审核上架的产品！");
                return;
            }
            var ids = [];
            $(".item").find("input:checked").each(function () {
                ids.push($(this).val());
            });
            Global.post("/Purchase/AuditPurchase", { ids: ids.join(",") }, function (data) {
                if (data.Status) {
                    location.href = location.href;
                };
            });
        })
    }

    //绑定货位
    ObjectJS.bindDepot = function (depotbox, depots, wareid, autoid, change) {

        depotbox.empty();
        var depot = $("<select data-id='" + autoid + "' data-wareid='" + wareid + "'></select>");
        for (var i = 0, j = depots.length; i < j; i++) {
            depot.append($("<option value='" + depots[i].DepotID + "' >" + depots[i].DepotCode + "</option>"))
        }

        if (!change) {
            depot.val(depotbox.data("id"));
        } else {
            depot.children().first().prop("checked", true);
        }
        //选择仓库
        depot.change(function () {
            Global.post("/Purchase/UpdateStorageDetailWare", {
                autoid: autoid,
                wareid: wareid,
                depotid: depot.val()
            }, function (data) {
                if (!data.Status) {
                    alert("操作失败,请刷新页面重新操作！");
                };
            });
        });

        depotbox.append(depot);

        if (change && depot.val()) {
            Global.post("/Purchase/UpdateStorageDetailWare", {
                autoid: autoid,
                wareid: wareid,
                depotid: depot.val()
            }, function (data) {
                if (!data.Status) {
                    alert("操作失败,请刷新页面重新操作！");
                };
            });
        };
    }


    module.exports = ObjectJS;
})