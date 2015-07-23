
define(function (require, exports, module) {
    var City = require("city"), CityObj,
        Global = require("global"),
        Verify = require("verify"), VerifyObject,
        doT = require("dot");
    require("pager");
    require("switch");
    var Params = {
        keyWords: "",
        pageSize: 20,
        pageIndex: 1,
        totalCount: 0
    };
    var ObjectJS = {};
    //添加页初始化
    ObjectJS.init = function () {
        ObjectJS.bindEvent();
    }
    //绑定事件
    ObjectJS.bindEvent = function () {
        var _self = this;
        CityObj = City.createCity({ elementID: "warehouseCity" });
        $("#btnSave").on("click", function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            _self.savaEntity();
        });

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });

        $("#warehouseName").focus();
    }
    //保存
    ObjectJS.savaEntity = function () {
        var _self = this;
        var entity = {
            WareID: _self.wareID,
            Name: $("#warehouseName").val().trim(),
            WareCode: $("#warehouseCode").val().trim(),
            ShortName: $("#shortName").val().trim(),
            CityCode: CityObj.getCityCode(),
            Status: $("#warehouseStatus").prop("checked") ? 1 : 0,
            Description: $("#description").val()
        };
        Global.post("/Warehouse/SaveWareHouse", { ware: JSON.stringify(entity) }, function (data) {
            if (data.ID.length > 0) {
                location.href = "/Warehouse/WareHouse"
            }
        })
    }
    //列表页初始化
    ObjectJS.initList = function () {
        var _self = this;
        _self.getList();
        _self.bindListEvent();
    }
    //绑定列表页事件
    ObjectJS.bindListEvent = function () {
        var _self = this;
        require.async("search", function () {
            $(".searth-module").searchKeys(function (keyWords) {
                Params.keyWords = keyWords;
                _self.getList();
            });
        });
    }
    //获取仓库列表
    ObjectJS.getList = function () {
        var _self = this;
        $("#warehouse-items").nextAll().remove();
        Global.post("/Warehouse/GetWareHouses", Params, function (data) {
            doT.exec("template/warehouse/warehouse_list.html", function (templateFun) {
                var innerText = templateFun(data.Items);
                innerText = $(innerText);
                $("#warehouse-items").after(innerText);

                //删除事件
                $(".ico-del").click(function () {
                    if (confirm("仓库删除后不可恢复,确认删除吗？")) {
                        Global.post("/Warehouse/DeleteWareHouse", { id: $(this).attr("data-id") }, function (data) {
                            if (data.Status) {
                                _self.getList();
                            } else {
                                alert("删除失败！");
                            }
                        });
                    }
                });
                //绑定启用插件
                innerText.find(".status").switch({
                    open_title: "点击启用",
                    close_title: "点击禁用",
                    value_key: "value",
                    change: function (data,callback) {
                        _self.editStatus(data, data.data("id"), data.data("value"), callback);
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
                    Brand.getList();
                }
            });
        });
    }
    //更改仓库状态
    ObjectJS.editStatus = function (obj, id, status, callback) {
        var _self = this;
        Global.post("/Warehouse/UpdateWareHouseStatus", {
            id: id,
            status: status ? 0 : 1
        }, function (data) {
            !!callback && callback(data.Status);
        });
    }
    //初始化编辑页数据
    ObjectJS.initEdit = function (model) {
        var _self = this;
        model = JSON.parse(model.replace(/&quot;/g, '"'));
        _self.bindDetailEvent(model);
        _self.bindDetail(model);
    }
    //获取详细信息
    ObjectJS.bindDetail = function (model) {
        var _self = this;
        _self.wareID = model.WareID;
        $("#warehouseCode").text(model.WareCode);

        $("#warehouseName").val(model.Name);

        $("#shortName").val(model.ShortName);
        if (model.Status == 1) {
            $("#warehouseStatus").prop("checked", "checked");
        }
        $("#description").val(model.Description);

        
    }
    //绑定详情事件
    ObjectJS.bindDetailEvent = function (model) {
        var _self = this;
        CityObj = City.createCity({
            elementID: "warehouseCity",
            cityCode: model.CityCode
        });
        
        $("#btnSave").on("click", function () {
            if (!VerifyObject.isPass()) {
                return;
            }
            _self.savaEntity();
        });

        VerifyObject = Verify.createVerify({
            element: ".verify",
            emptyAttr: "data-empty",
            verifyType: "data-type",
            regText: "data-text"
        });
       
    }

    module.exports = ObjectJS;
})